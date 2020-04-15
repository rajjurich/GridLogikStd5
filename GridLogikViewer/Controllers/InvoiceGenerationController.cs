using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class InvoiceGenerationController : Controller
    {
        //
        // GET: /InvoiceGeneration/
        public ActionResult Index()
        {
            return View();
        }
        //[HttpOptions]
        public String PDF(String MeterString, String MeterString1)
        {
            var obj = JsonConvert.DeserializeObject<string>(MeterString);  
            return "success";
        }
	}
   
}