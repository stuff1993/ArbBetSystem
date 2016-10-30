using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbBetSystem
{
    public partial class CredentialsForm : Form
    {
        MainForm mainForm;
        bool shouldSave;

        public CredentialsForm(MainForm parent, bool canCancel, bool shouldSave)
        {
            InitializeComponent();
            mainForm = parent;
            btnCancel.Enabled = canCancel;
            this.shouldSave = shouldSave;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mainForm.SetCredentials(new Creds(txtUser.Text, txtPass.Text), shouldSave);
            Close();
            Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
