using GridLogik.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ConsumptionController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        // GET api/consumption
        private async Task<List<MeterGroup>> ListMeterGroup()
        {
            List<MeterGroup> meterGroup = new List<MeterGroup>();
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            meterGroup = meterGroups.ToList();
            return meterGroup;
        }       
        private void FillMonthAndYear()
        {
            //Populate month data in controller
            List<SelectListItem> lstMonth = new List<SelectListItem>();
            int YearFrom = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["YearSelection"]);
            int currentYear = DateTime.Now.Year;
            List<SelectListItem> lstYear = new List<SelectListItem>();
            lstMonth.Add(new SelectListItem { Text = "January", Value = "1" });
            lstMonth.Add(new SelectListItem { Text = "February", Value = "2" });
            lstMonth.Add(new SelectListItem { Text = "March", Value = "3" });
            lstMonth.Add(new SelectListItem { Text = "April", Value = "4" });
            lstMonth.Add(new SelectListItem { Text = "May", Value = "5" });
            lstMonth.Add(new SelectListItem { Text = "June", Value = "6" });
            lstMonth.Add(new SelectListItem { Text = "July", Value = "7" });
            lstMonth.Add(new SelectListItem { Text = "August", Value = "8" });
            lstMonth.Add(new SelectListItem { Text = "September", Value = "9" });
            lstMonth.Add(new SelectListItem { Text = "October", Value = "10" });
            lstMonth.Add(new SelectListItem { Text = "November", Value = "11" });
            lstMonth.Add(new SelectListItem { Text = "December", Value = "12" });
            //Assign the value to ViewBag
            ViewBag.Months = new SelectList(lstMonth, "Value", "Text");


            for (int i = YearFrom; i <= currentYear; i++)
            {
                lstYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.Years = new SelectList(lstYear, "Value", "Text");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MetersByGroupID(string id)
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

        private void FillParameters()
        {
            //Populate Parameters in controller
            List<SelectListItem> lstParameters = new List<SelectListItem>();
            int currentYear = DateTime.Now.Year;
            List<SelectListItem> lstYear = new List<SelectListItem>();
            lstParameters.Add(new SelectListItem { Text = "All Phase Voltage", Value = "V" });
            lstParameters.Add(new SelectListItem { Text = "All Phase Current", Value = "I" });
            lstParameters.Add(new SelectListItem { Text = "Consumption", Value = "C" });

            //Assign the value to ViewBag
            ViewBag.Parameters = new SelectList(lstParameters, "Value", "Text");

        }
        public async Task<ActionResult> MonthWise()
        {


            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");

            FillMonthAndYear();
            return View("MonthWise");
        }
        public async Task<ActionResult> DayWise()
        {


            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");

            FillMonthAndYear();
            return View();
        }
        public async Task<ActionResult> HourWise()
        {

            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");
            FillParameters();
            return View();
        }
        public async Task<ActionResult> BlockWiseConsumption()
        {

            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");
            FillMonthAndYear();
            return View();
        }
        public async Task<ActionResult> HourWiseConsumption()
        {

            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");
            FillMonthAndYear();
            return View();
        }
        public async Task<ActionResult> DayWiseConsumption()
        {

            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");
            FillMonthAndYear();
            return View("DayWiseConsumption");
        }
        public async Task<ActionResult> YearWiseConsumption()
        {

            ViewBag.MeterGroup = new SelectList(await ListMeterGroup(), "Id", "GroupName");
            FillMonthAndYear();
            return View();
        }
        
    }
}


