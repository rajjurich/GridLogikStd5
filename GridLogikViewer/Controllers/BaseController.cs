using GridLogik.ViewModels;
using GridLogikViewer.GridLogikViewerModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    public class BaseController : Controller
    {  
        protected override void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            DBErrorLog(ex.Message, ex.StackTrace, filterContext.Controller.ToString());            
        }

        public void DBErrorLog(string error_Description, string error_trace, string error_module)
        {
            string url = ConfigurationManager.AppSettings["APIUrl"];
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

                //throw;
            }
        }
    }
}