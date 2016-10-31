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
        static private readonly int RUNNERS_PERCENT_INDEX = 4;
        ListView view;
        ListViewItem item;

        public PercentEntryForm(ListView view, ListViewItem lvi)
        {
            InitializeComponent();
            this.view = view;
            item = lvi;
            btnOk.Enabled = false;
            txtPercent.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (item.Tag.GetType() == typeof(Runner))
            {
                ((Runner)item.Tag).Percent = double.Parse(txtPercent.Text);
                item.SubItems[RUNNERS_PERCENT_INDEX].Text = txtPercent.Text + "%";
            }
            else if (item.Tag.GetType() == typeof(Event))
            {
                double p = double.Parse(txtPercent.Text);
                foreach (Runner r in ((Event)item.Tag).Runners)
                {
                    r.Percent = p;
                }
                foreach (ListViewItem i in view.Items)
                {
                    i.SubItems[RUNNERS_PERCENT_INDEX].Text = txtPercent.Text + "%";
                }
            }
            else if (item.Tag.GetType() == typeof(Meeting))
            {
                double p = double.Parse(txtPercent.Text);
                foreach (Event evt in ((Meeting)item.Tag).Events)
                {
                    foreach (Runner r in evt.Runners)
                    {
                        r.Percent = p;
                    }
                }
                foreach (ListViewItem i in view.Items)
                {
                    i.SubItems[RUNNERS_PERCENT_INDEX].Text = txtPercent.Text + "%";
                };
            }
            else
            {
                throw new Exception("Unrecognised type");
            }
            Close();
            Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
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
