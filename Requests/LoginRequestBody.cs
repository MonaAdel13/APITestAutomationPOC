﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Requests
{
    class LoginRequestBody
    {
        [JsonProperty("userName")]

        public string UserName { get; set; }

        [JsonProperty("password")]

        public string Password { get; set; }

    }
}
