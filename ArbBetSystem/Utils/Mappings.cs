using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbBetSystem.Utils {
    class Mappings {
        #region Value Maps
        public static Dictionary<string, string> CountryMappings;
        public static Dictionary<string, string> VenueMappings;

        public static bool isVenueMapped_DO(string venue) {
            return Mappings.VenueMappings.ContainsKey(venue);
        }

        public static bool isVenueMapped_BF(string venue) {
            return Mappings.VenueMappings.ContainsValue(venue);
        }

        public static string getBFFormattedVenue(string key) {
            string val;
            if (!VenueMappings.TryGetValue(key, out val)) {
                return StringUtils.Capitalise(key.ToLower());
            }
            return val;
        }

        public static string getCountry(string key) {
            string val;
            if (!CountryMappings.TryGetValue(key, out val)) {
                return key;
            }
            return val;
        }
        #endregion

        #region Display Maps
        public static Dictionary<string, string> Other;
        public static Dictionary<string, string> WinBackNames;
        public static Dictionary<string, string> WinLayNames;
        public static Dictionary<string, string> PlaceBackNames;
        public static Dictionary<string, string> PlaceLayNames;
        public static Dictionary<string, int> Order;

        #endregion
    }
}
