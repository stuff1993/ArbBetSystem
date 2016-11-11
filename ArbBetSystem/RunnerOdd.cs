using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class RunnerOdd : INotifyPropertyChanged
    {

        protected byte rNoField;
        protected string rNameField;
        protected string scrField;
        protected double amtBF_B1_pField;     // Betfair Back Amount 1 Place
        protected double amtBF_B1Field;       // Betfair Back Amount 1 Win
        protected double amtBF_B2_pField;     // Betfair Back Amount 2 Place
        protected double amtBF_B2Field;       // Betfair Back Amount 2 Win
        protected double amtBF_B3_pField;     // Betfair Back Amount 3 Place
        protected double amtBF_B3Field;       // Betfair Back Amount 3 Win
        protected double amtBF_L1_pField;     // Betfair Lay Amount 1 Place
        protected double amtBF_L1Field;       // Betfair Lay Amount 1 Win
        protected double amtBF_L2_pField;     // Betfair Lay Amount 2 Place
        protected double amtBF_L2Field;       // Betfair Lay Amount 2 Win
        protected double amtBF_L3_pField;     // Betfair Lay Amount 3 Place
        protected double amtBF_L3Field;       // Betfair Lay Amount 3 Win
        protected double lastBF_pField;       // Betfair Last Matched Price Place
        protected double lastBFField;         // Betfair Last Matched Price Win
        protected double matchBF_pField;      // Betfair Total Matched Amount Place
        protected double matchBFField;        // Betfair Total Matched Amount Win
        protected double oddsATField;
        protected double oddsBB2Field;
        protected double oddsBCField;
        protected double oddsBE_FXField;
        protected double oddsBEField;
        protected double oddsBF_B1_pField;    // Betfair Back Odds 1 Place
        protected double oddsBF_B1Field;      // Betfair Back Odds 1 Win
        protected double oddsBF_B2_pField;    // Betfair Back Odds 2 Place
        protected double oddsBF_B2Field;      // Betfair Back Odds 2 Win
        protected double oddsBF_B3_pField;    // Betfair Back Odds 3 Place
        protected double oddsBF_B3Field;      // Betfair Back Odds 3 Win
        protected double oddsBF_L1_pField;    // Betfair Lay Odds 1 Place
        protected double oddsBF_L1Field;      // Betfair Lay Odds 1 Win
        protected double oddsBF_L2_pField;    // Betfair Lay Odds 2 Place
        protected double oddsBF_L2Field;      // Betfair Lay Odds 2 Win
        protected double oddsBF_L3_pField;    // Betfair Lay Odds 3 Place
        protected double oddsBF_L3Field;      // Betfair Lay Odds 3 Win
        protected double oddsBF_WAPField;     // Betfair Weighted Average Price
        protected double oddsBMField;
        protected double oddsBSField;
        protected double oddsBTField;
        protected double oddsCB_pField;
        protected double oddsCBField;
        protected double oddsCRField;
        protected double oddsIAS_2Field;
        protected double oddsIASField;        // Sports Bet
        protected double oddsLB_pField;
        protected double oddsLBField;
        protected double oddsN_FXField;
        protected double oddsN_PField;
        protected double oddsNField;
        protected double oddsNZ_FXField;
        protected double oddsNZField;
        protected double oddsPB2Field;
        protected double oddsPBField;
        protected double oddsQ_FX_pField;
        protected double oddsQ_FXField;
        protected double oddsQ_PField;
        protected double oddsQField;
        protected double oddsSAField;
        protected double oddsSB_2Field;
        protected double oddsSB_3Field;
        protected double oddsSB_pField;
        protected double oddsSB2Field;
        protected double oddsSB5Field;
        protected double oddsSBField;         // William Hill
        protected double oddsTS2Field;
        protected double oddsUBField;
        protected double oddsV_FXField;
        protected double oddsV_PField;
        protected double oddsVField;
        protected double oddsWBField;
        protected double oddsYBBField;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <remarks/>
        public byte RNo
        {
            get
            {
                return this.rNoField;
            }
            set
            {
                this.rNoField = value;
            }
        }

        /// <remarks/>
        public string RName
        {
            get
            {
                return this.rNameField;
            }
            set
            {
                this.rNameField = value;
            }
        }

        /// <remarks/>
        public string Scr
        {
            get
            {
                return this.scrField;
            }
            set
            {
                this.scrField = value;
            }
        }

        /// <remarks/>
        public double OddsV
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
        public double OddsV_P
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
        public double OddsN
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
        public double OddsN_P
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
        public double OddsQ
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
        public double OddsQ_P
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
        public double OddsAT
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
        public double OddsNZ
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
        public double OddsBE
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
        public double OddsIAS
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
        public double OddsIAS_2
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
        public double OddsSB
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
        public double OddsSB_2
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
        public double OddsSB_3
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
        public double OddsCB
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
        public double OddsSB2
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
        public double OddsSA
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
        public double OddsCR
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
        public double OddsBC
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
        public double OddsWB
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
        public double OddsYBB
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
        public double OddsPB
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
        public double OddsBS
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
        public double OddsBF_B3
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
        public double AmtBF_B3
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
        public double OddsBF_B2
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
        public double AmtBF_B2
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
        public double OddsBF_B1
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
        public double AmtBF_B1
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
        public double OddsBF_L1
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
        public double AmtBF_L1
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
        public double OddsBF_L2
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
        public double AmtBF_L2
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
        public double OddsBF_L3
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
        public double AmtBF_L3
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
        public double OddsBF_B3_p
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
        public double AmtBF_B3_p
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
        public double OddsBF_B2_p
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
        public double AmtBF_B2_p
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
        public double OddsBF_B1_p
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
        public double AmtBF_B1_p
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
        public double OddsBF_L1_p
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
        public double AmtBF_L1_p
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
        public double OddsBF_L2_p
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
        public double AmtBF_L2_p
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
        public double OddsBF_L3_p
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
        public double AmtBF_L3_p
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
        public double OddsCB_p
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
        public double MatchBF
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
        public double MatchBF_p
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
        public double LastBF
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
        public double LastBF_p
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
        public double OddsV_FX
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
        public double OddsN_FX
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
        public double OddsQ_FX
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
        public double OddsLB
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
        public double OddsBF_WAP
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
        public double OddsBE_FX
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
        public double OddsTS2
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
        public double OddsSB_p
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
        public double OddsLB_p
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
        public double OddsQ_FX_p
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
        public double OddsBM
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
        public double OddsBT
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
        public double OddsBB2
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
        public double OddsPB2
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
        public double OddsSB5
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
        public double OddsUB
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
        public double OddsNZ_FX
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
