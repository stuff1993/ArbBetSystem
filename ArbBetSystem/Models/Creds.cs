using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbBetSystem
{
    public class Creds
    {
        public string Username { get; set; }
        public string Password { get; set; } // stored as string as need plaintext version for api login call

        public Creds(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public override string ToString()
        {
            return Username + Environment.NewLine + Password;
        }
    }
}
