using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace ArbBetSystem {
    public class Event : INotifyPropertyChanged {
        private string winMarketIdField;
        private string placeMarketIdField;
        private bool bfMatched = false;

        private uint eventNoField;
        private string nameField;
        private uint distanceField;
        private string trackField;
        private string trackRtgField;
        private uint startersField;
        private DateTime startTimeField;
        private string classField;
        private uint prizemoneyField;
        private string statusField;
        private string weatherTCField;
        private Weather weatherCondtionsField;
        private string railField;
        private string idField;
        private Runner[] runnerField;
        private Meeting parent;
        private bool hasOdds = false;

        private bool checkField;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Meeting Parent {
            get {
                return this.parent;
            }
            set {
                this.parent = value;
            }
        }

        public bool Check {
            get {
                return this.checkField;
            }
            set {
                if (value != this.checkField) {
                    this.checkField = value;
                    NotifyPropertyChanged();
                    parent.NotifyPropertyChanged("IsChecked");
                }

            }
        }

        public bool BFMatched {
            get {
                return this.bfMatched;
            }
        }

        public string PlaceMarketId {
            get {
                return this.placeMarketIdField;
            }
            set {
                this.placeMarketIdField = value;
                bfMatched = this.placeMarketIdField != null && this.winMarketIdField != null;
            }
        }

        public string WinMarketId {
            get {
                return this.winMarketIdField;
            }
            set {
                this.winMarketIdField = value;
                bfMatched = this.placeMarketIdField != null && this.winMarketIdField != null;
            }
        }

        /// <remarks/>
        public uint EventNo {
            get {
                return this.eventNoField;
            }
            set {
                this.eventNoField = value;
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
        public uint Distance {
            get {
                return this.distanceField;
            }
            set {
                this.distanceField = value;
            }
        }

        /// <remarks/>
        public string Track {
            get {
                return this.trackField;
            }
            set {
                this.trackField = value;
            }
        }

        /// <remarks/>
        public string TrackRtg {
            get {
                return this.trackRtgField;
            }
            set {
                this.trackRtgField = value;
            }
        }

        /// <remarks/>
        public uint Starters {
            get {
                return this.startersField;
            }
            set {
                this.startersField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "time")]
        public DateTime StartTime {
            get {
                return this.startTimeField;
            }
            set {
                this.startTimeField = value;
                NotifyPropertyChanged();
            }
        }

        /// <remarks/>
        public string Class {
            get {
                return this.classField;
            }
            set {
                this.classField = value;
            }
        }

        /// <remarks/>
        public uint Prizemoney {
            get {
                return this.prizemoneyField;
            }
            set {
                this.prizemoneyField = value;
            }
        }

        /// <remarks/>
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public string WeatherTC {
            get {
                return this.weatherTCField;
            }
            set {
                this.weatherTCField = value;
            }
        }

        /// <remarks/>
        public Weather WeatherCondtions {
            get {
                return this.weatherCondtionsField;
            }
            set {
                this.weatherCondtionsField = value;
            }
        }

        /// <remarks/>
        public string Rail {
            get {
                return this.railField;
            }
            set {
                this.railField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Runner")]
        public Runner[] Runners {
            get {
                return this.runnerField;
            }
            set {
                this.runnerField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }

        public void UpdateOdds(RunnerOdds odds) {
            hasOdds = true;

            foreach (Runner r in Runners) {
                if (odds != null) {
                    r.UpdateDOOdds(odds.GetRunner(r.No), this.WinMarketId == null && this.PlaceMarketId == null);
                }
            }
        }

        public void UpdateBFOdds(List<Models.BetFair.Runner> runners, bool place) {
            foreach (Runner r in this.Runners) {
                Models.BetFair.Runner ru = runners.Find(run => run.SelectionId == r.SelectionId);
                if (ru != null) {
                    r.UpdateBFOdds(ru, place);
                }
            }
        }

        public bool HasOdds() {
            return hasOdds;
        }

        public override string ToString() {
            return "Meeting: " + Parent + ", Event: " + EventNo + " - " + Name + " @ " + StartTime.ToShortTimeString();
        }

        public Event() {
            checkField = false;
        }

        public void MapChildren() {
            if (Runners != null) {
                foreach (Runner r in Runners) {
                    r.Parent = this;
                }
            }
        }
    }
}
