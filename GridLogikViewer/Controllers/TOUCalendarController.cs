using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogikViewer.Models;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class TOUCalendarController : Controller
    {
        //
        // GET: /TOUCalendar/
        public ActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(MstTOUCalendar objTOUCalendar)
        {
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }

        [HttpPut]
        public ActionResult Edit(MstTOUCalendar objTOUCalendar)
        {
            return View();
        }


        public ActionResult Delete(long id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(MstRole objTOUCalendar)
        {
            return View();
        }
	}
}