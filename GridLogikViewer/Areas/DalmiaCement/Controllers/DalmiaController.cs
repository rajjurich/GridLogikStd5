using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.DalmiaCement.Controllers
{
    public class DalmiaController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
     
        public ActionResult SLDOne()
        {
            return View("SLDOne");
        }

        public ActionResult SLD2()
        {
            return View("SLD2");
        }
        public ActionResult SLD3()
        {
            return View("SLD3");
        }

        public ActionResult SLD4()
        {
            return View("SLD4");
        }

        public ActionResult SLD5()
        {
            return View("SLD5");
        }

        public ActionResult SLD6()
        {
            return View("SLD6");
        }
	}
}