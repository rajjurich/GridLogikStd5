using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.SAIL_DSP.Controllers
{
    public class SAILDSPController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        public ActionResult BFN()
        {
            return View("BFN");
        }

        public ActionResult BMG_BLT()
        {
            return View("BMG_BLT");
        }
        public ActionResult COS()
        {
            return View("COS");
        }
        public ActionResult HT_5()
        {
            return View("HT_5");
        }
        public ActionResult HT_6()
        {
            return View("HT_6");
        }
        public ActionResult HT_7()
        {
            return View("HT_7");
        }
        public ActionResult HT_8()
        {
            return View("HT_8");
        }

        public ActionResult HT_8_3300kv()
        {
            return View("HT_8_3300kv");
        }
        public ActionResult MRS()
        {
            return View("MRS");
        }

        public ActionResult NSJR_11KV()
        {
            return View("NSJR_11KV");
        }

        public ActionResult NSJR_33kv()
        {
            return View("NSJR_33kv");
        }
        public ActionResult OLD_O2()
        {
            return View("OLD_O2");
        }

        public ActionResult OLD_SINTER()
        {
            return View("OLD_SINTER");
        }
        public ActionResult OLD_SINTER_3300KV()
        {
            return View("OLD_SINTER_3300KV");
        }
        public ActionResult PNB()
        {
            return View("PNB");
        }
        public ActionResult SEC()
        {
            return View("SEC");
        }
        public ActionResult SKP()
        {
            return View("SKP");
        }

        public ActionResult HT_12()
        {
            return View("HT_12");
        }
    }
}