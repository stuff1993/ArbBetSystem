using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    public class Meeting : INotifyPropertyChanged
    {
        public enum MeetingTypes { Racing = 1, Harness = 2, Greyhound = 4 };

        public static string getMeetingTypesString(int meetingType)
        {
            switch (meetingType)
            {
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

        private uint idField;
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

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <remarks/>
        public string Venue
        {
            get
            {
                return this.venueField;
            }
            set
            {
                this.venueField = value;
            }
        }

        /// <remarks/>
        public string Type
        {
            get
            {
                switch (this.typeField)
                {
                    default:
                    case MeetingTypes.Racing:
                        return "R";
                    case MeetingTypes.Harness:
                        return "T";
                    case MeetingTypes.Greyhound:
                        return "G";
                }
            }
            set
            {
                switch (value)
                {
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
        public string Track
        {
            get
            {
                return this.trackField;
            }
            set
            {
                this.trackField = value;
            }
        }

        /// <remarks/>
        public string TrackRtg
        {
            get
            {
                return this.trackRtgField;
            }
            set
            {
                this.trackRtgField = value;
            }
        }

        /// <remarks/>
        public string Rail
        {
            get
            {
                return this.railField;
            }
            set
            {
                this.railField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string CodeTC
        {
            get
            {
                return this.codeTCField;
            }
            set
            {
                this.codeTCField = value;
            }
        }

        /// <remarks/>
        public string CodeQ
        {
            get
            {
                return this.codeQField;
            }
            set
            {
                this.codeQField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Event")]
        public Event[] Events
        {
            get
            {
                return this.eventsField;
            }
            set
            {
                this.eventsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        public Meeting() { }

        public override string ToString()
        {
            return Venue + " - " + Country;
        }

        public bool IsChecked
        {
            get
            {
                bool ret = false;
                foreach (Event evt in Events)
                {
                    ret = ret || evt.Check;
                }
                return ret;
            }
            set
            {
                foreach (Event e in Events)
                {
                    e.Check = value;
                }
                NotifyPropertyChanged();
            }
        }

        public Meeting MergeWith(Meeting m)
        {
            Dictionary<string, bool> oldChecks = Events.ToDictionary(e => e.ID, e=> e.Check);
            Dictionary<string, Dictionary<byte, double>> oldPercent = Events.ToDictionary(e => e.ID, e => e.Runners.ToDictionary(r => r.No, r => r.Percent));

            foreach (Event e in m.Events)
            {
                if (oldChecks.ContainsKey(e.ID))
                {
                    e.Check = oldChecks[e.ID];
                    foreach (Runner r in e.Runners)
                    {
                        if (oldPercent.ContainsKey(e.ID) && oldPercent[e.ID].ContainsKey(r.No))
                        {
                            r.Percent = oldPercent[e.ID][r.No];
                        }
                    }
                }
            }

            return m;
        }

        public void MapChildren()
        {
            if (Events != null)
            {
                foreach (Event e in Events)
                {
                    e.Parent = this;
                    e.MapChildren();
                }
            }
        }
    }
}
