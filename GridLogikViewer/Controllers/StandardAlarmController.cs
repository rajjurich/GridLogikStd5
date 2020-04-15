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
     [Authorize]
    public class StandardAlarmController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        
       
        [AccessCheck(IdParamName = "StandardAlarm/index")]
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
            IEnumerable<StandardAlarmModel> standardalarmmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}StandardAlaram", _uri);

                var result = await client.GetAsync(uri);

                standardalarmmodel = await result.Content.ReadAsAsync<IEnumerable<StandardAlarmModel>>();

            }

            await BindDropDown();
            TempData["Status"] = "Success";
            return View(standardalarmmodel);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View("Create");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StandardAlarmModel objstandaralaram)
        {
            using (HttpClient client = new HttpClient())
            {
                objstandaralaram.status = 1;
                uri = string.Format("{0}StandardAlaram", _uri);

                for (int i = 1; i < objstandaralaram.multiplemeterID.Length; i++)
                {
                    objstandaralaram.meterid = Convert.ToInt32(objstandaralaram.multiplemeterID[i]);
                    var result1 = await client.PostAsJsonAsync(uri, objstandaralaram);
                    var contents1 = await result1.Content.ReadAsStringAsync();
                }
                    objstandaralaram.meterid = Convert.ToInt32(objstandaralaram.multiplemeterID[0]);
                    var result = await client.PostAsJsonAsync(uri, objstandaralaram);
                    var contents = await result.Content.ReadAsStringAsync();
                
                
                if (result.IsSuccessStatusCode)
                {
                    StandardAlarmModel mstmodel = await result.Content.ReadAsAsync<StandardAlarmModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "StandardAlarm");
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

                ViewBag.meterids = meterids;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            await BindDropDown();
            StandardAlarmModel mstmodel = await GetStandard(id);
            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, StandardAlarmModel objstandaralaram)
        {
            using (HttpClient client = new HttpClient())
            {
                objstandaralaram.status = 1;
                uri = string.Format("{0}StandardAlaram/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objstandaralaram);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    StandardAlarmModel mstmodel = await result.Content.ReadAsAsync<StandardAlarmModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "StandardAlarm");
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
        private async Task<StandardAlarmModel> GetStandard(int id)
        {
            StandardAlarmModel mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}StandardAlaram/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<StandardAlarmModel>();
            }
            return mstmodel;
        }


        public async Task<ActionResult> Delete(int id)
        {
            await BindDropDown();
            StandardAlarmModel mstmodel = await GetStandard(id);
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, StandardAlarmModel objUtility)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}StandardAlaram/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    StandardAlarmModel mstmodel = await result.Content.ReadAsAsync<StandardAlarmModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "StandardAlarm");
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