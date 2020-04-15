using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.Gadarwara.Controllers
{
    public class SummaryController : Controller
    {
        //
        // GET: /Gadarwara/Summary/
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        public ActionResult PlantSummary()
        {
            return View("PlantSummary");
        }

        public ActionResult SummaryOne()
        {
            return View("SummaryOne");
        }

        public ActionResult SummaryTwo()
        {
            return View("SummaryTwo");
        }
        public ActionResult SummaryThree()
        {
            return View("SummaryThree");
        }

        public ActionResult SummaryFour()
        {
            return View("SummaryFour");
        }
	}
}