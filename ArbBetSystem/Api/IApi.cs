using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbBetSystem.Api
{
    interface IApi
    {
        string SessionId
        {
            get;
            set;
        }

        Creds LoginDetails
        {
            get;
            set;
        }

        bool doLogin();
    }
}
