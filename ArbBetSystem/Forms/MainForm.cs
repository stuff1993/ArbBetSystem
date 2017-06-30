using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using T = System.Timers;

using ArbBetSystem.Api;
using ArbBetSystem.Models.BetFair;
using System.Security.Cryptography.X509Certificates;
using ArbBetSystem.Utils;

namespace ArbBetSystem.Forms {
    public partial class MainForm : Form {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainForm));

        private string credsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArbBetSystem" + Path.DirectorySeparatorChar + "Arbing.dat");
        internal static byte[] additionalEntropy = { 1, 5, 9, 2, 7 };
        private enum SavedCredentials { DynamicOdds, BetFair };

        T.Timer doTimer = new T.Timer();
        T.Timer bfTimer = new T.Timer();
        BackgroundWorker doBGWorker = new BackgroundWorker();
        BackgroundWorker bfBGWorker = new BackgroundWorker();

        DynamicOdds dynOdds;
        BetFair bfApi;
        MarketFilter mf = new MarketFilter();
        Dictionary<string, string> bfEventTypeIds;
        BindingList<Meeting> meetings = new BindingList<Meeting>();
        Dictionary<Event, BackgroundWorker> workers = new Dictionary<Event, BackgroundWorker>();
        int DOPreEventCheck;
        int DOPostEventCheck;
        int BFPreEventCheck;
        int BFPostEventCheck;
        TimeZoneInfo Zone = TimeZoneInfo.Local;
        bool CapOdds = false;

        public MainForm() {
            logger.Info("Starting...");
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Starting...");
            InitializeComponent();
            dgvMeetings.AutoGenerateColumns = false;
            dgvEvents.AutoGenerateColumns = false;
            dgvRunners.AutoGenerateColumns = false;

            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Reading Mapping Config");
            Mappings.Other = (ConfigurationManager.GetSection("Mappings/Other") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Mappings.WinBackNames = (ConfigurationManager.GetSection("Mappings/WinBack") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Mappings.WinLayNames = (ConfigurationManager.GetSection("Mappings/WinLay") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Mappings.PlaceBackNames = (ConfigurationManager.GetSection("Mappings/PlaceBack") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Mappings.PlaceLayNames = (ConfigurationManager.GetSection("Mappings/PlaceLay") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Mappings.Order = (ConfigurationManager.GetSection("Mappings/Order") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => int.Parse(n.Value.ToString()));

            Mappings.CountryMappings = (ConfigurationManager.GetSection("Mappings/BetFair/Countries") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Mappings.VenueMappings = (ConfigurationManager.GetSection("Mappings/BetFair/Venues") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Generating Runner grid");
            foreach (Dictionary<string, string> dict in new Dictionary<string, string>[] { Mappings.Other, Mappings.WinBackNames, Mappings.WinLayNames, Mappings.PlaceBackNames, Mappings.PlaceLayNames }) {
                foreach (KeyValuePair<string, string> pair in dict) {
                    DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
                    newCol.DataPropertyName = pair.Key;
                    newCol.HeaderText = pair.Value;
                    newCol.ReadOnly = true;
                    newCol.Name = "run" + pair.Key;
                    dgvRunners.Columns.Add(newCol);
                }
            }

            foreach (KeyValuePair<string, int> pair in Mappings.Order.OrderBy(i => i.Value)) {
                if (dgvRunners.Columns.Contains("run" + pair.Key))
                    dgvRunners.Columns["run" + pair.Key].DisplayIndex = pair.Value;
            }

            dgvRunners.AutoResizeColumns();
            dgvMeetings.DataSource = meetings;
            meetingsToolStripMenuItem.DropDown.Closing += DropDown_Closing;
        }

        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked) {
                e.Cancel = true;
            }
        }

        private void InitDynOdds() {
            if (dynOdds == null) {
                dynOdds = new DynamicOdds(Properties.Settings.Default.DynamicOddsUrl);
                dynOdds.LoginDetails = LoadCredentials(SavedCredentials.DynamicOdds);
            }

        }

        private void InitBetFair() {
            if (bfApi == null) {
                bfApi = new BetFair(Properties.Settings.Default.BetFairAppKey,
                      new X509Certificate2(Properties.Settings.Default.BetFairCert, Properties.Settings.Default.BetFairCertPass),
                      Properties.Settings.Default.BetFairApiUrl,
                      Properties.Settings.Default.BetFairAuthUrl);
                bfApi.LoginDetails = LoadCredentials(SavedCredentials.BetFair);
            }
        }

        private void CheckDynamicOdds() {
            if (dynOdds == null) {
                logger.Error("DynamicOdds object is null");
                MessageBox.Show("DynamicOdds object is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void CheckBetFair() {
            if (bfApi == null) {
                logger.Error("BetFair object is null");
                MessageBox.Show("BetFair object is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private Creds LoadCredentials(SavedCredentials toLoad) {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArbBetSystem"))) { Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArbBetSystem")); }
            if (File.Exists(credsFileName)) {
                string[] contents = Encoding.UTF8.GetString(CryptoUtils.Unprotect(FileUtils.FileToByteArray(credsFileName), additionalEntropy)).Split(Environment.NewLine.ToCharArray());
                try {
                    if (string.IsNullOrWhiteSpace(contents[(int)toLoad * 4]) || string.IsNullOrWhiteSpace(contents[((int)toLoad * 4) + 2])) {
                        logger.Info("Credentials for " + toLoad + " contained an empty string");
                        return SetCredentials(toLoad, false);
                    }
                    return new Creds(contents[(int)toLoad * 4], contents[((int)toLoad * 4) + 2]);
                } catch (IndexOutOfRangeException) {
                    logger.Warn("Credentials file smaller than expected");
                    return SetCredentials(toLoad, false);
                }
            } else {
                logger.Info("No credentials file");
                return SetCredentials(toLoad, false);
            }
        }

        private bool SaveCredentials() {
            return FileUtils.ByteArrayToFile(
                credsFileName,
                CryptoUtils.Protect(
                    Encoding.UTF8.GetBytes(
                        (dynOdds != null && dynOdds.LoginDetails != null ? dynOdds.LoginDetails.ToString() : "" + Environment.NewLine + "" + Environment.NewLine)
                        + (bfApi != null && bfApi.LoginDetails != null ? bfApi.LoginDetails.ToString() : "" + Environment.NewLine + "" + Environment.NewLine)),
                    additionalEntropy)
                    );
        }

        private Creds InputCredentials(string title = "", bool canCancel = true) {
            CredentialsForm loginForm = new CredentialsForm(title, canCancel);
            loginForm.ShowDialog();
            Creds credentials = null;
            if (loginForm.DialogResult == DialogResult.OK)
                credentials = loginForm.LoginDetails;
            loginForm.Dispose();
            return credentials;
        }

        private Creds SetCredentials(SavedCredentials toUpdate, bool canCancel = true) {
            Creds newCreds = InputCredentials(toUpdate.ToString(), canCancel);
            if (newCreds == null || newCreds.Username == null || newCreds.Password == null) {
                logger.Warn("Credentials contains a null value. Not set");
                return null;
            }
            switch (toUpdate) {
                case SavedCredentials.BetFair:
                    bfApi.LoginDetails = newCreds;
                    break;
                case SavedCredentials.DynamicOdds:
                    dynOdds.LoginDetails = newCreds;
                    break;
            }
            SaveCredentials();
            return newCreds;
        }

        private bool DOLogin() {
            CheckDynamicOdds();
            return dynOdds.doLogin();
            //return dynOdds.Login("6C72J91KF17U3861YJX7HEGHQ15Z40EH");
        }

        private bool BFLogin() {
            CheckBetFair();
            return bfApi.doLogin();
        }

        private void UpdateMeetings() {
            GetDOMeetings();
            MapBFtoDO();
            UpdateBFOdds();
        }

        private void forceDOLoginToolStripMenuItem_Click(object sender, EventArgs e) {
            DOLogin();
        }

        private void forceBFLoginToolStripMenuItem_Click(object sender, EventArgs e) {
            BFLogin();
        }

        private void changeDOLoginToolStripMenuItem_Click(object sender, EventArgs e) {
            SetCredentials(SavedCredentials.DynamicOdds);
            DOLogin();
        }

        private void changeBFLoginToolStripMenuItem_Click(object sender, EventArgs e) {
            SetCredentials(SavedCredentials.BetFair);
            BFLogin();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e) {
            // Utilise timers for each datasource.
            // Each tick creates short lived thread and terminates any existing
            doTimer.Start();
            bfTimer.Start();
            // toggle text and functionality
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e) {
            doTimer.Stop();
            bfTimer.Stop();
            logger.Info("Checking Stopped");
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
        }

        private void updateMeetingListToolStripMenuItem_Click(object sender, EventArgs e) {
            UpdateMeetings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
        }

        private void MainForm_Load(object sender, EventArgs e) {
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Loading Check Params");
            DOPreEventCheck = Properties.Settings.Default.DynamicOddsPreEventCheck;
            DOPostEventCheck = Properties.Settings.Default.DynamicOddsPostEventCheck;
            BFPreEventCheck = Properties.Settings.Default.BetFairPreEventCheck;
            BFPostEventCheck = Properties.Settings.Default.BetFairPostEventCheck;

            doBGWorker.DoWork += new DoWorkEventHandler(doWorker);
            bfBGWorker.DoWork += new DoWorkEventHandler(bfWorker);

            doTimer.Interval = Properties.Settings.Default.DynamicOddsPollInterval;
            doTimer.AutoReset = true;
            doTimer.Elapsed += doElapsed;
            bfTimer.Interval = Properties.Settings.Default.BetFairPollInterval;
            bfTimer.AutoReset = true;
            bfTimer.Elapsed += bfElapsed;

            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Init DO");
            InitDynOdds();
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Init BF");
            InitBetFair();
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Login DO");
            DOLogin();
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Login BF");
            BFLogin();

            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Get BF Event Types");
            bfEventTypeIds = (bfApi.listEventTypes(new MarketFilter())
                    .Where(evt => Meeting.getBFMeetingTypesStrings(7).Contains(evt.EventType.Name))
                    .ToDictionary(evt => evt.EventType.Name, evt => evt.EventType.Id));
            mf.EventTypeIds = new HashSet<string>(bfEventTypeIds.Values);
            mf.MarketStartTime = new TimeRange() { To = DateTime.Now.AddDays(1), From = DateTime.Now.AddHours(-1) };
            mf.MarketTypeCodes = new HashSet<string>() { "WIN", "PLACE" };

            Thread.Sleep(1000);
            UpdateMeetings();
        }

        private void dgvMeetings_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            switch (e.ColumnIndex) {
                case 0:
                    DataGridViewCheckBoxCell cell = ((DataGridView)sender).CurrentCell as DataGridViewCheckBoxCell;

                    if (cell != null && !cell.ReadOnly) {
                        cell.Value = cell.Value == null || !((bool)cell.Value);
                        this.dgvMeetings.RefreshEdit();
                    }
                    break;
            }
        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            switch (e.ColumnIndex) {
                case 0:
                    DataGridViewCheckBoxCell cell = ((DataGridView)sender).CurrentCell as DataGridViewCheckBoxCell;

                    if (cell != null && !cell.ReadOnly) {
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

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (sender.GetType() != typeof(DataGridView)) { throw new ArgumentException("Sender is not a DataGridView"); }
            if (((DataGridView)sender).CurrentCell.GetType() != typeof(DataGridViewButtonCell)) {
                PercentEntryForm pef = new PercentEntryForm(((DataGridView)sender).CurrentRow.DataBoundItem);
                pef.ShowDialog();
                pef.Dispose();
            }
        }

        private void dgvMeetings_SelectionChanged(object sender, EventArgs e) {
            dgvEvents.DataSource = new BindingList<Event>(((Meeting)((DataGridView)sender).SelectedRows[0].DataBoundItem).Events);
        }

        private void dgvEvents_SelectionChanged(object sender, EventArgs e) {
            if (((DataGridView)sender).SelectedRows.Count == 0) { return; }
            Event evt = ((DataGridView)sender).SelectedRows[0].DataBoundItem as Event;

            if (!(evt.HasOdds())) {
                try {
                    evt.UpdateOdds(dynOdds.GetRunnerOdds(evt));
                } catch (System.Exception ex) {
                    MessageBox.Show("Error getting odds:" + Environment.NewLine + ex.Message,
                        "API Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            dgvRunners.DataSource = new BindingList<Runner>(((Event)((DataGridView)sender).SelectedRows[0].DataBoundItem).Runners);
        }

        private void pickTimeZoneToolStripMenuItem_Click(object sender, EventArgs e) {
            TimeZoneSelectorForm dialog = new TimeZoneSelectorForm(Zone);

            if (dialog.ShowDialog() == DialogResult.OK) {
                Zone = dialog.SelectedTimeZone;
            }
            dialog.Dispose();

            return;
        }

        private void capOddsToolStripMenuItem_Click(object sender, EventArgs e) {
            CapOdds = capOddsToolStripMenuItem.Checked;
        }

        private void doElapsed(object sender, T.ElapsedEventArgs e) {
            if (!doBGWorker.IsBusy) {
                doBGWorker.RunWorkerAsync();
            }
        }

        private void bfElapsed(object sender, T.ElapsedEventArgs e) {
            if (!bfBGWorker.IsBusy) {
                bfBGWorker.RunWorkerAsync();
            }
        }

        private void doWorker(object sender, DoWorkEventArgs args) {
            foreach (Event evt in meetings.SelectMany(m => m.Events).Where(evt => evt.Check
                    && evt.StartTime.AddMinutes(-DOPreEventCheck) <= DateTime.Now
                    && evt.StartTime.AddMinutes(DOPostEventCheck) >= DateTime.Now)) {
                try {
                    RunnerOdds odds = dynOdds.GetRunnerOdds(evt);
                    foreach (Runner r in evt.Runners) {
                        r.UpdateDOOdds(odds.GetRunner(r.No), evt.WinMarketId == null && evt.PlaceMarketId == null);
                        r.CheckMatch(Mappings.WinBackNames, Mappings.WinLayNames, Mappings.PlaceBackNames, Mappings.PlaceLayNames, CapOdds);
                    }
                } catch (HttpRequestException ex) {
                    logger.Warn("GetRunnerOdds - Suppressed: " + evt, ex);
                    if (ex.Message == "Incorrect SessionID Provided") {
                        // Hack around weird sessionIds
                        logger.Warn("Attempting Auto Login");
                        dynOdds.doLogin();
                    }
                }
            }
        }

        private void bfWorker(object sender, DoWorkEventArgs e) {
            UpdateBFOdds();
        }

        private bool GetDOMeetings() {
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " DO Meetings");
            CheckDynamicOdds();
            BindingList<Meeting> newDynOdds = null;
            try {
                int type = (racingToolStripMenuItem.Checked ? (int)Meeting.MeetingTypes.Racing : 0)
                    + (harnessToolStripMenuItem.Checked ? (int)Meeting.MeetingTypes.Harness : 0)
                    + (greyhoundToolStripMenuItem.Checked ? (int)Meeting.MeetingTypes.Greyhound : 0);

                DateTime date = DateTime.UtcNow.Add(Zone.GetUtcOffset(DateTime.UtcNow));

                Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Get DO Meetings");
                newDynOdds = dynOdds.GetMeetingsAll(date, type);
                mf.EventTypeIds = new HashSet<string>(bfEventTypeIds.Where(evt => Meeting.getBFMeetingTypesStrings(type).Contains(evt.Key)).Select(evt => evt.Value));

            } catch (HttpRequestException e) {
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + e.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            } catch (System.Exception e) {
                logger.Error("Error updating meetings", e);
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + e.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (newDynOdds == null) {
                logger.Error("Meetings list is null");
                MessageBox.Show("Error updating meetings:" + Environment.NewLine + "Meetings list is null",
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Add/Remove Meetings");
            List<Meeting> toRemove = meetings.Where(o1 => newDynOdds.All(o2 => o2.ID != o1.ID)).ToList();
            dgvMeetings.SelectionChanged -= dgvMeetings_SelectionChanged;
            foreach (Meeting m in toRemove) { // meeting not in tmp
                meetings.Remove(meetings.First(o1 => o1.ID == m.ID));
            }

            foreach (Meeting m in newDynOdds.Where(o1 => meetings.Any(o2 => o2.ID == o1.ID))) { // meeting in both
                Meeting old = meetings.First(o1 => o1.ID == m.ID);
                int i = meetings.IndexOf(old);
                meetings[i] = old.MergeWith(m);
                meetings.ResetItem(i);
            }

            foreach (Meeting m in newDynOdds.Where(o1 => meetings.All(o2 => o2.ID != o1.ID))) { // meeting only in tmp
                meetings.Add(m);
            }

            dgvMeetings.SelectionChanged += dgvMeetings_SelectionChanged;
            if (dgvMeetings.Rows.Count > 0) {
                dgvMeetings.Rows[0].Selected = true;
                dgvMeetings_SelectionChanged(dgvMeetings, null);
            }

            return true;
        }

        private void MapBFtoDO() {
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " MapBFtoDO");
            ISet<MarketProjection> projections = new HashSet<MarketProjection>();
            projections.Add(MarketProjection.COMPETITION);
            projections.Add(MarketProjection.EVENT);
            projections.Add(MarketProjection.EVENT_TYPE);
            projections.Add(MarketProjection.MARKET_DESCRIPTION);
            projections.Add(MarketProjection.RUNNER_DESCRIPTION);
            projections.Add(MarketProjection.RUNNER_METADATA);
            // Restrict BF matching to UK horse races
            // This is ridiculous O(n^4) loop to match events between DO and BF
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Loop through Meetings");
            foreach (Meeting m in meetings.Where(m => m.Country == "UK" && m.Type == "R")) {
                Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " " + m.ToString());
                mf.Venues = new HashSet<string>() { m.BFVenue };
                mf.MarketCountries = new HashSet<string>() { m.BFCountry };
                Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Get BF Catalogue");
                IList<MarketCatalogue> MarketCatalogues = bfApi.listMarketCatalogue(mf, projections, MarketSort.FIRST_TO_START, "100");
                foreach (MarketCatalogue cat in MarketCatalogues) {
                    foreach (Event e in m.Events.Where(e => e.WinMarketId == null || e.PlaceMarketId == null)) {
                        bool Found = true;
                        foreach (RunnerDescription runner in cat.Runners) {
                            if (e.Runners.All(r => r.BFNumName != runner.RunnerName.ToLower() && r.BFName != runner.RunnerName.ToLower())) {
                                Found = false;
                                break;
                            }
                        }
                        if (Found) {
                            if (cat.Description.MarketType == "WIN") {
                                e.WinMarketId = cat.MarketId;
                            } else if (cat.Description.MarketType == "PLACE") {
                                e.PlaceMarketId = cat.MarketId;
                            } else {
                                logger.Debug("Catalogue Market Type not WIN or PLACE: " + cat.Description.MarketType);
                            }
                            foreach (RunnerDescription runner in cat.Runners) {
                                e.Runners.Where(r => r.BFNumName == runner.RunnerName.ToLower() || r.BFName == runner.RunnerName.ToLower()).First().SelectionId = runner.SelectionId;
                            }
                            break;
                        }
                    }
                    if (m.Events.Where(e => e.WinMarketId == null || e.PlaceMarketId == null).Count() == 0) {
                        break;
                    }
                }
            }

            return;
        }

        private bool UpdateBFOdds() {
            Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " UpdateBFOdds");
            IList<Event> events = meetings.SelectMany(m => m.Events).Where(e => e.WinMarketId != null || e.PlaceMarketId != null).ToList();
            PriceProjection projection = new PriceProjection() { PriceData = new HashSet<PriceData>() { PriceData.EX_ALL_OFFERS } };

            foreach (Event e in events) {
                if (e.WinMarketId != null) {
                    Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " List Win Market for " + e.ToString());
                    IList<MarketBook> mb = bfApi.listMarketBook(new List<string>() { e.WinMarketId }, projection);
                    if (mb.Count > 1) {
                        logger.Warn("Multiple markets found for id " + e.WinMarketId + ", using first returned.");
                    }
                    if (mb.Count == 0) {
                        logger.Error("No market found for id " + e.WinMarketId);
                    } else {
                        Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Update Runners");
                        e.UpdateBFOdds(mb.First().Runners, false);
                    }
                }

                if (e.PlaceMarketId != null) {
                    Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " List Place Market for " + e.ToString());
                    IList<MarketBook>  mb = bfApi.listMarketBook(new List<string>() { e.PlaceMarketId }, projection);
                    if (mb.Count > 1) {
                        logger.Warn("Multiple markets found for id " + e.PlaceMarketId + ", using first returned.");
                    }
                    if (mb.Count == 0) {
                        logger.Error("No market found for id " + e.PlaceMarketId);
                    } else {
                        Console.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + " Update Runners");
                        e.UpdateBFOdds(mb.First().Runners, true);
                    }
                }
            }

            //IList<string> marketIds = meetings.SelectMany(m => m.Events).Where(e => e.MarketId != null).Select(e => e.MarketId).ToList();

            //List<MarketBook> y = new List<MarketBook>();
            //foreach (string id in marketIds) {
            //    y.AddRange(bfApi.listMarketBook(new List<string>() { id }, projection));
            //}

            return true;
        }

        private void matchUKVenuesToolStripMenuItem_Click(object sender, EventArgs e) {
            IList<string> doVenues = meetings.Where(m => m.Country == "UK" && m.Type == "R").Select(m => m.Venue).ToList();

            MarketFilter venueFilter = new MarketFilter();
            venueFilter.EventTypeIds = new HashSet<string>(bfEventTypeIds.Values);
            venueFilter.MarketCountries = new HashSet<string>() { Mappings.getCountry("UK") };
            IList<string> bfVenues = bfApi.listVenues(venueFilter).Select(v => v.Venue).ToList();

            MatchUKVenues muv = new MatchUKVenues(doVenues, bfVenues);
            muv.ShowDialog();
            muv.Dispose();
        }
    }
}

