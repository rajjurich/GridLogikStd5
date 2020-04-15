using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Reflection;
using System.Data.Entity;
using GridLogikViewer.Filters;
using System.Threading.Tasks;
using System.Net.Http;
namespace GridLogikViewer.Controllers
{
      [Authorize]
    public class AlarmController : Controller
    {
        
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
      
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        #region this is list for DataType dropdown
        private IEnumerable<SelectListItem> Condition1List()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "<=", Text = "<=" },
                new { ID = ">=", Text = ">=" },
                new { ID = "inbetwn", Text = "inbetwn" },
            }, "ID", "Text");
            return list;
        }


        private IEnumerable<SelectListItem> Condition2List()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "AND", Text = "AND" },
                new { ID = "OR", Text = "OR" },
                new { ID = "END", Text = "END" },
            }, "ID", "Text");
            return list;
        }

        private List<Meter> MeterList()
        {
            List<Meter> meter = new List<Meter>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterAPI");
                meter = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
            return meter;
        }


        private List<PropertyInfo> InstaceDataList()
        {
            InstanceData instaceData = new InstanceData();
            System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);
            return list;
        }

        #endregion
        //private Tuple<string, bool> AlarmList(FormCollection form)
        //{
        //    int counter = 0;
        // //   int numerOfExistingRecords = form.Count;
        //    //if (numerOfExistingRecords == 0)
        //        //Gives number of rows added while adding
        //        counter = ((form.Count) - 6) / 4;
        //   // else
        //        //gives number of row excluding existion rows while editing
        //     //   counter = ((form.Count - 2) - (numerOfExistingRecords * 7)) / 5;
        //    bool errorFlag = false;
        //    string parameter = "";
        //   // List<string> parameterList = new List<string>();
            
        //    // var list = new SelectList(new[] 
        //    //{
        //    //    new { ID = "Integer", Text = "Integer" },
        //    //    new { ID = "String", Text = "String" },
        //    //    new { ID = "Date time", Text = "Date time" },
        //    //}, "ID", "Text");
        //    Array test = new Array[3];
        //    //putting all rows into the list
        //    for (int loop = 1; loop <= counter; loop++)
        //    {
               
        //        Alarm model = new Alarm();
        //        model.Id = Convert.ToInt32(Request.Form["metermodel.modelname"]);
        //        if (Request.Form["name" + (loop)].Count() != 0)//to check all rows are filled or not
        //           parameter = parameter + Request.Form["name" + (loop)];
        //        else
        //        {
        //            errorFlag = true;
        //            break;
        //        }
        //        if (Request.Form["parameter" + (loop)].Count() != 0)
        //              parameter = parameter +Request.Form["parameter" + (loop)];
        //        else
        //        {
        //            errorFlag = true;
        //            break;
        //        }
        //        if (Request.Form["datatype" + (loop)].Count() != 0)
        //            parameter = parameter + Request.Form["datatype" + (loop)];
        //        else
        //        {
        //            errorFlag = true;
        //            break;
        //        }
        //        if (Request.Form["instancedata" + (loop)].Count() != 0)
        //            parameter = parameter + Request.Form["instancedata" + (loop)];
        //        else
        //        {
        //            errorFlag = true;
        //            break;
        //        }                
        //      //  parameterList.Add(parameter);
        //    }
        //    return new Tuple<string, bool>(parameter, errorFlag);
        //}
        
         //GET: /Alarm/
          [AccessCheck(IdParamName = "Alarm/Index")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";

            IEnumerable<Alarm> alarmmodels;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Alarm", _uri);

                var result = await client.GetAsync(uri);

                alarmmodels = await result.Content.ReadAsAsync<IEnumerable<Alarm>>();
            }

            //List<Alarm> alarm = new List<Alarm>();
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "AlarmAPI");
            //    alarm = JsonConvert.DeserializeObject<List<Alarm>>(s);
            //}
            return View(alarmmodels);
        }

        //
        // GET: /Alarm/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                List<Meter> meter = new List<Meter>();
                Alarm alarm = new Alarm();

                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "AlarmAPI" + "/" + id);
                    string p = client.DownloadString(url + "MeterAPI");
                    if (s != null)
                    {
                        alarm = JsonConvert.DeserializeObject<Alarm>(s);
                        meter = JsonConvert.DeserializeObject<List<Meter>>(p);
                        ViewBag.MeterID = new SelectList(meter, "Id", "MeterName", alarm.MeterID);
                        InstanceData test = new InstanceData();
                        System.Reflection.PropertyInfo[] array = test.GetType().GetProperties();
                        List<PropertyInfo> list = array.ToList();
                        list.RemoveRange(0, 5);
                        ViewBag.InstanceParameterID = new SelectList(list, "Name", "Name");
                        ViewBag.Condition1List = Condition1List();
                        ViewBag.Condition2List = Condition2List();
                        alarm.Id = id;
                    }
                    else
                    {
                        return View(new Alarm());
                    }
                }
                return View(alarm);
            }
            catch 
            {
                return View(new Alarm());
            }
        }

        //
        // GET: /Alarm/Create
        public ActionResult Create()
        {

            List<Meter> meter = new List<Meter>();
          
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "MeterAPI");
              

                meter = JsonConvert.DeserializeObject<List<Meter>>(s);
            }
           
           ViewBag.MeterID = new SelectList(meter, "Id", "MeterName");
            InstanceData test = new InstanceData();
            System.Reflection.PropertyInfo[] array = test.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);
            ViewBag.InstanceParameterID = new SelectList(list, "Name", "Name");
            ViewBag.Condition1List = Condition1List();
            ViewBag.Condition2List = Condition2List();
            return View(new Alarm());

        }

        //
        // POST: /Alarm/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alarm alarm, FormCollection form)
        {
            try
            {
                List<AlarmCondition> alarmconditionlist = new List<AlarmCondition>();
                Tuple<List<AlarmCondition>, bool> alarmconditionDetails = AddressList(form, alarmconditionlist);

                if (alarmconditionDetails.Item2)
                {
                    var jsonData = new
                    {
                        error = true,
                        message = "Please fill all details",
                    };
                    return Json(jsonData);
                }
                alarm.AlarmConditions = alarmconditionDetails.Item1.ToList();
                
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "AlarmAPI", JsonConvert.SerializeObject(alarm));
                }
                var jsonDataSuceess = new
                {
                    success = true,
                    message = "Data saved successfully",
                };
                return Json(jsonDataSuceess);
            }
            catch
            {
                return RedirectToAction("alarm");
            }
        }

        private Tuple<List<AlarmCondition>, bool> AddressList(FormCollection form, List<AlarmCondition> alarmconditionlist)
        {
            int counter = 0;
            int numerOfExistingRecords = alarmconditionlist.Count;
            if (numerOfExistingRecords == 0)
                //Gives number of rows added while adding
                counter = ((form.Count) - 5) / 4;
            else
                //gives number of row excluding existion rows while editing
                counter = ((form.Count - 2) - (numerOfExistingRecords * 5)) / 4;
            bool errorFlag = false;
            foreach(var alarm in alarmconditionlist)
            {
                if (alarm.Parameter == null)
                    numerOfExistingRecords--;
            }
            //putting all rows into the list
            for (int loop = 1; loop <= counter; loop++)
            {
                AlarmCondition model = new AlarmCondition();
                model.AlarmId = Convert.ToInt32(Request.Form["Id"]);
                if (Request.Form["parameter" + (loop + numerOfExistingRecords)].Count() != 0)//to check all rows are filled or not
                    model.Parameter = Request.Form["parameter" + (loop + numerOfExistingRecords)];
                else
                {
                    errorFlag = true;
                    break;
                }
                if (Request.Form["operator" + (loop + numerOfExistingRecords)].Count() != 0)
                    model.Operator = Request.Form["operator" + (loop + numerOfExistingRecords)];
                else
                {
                    errorFlag = true;
                    break;
                }
                if (Request.Form["value" + (loop + numerOfExistingRecords)].Count() != 0)
                    model.Value =Convert.ToString(Request.Form["value" + (loop + numerOfExistingRecords)]);
                    
                else
                {
                    errorFlag = true;
                    break;
                }
              
                alarmconditionlist.Add(model);
            }
            return new Tuple<List<AlarmCondition>, bool>(alarmconditionlist, errorFlag);
        }
      
        
        // GET: /Alarm/Edit/5

        public ActionResult Edit(int id)
        {
            
            try
            {
                 List<Meter> meter = new List<Meter>();
                Alarm alarm = new Alarm();
               
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "AlarmAPI" + "/" + id);
                    string p = client.DownloadString(url + "MeterAPI");
                    if (s != null)
                    {
                        alarm = JsonConvert.DeserializeObject<Alarm>(s);
                        meter = JsonConvert.DeserializeObject<List<Meter>>(p);
                        ViewBag.MeterID = new SelectList(meter, "Id", "MeterName", alarm.MeterID);
                        InstanceData test = new InstanceData();
                        System.Reflection.PropertyInfo[] array = test.GetType().GetProperties();
                        List<PropertyInfo> list = array.ToList();
                        list.RemoveRange(0, 5);
                        ViewBag.InstanceParameterID = new SelectList(list, "Name", "Name");
                        ViewBag.Condition1List = Condition1List();
                        ViewBag.Condition2List = Condition2List();
                        alarm.Id = id;
                    }
                    else
                    {
                        return View(new Alarm());
                    }
                }
                return View(alarm);
            }
            catch
            {
                return View(new Alarm());
            }
        }
         //POST: /Alarm/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Alarm alarm, FormCollection form)
        {
            try
            {
                // TODO: Add update logic here
                Tuple<List<AlarmCondition>, bool> alarmconditionDetails = AddressList(form, alarm.AlarmConditions);

                if (alarmconditionDetails.Item2)
                {
                    var jsonData = new
                    {
                        error = true,
                        message = "Please fill all details",
                    };
                    return Json(jsonData);
                }
                alarm.AlarmConditions = alarmconditionDetails.Item1.ToList();

                if (!ModelState.IsValid)
                {
                    return View(alarm);
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "AlarmAPI", JsonConvert.SerializeObject(alarm));
                }

                var jsonDataSuceess = new
                {
                    success = true,
                    message = "Data edited successfully",
                };
                return Json(jsonDataSuceess);
               // return RedirectToAction("Index", "Alarm");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Alarm/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Alarm alarm = new Alarm();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "AlarmAPI" + "/" + id);
                    if (s != null)
                    {
                        alarm = JsonConvert.DeserializeObject<Alarm>(s);
                    }
                    else
                    {
                        return View(new Alarm());
                    }
                }
                return View(alarm);
            }
            catch
            {
                return View(new Alarm());
            }
        }

        //
        // POST: /Alarm/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Alarm alarm)
        {
            try
            {
                // TODO: Add delete logic here
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "AlarmAPI" + "/" + id, "DELETE", JsonConvert.SerializeObject(alarm));
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(alarm);
            }
        }
	}
}



