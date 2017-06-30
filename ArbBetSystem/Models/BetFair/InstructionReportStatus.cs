using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArbBetSystem.Models.BetFair
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstructionReportStatus
    {
        SUCCESS,
        FAILURE,
        TIMEOUT,
    }
}