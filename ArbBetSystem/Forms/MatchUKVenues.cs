using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace ArbBetSystem.Forms {
    public partial class MatchUKVenues : Form {
        private Dictionary<string, bool> doVenues_All = new Dictionary<string, bool>();
        private Dictionary<string, bool> bfVenues_All = new Dictionary<string, bool>();
        private BindingList<string> doVenues_Display;
        private BindingList<string> bfVenues_Display;
        private XmlDocument config = new XmlDocument();

        private readonly string DEFAULT_MAPPING = "<Default>";
        private readonly string XPATH_VENUE_CONFIG = "//Mappings/BetFair/Venues";

        public MatchUKVenues(IEnumerable<string> doVenues, IEnumerable<string> bfVenues) {
            InitializeComponent();
            config.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            XmlNodeList nodes = config.SelectNodes(XPATH_VENUE_CONFIG  + "/add");
            bfVenues_All.Add(DEFAULT_MAPPING, false);
            foreach (XmlNode node in nodes) {
                doVenues_All.Add(node.Attributes["key"].Value, true);
                bfVenues_All.Add(node.Attributes["value"].Value, true);
            }

            foreach (string v in doVenues.Where(v => !doVenues_All.ContainsKey(v))) {
                doVenues_All.Add(v, false);
            }
            foreach (string v in bfVenues.Where(v => !bfVenues_All.ContainsKey(v))) {
                bfVenues_All.Add(v, false);
            }

            doVenues_Display = new BindingList<string>(doVenues_All.Where(p => !p.Value || chkShowMatchedDO.Checked).Select(p => p.Key).ToList());
            bfVenues_Display = new BindingList<string>(bfVenues_All.Where(p => !p.Value || chkShowMatchedBF.Checked).Select(p => p.Key).ToList());

            lstDoVenues.DataSource = doVenues_Display;
            cmbBFVenues.DataSource = bfVenues_Display;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            XmlNode node = config.SelectSingleNode(XPATH_VENUE_CONFIG + "/add[@key='" + lstDoVenues.SelectedValue.ToString() + "']");

            if (cmbBFVenues.SelectedValue.ToString() == DEFAULT_MAPPING) {
                // Remove config if exists
                if (node != null) {
                    node.ParentNode.RemoveChild(node);
                }
                doVenues_All[lstDoVenues.SelectedValue.ToString()] = false;
                bfVenues_All[cmbBFVenues.SelectedValue.ToString()] = false;
            } else {
                // Add/edit config
                if (node != null) {
                    config.SelectSingleNode(XPATH_VENUE_CONFIG + "/add[@key='" + lstDoVenues.SelectedValue.ToString() + "']").Attributes["value"].Value = cmbBFVenues.SelectedValue.ToString();
                } else {
                    XmlElement newNode = config.CreateElement("add");
                    newNode.SetAttribute("key", lstDoVenues.SelectedValue.ToString());
                    newNode.SetAttribute("value", cmbBFVenues.SelectedValue.ToString());
                    config.SelectSingleNode(XPATH_VENUE_CONFIG).AppendChild(newNode);
                    doVenues_All[lstDoVenues.SelectedValue.ToString()] = true;
                    bfVenues_All[cmbBFVenues.SelectedValue.ToString()] = true;
                }
            }
            config.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("Mappings/BetFair");
        }

        private void lstDoVenues_SelectedIndexChanged(object sender, EventArgs e) {
            XmlNode node = config.SelectSingleNode(XPATH_VENUE_CONFIG + "/add[@key='" + lstDoVenues.SelectedValue.ToString() + "']");
            if (node != null) {
                cmbBFVenues.SelectedIndex = cmbBFVenues.FindString(node.Attributes["value"].Value);
            } else {
                cmbBFVenues.SelectedIndex = cmbBFVenues.FindString(DEFAULT_MAPPING);
            }
        }

        private void chkShowMatchedDO_CheckedChanged(object sender, EventArgs e) {
            if (chkShowMatchedDO.Checked) {
                chkShowMatchedBF.Checked = true;
                chkShowMatchedBF.Enabled = false;
                foreach (string k in doVenues_All.Where(p => p.Value).Select(p => p.Key).ToList()) {
                    doVenues_Display.Add(k);
                }
            } else {
                chkShowMatchedBF.Enabled = true;
                foreach (string k in doVenues_All.Where(p => p.Value).Select(p => p.Key).ToList()) {
                    doVenues_Display.Remove(k);
                }
            }
        }

        private void chkShowMatchedBF_CheckedChanged(object sender, EventArgs e) {
            if (chkShowMatchedBF.Checked) {
                foreach (string k in bfVenues_All.Where(p => p.Value).Select(p => p.Key).ToList()) {
                    bfVenues_Display.Add(k);
                }
            } else {
                foreach (string k in bfVenues_All.Where(p => p.Value).Select(p => p.Key).ToList()) {
                    bfVenues_Display.Remove(k);
                }
            }
        }
    }
}
