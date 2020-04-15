using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.JPLAvantha.Controllers
{
    public class JPLAvanthaController : Controller
    {
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