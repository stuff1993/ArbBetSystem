using log4net;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ArbBetSystem
{
    public partial class PercentEntryForm : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PercentEntryForm));
        object obj;

        public PercentEntryForm(object o)
        {
            InitializeComponent();
            obj = o;
            btnOk.Enabled = false;
            txtWinPercent.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            double num;
            if (obj.GetType() == typeof(Runner))
            {
                Runner r = (Runner)obj;
                r.WinPercent = double.TryParse(txtWinPercent.Text, out num) ? double.Parse(txtWinPercent.Text) : r.WinPercent;
                r.PlacePercent = double.TryParse(txtPlacePercent.Text, out num) ? double.Parse(txtPlacePercent.Text) : r.PlacePercent;
                logger.Info("Percent changed on " + ((Runner)obj).Parent + ", " + ((Runner)obj) + " to " + txtWinPercent.Text + " / " + txtPlacePercent.Text);
            }
            else if (obj.GetType() == typeof(Event))
            {
                double? wp = double.TryParse(txtWinPercent.Text, out num) ? (double?)double.Parse(txtWinPercent.Text) : null;
                double? pp = double.TryParse(txtPlacePercent.Text, out num) ? (double?)double.Parse(txtPlacePercent.Text) : null;
                foreach (Runner r in ((Event)obj).Runners)
                {
                    r.WinPercent = wp.HasValue ? wp.Value : r.WinPercent;
                    r.PlacePercent = pp.HasValue ? pp.Value : r.PlacePercent;
                }
                logger.Info("Percent changed on " + ((Event)obj) + " to " + txtWinPercent.Text + " / " + txtPlacePercent.Text);
            }
            else if (obj.GetType() == typeof(Meeting))
            {
                double? wp = double.TryParse(txtWinPercent.Text, out num) ? (double?)double.Parse(txtWinPercent.Text) : null;
                double? pp = double.TryParse(txtPlacePercent.Text, out num) ? (double?)double.Parse(txtPlacePercent.Text) : null;
                foreach (Runner r in ((Meeting)obj).Events.SelectMany(evt => evt.Runners))
                {
                    r.WinPercent = wp.HasValue ? wp.Value : r.WinPercent;
                    r.PlacePercent = pp.HasValue ? pp.Value : r.PlacePercent;
                }
                logger.Info("Percent changed on " + ((Meeting)obj) + " to " + txtWinPercent.Text + " / " + txtPlacePercent.Text);
            }
            else
            {
                throw new Exception("Unrecognised type");
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPercent_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (
                (double.TryParse(txtWinPercent.Text, out num) || txtWinPercent.Text.Length == 0)
                &&
                (double.TryParse(txtPlacePercent.Text, out num) || txtPlacePercent.Text.Length == 0)
                && 
                (txtWinPercent.Text.Length != 0 || txtPlacePercent.Text.Length != 0)
               )
            {
                btnOk.Enabled = true;
            }
            else
            {
                btnOk.Enabled = false;
            }
        }
    }
}
