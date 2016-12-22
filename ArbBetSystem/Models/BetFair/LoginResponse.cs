using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ArbBetSystem.Models.BetFair
{
    [DataContract]
    class LoginResponse
    {
        [DataMember(Name = "sessionToken")]
        public string SessionToken { get; set; }
        [DataMember(Name = "loginStatus")]
        public string LoginStatus { get; set; }
    }
}
