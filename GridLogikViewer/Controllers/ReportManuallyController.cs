using GridLogikViewer.GridLogikViewerModels;
using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ReportManuallyController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /ReportManuaaly/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        //public FileResult GetReport(string id)
        //{
        //    try
        //    {
        //        if (!(string.IsNullOrWhiteSpace(id)))
        //        {
        //            using (WebClient client = new WebClient())
        //            {
        //                client.Headers.Add("Content-Type", "application/json");
        //                string s = client.DownloadString(url + "ReportManuallyApi/GetReportStatus/" + id);
        //                dynamic dynJson1 = JValue.Parse(s);
        //                dynamic dynJson = dynJson1.Data.result;

        //                foreach (var dr in dynJson)
        //                {
        //                    string path = dr.filepath;
        //                    System.IO.FileInfo file = new System.IO.FileInfo(path);
        //                    if (file.Exists)
        //                    {
        //                        byte[] fileBytes = System.IO.File.ReadAllBytes(@path);
        //                        string fileName = file.Name;
        //                        return File(fileBytes, "application/ms-excel", file.Name);
        //                    }
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
        //        return null;
        //    }
        //}

    }
}