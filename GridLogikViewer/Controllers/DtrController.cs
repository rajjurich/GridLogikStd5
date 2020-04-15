using GridLogikViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DtrController : Controller
    {
        //
        // GET: /DST/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DST/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DST/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstDtr objDtr)
        {
            return View();
        }

        //
        // GET: /DST/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstDtr objDtr)
        {
            return View();
        }

        //
        // GET: /DST/Delete/5
        public ActionResult Delete(long id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MstDtr objDtr)
        {
            return View();
        }
    }
}
