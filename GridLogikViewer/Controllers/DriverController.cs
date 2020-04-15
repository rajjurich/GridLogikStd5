using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using GridLogik.ViewModels;
using System.Web.Mvc.Html;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DriverController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /Driver/
        public ActionResult Index()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            ViewBag.MeterModelID = new SelectList(meterModel, "ID", "ModelName");

            InstanceData test = new InstanceData();
            
            System.Reflection.PropertyInfo[] array = test.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);

            ViewBag.InstanceParameterID = new SelectList(list, "Name", "Name");
            return View();
        }

        public ActionResult AddAddressDetails()
        {
            MemoryMap_Addressdetails addressDetails = new MemoryMap_Addressdetails();
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            ViewBag.MeterModelID = new SelectList(meterModel, "ID", "ModelName");

            ViewBag.DataTypeID = new SelectList(new List<string> { "Test1", "Test2" });

            InstanceData test = new InstanceData();
            System.Reflection.PropertyInfo[] array = test.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);
            ViewBag.InstanceParameterID = new SelectList(list, "Name", "Name");
            return PartialView("_AddressDetail", addressDetails);
        }

        public ActionResult Create()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            ViewBag.MeterModelID = new SelectList(meterModel, "ID", "ModelName");

            ViewBag.DataTypeID = new SelectList(new List<string> { "Test1", "Test2" });

            InstanceData test = new InstanceData();
            System.Reflection.PropertyInfo[] array = test.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);
            ViewBag.InstanceParameterID = new SelectList(list, "Name", "Name");

            DriversViewModel driver = new DriversViewModel();
            driver.MeterModel = new MeterModel();
            driver.AddressDetails = new List<MemoryMap_Addressdetails>();
            driver.Range = new List<MemoryMap_Range>();
            return View(driver);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            return View();
        }
	}
}