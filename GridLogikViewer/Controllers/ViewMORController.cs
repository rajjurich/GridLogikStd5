using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ViewMORController : Controller
    {
        //
        // GET: /ViewMOR/
        public ActionResult Index()
        {
            List<SelectListItem> lst = new List<SelectListItem>() { 
            new SelectListItem{Text="Unit Commitment",Value="2"},
            new SelectListItem{Text="Economic Dispatch",Value="1"}
            };

            ViewBag.Mode = lst;
            return View();
        }
	}
}