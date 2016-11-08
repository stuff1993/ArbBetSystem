using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbBetSystem
{
    public partial class MainForm : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainForm));

        private string credsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArbBetSystem" + Path.DirectorySeparatorChar + "Arbing.dat");
        internal static byte[] additionalEntropy = { 1, 5, 9, 2, 7 };

        Creds creds;
        DynamicOdds dynOdds;
        List<Meeting> meetings = new List<Meeting>();
        BackgroundWorker bw = new BackgroundWorker();

        public MainForm()
        {
            logger.Info("Starting...");
            InitializeComponent();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(CheckOdds);
        }

        private void CheckOdds(object sender, DoWorkEventArgs args)
        {
            logger.Info("Checking odds");
            BackgroundWorker worker = sender as BackgroundWorker;
            Dictionary<Event, BackgroundWorker> workers = new Dictionary<Event, BackgroundWorker>();
            List<Event> events = meetings.SelectMany(m => m.Events).Where(e => e.Check).ToList();
            foreach (Event e in events)
            {
                BackgroundWorker getodds = new BackgroundWorker();
                getodds.WorkerSupportsCancellation = true;
                getodds.DoWork += new DoWorkEventHandler(GetAndCheckOdds);
                workers.Add(e, getodds);
                getodds.RunWorkerAsync(e);
            }

            while (!worker.CancellationPending)
            {
                Thread.Sleep(1000);
            }
            foreach(BackgroundWorker work in workers.Values)
            {
                work.CancelAsync();
            }
            args.Cancel = true;
        }

        private void GetAndCheckOdds(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Event e = args.Argument as Event;
            logger.Info("Checking event " + e.ToString());
            Dictionary<Runner, Dictionary<string, bool>> hasMatched = new Dictionary<Runner, Dictionary<string, bool>>();
            foreach (Runner r in e.Runners)
            {
                Dictionary<string, bool> backMatched = new Dictionary<string, bool>();
                foreach (KeyValuePair<string, double> pair in r.GetBacks())
                {
                    backMatched.Add(pair.Key, false);
                }
                hasMatched.Add(r, backMatched);
            }

            while (!worker.CancellationPending)
            {
                RunnerOdds odds = dynOdds.GetRunnerOdds(e.ID);
                foreach (Runner r in e.Runners)
                {
                    r.AddOdds(odds.GetRunner(r.No));
                    if (r.Percent != 0)
                    {
                        double lay = r.GetLays().First().Value;
                        foreach (KeyValuePair<string, double> pair in r.GetBacks())
                        {
                            if (lay * (1 + r.Percent/100.0) < pair.Value && !hasMatched[r][pair.Key])
                            {
                                hasMatched[r][pair.Key] = true;
                                SystemSounds.Exclamation.Play();
                                MessageBox.Show("Event: " + e.ToString() + Environment.NewLine + "Runner: " + r.No + Environment.NewLine + "Lay: " + lay + Environment.NewLine + "Back: " + pair.Value + Environment.NewLine + "Book: " + pair.Key, "Match found:", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                logger.Info("Match found: Event: " + e.ToString() + ", Runner: " + r.No + ", Lay: " + lay + ", Back: " + pair.Value + ", Book: " + pair.Key);
                            }
                            else if (lay * (1 + r.Percent / 100.0) >= pair.Value)
                            {
                                hasMatched[r][pair.Key] = false;
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }
            args.Cancel = true;
        }

        private void LoadCredentials()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArbBetSystem"))) { Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArbBetSystem")); }
            if (File.Exists(credsFileName))
            {
                string[] contents = Encoding.UTF8.GetString(CryptoUtils.Unprotect(FileUtils.FileToByteArray(credsFileName), additionalEntropy)).Split(Environment.NewLine.ToCharArray());
                creds = new Creds(contents[0], contents[2]);
            }
            else
            {
                logger.Info("No credentials file");
                UpdateCredentials(false, true);
            }
        }

        private bool SaveCredentials()
        {
            return FileUtils.ByteArrayToFile(credsFileName, CryptoUtils.Protect(Encoding.UTF8.GetBytes(creds.ToString()), additionalEntropy));
        }

        private void UpdateCredentials(bool canCancel, bool Save = false)
        {
            CredentialsForm loginForm = new CredentialsForm(this, canCancel, Save);
            loginForm.ShowDialog();
            loginForm.BringToFront();
        }

        private void InitDynOdds()
        {
            dynOdds = new DynamicOdds(ConfigurationManager.AppSettings["DynamicOddsUrl"]);
        }

        public void SetCredentials(Creds newCreds, bool Save = false)
        {
            if (newCreds == null || newCreds.Username == null || newCreds.Password == null)
            {
                logger.Warn("Credentials contains a null value. Not set");
                return;
            }
            creds = newCreds;
            if (Save) { SaveCredentials(); }
        }

        private void CheckDynamicOdds()
        {
            if (dynOdds == null)
            {
                logger.Error("DynamicOdds object is null");
                MessageBox.Show("DynamicOdds object is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private bool Login()
        {
            CheckDynamicOdds();
            return dynOdds.Login(creds);
            //return dynOdds.Login("6C72J91KF17U3861YJX7HEGHQ15Z40EH");
        }

        private bool UpdateMeetings()
        {
            CheckDynamicOdds();
            try
            {
                List<Meeting> tmp = dynOdds.GetMeetingsAll();
                if (tmp == null)
                {
                    logger.Error("Meetings list is null");
                    throw new Exception("Meetings list is null");
                }
                meetings = tmp;

                lvwMeetings.Items.Clear();

                foreach (Meeting m in meetings)
                {
                    ListViewItem item = new ListViewItem(m.ToString());
                    item.Tag = m;
                    item.Checked = m.IsChecked();
                    lvwMeetings.Items.Add(item);
                }
                lvwMeetings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvwMeetings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (lvwMeetings.Items.Count > 0) { lvwMeetings.Items[0].Selected = true; }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + e.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
        }

        private void forceLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // start thread
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            // toggle text and functionality
        }

        private void changeCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCredentials(true, true);
            Login();
        }

        private void updateMeetingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateMeetings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop thread if running
            bw.CancelAsync();
            logger.Info("Checking Stopped on close");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCredentials();
            InitDynOdds();
            Login();
            UpdateMeetings();
        }

        private void lvwMeetings_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                lvwEvents.Items.Clear();

                foreach (Event evt in ((Meeting)e.Item.Tag).Events)
                {
                    ListViewItem item = new ListViewItem(evt.ToString());
                    item.Tag = evt;
                    lvwEvents.Items.Add(item);
                }
                lvwEvents.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvwEvents.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (lvwEvents.Items.Count > 0) { lvwEvents.Items[0].Selected = true; }
            }
        }

        private void lvwEvents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                lvwRunners.Items.Clear();

                RunnerOdds odds = null;

                if (!((Event)e.Item.Tag).HasOdds())
                {
                    try
                    {
                        odds = dynOdds.GetRunnerOdds(((Event)e.Item.Tag).ID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error getting odds:" + Environment.NewLine + ex.Message,
                            "API Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }

                foreach (Runner r in ((Event)e.Item.Tag).Runners)
                {
                    if (odds != null)
                    {
                        r.AddOdds(odds.GetRunner(r.No));
                    }
                    ListViewItem item = new ListViewItem(r.No.ToString());
                    item.Tag = r;
                    item.SubItems.Add(r.Name);
                    item.SubItems.Add(r.Jockey);
                    item.SubItems.Add(r.Trainer);
                    item.SubItems.Add(r.GetPercent());
                    item.SubItems.Add(r.GetLays().FirstOrDefault(l => l.Key == "BetFair Lay 1").Value.ToString());
                    item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "William Hill").Value.ToString());
                    item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "Sports Bet").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsAT").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBB2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBC").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBE").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBE_FX").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBF_B1").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBF_B1_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBF_B2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBF_B2_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBF_B3").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBF_B3_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBM").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBS").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsBT").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsCB").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsCB_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsCR").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsIAS_2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsLB").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsLB_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsN").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsNZ").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsNZ_FX").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsN_FX").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsN_P").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsPB").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsPB2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsQ").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsQ_FX").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsQ_FX_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsQ_P").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsSA").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsSB2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsSB5").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsSB_2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsSB_3").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsSB_p").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsTS2").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsUB").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsV").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsV_FX").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsV_P").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsWB").Value.ToString());
                    //item.SubItems.Add(r.GetBacks().FirstOrDefault(l => l.Key == "OddsYBB").Value.ToString());
                    lvwRunners.Items.Add(item);
                }
                lvwRunners.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvwRunners.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (lvwRunners.Items.Count > 0) { lvwRunners.Items[0].Selected = true; }
            }
        }

        private void lvwMeetings_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            lvwEvents.ItemChecked -= lvwEvents_ItemChecked;
            foreach (ListViewItem item in lvwEvents.Items)
            {
                ((Event)item.Tag).Check = e.Item.Checked;
                item.Checked = e.Item.Checked;
            }
            lvwEvents.ItemChecked += lvwEvents_ItemChecked;
        }

        private void lvwEvents_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            lvwMeetings.ItemChecked -= lvwMeetings_ItemChecked;
            ((Event)e.Item.Tag).Check = e.Item.Checked;
            lvwMeetings.SelectedItems[0].Checked = ((Meeting)lvwMeetings.SelectedItems[0].Tag).IsChecked();
            lvwMeetings.ItemChecked += lvwMeetings_ItemChecked;
        }

        private void ListViews_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender.GetType() != typeof(ListView)) { throw new ArgumentException("Sender is not a ListView"); }
            ListViewHitTestInfo info = ((ListView)sender).HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                new PercentEntryForm(lvwRunners, item).ShowDialog();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
            logger.Info("Checking Stopped");
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
        }
    }
}
