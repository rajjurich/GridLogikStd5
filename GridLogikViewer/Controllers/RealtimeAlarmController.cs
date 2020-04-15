using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
using GridLogikViewer.Utilities;
using System.Reflection;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class RealtimeAlarmController : Controller
    {
        //
        // GET: /RealtimeAlarm/
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
      
        [AccessCheck(IdParamName = "RealtimeAlarm/index")]
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

            IEnumerable<RealTimeAlarmModel> Realtimeobj;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}RealtimeAlarm", _uri);

                var result = await client.GetAsync(uri);

                Realtimeobj = await result.Content.ReadAsAsync<IEnumerable<RealTimeAlarmModel>>();

            }


            return View(Realtimeobj);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RealTimeAlarmModel objRealTimeAlarmModel)
        {
            foreach(var i in objRealTimeAlarmModel.multiplemeterID)
            {
                objRealTimeAlarmModel.meterid = objRealTimeAlarmModel.meterid+Convert.ToString(i) + ",";
            }

            foreach (var j in objRealTimeAlarmModel.multipleparameters)
            {
                objRealTimeAlarmModel.parameter = objRealTimeAlarmModel.parameter+Convert.ToString(j) + ",";
            }

            objRealTimeAlarmModel.meterid=objRealTimeAlarmModel.meterid.TrimEnd(',');
            objRealTimeAlarmModel.parameter=objRealTimeAlarmModel.parameter.TrimEnd(',');
                await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                await BindDropDown();
                uri = string.Format("{0}RealtimeAlarm", _uri);

                var result = await client.PostAsJsonAsync(uri, objRealTimeAlarmModel);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    RealTimeAlarmModel mstmodel = await result.Content.ReadAsAsync<RealTimeAlarmModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "RealtimeAlarm");
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = contents;
                    return View();
                }
            }
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Meter", _uri);
                var result = await client.GetAsync(uri);
                var metercontent = await result.Content.ReadAsAsync<List<Meter>>();
                var meterids = metercontent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.MeterName
                });

                InstanceData instaceData = new InstanceData();
                System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
                List<PropertyInfo> list = array.ToList();
                list.RemoveRange(0, 5);

                List<PropertyInfo> parameterList = new List<PropertyInfo>();
                parameterList = list;
                var parame=parameterList.Select(c=>new SelectListItem
                {
                    Value=c.Name.ToString(),
                    Text=c.Name
                });



                ViewBag.parame = parame;
                ViewBag.meterids = meterids;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            List<Meter> meter = new List<Meter>();
            List<Meter> EditMeter = new List<Meter>();
            List<PropertyInfo> EditParams = new List<PropertyInfo>();
            InstanceData instance = new InstanceData();
            RealTimeAlarmModel mstmodel = await GetRealTimeData(id);
            Meter selectedMeter = new Meter();
            InstanceData instaceData = new InstanceData();
            System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);

            List<PropertyInfo> parameterList = new List<PropertyInfo>();
            parameterList = list;
            

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Meter", _uri);
                var result = await client.GetAsync(uri);
                meter = await result.Content.ReadAsAsync<List<Meter>>();
                meter.RemoveAll(item => item == null);
            }


            Meter EditSingleMeter = new Meter();
            await BindDropDown();
            
            string[] EditSingleMeterID = mstmodel.meterid.Split(',');
            if (EditSingleMeterID.Length > 0)
            {
                foreach (var item in EditSingleMeterID)
                {
                    try
                    {
                        EditSingleMeter = meter.Find(x => x.ID == Convert.ToInt32(item));

                        if (EditSingleMeter != null)
                        {
                            EditMeter.Add(EditSingleMeter);
                        }

                    }
                    catch (Exception)
                    {


                    }

                }
            }

            string[] EditSingleParameterID = mstmodel.parameter.Split(',');
            if (EditSingleParameterID.Length > 0)
            {
                foreach (var item in EditSingleParameterID)
                {
                    try
                    {
                        PropertyInfo propertyInfo = instance.GetType().GetProperty(item);
                        EditParams.Add(propertyInfo);

                    }
                    catch (Exception)
                    {


                    }
                }
            }



            List<long> lm = new List<long>();
            string[] meterstoberemove = mstmodel.meterid.Split(',');
            if (meterstoberemove.Length > 0)
            {
                foreach (var item in meterstoberemove)
                {
                    lm.Add(Convert.ToInt32(item));
                    
                    try
                    {
                        selectedMeter = meter.Find(x => x.ID == Convert.ToInt32(item));
                        
                        if (selectedMeter != null)
                        {

                            meter.Remove(selectedMeter);
                        }
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

            
                }
            }


            string[] paramtoberemove = mstmodel.parameter.Split(',');
            if (paramtoberemove.Length > 0)
            {
                foreach (var item in paramtoberemove)
                {
                    PropertyInfo propertyInfo = instance.GetType().GetProperty(item);
                    parameterList.Remove(propertyInfo);

                }
            }

            ViewBag.InstanceParameterID = new SelectList(parameterList, "Name", "Name");

            ViewBag.MeterSelectionList = new SelectList(meter, "Id", "MeterName");


            ViewBag.SelectedParameters = new SelectList(EditParams, "Name", "Name");
            ViewBag.SelectedMeters = new SelectList(EditMeter, "Id", "MeterName");

            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RealTimeAlarmModel objreal)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}RealtimeAlarm/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objreal);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    RealTimeAlarmModel mstmodel = await result.Content.ReadAsAsync<RealTimeAlarmModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "RealtimeAlarm");
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = contents;
                    return View();
                }
            }
        }



        private async Task<RealTimeAlarmModel> GetRealTimeData(int id)
        {
            RealTimeAlarmModel mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}RealtimeAlarm/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<RealTimeAlarmModel>();
            }
            return mstmodel;
        }

        public async Task<ActionResult> Delete(int id)
        {
            await BindDropDown();
            RealTimeAlarmModel mstmodel = await GetRealTimeData(id);
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, RealTimeAlarmModel objUtility)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}RealtimeAlarm/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    RealTimeAlarmModel mstmodel = await result.Content.ReadAsAsync<RealTimeAlarmModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "RealtimeAlarm");
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = contents;
                    return View();
                }
            }
        }
       
	}
}