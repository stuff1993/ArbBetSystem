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
        private string nameField;
        private string jockeyField;
        private string trainerField;
        private byte barField;
        private string hcpField;
        private byte scrField;
        private byte noField;

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
    }
}
