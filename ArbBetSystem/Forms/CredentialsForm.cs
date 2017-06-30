using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbBetSystem.Forms
{
    public partial class CredentialsForm : Form
    {
        public Creds LoginDetails { get; private set; }
        private bool canCancel;

        public CredentialsForm(string title = "", bool canCancel = true)
        {
            InitializeComponent();
            this.canCancel = canCancel;
            if (!canCancel) this.btnCancel.Text = "Close";
            this.Text = this.Text + " - " + title;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LoginDetails = new Creds(txtUser.Text, txtPass.Text);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!canCancel) Environment.Exit(1);
            Close();
        }
    }
}
