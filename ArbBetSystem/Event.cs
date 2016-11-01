using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    public class Event
    {
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

        private bool checkField;

        public bool Check
        {
            get
            {
                return this.checkField;
            }
            set
            {
                this.checkField = value;
            }
        }

        /// <remarks/>
        public uint EventNo
        {
            get
            {
                return this.eventNoField;
            }
            set
            {
                this.eventNoField = value;
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
        public uint Distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
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
        public uint Starters
        {
            get
            {
                return this.startersField;
            }
            set
            {
                this.startersField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute(DataType = "time")]
        public DateTime StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        /// <remarks/>
        public string Class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }

        /// <remarks/>
        public uint Prizemoney
        {
            get
            {
                return this.prizemoneyField;
            }
            set
            {
                this.prizemoneyField = value;
            }
        }

        /// <remarks/>
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public string WeatherTC
        {
            get
            {
                return this.weatherTCField;
            }
            set
            {
                this.weatherTCField = value;
            }
        }

        /// <remarks/>
        public Weather WeatherCondtions
        {
            get
            {
                return this.weatherCondtionsField;
            }
            set
            {
                this.weatherCondtionsField = value;
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
        [XmlElementAttribute("Runner")]
        public Runner[] Runners
        {
            get
            {
                return this.runnerField;
            }
            set
            {
                this.runnerField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string ID
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

        public bool HasOdds()
        {
            return Runners.Any(r => r.HasOdds());
        }

        public override string ToString()
        {
            return Name + " @ " + StartTime.ToShortTimeString();
        }

        public Event()
        {
            checkField = false;
        }
    }
}
