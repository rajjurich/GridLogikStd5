using GridLogikViewer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DcuController : Controller
    {
        // GET: /Dcu/        
        public ActionResult Index()
        {            
            return View();
        }

        //
        // GET: /Dcu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Dcu/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstDcu objDcu)
        {
            return View();
        }

        //
        // GET: /dcu/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstDcu objDcu)
        {
            return View();
        }

        //
        // GET: /dcu/Delete/5
        public ActionResult Delete(long id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MstDcu objDcu)
        {
            return View();
        }
	}
}