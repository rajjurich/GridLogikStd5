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
    public class MenuController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
      
        [AccessCheck(IdParamName = "Menu/Index")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            IEnumerable<MstMenuModels> mstMenuModels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Menu", _uri);

                var result = await client.GetAsync(uri);

                mstMenuModels = await result.Content.ReadAsAsync<IEnumerable<MstMenuModels>>();

            }
            await BindDropDown();
            return View(mstMenuModels);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstMenuModels objmenu)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Menu", _uri);

                var result = await client.PostAsJsonAsync(uri, objmenu);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstMenuModels mstmodel = await result.Content.ReadAsAsync<MstMenuModels>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Menu");
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
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View("Create");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            await BindDropDown();
            MstMenuModels mstmodel = await GetMenuModel(Convert.ToInt32(id));
            return View(mstmodel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, MstMenuModels objMenu)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Menu/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objMenu);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstMenuModels mstmodel = await result.Content.ReadAsAsync<MstMenuModels>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Menu");
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


        public async Task<ActionResult> Delete(long id)
        {
            await BindDropDown();
            MstMenuModels mstmodel = await GetMenuModel(Convert.ToInt32(id));
            return View(mstmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MstMenuModels objMenu)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Menu/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstMenuModels mstmodel = await result.Content.ReadAsAsync<MstMenuModels>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Menu");
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
                uri = string.Format("{0}Module", _uri);
                var result = await client.GetAsync(uri);
                var utilityContent = await result.Content.ReadAsAsync<List<ModuleModel>>();
                var Modules = utilityContent.Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.modulename
                });
                ViewBag.Modules = Modules;
            }
        }

        private async Task<MstMenuModels> GetMenuModel(int id)
        {
            MstMenuModels mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Menu/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<MstMenuModels>();
            }
            return mstmodel;
        }
    }
}