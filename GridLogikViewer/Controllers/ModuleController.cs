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
    public class ModuleController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;     

        //
        // GET: /Module/
        [AccessCheck(IdParamName = "Module/Index")]
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

            IEnumerable<ModuleModel> mstMenuModels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Module", _uri);

                var result = await client.GetAsync(uri);

                mstMenuModels = await result.Content.ReadAsAsync<IEnumerable<ModuleModel>>();

            }
            
            return View(mstMenuModels);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ModuleModel objmenu)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Module", _uri);

                var result = await client.PostAsJsonAsync(uri, objmenu);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ModuleModel mstmodel = await result.Content.ReadAsAsync<ModuleModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Module");
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
        public async Task<ActionResult> Edit(long id)
        {

            ModuleModel mstmodel = await GetModule(Convert.ToInt32(id));
            return View(mstmodel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ModuleModel objMenu)
        {
            
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Module/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objMenu);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ModuleModel mstmodel = await result.Content.ReadAsAsync<ModuleModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Module");
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
            ModuleModel mstmodel = await GetModule(Convert.ToInt32(id));
            return View(mstmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ModuleModel objMenu)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Module/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ModuleModel mstmodel = await result.Content.ReadAsAsync<ModuleModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Module");
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

        private async Task<ModuleModel> GetModule(int id)
        {
            ModuleModel mstmodel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Module/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstmodel = await result.Content.ReadAsAsync<ModuleModel>();
            }
            return mstmodel;
        }
	}
}