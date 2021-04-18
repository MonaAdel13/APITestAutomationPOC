using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Responses
{
    class ReactantListResponse
    {
        public ReactantListResponse()
        {
            results = new List<ReactantList>();
        }

        //hint -- mapping json property to json property 
        //[JsonProperty("pageNum")]
        public int PageNumber { get; set; }

        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }

        public IList<ReactantList> results { get; set; }
    }

    
    public class ReactantList
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int reactantCount { get; set; }



    }
}
