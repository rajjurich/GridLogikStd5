using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using Newtonsoft.Json;
using GridLogikViewer.Filters;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class InputDataController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        private List<PropertyInfo> InstaceDataList()
        {
            InstanceData instaceData = new InstanceData();
            System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 9);
            return list;
        }

        private IEnumerable<SelectListItem> OperatorList()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "+", Text = "+" },
                new { ID = "-", Text = "-" },
                new { ID = "*", Text = "*" },
                 new { ID = "%", Text = "%" },
            }, "ID", "Text");
            return list;
        }
        // GET: InputData
        [AccessCheck(IdParamName = "InputData/Index")]
        public ActionResult Index()
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            InputData model = new InputData();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "InputDataAPI");
                model.InputDataList = JsonConvert.DeserializeObject<List<InputData>>(s);
            }

            List<Meter> meter = new List<Meter>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterAPI");
                meter = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
            ViewBag.InputDataList = new SelectList(model.InputDataList, "IpNo", "ParaName");
            return View("InputData", model);
        }

        public ActionResult AddInputData()
        {
            InputData model = new InputData();
            return View("_AddInputData", model);
        }

        public ActionResult AddRealTimeData()
        {
            InputData model = new InputData();
            List<Meter> meter = new List<Meter>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "InputDataAPI");
                model.InputDataList = JsonConvert.DeserializeObject<List<InputData>>(s);
            }
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterAPI");
                meter = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
            ViewBag.MeterList = new SelectList(meter, "Id", "MeterName");
            ViewBag.ParameterList = new SelectList(InstaceDataList(), "Name", "Name");
            ViewBag.InputDataList = new SelectList(model.InputDataList, "IpNo", "ParaName");
            ViewBag.OperatorList = new SelectList(OperatorList(), "Value", "Text");
            return View("_AddRealTimeData", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(InputData model)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                string s = client.UploadString(url + "InputDataAPI", JsonConvert.SerializeObject(model));
                TempData["Success"] = "Record Added Successfully!";
            }           
            return RedirectToAction("Index");
        }

        public ActionResult DeleteDetails(int id)
        {
            InputData model = new InputData();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "InputDataAPI" + "/" + id);
                model = JsonConvert.DeserializeObject<InputData>(s);
            }
            return View("Delete", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInputData(InputData model)
        {
            try
            {
                InputData inputData = new InputData();
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "InputDataAPI" + "/" + model.IpNo, "DELETE", JsonConvert.SerializeObject(inputData));
                    if (s != null)
                    {
                        inputData = JsonConvert.DeserializeObject<InputData>(s);                        
                        TempData["Success"] = "Data Deleted successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                //return RedirectToAction("Index");
            }
            catch
            {
                return View(new InputData());
            }
        }
    }
}