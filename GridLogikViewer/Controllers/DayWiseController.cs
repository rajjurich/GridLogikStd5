using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DayWiseController : Controller
    {
        //
        // GET: /DayWise/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(int id)
        {
            return View();
        }

	}
}