using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    public class Runner : RunnerOdd
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Runner));

        // Maps Properties for Back Bet prices to recognisable strings
        public static readonly Dictionary<string, string> Backs = new Dictionary<string, string>
        {
            { "OddsSB", "William Hill" },
            { "OddsIAS", "Sports Bet" }
        };
        // Maps Properties for Lay Bet prices to recognisable strings
        public static Dictionary<string, string> Lays = new Dictionary<string, string>
        {
            { "OddsBF_L1", "BetFair Lay" }
        };
    

        private string nameField;
        private string jockeyField;
        private string trainerField;
        private byte barField;
        private string hcpField;
        private byte noField;
        private double percentField = 0;
        [XmlIgnore]
        private Event parent;

        public override string ToString()
        {
            return "Runner: " + No + " - " + RName;
        }

        [XmlIgnore]
        public Event Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Jockey
        {
            get
            {
                return this.jockeyField;
            }
            set
            {
                this.jockeyField = value;
            }
        }

        /// <remarks/>
        public string Trainer
        {
            get
            {
                return this.trainerField;
            }
            set
            {
                this.trainerField = value;
            }
        }

        public byte Bar
        {
            get
            {
                return this.barField;
            }
            set
            {
                this.barField = value;
            }
        }

        public string Hcp
        {
            get
            {
                return this.hcpField;
            }
            set
            {
                this.hcpField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte No
        {
            get
            {
                return this.noField;
            }
            set
            {
                this.noField = value;
            }
        }

        [XmlIgnore]
        public double Percent
        {
            get
            {
                return this.percentField;
            }
            set
            {
                this.percentField = value;
                NotifyPropertyChanged();
            }
        }

        public string GetPercent()
        {
            return Percent.ToString() + "%";
        }

        public void UpdateOdds(RunnerOdd odds)
        {
            PropertyInfo[] ps = odds.GetType().GetProperties().Where(p => p.Name.ToLower() != "no" && p.Name.ToLower() != "name" && p.Name.ToLower() != "scr").ToArray();

            foreach (PropertyInfo p in ps)
            {
                if (p.GetValue(this, null) != p.GetValue(odds, null))
                {
                    p.SetValue(this, p.GetValue(odds, null));
                }
            }
        }

        public bool MatchesOdds(RunnerOdd odds)
        {
            return (Name == odds.RName
                && No == odds.RNo);
        }

        /// <remarks/>
        public new double OddsV
        {
            get
            {
                return this.oddsVField;
            }
            set
            {
                if (this.oddsVField != value)
                {
                    this.oddsVField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsV_P
        {
            get
            {
                return this.oddsV_PField;
            }
            set
            {
                if (this.oddsV_PField != value)
                {
                    this.oddsV_PField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsN
        {
            get
            {
                return this.oddsNField;
            }
            set
            {
                if (this.oddsNField != value)
                {
                    this.oddsNField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsN_P
        {
            get
            {
                return this.oddsN_PField;
            }
            set
            {
                if (this.oddsN_PField != value)
                {
                    this.oddsN_PField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ
        {
            get
            {
                return this.oddsQField;
            }
            set
            {
                if (this.oddsQField != value)
                {
                    this.oddsQField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ_P
        {
            get
            {
                return this.oddsQ_PField;
            }
            set
            {
                if (this.oddsQ_PField != value)
                {
                    this.oddsQ_PField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsAT
        {
            get
            {
                return this.oddsATField;
            }
            set
            {
                if (this.oddsATField != value)
                {
                    this.oddsATField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsNZ
        {
            get
            {
                return this.oddsNZField;
            }
            set
            {
                if (this.oddsNZField != value)
                {
                    this.oddsNZField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBE
        {
            get
            {
                return this.oddsBEField;
            }
            set
            {
                if (this.oddsBEField != value)
                {
                    this.oddsBEField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsIAS
        {
            get
            {
                return this.oddsIASField;
            }
            set
            {
                if (this.oddsIASField != value)
                {
                    this.oddsIASField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsIAS_2
        {
            get
            {
                return this.oddsIAS_2Field;
            }
            set
            {
                if (this.oddsIAS_2Field != value)
                {
                    this.oddsIAS_2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB
        {
            get
            {
                return this.oddsSBField;
            }
            set
            {
                if (this.oddsSBField != value)
                {
                    this.oddsSBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB_2
        {
            get
            {
                return this.oddsSB_2Field;
            }
            set
            {
                if (this.oddsSB_2Field != value)
                {
                    this.oddsSB_2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB_3
        {
            get
            {
                return this.oddsSB_3Field;
            }
            set
            {
                if (this.oddsSB_3Field != value)
                {
                    this.oddsSB_3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsCB
        {
            get
            {
                return this.oddsCBField;
            }
            set
            {
                if (this.oddsCBField != value)
                {
                    this.oddsCBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB2
        {
            get
            {
                return this.oddsSB2Field;
            }
            set
            {
                if (this.oddsSB2Field != value)
                {
                    this.oddsSB2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSA
        {
            get
            {
                return this.oddsSAField;
            }
            set
            {
                if (this.oddsSAField != value)
                {
                    this.oddsSAField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsCR
        {
            get
            {
                return this.oddsCRField;
            }
            set
            {
                if (this.oddsCRField != value)
                {
                    this.oddsCRField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBC
        {
            get
            {
                return this.oddsBCField;
            }
            set
            {
                if (this.oddsBCField != value)
                {
                    this.oddsBCField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsWB
        {
            get
            {
                return this.oddsWBField;
            }
            set
            {
                if (this.oddsWBField != value)
                {
                    this.oddsWBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsYBB
        {
            get
            {
                return this.oddsYBBField;
            }
            set
            {
                if (this.oddsYBBField != value)
                {
                    this.oddsYBBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsPB
        {
            get
            {
                return this.oddsPBField;
            }
            set
            {
                if (this.oddsPBField != value)
                {
                    this.oddsPBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBS
        {
            get
            {
                return this.oddsBSField;
            }
            set
            {
                if (this.oddsBSField != value)
                {
                    this.oddsBSField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B3
        {
            get
            {
                return this.oddsBF_B3Field;
            }
            set
            {
                if (this.oddsBF_B3Field != value)
                {
                    this.oddsBF_B3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B3
        {
            get
            {
                return this.amtBF_B3Field;
            }
            set
            {
                if (this.amtBF_B3Field != value)
                {
                    this.amtBF_B3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B2
        {
            get
            {
                return this.oddsBF_B2Field;
            }
            set
            {
                if (this.oddsBF_B2Field != value)
                {
                    this.oddsBF_B2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B2
        {
            get
            {
                return this.amtBF_B2Field;
            }
            set
            {
                if (this.amtBF_B2Field != value)
                {
                    this.amtBF_B2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B1
        {
            get
            {
                return this.oddsBF_B1Field;
            }
            set
            {
                if (this.oddsBF_B1Field != value)
                {
                    this.oddsBF_B1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B1
        {
            get
            {
                return this.amtBF_B1Field;
            }
            set
            {
                if (this.amtBF_B1Field != value)
                {
                    this.amtBF_B1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L1
        {
            get
            {
                return this.oddsBF_L1Field;
            }
            set
            {
                if (this.oddsBF_L1Field != value)
                {
                    this.oddsBF_L1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L1
        {
            get
            {
                return this.amtBF_L1Field;
            }
            set
            {
                if (this.amtBF_L1Field != value)
                {
                    this.amtBF_L1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L2
        {
            get
            {
                return this.oddsBF_L2Field;
            }
            set
            {
                if (this.oddsBF_L2Field != value)
                {
                    this.oddsBF_L2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L2
        {
            get
            {
                return this.amtBF_L2Field;
            }
            set
            {
                if (this.amtBF_L2Field != value)
                {
                    this.amtBF_L2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L3
        {
            get
            {
                return this.oddsBF_L3Field;
            }
            set
            {
                if (this.oddsBF_L3Field != value)
                {
                    this.oddsBF_L3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L3
        {
            get
            {
                return this.amtBF_L3Field;
            }
            set
            {
                if (this.amtBF_L3Field != value)
                {
                    this.amtBF_L3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B3_p
        {
            get
            {
                return this.oddsBF_B3_pField;
            }
            set
            {
                if (this.oddsBF_B3_pField != value)
                {
                    this.oddsBF_B3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B3_p
        {
            get
            {
                return this.amtBF_B3_pField;
            }
            set
            {
                if (this.amtBF_B3_pField != value)
                {
                    this.amtBF_B3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B2_p
        {
            get
            {
                return this.oddsBF_B2_pField;
            }
            set
            {
                if (this.oddsBF_B2_pField != value)
                {
                    this.oddsBF_B2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B2_p
        {
            get
            {
                return this.amtBF_B2_pField;
            }
            set
            {
                if (this.amtBF_B2_pField != value)
                {
                    this.amtBF_B2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B1_p
        {
            get
            {
                return this.oddsBF_B1_pField;
            }
            set
            {
                if (this.oddsBF_B1_pField != value)
                {
                    this.oddsBF_B1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B1_p
        {
            get
            {
                return this.amtBF_B1_pField;
            }
            set
            {
                if (this.amtBF_B1_pField != value)
                {
                    this.amtBF_B1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L1_p
        {
            get
            {
                return this.oddsBF_L1_pField;
            }
            set
            {
                if (this.oddsBF_L1_pField != value)
                {
                    this.oddsBF_L1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L1_p
        {
            get
            {
                return this.amtBF_L1_pField;
            }
            set
            {
                if (this.amtBF_L1_pField != value)
                {
                    this.amtBF_L1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L2_p
        {
            get
            {
                return this.oddsBF_L2_pField;
            }
            set
            {
                if (this.oddsBF_L2_pField != value)
                {
                    this.oddsBF_L2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L2_p
        {
            get
            {
                return this.amtBF_L2_pField;
            }
            set
            {
                if (this.amtBF_L2_pField != value)
                {
                    this.amtBF_L2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L3_p
        {
            get
            {
                return this.oddsBF_L3_pField;
            }
            set
            {
                if (this.oddsBF_L3_pField != value)
                {
                    this.oddsBF_L3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L3_p
        {
            get
            {
                return this.amtBF_L3_pField;
            }
            set
            {
                if (this.amtBF_L3_pField != value)
                {
                    this.amtBF_L3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsCB_p
        {
            get
            {
                return this.oddsCB_pField;
            }
            set
            {
                if (this.oddsCB_pField != value)
                {
                    this.oddsCB_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double MatchBF
        {
            get
            {
                return this.matchBFField;
            }
            set
            {
                if (this.matchBFField != value)
                {
                    this.matchBFField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double MatchBF_p
        {
            get
            {
                return this.matchBF_pField;
            }
            set
            {
                if (this.matchBF_pField != value)
                {
                    this.matchBF_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double LastBF
        {
            get
            {
                return this.lastBFField;
            }
            set
            {
                if (this.lastBFField != value)
                {
                    this.lastBFField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double LastBF_p
        {
            get
            {
                return this.lastBF_pField;
            }
            set
            {
                if (this.lastBF_pField != value)
                {
                    this.lastBF_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsV_FX
        {
            get
            {
                return this.oddsV_FXField;
            }
            set
            {
                if (this.oddsV_FXField != value)
                {
                    this.oddsV_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsN_FX
        {
            get
            {
                return this.oddsN_FXField;
            }
            set
            {
                if (this.oddsN_FXField != value)
                {
                    this.oddsN_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ_FX
        {
            get
            {
                return this.oddsQ_FXField;
            }
            set
            {
                if (this.oddsQ_FXField != value)
                {
                    this.oddsQ_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsLB
        {
            get
            {
                return this.oddsLBField;
            }
            set
            {
                if (this.oddsLBField != value)
                {
                    this.oddsLBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_WAP
        {
            get
            {
                return this.oddsBF_WAPField;
            }
            set
            {
                if (this.oddsBF_WAPField != value)
                {
                    this.oddsBF_WAPField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBE_FX
        {
            get
            {
                return this.oddsBE_FXField;
            }
            set
            {
                if (this.oddsBE_FXField != value)
                {
                    this.oddsBE_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsTS2
        {
            get
            {
                return this.oddsTS2Field;
            }
            set
            {
                if (this.oddsTS2Field != value)
                {
                    this.oddsTS2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB_p
        {
            get
            {
                return this.oddsSB_pField;
            }
            set
            {
                if (this.oddsSB_pField != value)
                {
                    this.oddsSB_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsLB_p
        {
            get
            {
                return this.oddsLB_pField;
            }
            set
            {
                if (this.oddsLB_pField != value)
                {
                    this.oddsLB_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ_FX_p
        {
            get
            {
                return this.oddsQ_FX_pField;
            }
            set
            {
                if (this.oddsQ_FX_pField != value)
                {
                    this.oddsQ_FX_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBM
        {
            get
            {
                return this.oddsBMField;
            }
            set
            {
                if (this.oddsBMField != value)
                {
                    this.oddsBMField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBT
        {
            get
            {
                return this.oddsBTField;
            }
            set
            {
                if (this.oddsBTField != value)
                {
                    this.oddsBTField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBB2
        {
            get
            {
                return this.oddsBB2Field;
            }
            set
            {
                if (this.oddsBB2Field != value)
                {
                    this.oddsBB2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsPB2
        {
            get
            {
                return this.oddsPB2Field;
            }
            set
            {
                if (this.oddsPB2Field != value)
                {
                    this.oddsPB2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB5
        {
            get
            {
                return this.oddsSB5Field;
            }
            set
            {
                if (this.oddsSB5Field != value)
                {
                    this.oddsSB5Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsUB
        {
            get
            {
                return this.oddsUBField;
            }
            set
            {
                if (this.oddsUBField != value)
                {
                    this.oddsUBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsNZ_FX
        {
            get
            {
                return this.oddsNZ_FXField;
            }
            set
            {
                if (this.oddsNZ_FXField != value)
                {
                    this.oddsNZ_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
