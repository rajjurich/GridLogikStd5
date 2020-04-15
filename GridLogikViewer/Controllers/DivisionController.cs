using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using GridLogikViewer.GridLogikViewerModels;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
using GridLogikViewer.Utilities;
//using GridLogikViewer.APIModels;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DivisionController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
       
        // GET: /Division/
        [AccessCheck(IdParamName = "Division/Index")]
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
            IEnumerable<MstDivisionModel> mstdivmodels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstDivision", _uri);

                var result = await client.GetAsync(uri);

                mstdivmodels = await result.Content.ReadAsAsync<IEnumerable<MstDivisionModel>>();

            }
            await BindDropDown();
            return View(mstdivmodels);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstDivisionModel objZone)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstDivision", _uri);

                var result = await client.PostAsJsonAsync(uri, objZone);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstDivisionModel mstmodel = await result.Content.ReadAsAsync<MstDivisionModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Division");
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
            MstDivisionModel mstmodel = await GetMstDivisionModel(id);
            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MstDivisionModel objZone)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstDivision/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objZone);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstDivisionModel mstmodel = await result.Content.ReadAsAsync<MstDivisionModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Division");
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
            MstDivisionModel mstmodel = await GetMstDivisionModel(id);
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MstDivisionModel objZone)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstDivision/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstDivisionModel mstmodel = await result.Content.ReadAsAsync<MstDivisionModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Division");
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

                uri = string.Format("{0}MstZone", _uri);
                var result1 = await client.GetAsync(uri);
                var mstzoneContent = await result1.Content.ReadAsAsync<List<MstZoneModel>>();
                var mstzones = mstzoneContent.Select(c => new SelectListItem
                {
                    Value = c.znrecid.ToString(),
                    Text = c.znname
                });

                uri = string.Format("{0}MstRegion", _uri);
                var resultforregion = await client.GetAsync(uri);
                var mstregioncontent = await resultforregion.Content.ReadAsAsync<List<MstRegionModel>>();
                var msrregion = mstregioncontent.Select(c => new SelectListItem
                {
                    Value = c.RgnRecID.ToString(),
                    Text = c.RgnName
                });


                ViewBag.msrregion = msrregion;
                ViewBag.mstzones = mstzones;
                ViewBag.Utilities = Utilities;
            }
        }

        private async Task<MstDivisionModel> GetMstDivisionModel(int id)
        {
            MstDivisionModel mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstDivision/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<MstDivisionModel>();
            }
            return mstmodel;
        }
    }
}
