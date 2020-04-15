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

    [Authorize]
    public class UtilityController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
     
        // GET: /Utility/
        [AccessCheck(IdParamName = "Utility/Index")]
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
            IEnumerable<MstUtility> mstutilityModels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstUtility", _uri);

                var result = await client.GetAsync(uri);

                mstutilityModels = await result.Content.ReadAsAsync<IEnumerable<MstUtility>>();

            }

            
            return View(mstutilityModels);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstUtility objmstUtility)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstUtility", _uri);

                var result = await client.PostAsJsonAsync(uri, objmstUtility);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstUtility mstmodel = await result.Content.ReadAsAsync<MstUtility>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Utility");
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
            MstUtility mstmodel = await GetUtilityModel(id);
            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,MstUtility objUtility)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstUtility/{1}", _uri,id);

                var result = await client.PutAsJsonAsync(uri, objUtility);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstUtility mstmodel = await result.Content.ReadAsAsync<MstUtility>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Utility");
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
            MstUtility mstmodel = await GetUtilityModel(id);
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id,MstUtility objUtility)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstUtility/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstUtility mstmodel = await result.Content.ReadAsAsync<MstUtility>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Utility");
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

        private async Task<MstUtility> GetUtilityModel(int id)
        {
            MstUtility mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MstUtility/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<MstUtility>();
            }
            return mstmodel;
        }

     

    }
