using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GridLogikViewer.GridLogikViewerModels;
using GridLogikViewer.Filters;
using System.Threading.Tasks;
using System.Net.Http;
using GridLogikViewer.Utilities;


namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class OpcShortNameController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;

        //
        // GET: /OpcShortName/
        //  [AccessCheck(IdParamName = "OpcShortName/Index")]
        [AccessCheck(IdParamName = "OpcShortName/Index")]
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


             IEnumerable<clsOpcname> clsdata;
             using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName", url);
                var result = await client.GetAsync(uri);
                clsdata = await result.Content.ReadAsAsync<IEnumerable<clsOpcname>>();
            }
            return View(clsdata);

        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(clsOpcname objZone)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName", url);

                var result = await client.PostAsJsonAsync(uri, objZone);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    clsOpcname opcmetermodel = await result.Content.ReadAsAsync<clsOpcname>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "OpcShortName");
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
            await BindDropDownWithParam(id);
            clsOpcname clsOpcname = await GetOpcModel(id);
            return View(clsOpcname);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, clsOpcname objZone)
        {
            await BindDropDownWithParam(id);
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName/{1}", url, id);

                var result = await client.PutAsJsonAsync(uri, objZone);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    clsOpcname clsOpcname = await result.Content.ReadAsAsync<clsOpcname>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "OpcShortName");
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
            await BindDropDownWithParam(id);
            clsOpcname clsOpcname = await GetOpcModel(id);
            return View(clsOpcname);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, clsOpcname objZone)
        {
            await BindDropDownWithParam(id);
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName/{1}", url, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    clsOpcname clsOpcname = await result.Content.ReadAsAsync<clsOpcname>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "OpcShortName");
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


        private async Task<clsOpcname> GetOpcModel(int id)
        {
            clsOpcname opcmetermodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName/{1}", url, id);

                var result = await client.GetAsync(uri);

                opcmetermodel = await result.Content.ReadAsAsync<clsOpcname>();
            }
            return opcmetermodel;
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName/GetMeterOPC", url);
                var result = await client.GetAsync(uri);
                var utilityContent = await result.Content.ReadAsAsync<List<Meter>>();
                var meters = utilityContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.MeterName
                });
                ViewBag.meters = meters;
            }
        }


        private async Task BindDropDownWithParam(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OpcShortName/GetMeterOPCID/{1}", url, id);
                var result = await client.GetAsync(uri);
                var utilityContent = await result.Content.ReadAsAsync<List<Meter>>();
                var metersparam = utilityContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.MeterName
                });
                ViewBag.metersparam = metersparam;
            }
        }
    }
}