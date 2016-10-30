using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArbBetSystem
{
    [XmlRoot (ElementName="WeatherConditions")]
    public class Weather
    {
        private string tempCField;
        private string humidityField;
        private string windSpeedField;
        private string windDirField;

        /// <remarks/>
        public string TempC
        {
            get
            {
                return this.tempCField;
            }
            set
            {
                this.tempCField = value;
            }
        }

        /// <remarks/>
        public string Humidity
        {
            get
            {
                return this.humidityField;
            }
            set
            {
                this.humidityField = value;
            }
        }

        /// <remarks/>
        public string WindSpeed
        {
            get
            {
                return this.windSpeedField;
            }
            set
            {
                this.windSpeedField = value;
            }
        }

        /// <remarks/>
        public string WindDir
        {
            get
            {
                return this.windDirField;
            }
            set
            {
                this.windDirField = value;
            }
        }
    }
}
