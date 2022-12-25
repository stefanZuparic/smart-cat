using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace etl.Helpers
{
    internal class ApiHelper
    {
        public static HttpClient ShiftsAPI { get; set; }

        public static void InitialazeApi()
        {
            ShiftsAPI = new HttpClient();
            ShiftsAPI.DefaultRequestHeaders.Accept.Clear();
            ShiftsAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }
    }
}
