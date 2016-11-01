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

        private void AddOdds(int length)
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
                byte[] tmp = value.Split(',').Select(s => Convert.ToByte(s, 10)).ToArray(); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).RNo = tmp[i]; }
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
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).RName = tmp[i]; }
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
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).Scr = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsV
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsV));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsV = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsV_P
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsV_P));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsV_P = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsN
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsN));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsN = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsN_P
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsN_P));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsN_P = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsQ
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsQ = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsQ_P
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ_P));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsQ_P = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsAT
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsAT));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsAT = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsNZ
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsNZ));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsNZ = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBE
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBE));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBE = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsIAS
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsIAS));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsIAS = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsIAS_2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsIAS_2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsIAS_2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSB_2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB_2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSB_2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSB_3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB_3));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSB_3 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsCB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsCB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsCB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSB2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSB2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSA
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSA));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSA = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsCR
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsCR));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsCR = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBC
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBC));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBC = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsWB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsWB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsWB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsYBB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsYBB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsYBB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsPB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsPB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsPB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBS
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBS));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBS = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_B3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B3));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_B3 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_B3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B3));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_B3 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_B2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_B2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_B2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_B2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_B1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B1));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_B1 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_B1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B1));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_B1 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_L1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L1));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_L1 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_L1
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L1));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_L1 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_L2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_L2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_L2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_L2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_L3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L3));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_L3 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_L3
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L3));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_L3 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_B3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B3_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_B3_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_B3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B3_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_B3_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_B2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B2_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_B2_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_B2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B2_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_B2_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_B1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_B1_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_B1_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_B1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_B1_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_B1_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_L1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L1_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_L1_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_L1_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L1_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_L1_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_L2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L2_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_L2_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_L2_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L2_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_L2_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_L3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_L3_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_L3_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string AmtBF_L3_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.AmtBF_L3_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).AmtBF_L3_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsCB_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsCB_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsCB_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string MatchBF
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.MatchBF));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).MatchBF = tmp[i]; }
            }
        }

        /// <remarks/>
        public string MatchBF_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.MatchBF_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).MatchBF_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string LastBF
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.LastBF));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).LastBF = tmp[i]; }
            }
        }

        /// <remarks/>
        public string LastBF_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.LastBF_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).LastBF_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsV_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsV_FX));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsV_FX = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsN_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsN_FX));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsN_FX = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsQ_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ_FX));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsQ_FX = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsLB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsLB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsLB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBF_WAP
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBF_WAP));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBF_WAP = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBE_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBE_FX));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBE_FX = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsTS2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsTS2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsTS2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSB_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSB_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsLB_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsLB_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsLB_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsQ_FX_p
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsQ_FX_p));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsQ_FX_p = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBM
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBM));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBM = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBT
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBT));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBT = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsBB2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsBB2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsBB2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsPB2
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsPB2));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsPB2 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsSB5
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsSB5));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsSB5 = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsUB
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsUB));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsUB = tmp[i]; }
            }
        }

        /// <remarks/>
        public string OddsNZ_FX
        {
            get
            {
                return string.Join(",", this.Odds.Select(o => o.OddsNZ_FX));
            }
            set
            {
                string[] tmp = value.Split(','); AddOdds(tmp.Length); for (int i = 0; i < tmp.Length; i++) { Odds.ElementAt(i).OddsNZ_FX = tmp[i]; }
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
