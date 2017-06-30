using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArbBetSystem.Utils {
    static class StringUtils {
        public static string Capitalise(string s) {
            // Check for empty string.
            if (string.IsNullOrEmpty(s)) {
                return string.Empty;
            }
            // Return char and concat substring.
            string retStr = "";
            foreach (string s2 in s.Split(' ')) {
                retStr += char.ToUpper(s2[0]) + s2.Substring(1) + " ";
            }
            return retStr.TrimEnd();
        }
    }
}
