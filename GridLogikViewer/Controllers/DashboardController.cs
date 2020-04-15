using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plant()
        {
            return View();
        }

        public ActionResult EMS()
        {
            return View();
        }

        public ActionResult ABT()
        {
            return View();
        }
	}
}