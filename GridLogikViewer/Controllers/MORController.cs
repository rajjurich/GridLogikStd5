using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GridLogikViewer.Controllers
{
    public class MORController : Controller
    {
        //
        // GET: /MOR/
        public ActionResult Index()
        {
            List<SelectListItem> lst = new List<SelectListItem>() { 
            new SelectListItem{Text="Unit Commitment",Value="2"},
            new SelectListItem{Text="Economic Dispatch",Value="1"}
            };

            ViewBag.Mode = lst;
            return View();
        }

        //
        // GET: /MOR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MOR/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MOR/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MOR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MOR/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MOR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MOR/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
