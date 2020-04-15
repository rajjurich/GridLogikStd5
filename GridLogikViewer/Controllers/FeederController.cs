using GridLogikViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class FeederController : Controller
    {
        // GET: /Feeder/        
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Feeder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Feeder/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstFeeder objDcu)
        {
            return View();
        }

        //
        // GET: /Feeder/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstFeeder objDcu)
        {
            return View();
        }

        //
        // GET: /Feeder/Delete/5
        public ActionResult Delete(long id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MstFeeder objDcu)
        {
            return View();
        }
	}
}