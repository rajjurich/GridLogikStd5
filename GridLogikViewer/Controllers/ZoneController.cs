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
    public class ZoneController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
                
        //
        [AccessCheck(IdParamName = "Zone/Index")]
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
            IEnumerable<MstZoneModel> mstzonemodels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstZone", _uri);

                var result = await client.GetAsync(uri);

                mstzonemodels = await result.Content.ReadAsAsync<IEnumerable<MstZoneModel>>();

            }
            await BindDropDown();
            return View(mstzonemodels);
            
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstZoneModel objZone)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstZone", _uri);

                var result = await client.PostAsJsonAsync(uri, objZone);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstZoneModel mstmodel = await result.Content.ReadAsAsync<MstZoneModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Zone");
                }
                else
                {
                    await BindDropDown();
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
            await BindDropDown();
            MstZoneModel mstmodel = await GetMstZoneModel(id);
            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,MstZoneModel objZone)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstZone/{1}", _uri,id);

                var result = await client.PutAsJsonAsync(uri, objZone);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstZoneModel mstmodel = await result.Content.ReadAsAsync<MstZoneModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Zone");
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
            await BindDropDown();
            MstZoneModel mstmodel = await GetMstZoneModel(id);
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id,MstZoneModel objZone)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstZone/{1}", _uri,id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstZoneModel mstmodel = await result.Content.ReadAsAsync<MstZoneModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Zone");
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
                uri = string.Format("{0}MstUtility", _uri);
                var result = await client.GetAsync(uri);
                var utilityContent = await result.Content.ReadAsAsync<List<MstUtility>>();
                var Utilities = utilityContent.Select(c => new SelectListItem
                {
                    Value = c.utilrecid.ToString(),
                    Text = c.utilname
                });
                ViewBag.Utilities = Utilities;
            }
        }

        private async Task<MstZoneModel> GetMstZoneModel(int id)
        {
            MstZoneModel mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstZone/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<MstZoneModel>();
            }
            return mstmodel;
        }


	}
}