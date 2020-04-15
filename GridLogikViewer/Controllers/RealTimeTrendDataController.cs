using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using GridLogikViewer.APIModels;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using GridLogik.ViewModels;


namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class RealTimeTrendDataController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        public ActionResult Index()
        {
            List<MeterGroup> MeterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
                MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }

            ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");
            return View();
        }
        [HttpGet]
   
        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult MetersByGroupID(int id)
        {
            List<Meter> Meters = new List<Meter>();
            using (WebClient client = new WebClient())
            {

                string s = client.DownloadString(url + "MeterAPI/GetMetersByGroupID/" + id);
                Meters = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
            SelectList objMeters = new SelectList(Meters, "ID", "MeterName");
            return Json(objMeters);
        }

        //public string MetersByGroupID(int id)
        //{
        //    List<Meter> Meters = new List<Meter>();
        //    using (WebClient client = new WebClient())
        //    {

        //        string s = client.DownloadString(url + "MeterAPI/GetMetersByGroupID/" + id);
        //        Meters = JsonConvert.DeserializeObject<List<Meter>>(s);
        //    }
        //    SelectList objMeters = new SelectList(Meters, "ID", "MeterName");
        //    return "got it";
        //}

	}
}