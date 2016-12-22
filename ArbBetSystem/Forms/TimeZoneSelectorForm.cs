using System;
using System.Windows.Forms;

namespace ArbBetSystem
{
    public partial class TimeZoneSelectorForm : Form
    {
        public TimeZoneInfo SelectedTimeZone { get; private set; }

        public TimeZoneSelectorForm(TimeZoneInfo current)
        {
            InitializeComponent();
            cmbTimeZones.DataSource = TimeZoneInfo.GetSystemTimeZones();
            int i = cmbTimeZones.Items.IndexOf(current);
            if (i != -1)
            {
                cmbTimeZones.SelectedItem = current;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            SelectedTimeZone = (TimeZoneInfo)cmbTimeZones.SelectedItem;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            SelectedTimeZone = TimeZoneInfo.Local;
            Close();
        }
    }
}
