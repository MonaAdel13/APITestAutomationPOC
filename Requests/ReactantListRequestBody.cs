using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Requests
{
    class ReactantListRequestBody
    {
       
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]

            public string Description { get; set; }

            [JsonProperty("reactantIds")]

            public IList<int> ReactantIds { get; set; }
        
    }
}
