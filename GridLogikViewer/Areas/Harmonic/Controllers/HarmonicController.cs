using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
namespace GridLogikViewer.Areas.Harmonic.Controllers
{
    public class HarmonicController : Controller
    {
        //
        
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        public ActionResult Index()
        {
            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("Index", model);
        }
        private List<MeterGroup> ListMeterGroup()
        {
            List<MeterGroup> meterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"]));
                meterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }
            return meterGroup;
        }

        public ActionResult Harmonicthd()
        {
            List<MeterGroup> MeterGroup = new List<MeterGroup>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"]));
                MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            }

            ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");
            FillParameters();
            return View();
        }

        private void FillParameters()
        {
            //Populate Parameters in controller
            List<SelectListItem> lstParameters = new List<SelectListItem>();
            //int currentYear = DateTime.Now.Year;
            //List<SelectListItem> lstYear = new List<SelectListItem>();
            lstParameters.Add(new SelectListItem { Text = "THD", Value = "T" });

            //Assign the value to ViewBag
            ViewBag.Parameters = new SelectList(lstParameters, "Value", "Text");
        }

        public ActionResult MetersByGroupID(int id)
        {
            List<Meter> Meters = new List<Meter>();
            using (WebClient client = new WebClient())
            {

                string s = client.DownloadString(url + "MeterAPI/GetMetersByGroupID/" + id);
                Meters = JsonConvert.DeserializeObject<List<Meter>>(s);
                Meters.RemoveAll(item => item == null);
            }
            SelectList objMeters = new SelectList(Meters, "ID", "MeterName");
            return Json(objMeters, JsonRequestBehavior.AllowGet);
        }

    }
}