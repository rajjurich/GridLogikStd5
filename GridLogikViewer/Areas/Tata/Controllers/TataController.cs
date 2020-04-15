using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.Tata.Controllers
{
    public class TataController : Controller
    {
        //
        // GET: /Tata/Tata/
        //
        // GET: /TATA/Tata/
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        public ActionResult Screen1()
        {
            return View("Screen1");
        }
        public ActionResult Screen2()
        {
            return View("Screen2");
        }
        public ActionResult Screen3()
        {
            return View("Screen3");
        }
        public ActionResult Screen4()
        {
            return View("Screen4");
        }
        public ActionResult Screen5()
        {
            return View("Screen5");
        }
        public ActionResult Screen6()
        {
            return View("Screen6");
        }
        public ActionResult Screen7()
        {
            return View("Screen7");
        }
        public ActionResult Screen8()
        {
            return View("Screen8");
        }

        public ActionResult Screen9()
        {
            return View("Screen9");
        }
        public ActionResult Screen10()
        {
            return View("Screen10");
        }
        public ActionResult Screen11()
        {
            return View("Screen11");
        }
        public ActionResult Screen12()
        {
            return View("Screen12");
        }
        public ActionResult Screen13()
        {
            return View("Screen13");
        }
        public ActionResult Screen14()
        {
            return View("Screen14");
        }
        public ActionResult Screen15()
        {
            return View("Screen15");
        }
        public ActionResult Screen16()
        {
            return View("Screen16");
        }
        public ActionResult Screen17()
        {
            return View("Screen17");

        }
        public ActionResult Screen18()
        {
            return View("Screen18");
        }
        public ActionResult Screen19()
        {
            return View("Screen19");
        }
        public ActionResult Screen20()
        {
            return View("Screen20");
        }
        public ActionResult Screen21()
        {
            return View("Screen21");
        }
    }
}