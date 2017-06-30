using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ArbBetSystem {
    public class Runner : RunnerOdd {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Runner));

        // For BetFair mapping
        private static readonly string pattern = "[\\']";
        private long selectionIdField;

        private string nameField;
        private string jockeyField;
        private string trainerField;
        private uint? barField;
        private string hcpField;
        private uint noField;
        private double winPercentField = 0;
        private double placePercentField = 0;
        private List<string> winMatchField = new List<string>();
        private List<string> placeMatchField = new List<string>();
        [XmlIgnore]
        private Event parent;

        public override string ToString() {
            return "Runner: " + No + " - " + Name??RName;
        }

        public string BFNumName {
            get {
                return No + ". " + BFName.ToLower();
            }
        }

        public string BFName {
            get {
                return Regex.Replace(Name, pattern, "").ToLower();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CheckMatch(Dictionary<string, string> WinBackNames, Dictionary<string, string> WinLayNames, Dictionary<string, string> PlaceBackNames, Dictionary<string, string> PlaceLayNames, bool CapOdds) {
            foreach (string layField in WinLayNames.Keys) {
                double lay = (double)typeof(RunnerOdd).GetProperty(layField).GetValue(this);
                if (this.WinPercent != 0 && lay > 0) {
                    foreach (KeyValuePair<string, string> back in WinBackNames) {
                        double val = (double)typeof(RunnerOdd).GetProperty(back.Key).GetValue(this);
                        if (val > 0 && lay * (1 + this.WinPercent / 100.0) < val && !winMatchField.Contains(back.Key) && (val < 10 && lay < 10 || !CapOdds)) {
                            winMatchField.Add(back.Key);
                            logger.Info("Match found: " + parent + ", " + this + ", Lay: " + lay + ", Back: " + val + ", Book: " + back.Value);
                            SystemSounds.Exclamation.Play();
                            new Thread(() => {
                                MessageBox.Show(parent + Environment.NewLine + this + Environment.NewLine + "Lay: " + lay + Environment.NewLine + "Back: " + val + Environment.NewLine + "Book: " + back.Value, "Match found:", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            }).Start();
                        } else if (lay * (1 + this.WinPercent / 100.0) >= val) {
                            winMatchField.Remove(back.Key);
                        }
                    }
                }
            }

            foreach (string layField in PlaceLayNames.Keys) {
                double lay = (double)typeof(RunnerOdd).GetProperty(layField).GetValue(this);
                if (this.PlacePercent != 0 && lay > 0) {
                    foreach (KeyValuePair<string, string> back in PlaceBackNames) {
                        double val = (double)typeof(RunnerOdd).GetProperty(back.Key).GetValue(this);
                        if (val > 0 && lay * (1 + this.PlacePercent / 100.0) < val && !placeMatchField.Contains(back.Key) && (val < 10 && lay < 10 || !CapOdds)) {
                            placeMatchField.Add(back.Key);
                            logger.Info("Match found: " + parent + ", " + this + ", Lay: " + lay + ", Back: " + val + ", Book: " + back.Value);
                            SystemSounds.Exclamation.Play();
                            new Thread(() => {
                                MessageBox.Show(parent + Environment.NewLine + this + Environment.NewLine + "Lay: " + lay + Environment.NewLine + "Back: " + val + Environment.NewLine + "Book: " + back.Value, "Match found:", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            }).Start();
                        } else if (lay * (1 + this.PlacePercent / 100.0) >= val) {
                            placeMatchField.Remove(back.Key);
                        }
                    }
                }
            }
        }                      
                    
        [XmlIgnore]
        public Event Parent {
            get {
                return this.parent;
            }
            set {
                this.parent = value;
            }
        }

        public long SelectionId {
            get {
                return this.selectionIdField;
            }
            set {
                this.selectionIdField = value;
            }
        }

        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Jockey {
            get {
                return this.jockeyField;
            }
            set {
                this.jockeyField = value;
            }
        }

        /// <remarks/>
        public string Trainer {
            get {
                return this.trainerField;
            }
            set {
                this.trainerField = value;
            }
        }

        [XmlElement("Bar")]
        public string BarAsText {
            set {
                this.barField = !string.IsNullOrEmpty(value) ? uint.Parse(value) : default(uint?);
            }
        }

        [XmlIgnore]
        public uint? Bar {
            get {
                return this.barField;
            }
            set {
                this.barField = value;
            }
        }

        public string Hcp {
            get {
                return this.hcpField;
            }
            set {
                this.hcpField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint No {
            get {
                return this.noField;
            }
            set {
                this.noField = value;
            }
        }

        [XmlIgnore]
        public double WinPercent {
            get {
                return this.winPercentField;
            }
            set {
                this.winPercentField = value;
                NotifyPropertyChanged();
            }
        }

        [XmlIgnore]
        public double PlacePercent {
            get {
                return this.placePercentField;
            }
            set {
                this.placePercentField = value;
                NotifyPropertyChanged();
            }
        }

        [XmlIgnore]
        public List<string> WinMatched {
            get {
                return winMatchField;
            }
        }

        [XmlIgnore]
        public List<string> PlaceMatched {
            get {
                return placeMatchField;
            }
        }

        public string GetPercent() {
            return WinPercent.ToString() + "%";
        }

        public void UpdateDOOdds(RunnerOdd odds, bool updateBF = false) {
            PropertyInfo[] ps = odds.GetType().GetProperties().Where(p => p.Name.StartsWith("Odds") && (!p.Name.Contains("BF") || updateBF)).ToArray();

            foreach (PropertyInfo p in ps) {
                if (p.GetValue(this, null) != p.GetValue(odds, null)) {
                    p.SetValue(this, p.GetValue(odds, null));
                }
            }
        }

        public void UpdateBFOdds(Models.BetFair.Runner runner, bool place) {
            if (place) {
                switch (runner.ExchangePrices.AvailableToBack.Count) {
                    case 3:
                        this.OddsBF_B3_p = runner.ExchangePrices.AvailableToBack[2].Price;
                        goto case 2;
                    case 2:
                        this.OddsBF_B2_p = runner.ExchangePrices.AvailableToBack[1].Price;
                        goto case 1;
                    case 1:
                        this.OddsBF_B1_p = runner.ExchangePrices.AvailableToBack[0].Price;
                        break;
                }

                switch (runner.ExchangePrices.AvailableToLay.Count) {
                    case 3:
                        this.OddsBF_L3_p = runner.ExchangePrices.AvailableToLay[2].Price;
                        goto case 2;
                    case 2:
                        this.OddsBF_L2_p = runner.ExchangePrices.AvailableToLay[1].Price;
                        goto case 1;
                    case 1:
                        this.OddsBF_L1_p = runner.ExchangePrices.AvailableToLay[0].Price;
                        break;
                }
            } else {
                switch (runner.ExchangePrices.AvailableToBack.Count) {
                    case 3:
                        this.OddsBF_B3 = runner.ExchangePrices.AvailableToBack[2].Price;
                        goto case 2;
                    case 2:
                        this.OddsBF_B2 = runner.ExchangePrices.AvailableToBack[1].Price;
                        goto case 1;
                    case 1:
                        this.OddsBF_B1 = runner.ExchangePrices.AvailableToBack[0].Price;
                        break;
                }

                switch (runner.ExchangePrices.AvailableToLay.Count) {
                    case 3:
                        this.OddsBF_L3 = runner.ExchangePrices.AvailableToLay[2].Price;
                        goto case 2;
                    case 2:
                        this.OddsBF_L2 = runner.ExchangePrices.AvailableToLay[1].Price;
                        goto case 1;
                    case 1:
                        this.OddsBF_L1 = runner.ExchangePrices.AvailableToLay[0].Price;
                        break;
                }
            }
        }

        public bool MatchesOdds(RunnerOdd odds) {
            return (Name == odds.RName
                && No == odds.RNo);
        }

        /// <remarks/>
        public new double OddsV {
            get {
                return this.oddsVField;
            }
            set {
                if (this.oddsVField != value) {
                    this.oddsVField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsV_P {
            get {
                return this.oddsV_PField;
            }
            set {
                if (this.oddsV_PField != value) {
                    this.oddsV_PField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsN {
            get {
                return this.oddsNField;
            }
            set {
                if (this.oddsNField != value) {
                    this.oddsNField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsN_P {
            get {
                return this.oddsN_PField;
            }
            set {
                if (this.oddsN_PField != value) {
                    this.oddsN_PField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ {
            get {
                return this.oddsQField;
            }
            set {
                if (this.oddsQField != value) {
                    this.oddsQField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ_P {
            get {
                return this.oddsQ_PField;
            }
            set {
                if (this.oddsQ_PField != value) {
                    this.oddsQ_PField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsAT {
            get {
                return this.oddsATField;
            }
            set {
                if (this.oddsATField != value) {
                    this.oddsATField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsNZ {
            get {
                return this.oddsNZField;
            }
            set {
                if (this.oddsNZField != value) {
                    this.oddsNZField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBE {
            get {
                return this.oddsBEField;
            }
            set {
                if (this.oddsBEField != value) {
                    this.oddsBEField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsIAS {
            get {
                return this.oddsIASField;
            }
            set {
                if (this.oddsIASField != value) {
                    this.oddsIASField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsIAS_2 {
            get {
                return this.oddsIAS_2Field;
            }
            set {
                if (this.oddsIAS_2Field != value) {
                    this.oddsIAS_2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB {
            get {
                return this.oddsSBField;
            }
            set {
                if (this.oddsSBField != value) {
                    this.oddsSBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB_2 {
            get {
                return this.oddsSB_2Field;
            }
            set {
                if (this.oddsSB_2Field != value) {
                    this.oddsSB_2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB_3 {
            get {
                return this.oddsSB_3Field;
            }
            set {
                if (this.oddsSB_3Field != value) {
                    this.oddsSB_3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsCB {
            get {
                return this.oddsCBField;
            }
            set {
                if (this.oddsCBField != value) {
                    this.oddsCBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB2 {
            get {
                return this.oddsSB2Field;
            }
            set {
                if (this.oddsSB2Field != value) {
                    this.oddsSB2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSA {
            get {
                return this.oddsSAField;
            }
            set {
                if (this.oddsSAField != value) {
                    this.oddsSAField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsCR {
            get {
                return this.oddsCRField;
            }
            set {
                if (this.oddsCRField != value) {
                    this.oddsCRField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBC {
            get {
                return this.oddsBCField;
            }
            set {
                if (this.oddsBCField != value) {
                    this.oddsBCField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsWB {
            get {
                return this.oddsWBField;
            }
            set {
                if (this.oddsWBField != value) {
                    this.oddsWBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsYBB {
            get {
                return this.oddsYBBField;
            }
            set {
                if (this.oddsYBBField != value) {
                    this.oddsYBBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsPB {
            get {
                return this.oddsPBField;
            }
            set {
                if (this.oddsPBField != value) {
                    this.oddsPBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBS {
            get {
                return this.oddsBSField;
            }
            set {
                if (this.oddsBSField != value) {
                    this.oddsBSField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B3 {
            get {
                return this.oddsBF_B3Field;
            }
            set {
                if (this.oddsBF_B3Field != value) {
                    this.oddsBF_B3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B3 {
            get {
                return this.amtBF_B3Field;
            }
            set {
                if (this.amtBF_B3Field != value) {
                    this.amtBF_B3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B2 {
            get {
                return this.oddsBF_B2Field;
            }
            set {
                if (this.oddsBF_B2Field != value) {
                    this.oddsBF_B2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B2 {
            get {
                return this.amtBF_B2Field;
            }
            set {
                if (this.amtBF_B2Field != value) {
                    this.amtBF_B2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B1 {
            get {
                return this.oddsBF_B1Field;
            }
            set {
                if (this.oddsBF_B1Field != value) {
                    this.oddsBF_B1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B1 {
            get {
                return this.amtBF_B1Field;
            }
            set {
                if (this.amtBF_B1Field != value) {
                    this.amtBF_B1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L1 {
            get {
                return this.oddsBF_L1Field;
            }
            set {
                if (this.oddsBF_L1Field != value) {
                    this.oddsBF_L1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L1 {
            get {
                return this.amtBF_L1Field;
            }
            set {
                if (this.amtBF_L1Field != value) {
                    this.amtBF_L1Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L2 {
            get {
                return this.oddsBF_L2Field;
            }
            set {
                if (this.oddsBF_L2Field != value) {
                    this.oddsBF_L2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L2 {
            get {
                return this.amtBF_L2Field;
            }
            set {
                if (this.amtBF_L2Field != value) {
                    this.amtBF_L2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L3 {
            get {
                return this.oddsBF_L3Field;
            }
            set {
                if (this.oddsBF_L3Field != value) {
                    this.oddsBF_L3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L3 {
            get {
                return this.amtBF_L3Field;
            }
            set {
                if (this.amtBF_L3Field != value) {
                    this.amtBF_L3Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B3_p {
            get {
                return this.oddsBF_B3_pField;
            }
            set {
                if (this.oddsBF_B3_pField != value) {
                    this.oddsBF_B3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B3_p {
            get {
                return this.amtBF_B3_pField;
            }
            set {
                if (this.amtBF_B3_pField != value) {
                    this.amtBF_B3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B2_p {
            get {
                return this.oddsBF_B2_pField;
            }
            set {
                if (this.oddsBF_B2_pField != value) {
                    this.oddsBF_B2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B2_p {
            get {
                return this.amtBF_B2_pField;
            }
            set {
                if (this.amtBF_B2_pField != value) {
                    this.amtBF_B2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_B1_p {
            get {
                return this.oddsBF_B1_pField;
            }
            set {
                if (this.oddsBF_B1_pField != value) {
                    this.oddsBF_B1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_B1_p {
            get {
                return this.amtBF_B1_pField;
            }
            set {
                if (this.amtBF_B1_pField != value) {
                    this.amtBF_B1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L1_p {
            get {
                return this.oddsBF_L1_pField;
            }
            set {
                if (this.oddsBF_L1_pField != value) {
                    this.oddsBF_L1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L1_p {
            get {
                return this.amtBF_L1_pField;
            }
            set {
                if (this.amtBF_L1_pField != value) {
                    this.amtBF_L1_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L2_p {
            get {
                return this.oddsBF_L2_pField;
            }
            set {
                if (this.oddsBF_L2_pField != value) {
                    this.oddsBF_L2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L2_p {
            get {
                return this.amtBF_L2_pField;
            }
            set {
                if (this.amtBF_L2_pField != value) {
                    this.amtBF_L2_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_L3_p {
            get {
                return this.oddsBF_L3_pField;
            }
            set {
                if (this.oddsBF_L3_pField != value) {
                    this.oddsBF_L3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double AmtBF_L3_p {
            get {
                return this.amtBF_L3_pField;
            }
            set {
                if (this.amtBF_L3_pField != value) {
                    this.amtBF_L3_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsCB_p {
            get {
                return this.oddsCB_pField;
            }
            set {
                if (this.oddsCB_pField != value) {
                    this.oddsCB_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double MatchBF {
            get {
                return this.matchBFField;
            }
            set {
                if (this.matchBFField != value) {
                    this.matchBFField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double MatchBF_p {
            get {
                return this.matchBF_pField;
            }
            set {
                if (this.matchBF_pField != value) {
                    this.matchBF_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double LastBF {
            get {
                return this.lastBFField;
            }
            set {
                if (this.lastBFField != value) {
                    this.lastBFField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double LastBF_p {
            get {
                return this.lastBF_pField;
            }
            set {
                if (this.lastBF_pField != value) {
                    this.lastBF_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsV_FX {
            get {
                return this.oddsV_FXField;
            }
            set {
                if (this.oddsV_FXField != value) {
                    this.oddsV_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsN_FX {
            get {
                return this.oddsN_FXField;
            }
            set {
                if (this.oddsN_FXField != value) {
                    this.oddsN_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ_FX {
            get {
                return this.oddsQ_FXField;
            }
            set {
                if (this.oddsQ_FXField != value) {
                    this.oddsQ_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsLB {
            get {
                return this.oddsLBField;
            }
            set {
                if (this.oddsLBField != value) {
                    this.oddsLBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBF_WAP {
            get {
                return this.oddsBF_WAPField;
            }
            set {
                if (this.oddsBF_WAPField != value) {
                    this.oddsBF_WAPField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBE_FX {
            get {
                return this.oddsBE_FXField;
            }
            set {
                if (this.oddsBE_FXField != value) {
                    this.oddsBE_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsTS2 {
            get {
                return this.oddsTS2Field;
            }
            set {
                if (this.oddsTS2Field != value) {
                    this.oddsTS2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB_p {
            get {
                return this.oddsSB_pField;
            }
            set {
                if (this.oddsSB_pField != value) {
                    this.oddsSB_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsLB_p {
            get {
                return this.oddsLB_pField;
            }
            set {
                if (this.oddsLB_pField != value) {
                    this.oddsLB_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsQ_FX_p {
            get {
                return this.oddsQ_FX_pField;
            }
            set {
                if (this.oddsQ_FX_pField != value) {
                    this.oddsQ_FX_pField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBM {
            get {
                return this.oddsBMField;
            }
            set {
                if (this.oddsBMField != value) {
                    this.oddsBMField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBT {
            get {
                return this.oddsBTField;
            }
            set {
                if (this.oddsBTField != value) {
                    this.oddsBTField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsBB2 {
            get {
                return this.oddsBB2Field;
            }
            set {
                if (this.oddsBB2Field != value) {
                    this.oddsBB2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsPB2 {
            get {
                return this.oddsPB2Field;
            }
            set {
                if (this.oddsPB2Field != value) {
                    this.oddsPB2Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsSB5 {
            get {
                return this.oddsSB5Field;
            }
            set {
                if (this.oddsSB5Field != value) {
                    this.oddsSB5Field = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsUB {
            get {
                return this.oddsUBField;
            }
            set {
                if (this.oddsUBField != value) {
                    this.oddsUBField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <remarks/>
        public new double OddsNZ_FX {
            get {
                return this.oddsNZ_FXField;
            }
            set {
                if (this.oddsNZ_FXField != value) {
                    this.oddsNZ_FXField = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
