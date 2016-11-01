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
                lays.Add("BetFair Lay 1", d);
            }
            else
            {
                logger.Debug("Betfair Lay Odds not parsed: " + No + " - " + Name + " : " + odds.OddsBF_L1);
            }
            
            if (double.TryParse(odds.OddsWB, out d)) // might be oddsSB
            {
                backs.Add("William Hill", d);
            }
            else
            {
                logger.Debug("William Hill Odds not parsed: " + No + " - " + Name + " : " + odds.OddsWB);
            }

            if (double.TryParse(odds.OddsCR, out d)) // no data here???
            {
                backs.Add("Crown Bet", d);
            }
            else
            {
                logger.Debug("Crown Bet Odds not parsed: " + No + " - " + Name + " : " + odds.OddsCR);
            }

            if (double.TryParse(odds.OddsSB, out d)) // seems to be WH?????
            {
                backs.Add("SportsBet", d);
            }
            else
            {
                logger.Debug("SportsBet Odds not parsed: " + No + " - " + Name + " : " + odds.OddsSB);
            }
        }

        public bool MatchesOdds(RunnerOdd odds)
        {
            return (Name == odds.RName
                && No == odds.RNo);
        }
    }
}
