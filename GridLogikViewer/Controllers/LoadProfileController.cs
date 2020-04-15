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
    [Authorize]
    public class LoadProfileController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /LoadProfile/
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
        public async Task<ActionResult> Index()
        {
            List<MeterGroup> MeterGroup = await ListMeterGroup();
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
            //    MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}

            ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");

            FillMonthAndYear();
            FillParameters();
            return View();
        }

        private void FillMonthAndYear()
        {
            //Populate month data in controller
            List<SelectListItem> lstMonth = new List<SelectListItem>();
            int currentYear = DateTime.Now.Year;
            int YearFrom = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["YearSelection"]);
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

        private void FillParameters()
        {
            //Populate Parameters in controller
            List<SelectListItem> lstParameters = new List<SelectListItem>();
            int currentYear = DateTime.Now.Year;
            List<SelectListItem> lstYear = new List<SelectListItem>();
            lstParameters.Add(new SelectListItem { Text = "All Phase Voltage", Value = "V" });
            lstParameters.Add(new SelectListItem { Text = "All Phase Current", Value = "I" });
            lstParameters.Add(new SelectListItem { Text = "kwh", Value = "kwh" });
            lstParameters.Add(new SelectListItem { Text = "kw", Value = "kw" });
            lstParameters.Add(new SelectListItem { Text = "kvarh", Value = "kvarh" });
            lstParameters.Add(new SelectListItem { Text = "pf", Value = "pf" });
            lstParameters.Add(new SelectListItem { Text = "kva", Value = "kva" });

            //Assign the value to ViewBag
            ViewBag.Parameters = new SelectList(lstParameters, "Value", "Text");

        }

        public async Task<ActionResult> CompareLoadProfile()
        {
            List<MeterGroup> MeterGroup = await ListMeterGroup();
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
            //    MeterGroup = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}

            ViewBag.MeterGroup = new SelectList(MeterGroup, "Id", "GroupName");

            FillMonthAndYear();
            FillCompareParameters();
            return View();
        }
        private void FillCompareParameters()
        {
            //Populate Parameters in controller
            List<SelectListItem> lstParameters = new List<SelectListItem>();
            int currentYear = DateTime.Now.Year;
            List<SelectListItem> lstYear = new List<SelectListItem>();
            lstParameters.Add(new SelectListItem { Text = "Phase 1 Voltage", Value = "vrn" });
            lstParameters.Add(new SelectListItem { Text = "Phase 2 Voltage", Value = "vyn" });
            lstParameters.Add(new SelectListItem { Text = "Phase 3 Voltage", Value = "vbn" });
            lstParameters.Add(new SelectListItem { Text = "Phase 1 Current", Value = "ir" });
            lstParameters.Add(new SelectListItem { Text = "Phase 2 Current", Value = "iy" });
            lstParameters.Add(new SelectListItem { Text = "Phase 3 Current", Value = "ib" });
            lstParameters.Add(new SelectListItem { Text = "kwh_import", Value = "kwh_import" });
            lstParameters.Add(new SelectListItem { Text = "kwh_export", Value = "kwh_export" });
            lstParameters.Add(new SelectListItem { Text = "kw", Value = "kw" });
            //Assign the value to ViewBag
            ViewBag.Parameters = new SelectList(lstParameters, "Value", "Text");

        }
    }
}