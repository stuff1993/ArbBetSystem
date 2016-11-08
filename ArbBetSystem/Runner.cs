using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    public class Runner
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Runner));

        private string nameField;
        private string jockeyField;
        private string trainerField;
        private byte barField;
        private string hcpField;
        private byte scrField;
        private byte noField;
        private double percentField = 0;

        private Dictionary<string, double> lays = new Dictionary<string, double>();
        private Dictionary<string, double> backs = new Dictionary<string, double>();

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

        /// <remarks/>
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

        /// <remarks/>
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
        public byte Scr
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
            }
        }

        public string GetPercent()
        {
            return Percent.ToString() + "%";
        }

        public Dictionary<string, double> GetLays()
        {
            return new Dictionary<string, double>(lays);
        }

        public Dictionary<string, double> GetBacks()
        {
            return new Dictionary<string, double>(backs);
        }

        public bool HasOdds()
        {
            return lays.Count() > 0 || backs.Count() > 0;
        }

        public void AddOdds(RunnerOdd odds)
        {
            double d;
            if (double.TryParse(odds.OddsBF_L1, out d))
            {
                lays["BetFair Lay 1"] = d;
            }
            else
            {
                logger.Debug("Betfair Lay Odds not parsed: " + No + " - " + Name + " : " + odds.OddsBF_L1);
            }

            //AddBackOdd(odds.OddsAT, "OddsAT");
            //AddBackOdd(odds.OddsBB2, "OddsBB2");
            //AddBackOdd(odds.OddsBC, "OddsBC");
            //AddBackOdd(odds.OddsBE, "OddsBE");
            //AddBackOdd(odds.OddsBE_FX, "OddsBE_FX");
            //AddBackOdd(odds.OddsBF_B1, "OddsBF_B1");
            //AddBackOdd(odds.OddsBF_B1_p, "OddsBF_B1_p");
            //AddBackOdd(odds.OddsBF_B2, "OddsBF_B2");
            //AddBackOdd(odds.OddsBF_B2_p, "OddsBF_B2_p");
            //AddBackOdd(odds.OddsBF_B3, "OddsBF_B3");
            //AddBackOdd(odds.OddsBF_B3_p, "OddsBF_B3_p");
            //AddBackOdd(odds.OddsBM, "OddsBM");
            //AddBackOdd(odds.OddsBS, "OddsBS");
            //AddBackOdd(odds.OddsBT, "OddsBT");
            //AddBackOdd(odds.OddsCB, "OddsCB");
            //AddBackOdd(odds.OddsCB_p, "OddsCB_p");
            //AddBackOdd(odds.OddsCR, "OddsCR");
            AddBackOdd(odds.OddsIAS, "Sports Bet");
            //AddBackOdd(odds.OddsIAS_2, "OddsIAS_2");
            //AddBackOdd(odds.OddsLB, "OddsLB");
            //AddBackOdd(odds.OddsLB_p, "OddsLB_p");
            //AddBackOdd(odds.OddsN, "OddsN");
            //AddBackOdd(odds.OddsNZ, "OddsNZ");
            //AddBackOdd(odds.OddsNZ_FX, "OddsNZ_FX");
            //AddBackOdd(odds.OddsN_FX, "OddsN_FX");
            //AddBackOdd(odds.OddsN_P, "OddsN_P");
            //AddBackOdd(odds.OddsPB, "OddsPB");
            //AddBackOdd(odds.OddsPB2, "OddsPB2");
            //AddBackOdd(odds.OddsQ, "OddsQ");
            //AddBackOdd(odds.OddsQ_FX, "OddsQ_FX");
            //AddBackOdd(odds.OddsQ_FX_p, "OddsQ_FX_p");
            //AddBackOdd(odds.OddsQ_P, "OddsQ_P");
            //AddBackOdd(odds.OddsSA, "OddsSA");
            AddBackOdd(odds.OddsSB, "William Hill");
            //AddBackOdd(odds.OddsSB2, "OddsSB2");
            //AddBackOdd(odds.OddsSB5, "OddsSB5");
            //AddBackOdd(odds.OddsSB_2, "OddsSB_2");
            //AddBackOdd(odds.OddsSB_3, "OddsSB_3");
            //AddBackOdd(odds.OddsSB_p, "OddsSB_p");
            //AddBackOdd(odds.OddsTS2, "OddsTS2");
            //AddBackOdd(odds.OddsUB, "OddsUB");
            //AddBackOdd(odds.OddsV, "OddsV");
            //AddBackOdd(odds.OddsV_FX, "OddsV_FX");
            //AddBackOdd(odds.OddsV_P, "OddsV_P");
            //AddBackOdd(odds.OddsWB, "OddsWB");
            //AddBackOdd(odds.OddsYBB, "OddsYBB");
        }

        private bool AddBackOdd(string odd, string name)
        {
            double d;
            if (double.TryParse(odd, out d))
            {
                backs[name] = d;
                return true;
            }
            else
            {
                logger.Debug(name + " Odds not parsed: " + No + " - " + Name + " : " + odd);
                return false;
            }
        }

        public bool MatchesOdds(RunnerOdd odds)
        {
            return (Name == odds.RName
                && No == odds.RNo);
        }
    }
}
