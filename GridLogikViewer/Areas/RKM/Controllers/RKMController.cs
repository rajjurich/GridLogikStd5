using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.RKM.Controllers
{
    public class RKMController : Controller
    {
        public ActionResult Summary()
        {
            return View("Summary");
        }
	}
}