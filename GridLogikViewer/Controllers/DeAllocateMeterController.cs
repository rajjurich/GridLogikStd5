using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DeAllocateMeterController : Controller
    {
        //
        // GET: /DeAllocateMeter/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }
	}
}