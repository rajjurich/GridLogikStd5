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
    public class ConsumerMeterRelationController : Controller
    {
        //
        // GET: /ConsumerMeterRelation/
        [AccessCheck(IdParamName = "ConsumerMeterRelation/Index")]
        public ActionResult Index()
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            return View("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstConsumerMeterRelation objConsumerMeterRelation)
        {
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }

        [HttpPut]
        public ActionResult Edit(MstConsumerMeterRelation objConsumerMeterRelation)
        {
            return View();
        }


        public ActionResult Delete(long id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MstConsumerMeterRelation objConsumerMeterRelation)
        {
            return View();
        }
	}
}