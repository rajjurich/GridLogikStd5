using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.ABTMeterView.Controllers
{
    public class ABTMeterViewController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /MeterView/
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
                string s = client.DownloadString(url + "MeterGroupAPI");
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
            return View("ABTMeterView", model);
        }



        [HttpGet]
        public ActionResult MeterViewDetails(InstanceData model)
        {
            model.CurrentDate = DateTime.Now;
            return View("ABTMeterViewDetails", model);
        }



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