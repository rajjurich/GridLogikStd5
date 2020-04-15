using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.Mvc.Html;
using System.Net.Mime;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Net.Http;
using System.Threading.Tasks;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ABTMeterController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
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

        private async Task<List<MeterGroup>> ListMeterGroup()
        {
            //List<MeterGroup> meterGroup = new List<MeterGroup>();
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
            //    meterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}
            return meterGroups.ToList();
        }
        public ActionResult Index()
        {
            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
           // model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            return View("ABTMeter", model);
        }
        [HttpGet]
        public ActionResult ABTMeterDetails()//InstanceData model
        {
            InstanceData model = new InstanceData();
            //model.Meters = ListMeterModel();
          //  model.Groups = ListMeterGroup();
            model.CurrentDate = DateTime.Now;
            //model.CurrentDate = DateTime.Now;
            //return View("ABTMeterDetails", model);
            return View("ABTMeterDetails",model);
        }

        // GET: ABTMeter
        [HttpPost]
        [ValidateAntiForgeryToken]
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