using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    // [Authorize]
    public class GroupViewController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /GroupWiseData/
        public async Task<ActionResult> Index()
        {
            ProfileLogViewModel model = new ProfileLogViewModel();
            model.StartTimeList = TimeSlotList();
            model.EndTimeList = TimeSlotList();
            //model.Meters = ListMeterModel();
            model.Groups = await ListMeterGroup();
            //model.parameterlist = ListMeterGroup();
            return View(model);
        }

        private List<string> TimeSlotList()
        {
            var list = new List<string>()
            {
                "12:00 AM","12:15 AM","12:30 AM","12:45 AM","01:00 AM","01:15 AM","01:30 AM","01:45 AM","02:00 AM","02:15 AM",
                "02:30 AM","02:45 AM","03:00 AM","03:15 AM","03:30 AM","03:45 AM","04:00 AM","04:15 AM","04:30 AM","04:45 AM",
                "05:00 AM","05:15 AM","05:30 AM","05:45 AM","06:00 AM","06:15 AM","06:30 AM","06:45 AM","07:00 AM","07:15 AM",
                "07:30 AM","07:45 AM","08:00 AM","08:15 AM","08:30 AM","08:45 AM","09:00 AM","09:15 AM","09:30 AM","09:45 AM",
                "10:00 AM","10:15 AM","10:30 AM","10:45 AM","11:00 AM","11:15 AM","11:30 AM","11:45 AM","12:00 PM","12:15 PM",
                "12:30 PM","12:45 PM","01:00 PM","01:15 PM","01:30 PM","01:45 PM","02:00 PM","02:15 PM","02:30 PM","02:45 PM",
                "03:00 PM","03:15 PM","03:30 PM","03:45 PM","04:00 PM","04:15 PM","04:30 PM","04:45 PM","05:00 PM","05:15 PM",
                "05:30 PM","05:45 PM","06:00 PM","06:15 PM","06:30 PM","06:45 PM","07:00 PM","07:15 PM","07:30 PM","07:45 PM",
                "08:00 PM","08:15 PM","08:30 PM","08:45 PM","09:00 PM","09:15 PM","09:30 PM","09:45 PM","10:00 PM","10:15 PM",
                "10:30 PM","10:45 PM","11:00 PM","11:15 PM","11:30 PM","11:45 PM"
            };
            return list;
        }

        private async Task<List<MeterGroup>> ListMeterGroup()
        {
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterGroupAPI");
            //    meterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}
            return meterGroups.ToList();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> MetersBymultipleGroupID(string id)
        {
            List<MeterVM> Meters = new List<MeterVM>();

            IEnumerable<MeterVM> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Meter/GetMetersByMultipleGroupID/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterVM>>();
            }

            Meters = meterGroups.ToList();
            //using (WebClient client = new WebClient())
            //{

            //    string s = client.DownloadString(url + "meterAPI/GetMetersByMultipleGroupID" + "/" + id);
            //    Meters = JsonConvert.DeserializeObject<List<MeterVM>>(s);
            //}
            SelectList objMeters = new SelectList(Meters, "ID", "MeterName");
            return Json(objMeters);
        }

    }
}