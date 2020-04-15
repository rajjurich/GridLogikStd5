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

namespace GridLogikViewer.Controllers
{
    public class AlarmType
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
     [Authorize]
    public class ActionTypeController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
      
        // GET: /ActionType/
        [AccessCheck(IdParamName = "ActionType/Index")]
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
            IEnumerable<ActionTypeModel> ActionTypeModelModels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}ActionType", _uri);

                var result = await client.GetAsync(uri);

                ActionTypeModelModels = await result.Content.ReadAsAsync<IEnumerable<ActionTypeModel>>();

            }


            return View(ActionTypeModelModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ActionTypeModel objActionTypeModel)
        {
            using (HttpClient client = new HttpClient())
            {
                foreach (var m in objActionTypeModel.mobilenos)
                {
                    if (objActionTypeModel.mobileno == null)
                    {
                        objActionTypeModel.mobileno = m;
                    }
                    else
                    {
                        objActionTypeModel.mobileno = objActionTypeModel.mobileno + "," + m;
                    }
                }

                foreach (var e in objActionTypeModel.emailaddresses)
                {
                    if (objActionTypeModel.emailaddress == null)
                    {
                        objActionTypeModel.emailaddress = e;
                    }
                    else
                    {
                        objActionTypeModel.emailaddress = objActionTypeModel.emailaddress + "," + e;
                    }
                }
                uri = string.Format("{0}ActionType", _uri);

                var result = await client.PostAsJsonAsync(uri, objActionTypeModel);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ActionTypeModel ActionModel = await result.Content.ReadAsAsync<ActionTypeModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "ActionType");
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

        public async Task<ActionResult> GetAlarmByAlarmType(string AlarmType)
        {
            //Type typeParameterType = typeof(T);
            //List<T> Alarms = new List<T>();
            using (HttpClient client = new HttpClient())
            {
                if (AlarmType == "S")
                {
                    uri = string.Format("{0}StandardAlaram", _uri);
                    var resultstand = await client.GetAsync(uri);
                    var standcontent = await resultstand.Content.ReadAsAsync<List<StandardAlarmModel>>();
                    var standaralarm = standcontent.Select(c => new SelectListItem
                    {
                        Value = c.id.ToString(),
                        Text = c.alarmname
                    });

                    ViewBag.AlarmIDList = standaralarm;
                    return Json(standaralarm, JsonRequestBehavior.AllowGet);
                }
                else if (AlarmType == "R")
                {
                    uri = string.Format("{0}RealtimeAlarm", _uri);
                    var resultreal = await client.GetAsync(uri);
                    var realalarmcontent = await resultreal.Content.ReadAsAsync<List<RealTimeAlarmModel>>();
                    var realalarm = realalarmcontent.Select(c => new SelectListItem
                    {
                        Value = c.id.ToString(),
                        Text = c.alarmname
                    });
                    ViewBag.AlarmIDList = realalarm;
                    return Json(realalarm, JsonRequestBehavior.AllowGet);
                }
                else if (AlarmType == "M")
                {
                    uri = string.Format("{0}CommunicationDetailAPI", _uri);
                    var resultreal = await client.GetAsync(uri);
                    var realalarmcontent = await resultreal.Content.ReadAsAsync<List<CommunicationDetail>>();
                    var Communicationty = realalarmcontent.Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.communicationtypename
                    });
                    ViewBag.AlarmIDList = Communicationty;
                    return Json(Communicationty, JsonRequestBehavior.AllowGet);
                }
                else if (AlarmType == "T")
                {
                    uri = string.Format("{0}Meter", _uri);
                    var resultMeter = await client.GetAsync(uri);
                    var Metercontent = await resultMeter.Content.ReadAsAsync<List<Meter>>();
                    var Meterty = Metercontent.Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.MeterName
                    });
                    ViewBag.AlarmIDList = Meterty;
                    return Json(Meterty, JsonRequestBehavior.AllowGet);
                }
            }
            
           return Json("No Records found", JsonRequestBehavior.AllowGet);

        }


        private async Task<ActionTypeModel> GetActionType(int id)
        {
            ActionTypeModel ActionModel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}ActionType/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                ActionModel = await result.Content.ReadAsAsync<ActionTypeModel>();
            }
            return ActionModel;
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            ActionTypeModel ActionModel = await GetActionType(id);
            if (ActionModel.mobileno != "")
            {
                List<string> MobileList = new List<string>();
                string[] mobilenoitems = ActionModel.mobileno.Split(',');
                if (mobilenoitems.Length > 0)
                {
                    foreach (var item in mobilenoitems)
                    {
                        if (item != "")
                        {
                            MobileList.Add(item);
                        }
                    }
                }

                ViewBag.MobileNos = new MultiSelectList(MobileList);
            }
            if (ActionModel.emailaddress != "")
            {
                List<string> EmailList = new List<string>();
                string[] emailitems = ActionModel.emailaddress.Split(',');
                if (emailitems.Length > 0)
                {
                    foreach (var item in emailitems)
                    {
                        if (item != "")
                        {
                            EmailList.Add(item);
                        }
                    }
                }

                ViewBag.EmailIds = new MultiSelectList(EmailList);
            }
            await GetAlarmByAlarmType(Convert.ToString(ActionModel.alarmtype));
            return View(ActionModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ActionTypeModel objActionModel)
        {
            using (HttpClient client = new HttpClient())
            {
                foreach (var m in objActionModel.mobilenos)
                {
                    if (objActionModel.mobileno == null)
                    {
                        objActionModel.mobileno = m;
                    }
                    else
                    {
                        objActionModel.mobileno = objActionModel.mobileno + "," + m;
                    }
                }

                foreach (var e in objActionModel.emailaddresses)
                {
                    if (objActionModel.emailaddress == null)
                    {
                        objActionModel.emailaddress = e;
                    }
                    else
                    {
                        objActionModel.emailaddress = objActionModel.emailaddress + "," + e;
                    }
                }
                uri = string.Format("{0}ActionType/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objActionModel);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ActionTypeModel ActionModel = await result.Content.ReadAsAsync<ActionTypeModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "ActionType");
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

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ActionTypeModel ActionModel = await GetActionType(id);
            
            if (ActionModel.mobileno != "")
            {
                List<string> MobileList = new List<string>();
                string[] mobilenoitems = ActionModel.mobileno.Split(',');
                if (mobilenoitems.Length > 0)
                {
                    foreach (var item in mobilenoitems)
                    {
                        if (item != "")
                        {
                            MobileList.Add(item);
                        }
                    }
                }

                ViewBag.MobileNos = new MultiSelectList(MobileList);
            }
            if (ActionModel.emailaddress != "")
            {
                List<string> EmailList = new List<string>();
                string[] emailitems = ActionModel.emailaddress.Split(',');
                if (emailitems.Length > 0)
                {
                    foreach (var item in emailitems)
                    {
                        if (item != "")
                        {
                            EmailList.Add(item);
                        }
                    }
                }

                ViewBag.EmailIds = new MultiSelectList(EmailList);
            }
            await GetAlarmByAlarmType(Convert.ToString(ActionModel.alarmtype));
            return View(ActionModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ActionTypeModel objUtility)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}ActionType/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ActionTypeModel objActionModel = await result.Content.ReadAsAsync<ActionTypeModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "ActionType");
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