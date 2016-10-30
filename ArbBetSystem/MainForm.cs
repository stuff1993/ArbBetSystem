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
                return true;
            } catch (Exception e) {
                MessageBox.Show("GetMeetingsAll Error" + Environment.NewLine + e.Message, 
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
    }
}
