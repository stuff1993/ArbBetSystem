using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class RunnerOdds
    {

        private List<RunnerOdd> Odds = new List<RunnerOdd>();

        private void AddRunners(int length)
        {
            while (Odds.Count < length)
            {
                Odds.Add(new RunnerOdd());
            }
        }

        /// <remarks/>
        public string RNo
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.RNo));
            }
            set
            {
                byte[] tmp = value.Split(',').Select(s => Convert.ToByte(s, 10)).ToArray();
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    Odds.ElementAt(i).RNo = tmp[i];
                }
            }
        }

        /// <remarks/>
        public string RName
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.RName));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    Odds.ElementAt(i).RName = tmp[i];
                }
            }
        }

        /// <remarks/>
        public string Scr
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.Scr));
            }
            set
            {
                string[] tmp = value.Split(','); AddRunners(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).Scr = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsV
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsV.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsV = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsV = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsV_P
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsV_P.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsV_P = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsV_P = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsN
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsN.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsN = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsN = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsN_P
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsN_P.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsN_P = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsN_P = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsQ
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsQ = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsQ = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsQ_P
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ_P.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsQ_P = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsQ_P = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsAT
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsAT.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsAT = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsAT = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsNZ
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsNZ.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsNZ = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsNZ = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBE
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBE.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBE = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBE = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsIAS
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsIAS.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsIAS = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsIAS = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsIAS_2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsIAS_2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsIAS_2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsIAS_2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSB_2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB_2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSB_2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSB_2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSB_3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB_3.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSB_3 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSB_3 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsCB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsCB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsCB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsCB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSB2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSB2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSB2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSA
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSA.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSA = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSA = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsCR
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsCR.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsCR = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsCR = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBC
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBC.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBC = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBC = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsWB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsWB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsWB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsWB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsYBB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsYBB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsYBB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsYBB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsPB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsPB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsPB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsPB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBS
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBS.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBS = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBS = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_B3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B3.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_B3 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_B3 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_B3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B3.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_B3 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_B3 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_B2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_B2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_B2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_B2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_B2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_B2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_B1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B1.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_B1 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_B1 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_B1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B1.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_B1 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_B1 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_L1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L1.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_L1 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_L1 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_L1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L1.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_L1 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_L1 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_L2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_L2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_L2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_L2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_L2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_L2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_L3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L3.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_L3 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_L3 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_L3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L3.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_L3 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_L3 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_B3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B3_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_B3_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_B3_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_B3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B3_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_B3_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_B3_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_B2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B2_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_B2_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_B2_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_B2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B2_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_B2_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_B2_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_B1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B1_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_B1_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_B1_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_B1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B1_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_B1_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_B1_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_L1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L1_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_L1_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_L1_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_L1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L1_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_L1_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_L1_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_L2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L2_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_L2_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_L2_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_L2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L2_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_L2_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_L2_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_L3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L3_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_L3_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_L3_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string AmtBF_L3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L3_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).AmtBF_L3_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).AmtBF_L3_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsCB_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsCB_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsCB_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsCB_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string MatchBF
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.MatchBF.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).MatchBF = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).MatchBF = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string MatchBF_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.MatchBF_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).MatchBF_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).MatchBF_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string LastBF
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.LastBF.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).LastBF = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).LastBF = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string LastBF_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.LastBF_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).LastBF_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).LastBF_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsV_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsV_FX.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsV_FX = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsV_FX = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsN_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsN_FX.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsN_FX = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsN_FX = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsQ_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ_FX.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsQ_FX = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsQ_FX = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsLB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsLB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsLB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsLB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBF_WAP
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_WAP.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBF_WAP = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBF_WAP = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBE_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBE_FX.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBE_FX = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBE_FX = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsTS2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsTS2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsTS2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsTS2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSB_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSB_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSB_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsLB_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsLB_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsLB_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsLB_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsQ_FX_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ_FX_p.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsQ_FX_p = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsQ_FX_p = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBM
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBM.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBM = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBM = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBT
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBT.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBT = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBT = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsBB2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBB2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsBB2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsBB2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsPB2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsPB2.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsPB2 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsPB2 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsSB5
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB5.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsSB5 = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsSB5 = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsUB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsUB.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsUB = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsUB = -1;
                    }
                }
            }
        }

        /// <remarks/>
        public string OddsNZ_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsNZ_FX.ToString()));
            }
            set
            {
                string[] tmp = value.Split(',');
                AddRunners(tmp.Length);
                for (int i = 0; i < tmp.Length; i++)
                {
                    double d;
                    if (double.TryParse(tmp[i], out d))
                    {
                        Odds.ElementAt(i).OddsNZ_FX = d;
                    }
                    else
                    {
                        Odds.ElementAt(i).OddsNZ_FX = -1;
                    }
                }
            }
        }

        public int GetRunnerCount()
        {
            return Odds.Count;
        }

        public RunnerOdd GetRunner(byte no)
        {
            return Odds.SingleOrDefault(o => o.RNo == no);
        }

        public RunnerOdd GetRunner(string name)
        {
            return Odds.SingleOrDefault(o => o.RName == name);
        }
    }
}
