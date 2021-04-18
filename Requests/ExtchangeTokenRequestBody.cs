using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Requests
{
    class ExtchangeTokenRequestBody
    {
        [JsonProperty("accessToken")]

        public string AccessToken { get; set; }
        [JsonProperty("refereshToken")]
        public string RefreshToken { get; set; }
    }
}
