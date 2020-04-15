using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogikViewer.Models;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class TOUController : Controller
    {
        //
        // GET: /TOU/
        [AccessCheck(IdParamName = "TOU/Index")]
        public ActionResult Index()
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            return View();
        }

        //
        // GET: /TOU/Details/5
        public ActionResult Details(long id)
        {
            return View();
        }

        //
        // GET: /TOU/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TOU/Create
        [HttpPost]
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
        // GET: /TOU/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TOU/Edit/5
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
        // GET: /TOU/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TOU/Delete/5
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

        [HttpPost]
        public PartialViewResult TouSlot(string count)
        {
            ViewBag.count = Convert.ToInt16(count);
            return PartialView();
        }
    }
}
