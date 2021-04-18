using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Common
{
    public static class APIRequestConstants
    {
        public static string ReactantListURL = "api/reactant-lists/";

        public static string APIBaseURL  = "https://services-test.neuro-ace.net";

        public static string ReacntaListURLwithID  = "api/reactant-lists/{id}";

        public static string URLSegmaentID  = "id";

        public static string AccountLoginURL  = "api/account/login";

        public static string AccountLogoutURL = "api/account/Signout";

        public static string AuthorizationHeaderKey = "authorization";




    }

}
