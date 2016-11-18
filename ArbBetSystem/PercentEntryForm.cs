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
            txtPercent.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (obj.GetType() == typeof(Runner))
            {
                ((Runner)obj).Percent = double.Parse(txtPercent.Text);
                logger.Info("Percent changed on " + ((Runner)obj).Parent + ", " + ((Runner)obj) + " to " + txtPercent.Text);
            }
            else if (obj.GetType() == typeof(Event))
            {
                double p = double.Parse(txtPercent.Text);
                foreach (Runner r in ((Event)obj).Runners)
                {
                    r.Percent = p;
                }
                logger.Info("Percent changed on " + ((Event)obj) + " to " + p);
            }
            else if (obj.GetType() == typeof(Meeting))
            {
                double p = double.Parse(txtPercent.Text);
                foreach (Runner r in ((Meeting)obj).Events.SelectMany(evt => evt.Runners))
                {
                    r.Percent = p;
                }
                logger.Info("Percent changed on " + ((Meeting)obj) + " to " + p);
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
            if (double.TryParse(txtPercent.Text, out num))
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
