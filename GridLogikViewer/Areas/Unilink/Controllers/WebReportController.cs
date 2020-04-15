using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.Unilink.Controllers
{
    public class WebReportController : Controller
    {
        //
        // GET: /Unilink/WebReport/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.RowId = id;
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
        public ActionResult SetReportPath()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
	}
}