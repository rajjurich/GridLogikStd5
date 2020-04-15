using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.MPPGCL.Controllers
{
    public class MPPGCLController : Controller
    {
        //
        // GET: /MPPGCL/MPPGCL/
        public ActionResult SLDATPS()
        {
            return View();
        }

        public ActionResult SLDSARNI()
        {
            return View();
        }
        public ActionResult Summary()
        {
            return View("Summary");
        }
        public ActionResult Summary1()
        {
            return View("Summary1");
        }

        public ActionResult SummaryCentral()
        {
            return View("SummaryCentral");
        }


    }
}