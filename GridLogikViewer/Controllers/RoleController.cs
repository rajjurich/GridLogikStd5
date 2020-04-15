using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Configuration;
using GridLogikViewer.Utilities;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        [AccessCheck(IdParamName = "Role/Index")]
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

            IEnumerable<MstRole> meters;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role", _uri);

                var result = await client.GetAsync(uri);

                meters = await result.Content.ReadAsAsync<IEnumerable<MstRole>>();
            }

            return View(meters);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstRole MstRole)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role", _uri);

                var result = await client.PostAsJsonAsync(uri, MstRole);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterModel metermodel = await result.Content.ReadAsAsync<MeterModel>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index");
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
            MstRole MstRole = await GetMstRole(id);


            return View(MstRole);

        }
        private async Task<MstRole> GetMstRole(int id)
        {
            MstRole MstRole;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                MstRole = await result.Content.ReadAsAsync<MstRole>();
            }
            return MstRole;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MstRole MstRole)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, MstRole);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstRole mstRole = await result.Content.ReadAsAsync<MstRole>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index");
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

        [AccessCheck(IdParamName = "Role/Index")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";

            MstRole MstRole = await GetMstRole(id);


            return View(MstRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MstRole objRole)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstRole mstRole = await result.Content.ReadAsAsync<MstRole>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = contents;
                    return View(objRole);
                }
            }
        }
    }
}
