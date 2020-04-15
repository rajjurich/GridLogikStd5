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
    public class MeterTypeController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;       
        //
        // GET: /MeterType/
        [AccessCheck(IdParamName = "MeterType/index")]
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
            IEnumerable<MeterType> metertypemodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterType", _uri);

                var result = await client.GetAsync(uri);

                metertypemodel = await result.Content.ReadAsAsync<IEnumerable<MeterType>>();

            }


            return View(metertypemodel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeterType objmstMeterType)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterType", _uri);

                var result = await client.PostAsJsonAsync(uri, objmstMeterType);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterType mstmodel = await result.Content.ReadAsAsync<MeterType>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "MeterType");
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
        public async Task<ActionResult> Edit(int id)
        {
            MeterType mstmodel = await GetMeterTypeModel(id);
            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MeterType objMeterType)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterType/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objMeterType);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterType mstmodel = await result.Content.ReadAsAsync<MeterType>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "MeterType");
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


        public async Task<ActionResult> Delete(int id)
        {
            MeterType mstmodel = await GetMeterTypeModel(id);
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MeterType objMeterType)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterType/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterType mstmodel = await result.Content.ReadAsAsync<MeterType>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "MeterType");
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

        private async Task<MeterType> GetMeterTypeModel(int id)
        {
            MeterType mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterType/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<MeterType>();
            }
            return mstmodel;
        }
       
       
    }
}
