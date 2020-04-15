using GridLogikViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace GridLogikViewer.GridLogikViewerModels
{
    //to delete
    public class clsExceptionRepository
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        public void DBErrorLog(string error_Description, string error_trace, string error_module)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    MstErrorLog objErrorLog = new MstErrorLog();
                    objErrorLog.errordescription = error_Description;
                    objErrorLog.errortrace = error_trace;
                    objErrorLog.errordate = DateTime.Now;
                    objErrorLog.errormodule = error_module;
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync("Exception", objErrorLog).Result;

                }

            }
            catch (Exception)
            {
                
                throw;
            }    
        }
       
    }
}