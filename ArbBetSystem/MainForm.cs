using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public MainForm()
        {
            logger.Info("Starting...");
            InitializeComponent();
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
            //return dynOdds.Login("Z2QY9R4UIUU12NOS84Y8N87CK6STBS2I");
        }

        private bool UpdateMeetings()
        {
            CheckDynamicOdds();
            try {
                List<Meeting> tmp = dynOdds.GetMeetingsAll();
                if (tmp == null)
                {
                    logger.Error("Meetings list is null");
                    throw new Exception("Meetings list is null");
                }
                meetings = tmp;

                lvwMeetings.Items.Clear();

                foreach(Meeting m in meetings)
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
            } catch (Exception e) {
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

        private void lvwEvents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                lvwRunners.Items.Clear();

                foreach (Runner r in ((Event)e.Item.Tag).Runners)
                {
                    ListViewItem item = new ListViewItem(r.No.ToString());
                    item.Tag = r;
                    item.SubItems.Add(r.Name);
                    item.SubItems.Add(r.Jockey);
                    item.SubItems.Add(r.Trainer);
                    lvwRunners.Items.Add(item);
                }
                lvwRunners.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvwRunners.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (lvwRunners.Items.Count > 0) { lvwRunners.Items[0].Selected = true; }
                // check for existing odds
                if (true)
                {
                    // poll for odds
                    List<Runner> tmp = dynOdds.GetRunnersOdds(((Event)e.Item.Tag).ID);
                }
            }
        }

        private void lvwRunners_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            
        }
    }
}
