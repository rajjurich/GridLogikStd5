using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.GMR.Controllers
{
    public class GMRController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        public ActionResult Summary()
        {
            return View("Summary");
        }
        public ActionResult CurrentSummary()
        {
            return View("CurrentSummary");
        }
	}
}