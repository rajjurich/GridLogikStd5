using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using Newtonsoft.Json;

namespace GridLogikViewer.Areas.KRAHEJA.Controllers
{
    public class SummaryController : Controller
    {
        //
        // GET: /KRAHEJA/Summary/
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        public ActionResult GEPLSummary()
        {
            return View("GEPLSummary");
        }
        public ActionResult MBPLSummary()
        {
            return View("MBPLSummary");
        }
        public ActionResult Summary()
        {
            return View("Summary");
        }
        public ActionResult CentralSummary()
        {
            return View("CentralSummary");
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        public ActionResult Dashboard_New()
        {
            return View("Dashboard_New");
        }

        [HttpGet]       
        public JsonResult GetGlobalValuesLocation()
        {
            HttpClient ObjHttpclient = new HttpClient();
            ObjHttpclient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            ObjHttpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = ObjHttpclient.GetAsync("prmglobal/GetTablesIdentifiers/Locationfilter").Result;
            List<prmglobal> lstGlobal = null;
            if (response.IsSuccessStatusCode)
            {
                var objResponse = response.Content.ReadAsStringAsync().Result;
                lstGlobal = new List<prmglobal>();
                dynamic objPrmGlobal = JValue.Parse(objResponse);
                foreach (dynamic prm in objPrmGlobal.Data.result)
                {
                    if (prm.prmmodule.ToString().ToLower() != "global")
                    {
                        prmglobal obj = new prmglobal();
                        obj.prmidentifier = prm.prmidentifier.ToString();
                        obj.prmvalue = prm.prmvalue.ToString();
                        lstGlobal.Add(obj);
                    }
                }
            }           
            return Json(lstGlobal, JsonRequestBehavior.AllowGet);
        }      
    }
}