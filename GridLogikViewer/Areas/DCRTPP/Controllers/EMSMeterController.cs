using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using Newtonsoft.Json;


namespace GridLogikViewer.Areas.DCRTPP.Controllers
{
    public class EMSMeterController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        private List<MeterModel> ListMeterModel()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            return meterModel;
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
        public ActionResult Index()
        {

            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("EMSMeterDetails", model);
        }

        public ActionResult BOILERAUXILIARIES()
        {

            InstanceData model = new InstanceData();

            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("BOILERAUXILIARIES", model);
        }
        public ActionResult TURBINEAUX()
        {

            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("TURBINEAUX", model);
        }
        public ActionResult OTHERAUX()
        {

            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("OTHERAUX", model);
        }
        public ActionResult CHPArea()
        {

            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("CHPArea", model);
        }
        public ActionResult AHP()
        {

            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("AHP", model);
        }
        public ActionResult FEEDERSArea()
        {

            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
            model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("FEEDERSArea", model);
        }
       
        [HttpGet]
        public ActionResult EMSMeterDetails()
        {
            return View("EMSMeterDetails");
        }

        [HttpGet]
        public ActionResult GetEMSdetails(string id)
        {
            List<GetInstanceData> model = new List<GetInstanceData>();
            if (id != null)
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "GetAllInstanceData/" + id);
                    model = JsonConvert.DeserializeObject<List<GetInstanceData>>(s);

                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
           

        }


        // GET: ABTMeter
        [HttpPost]
        public ActionResult GetInstanceData(string id)
        {

            InstanceData model = new InstanceData();
            if (id != "")
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "InstanceDataAPI" + "/" + Convert.ToInt16(id));
                    model = JsonConvert.DeserializeObject<InstanceData>(s);
                }
            }
            return PartialView("_ABTMeter", model);
        }
    }
}