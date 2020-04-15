using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    public class ServiceIntimationController : Controller
    {
        //
        // GET: /ServiceIntimation/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ServiceIntimation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ServiceIntimation/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ServiceIntimation/Create
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
        // GET: /ServiceIntimation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ServiceIntimation/Edit/5
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
        // GET: /ServiceIntimation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ServiceIntimation/Delete/5
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
