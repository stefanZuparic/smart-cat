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
        public static HttpClient ApiShift { get; set; }

        public static void InitialazeApi()
        {
            ApiShift = new HttpClient();
            ApiShift.DefaultRequestHeaders.Accept.Clear();
            ApiShift.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }
    }
}
