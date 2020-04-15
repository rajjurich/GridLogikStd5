using GridLogik.ViewModels;
using GridLogikViewer.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ReportShedularController : Controller
    {
        //
        // GET: /ReportShedular/
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        [AccessCheck(IdParamName = "Meter/Index")]
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
        public ActionResult Create()
        {
            return View();

        }
        public ActionResult Edit(int id)
        {
            return View();
        }
        public ActionResult Delete(long id)
        {
            return View();
        }
    }
}