using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.ILFS.Controllers
{
    public class ILFSController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        public ActionResult ABTUnit1()
        {
            return View("ABTUnit1");
        }
        public ActionResult ABTUnit2()
        {
            return View("ABTUnit2");
        }
        public ActionResult Station()
        {
            return View("Station");
        }
        public ActionResult Unit1Block()
        {
            return View("Unit1Block");
        }
        public ActionResult Unit2Block()
        {
            return View("Unit2Block");
        }
        public ActionResult Calculate()
        {
            return View("Unit2Block");
        }
        public ActionResult SLD()
        {
            return View("SLD");
        }
        public ActionResult One()
        {
            return View("One");
        }
        public ActionResult Two()
        {
            return View("Two");
        }
        public ActionResult Three()
        {
            return View("Three");
        }
        public ActionResult Four()
        {
            return View("Four");
        }
        public ActionResult Five()
        {
            return View("Five");
        }
        public ActionResult Six()
        {
            return View("Six");
        }
        public ActionResult Seven()
        {
            return View("Seven");
        }
    }
}