using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        Dictionary<Event, BackgroundWorker> workers = new Dictionary<Event, BackgroundWorker>();
        int PollInterval;
        int PreEventCheck;
        int PostEventCheck;
        TimeZoneInfo Zone = TimeZoneInfo.Local;
        bool CapOdds = false;

        public MainForm()
        {
            logger.Info("Starting...");
            InitializeComponent();
            dgvMeetings.AutoGenerateColumns = false;
            dgvEvents.AutoGenerateColumns = false;
            dgvRunners.AutoGenerateColumns = false;

            foreach (KeyValuePair<string, string> pair in Runner.WinLayNames.Union(Runner.WinBackNames).Union(Runner.PlaceLayNames).Union(Runner.PlaceBackNames))
            {
                DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
                newCol.DataPropertyName = pair.Key;
                newCol.HeaderText = pair.Value;
                newCol.ReadOnly = true;
                newCol.Name = "run" + pair.Key;
                dgvRunners.Columns.Add(newCol);
            }

            dgvMeetings.DataSource = meetings;
            meetingsToolStripMenuItem.DropDown.Closing += DropDown_Closing;
        }

        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        private void CancelChecks()
        {
            var currentWorkers = workers.Where(w => w.Value.IsBusy).ToList();
            foreach (KeyValuePair<Event, BackgroundWorker> pair in currentWorkers)
            {
                pair.Value.CancelAsync();
                workers.Remove(pair.Key);
            }
        }

        private void GetAndCheckOdds(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Event e = args.Argument as Event;
            logger.Info("Checking event: " + e);

            // Runner object, Bet field, Arb found
            Dictionary<Runner, Dictionary<string, bool>> hasMatched = new Dictionary<Runner, Dictionary<string, bool>>();
            foreach (Runner r in e.Runners)
            {
                Dictionary<string, bool> backMatched = new Dictionary<string, bool>();
                foreach (KeyValuePair<string, string> back in Runner.WinBackNames)
                {
                    backMatched.Add(back.Key, false);
                }
                hasMatched.Add(r, backMatched);
            }

            while (!worker.CancellationPending)
            {
                if (e.StartTime.AddMinutes(-PreEventCheck) <= DateTime.Now
                    && e.StartTime.AddMinutes(PostEventCheck) >= DateTime.Now)
                {
                    try
                    {
                        RunnerOdds odds = dynOdds.GetRunnerOdds(e);
                        foreach (Runner r in e.Runners)
                        {
                            r.UpdateOdds(odds.GetRunner(r.No));
                            foreach (string layField in Runner.WinLayNames.Keys)
                            {
                                double lay = (double)typeof(RunnerOdd).GetProperty(layField).GetValue(r);
                                if (r.WinPercent != 0 && lay > 0)
                                {
                                    foreach (KeyValuePair<string, string> back in Runner.WinBackNames)
                                    {
                                        double val = (double)typeof(RunnerOdd).GetProperty(back.Key).GetValue(r);
                                        if (val > 0 && lay * (1 + r.WinPercent / 100.0) < val && !hasMatched[r][back.Key] && (val < 10 && lay < 10 || !CapOdds))
                                        {
                                            hasMatched[r][back.Key] = true;
                                            logger.Info("Match found: " + e + ", " + r + ", Lay: " + lay + ", Back: " + val + ", Book: " + back.Value);
                                            SystemSounds.Exclamation.Play();
                                            new Thread(() =>
                                            {
                                                MessageBox.Show(e + Environment.NewLine + r + Environment.NewLine + "Lay: " + lay + Environment.NewLine + "Back: " + val + Environment.NewLine + "Book: " + back.Value, "Match found:", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                            }).Start();
                                        }
                                        else if (lay * (1 + r.WinPercent / 100.0) >= val)
                                        {
                                            hasMatched[r][back.Key] = false;
                                        }
                                    }
                                }
                            }

                            foreach (string layField in Runner.PlaceLayNames.Keys)
                            {
                                double lay = (double)typeof(RunnerOdd).GetProperty(layField).GetValue(r);
                                if (r.PlacePercent != 0 && lay > 0)
                                {
                                    foreach (KeyValuePair<string, string> back in Runner.PlaceBackNames)
                                    {
                                        double val = (double)typeof(RunnerOdd).GetProperty(back.Key).GetValue(r);
                                        if (val > 0 && lay * (1 + r.PlacePercent / 100.0) < val && !hasMatched[r][back.Key] && (val < 10 && lay < 10 || !CapOdds))
                                        {
                                            hasMatched[r][back.Key] = true;
                                            logger.Info("Match found: " + e + ", " + r + ", Lay: " + lay + ", Back: " + val + ", Book: " + back.Value);
                                            SystemSounds.Exclamation.Play();
                                            new Thread(() =>
                                            {
                                                MessageBox.Show(e + Environment.NewLine + r + Environment.NewLine + "Lay: " + lay + Environment.NewLine + "Back: " + val + Environment.NewLine + "Book: " + back.Value, "Match found:", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                            }).Start();
                                        }
                                        else if (lay * (1 + r.PlacePercent / 100.0) >= val)
                                        {
                                            hasMatched[r][back.Key] = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        logger.Warn("GetRunnerOdds - Suppressed: " + e, ex);
                        if (ex.Message == "Incorrect SessionID Provided")
                        {
                            logger.Warn("Attempting Auto Login");
                            dynOdds.Login(creds);
                        }
                    }
                }
                else if (e.StartTime.AddMinutes(PostEventCheck) < DateTime.Now)
                {
                    logger.Info("Finished " + e);
                    return;
                }
                else
                {
                    logger.Debug("Skipped event: " + e + ", Event Time: " + e.StartTime);
                }

                Thread.Sleep(PollInterval);
            }
            args.Cancel = true;
        }

        private void InitDynOdds()
        {
            dynOdds = new DynamicOdds(Properties.Settings.Default.DynamicOddsUrl);
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
            loginForm.Dispose();
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
                int type = (racingToolStripMenuItem.Checked ? (int)Meeting.MeetingTypes.Racing : 0)
                    + (harnessToolStripMenuItem.Checked ? (int)Meeting.MeetingTypes.Harness : 0)
                    + (greyhoundToolStripMenuItem.Checked ? (int)Meeting.MeetingTypes.Greyhound : 0);

                DateTime date = DateTime.UtcNow.Add(Zone.GetUtcOffset(DateTime.UtcNow));

                tmp = dynOdds.GetMeetingsAll(date, type);
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + e.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception e)
            {
                logger.Error("Error updating meetings", e);
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
            if (dgvMeetings.Rows.Count > 0)
            {
                dgvMeetings.Rows[0].Selected = true;
                dgvMeetings_SelectionChanged(dgvMeetings, null);
            }

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
            logger.Info("Checking Odds");
            List<Event> events = meetings
                .SelectMany(m => m.Events)
                .Where(evt => evt.Check 
                && evt.StartTime > DateTime.Now.AddMinutes(-PostEventCheck)
                && !workers.ContainsKey(evt))
                .ToList();
            foreach (Event evt in events)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler(GetAndCheckOdds);
                workers.Add(evt, worker);
                worker.RunWorkerAsync(evt);
            }
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            // toggle text and functionality
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelChecks();
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
            if (workers.Count > 0)
            {
                CancelChecks();
                logger.Info("Checking Stopped on close");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            PollInterval = Properties.Settings.Default.PollInterval;
            PreEventCheck = Properties.Settings.Default.PreEventCheck;
            PostEventCheck = Properties.Settings.Default.PostEventCheck;

            LoadCredentials();
            InitDynOdds();
            Login();
            Thread.Sleep(1000);
            UpdateMeetings();
        }

        private void dgvMeetings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            switch (e.ColumnIndex)
            {
                case 0:
                    DataGridViewCheckBoxCell cell = ((DataGridView)sender).CurrentCell as DataGridViewCheckBoxCell;

                    if (cell != null && !cell.ReadOnly)
                    {
                        cell.Value = cell.Value == null || !((bool)cell.Value);
                        this.dgvMeetings.RefreshEdit();
                    }
                    break;
            }
        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            switch (e.ColumnIndex)
            {
                case 0:
                    DataGridViewCheckBoxCell cell = ((DataGridView)sender).CurrentCell as DataGridViewCheckBoxCell;

                    if (cell != null && !cell.ReadOnly)
                    {
                        cell.Value = cell.Value == null || !((bool)cell.Value);
                        this.dgvMeetings.RefreshEdit();
                    }
                    break;
                case 3:
                    ((Event)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem).StartTime = ((Event)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem).StartTime.AddDays(1);
                    this.dgvMeetings.RefreshEdit();
                    break;
                case 4:
                    ((Event)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem).StartTime = ((Event)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem).StartTime.AddDays(-1);
                    this.dgvMeetings.RefreshEdit();
                    break;
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            if (((DataGridView)sender).CurrentCell.GetType() != typeof(DataGridViewButtonCell))
            {
                PercentEntryForm pef = new PercentEntryForm(((DataGridView)sender).CurrentRow.DataBoundItem);
                pef.ShowDialog();
                pef.Dispose();
            }
        }

        private void dgvMeetings_SelectionChanged(object sender, EventArgs e)
        {
            dgvEvents.DataSource = new BindingList<Event>(((Meeting)((DataGridView)sender).SelectedRows[0].DataBoundItem).Events);
        }

        private void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).SelectedRows.Count == 0) { return; }
            Event evt = ((DataGridView)sender).SelectedRows[0].DataBoundItem as Event;

            if (!(evt.HasOdds()))
            {
                try
                {
                    evt.UpdateOdds(dynOdds.GetRunnerOdds(evt));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting odds:" + Environment.NewLine + ex.Message,
                        "API Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            dgvRunners.DataSource = new BindingList<Runner>(((Event)((DataGridView)sender).SelectedRows[0].DataBoundItem).Runners);
        }

        private void pickTimeZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeZoneSelectorForm dialog = new TimeZoneSelectorForm(Zone);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Zone = dialog.SelectedTimeZone;
            }
            dialog.Dispose();

            return;
        }

        private void capOddsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapOdds = capOddsToolStripMenuItem.Checked;
        }
    }
}
