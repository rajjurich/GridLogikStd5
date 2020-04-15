using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace GridLogikViewer.Models
{
    public class PrmGlobal
    {
        public long prmrecid { get; set; }
        public string prmmodule { get; set; }
        public string prmunit { get; set; }
        public string prmidentifier { get; set; }
        public string prmvalue { get; set; }
        public string rfu1 { get; set; }
        public string rfu2 { get; set; }

        public static string GetDateTimeFormat()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("prmglobal").Result;
            if (response.IsSuccessStatusCode)
            {
                var prmglobal = response.Content.ReadAsStringAsync().Result;

                dynamic objprmglobal = JValue.Parse(prmglobal);
                return "{0:" + objprmglobal.Data.result + "}";
            }
            return "";
        }
    }
    
}