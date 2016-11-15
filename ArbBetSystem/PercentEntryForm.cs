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
    public partial class PercentEntryForm : Form
    {
        // TODO: Make this dynamic
        object obj;

        public PercentEntryForm(object o)
        {
            InitializeComponent();
            this.obj = o;
            btnOk.Enabled = false;
            txtPercent.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (obj.GetType() == typeof(Runner))
            {
                ((Runner)obj).Percent = double.Parse(txtPercent.Text);
            }
            else if (obj.GetType() == typeof(Event))
            {
                double p = double.Parse(txtPercent.Text);
                foreach (Runner r in ((Event)obj).Runners)
                {
                    r.Percent = p;
                }
            }
            else if (obj.GetType() == typeof(Meeting))
            {
                double p = double.Parse(txtPercent.Text);
                foreach (Runner r in ((Meeting)obj).Events.SelectMany(evt => evt.Runners))
                {
                    r.Percent = p;
                }
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
