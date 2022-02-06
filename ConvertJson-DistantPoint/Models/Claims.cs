using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertJson_DistantPoint.Models
{
    public class Claims
    {
        [JsonProperty("Claim Type")]
        public string ClaimType { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
