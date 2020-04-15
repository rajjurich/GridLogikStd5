using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    public class MasterDashboardController : Controller
    {
        //
        // GET: /MasterDashboard/

        public ActionResult Index()
        {
            HttpContext.Session["mnutype"] = -1;
            return View();
        }

        [HttpGet]
        public ActionResult FillSubMenuType(int Type)
        {
            try
            {
                HttpContext.Session["mnutype"] = Type;

                return Json("S", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                using (StreamWriter SW = new StreamWriter("D:\\Log" + now.Day + now.Month + now.Year + ".txt", true))
                {
                    SW.WriteLine("--------------------------------------------------------");
                    SW.WriteLine(ex.Message);
                    SW.WriteLine(ex.StackTrace);
                    SW.Close();
                }
                return Json("F", JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult CallBatch()
        {
            try
            {
                string BatchPath = Convert.ToString(WebConfigurationManager.AppSettings["BatchPath"]);
                if (!string.IsNullOrEmpty(BatchPath))
                    System.Diagnostics.Process.Start(@BatchPath);
                return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }

    }
}