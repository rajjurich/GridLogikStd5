using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ThermalSummaryController : Controller
    {
        //
        // GET: /ThermalSummary/
        public ActionResult Index()
        {
            return View();
        }
	}
}