using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using ArbBetSystem.Utils;

namespace ArbBetSystem {
    public class Meeting : INotifyPropertyChanged {
        public enum MeetingTypes { Racing = 1, Harness = 2, Greyhound = 4 };

        private const string BF_HORSE_RACING = "Horse Racing";
        private const string BF_GREYHOUND_RACING = "Greyhound Racing";
        private const string BF_HARNESS_RACING = "T";

        public static string getDOMeetingTypesString(int meetingType) {
            switch (meetingType) {
                case 1:
                    return "R";
                case 2:
                    return "T";
                case 3:
                    return "R,T";
                case 4:
                    return "G";
                case 5:
                    return "R,G";
                case 6:
                    return "T,G";
                case 7:
                default:
                    return "R,T,G";
            }
        }

        public static ISet<string> getBFMeetingTypesStrings(int meetingType) {
            switch (meetingType) {
                case 1:
                    return new HashSet<string>() { BF_HORSE_RACING };
                case 2:
                    return new HashSet<string>() { BF_HARNESS_RACING };
                case 3:
                    return new HashSet<string>() { BF_HORSE_RACING, BF_HARNESS_RACING };
                case 4:
                    return new HashSet<string>() { BF_GREYHOUND_RACING };
                case 5:
                    return new HashSet<string>() { BF_HORSE_RACING, BF_GREYHOUND_RACING };
                case 6:
                    return new HashSet<string>() { BF_HARNESS_RACING, BF_GREYHOUND_RACING };
                case 7:
                default:
                    return new HashSet<string>() { BF_HORSE_RACING, BF_HARNESS_RACING, BF_GREYHOUND_RACING };
            }
        }

        private uint dOIdField;
        private string venueField;
        private MeetingTypes typeField;
        private Event[] eventsField;
        private string trackField;
        private string trackRtgField;
        private string railField;
        private string countryField;
        private string codeTCField;
        private string codeQField;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string BFVenue {
            get {
                return Mappings.getBFFormattedVenue(this.Venue);
            }
        }

        public string BFCountry {
            get {
                return Mappings.getCountry(this.Country);
            }
        }

        /// <remarks/>
        public string Venue {
            get {
                return this.venueField;
            }
            set {
                this.venueField = value;
            }
        }

        /// <remarks/>
        public string Type {
            get {
                switch (this.typeField) {
                    default:
                    case MeetingTypes.Racing:
                        return "R";
                    case MeetingTypes.Harness:
                        return "T";
                    case MeetingTypes.Greyhound:
                        return "G";
                }
            }
            set {
                switch (value) {
                    default:
                    case "R":
                        this.typeField = MeetingTypes.Racing;
                        break;
                    case "H":
                    case "T":
                        this.typeField = MeetingTypes.Harness;
                        break;
                    case "G":
                        this.typeField = MeetingTypes.Greyhound;
                        break;
                }
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
        public string Rail {
            get {
                return this.railField;
            }
            set {
                this.railField = value;
            }
        }

        /// <remarks/>
        public string Country {
            get {
                return this.countryField;
            }
            set {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string CodeTC {
            get {
                return this.codeTCField;
            }
            set {
                this.codeTCField = value;
            }
        }

        /// <remarks/>
        public string CodeQ {
            get {
                return this.codeQField;
            }
            set {
                this.codeQField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Event")]
        public Event[] Events {
            get {
                return this.eventsField;
            }
            set {
                this.eventsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint ID {
            get {
                return this.dOIdField;
            }
            set {
                this.dOIdField = value;
            }
        }

        public bool IsChecked {
            get {
                bool ret = false;
                foreach (Event evt in Events) {
                    ret = ret || evt.Check;
                }
                return ret;
            }
            set {
                foreach (Event e in Events) {
                    e.Check = value;
                }
                NotifyPropertyChanged();
            }
        }

        public override string ToString() {
            return Venue + " - " + Country;
        }

        public Meeting MergeWith(Meeting m) {
            Dictionary<string, bool> oldChecks = Events.ToDictionary(e => e.ID, e => e.Check);
            Dictionary<string, Dictionary<uint, KeyValuePair<double, double>>> oldPercent = Events.ToDictionary(e => e.ID, e => e.Runners.ToDictionary(r => r.No, r => new KeyValuePair<double, double>(r.WinPercent, r.PlacePercent)));

            foreach (Event e in m.Events) {
                if (oldChecks.ContainsKey(e.ID)) {
                    e.Check = oldChecks[e.ID];
                    foreach (Runner r in e.Runners) {
                        if (oldPercent.ContainsKey(e.ID) && oldPercent[e.ID].ContainsKey(r.No)) {
                            r.WinPercent = oldPercent[e.ID][r.No].Key;
                            r.PlacePercent = oldPercent[e.ID][r.No].Value;
                        }
                    }
                }
            }

            return m;
        }

        public void MapChildren(DateTime date) {
            if (Events != null) {
                /*
                // Useless for meetings starting after midnight
                DateTime previous = date.Date;
                foreach (Event e in Events)
                {
                    e.Parent = this;
                    e.StartTime = e.StartTime.Add(date.Date - DateTime.Parse("01/01/0001 12:00:00 AM"));
                    if (e.StartTime < previous)
                    {
                        e.StartTime = e.StartTime.AddDays(1);
                    }
                    previous = e.StartTime;
                    e.MapChildren();
                }
                */

                // Assumes all times before config controlled time are for next day. Unlikely that events in early morning will be for the current day
                foreach (Event e in Events) {
                    e.Parent = this;
                    if (e.StartTime < Properties.Settings.Default.TimeRollover) {
                        e.StartTime = e.StartTime.Add(date.Date - DateTime.MinValue).AddDays(1);
                    } else {
                        e.StartTime = e.StartTime.Add(date.Date - DateTime.MinValue);
                    }
                    e.MapChildren();
                }
            }
        }
    }
}
