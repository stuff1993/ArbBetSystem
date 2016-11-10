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
        BindingList<Meeting> meetings = new BindingList<Meeting>();
        BackgroundWorker bw = new BackgroundWorker();

        public MainForm()
        {
            logger.Info("Starting...");
            InitializeComponent();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(CheckOdds);
            dgvMeetings.AutoGenerateColumns = false;
            dgvEvents.AutoGenerateColumns = false;
            dgvRunners.AutoGenerateColumns = false;
            dgvMeetings.DataSource = meetings;
            meetingsToolStripMenuItem.DropDown.Closing += DropDown_Closing;
            layBetsToolStripMenuItem.DropDown.Closing += DropDown_Closing;
            backBetsToolStripMenuItem.DropDown.Closing += DropDown_Closing;
        }

        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
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
                foreach (KeyValuePair<string, double> pair in r.Backs)
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
                        double lay = r.Lays.First().Value;
                        foreach (KeyValuePair<string, double> pair in r.Backs)
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

        private void InitDynOdds()
        {
            dynOdds = new DynamicOdds(ConfigurationManager.AppSettings["DynamicOddsUrl"]);
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

        private bool Login()
        {
            CheckDynamicOdds();
            return dynOdds.Login(creds);
            //return dynOdds.Login("6C72J91KF17U3861YJX7HEGHQ15Z40EH");
        }

        private bool UpdateMeetings()
        {
            CheckDynamicOdds();
            BindingList<Meeting> tmp = null;
            try
            {
                int type = (racingToolStripMenuItem.Checked ? 1 : 0) + (harnessToolStripMenuItem.Checked ? 2 : 0) + (greyhoundToolStripMenuItem.Checked ? 4 : 0);
                tmp = dynOdds.GetMeetingsAll(type);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + e.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (tmp == null)
            {
                logger.Error("Meetings list is null");
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + "Meetings list is null",
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }


            List<Meeting> toRemove = meetings.Where(o1 => tmp.All(o2 => o2.ID != o1.ID)).ToList();
            dgvMeetings.SelectionChanged -= dgvMeetings_SelectionChanged;
            foreach (Meeting m in toRemove) // meeting not in tmp
            {
                meetings.Remove(meetings.First(o1 => o1.ID == m.ID));
            }

            foreach (Meeting m in tmp.Where(o1 => meetings.Any(o2 => o2.ID == o1.ID))) // meeting in both
            {
                Meeting old = meetings.First(o1 => o1.ID == m.ID);
                int i = meetings.IndexOf(old);
                meetings[i] = old.MergeWith(m);
                meetings.ResetItem(i);
            }

            foreach (Meeting m in tmp.Where(o1 => meetings.All(o2 => o2.ID != o1.ID))) // meeting only in tmp
            {
                meetings.Add(m);
            }
            dgvMeetings.SelectionChanged += dgvMeetings_SelectionChanged;
            dgvMeetings_SelectionChanged(dgvMeetings, null);

            return true;

        }

        private void forceLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void changeCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCredentials(true, true);
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

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
            logger.Info("Checking Stopped");
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
        }

        private void updateMeetingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateMeetings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop thread if running
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                logger.Info("Checking Stopped on close");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCredentials();
            InitDynOdds();
            Login();
            UpdateMeetings();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            if (((DataGridView)sender).CurrentCell.GetType() != typeof(DataGridViewCheckBoxCell)) { return; }
            DataGridViewCheckBoxCell cell = ((DataGridView)sender).CurrentCell as DataGridViewCheckBoxCell;

            if (cell != null && !cell.ReadOnly)
            {
                cell.Value = cell.Value == null || !((bool)cell.Value);
                this.dgvMeetings.RefreshEdit();
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            new PercentEntryForm(((DataGridView)sender).CurrentRow.DataBoundItem).ShowDialog();
        }

        private void dgvMeetings_SelectionChanged(object sender, EventArgs e)
        {
            dgvEvents.DataSource = new BindingList<Event>(((Meeting)((DataGridView)sender).SelectedRows[0].DataBoundItem).Events);
        }

        private void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).SelectedRows.Count == 0) { return; }
            RunnerOdds odds = null;
            Event evt = ((DataGridView)sender).SelectedRows[0].DataBoundItem as Event;

            if (!(evt.HasOdds()))
            {
                try
                {
                    odds = dynOdds.GetRunnerOdds(evt.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting odds:" + Environment.NewLine + ex.Message,
                        "API Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            foreach (Runner r in evt.Runners)
            {
                if (odds != null)
                {
                    r.AddOdds(odds.GetRunner(r.No));
                }
            }

            dgvRunners.DataSource = new BindingList<Runner>(((Event)((DataGridView)sender).SelectedRows[0].DataBoundItem).Runners);
        }

        private void racingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
