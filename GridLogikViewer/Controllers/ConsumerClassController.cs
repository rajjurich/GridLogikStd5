using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class ConsumerClassController : Controller
    {
        //
        // GET: /ConsumerClass/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ConsumerClass/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ConsumerClass/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ConsumerClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

            return View();
        }

        //
        // GET: /ConsumerClass/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ConsumerClass/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // GET: /ConsumerClass/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ConsumerClass/Delete/5
       [HttpPut]
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
