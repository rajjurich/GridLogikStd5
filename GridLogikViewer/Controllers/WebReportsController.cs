using GridLogik.ViewModels;
using GridLogikViewer.Filters;
using GridLogikViewer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class WebReportsController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /WebReports/
        [AccessCheck(IdParamName = "WebReports/index")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            var data = ViewData.Model as MstRoleMenuAccess;
            if (data != null)
            {
                if (data.rmacreateaccess == 0)
                    ViewBag.CreateAccess = "False";
                if (data.rmadeleteaccess == 0)
                    ViewBag.DeleteAccess = "False";
                if (data.rmaupdateaccess == 0)
                    ViewBag.EditAccess = "False";
            }

            IEnumerable<ClientFolderMap> clientFolderMaps;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}webreports", _uri);

                var result = await client.GetAsync(uri);

                clientFolderMaps = await result.Content.ReadAsAsync<IEnumerable<ClientFolderMap>>();
            }
            return View(clientFolderMaps);
        }

        //
        // GET: /WebReports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }

        public FileResult Download(int id, string filename, string fullfilename)
        {
            return File(fullfilename, System.Web.MimeMapping.GetMimeMapping(filename), filename);
        }
        //
        // GET: /WebReports/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }
        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role", _uri);
                var result = await client.GetAsync(uri);
                var roles = await result.Content.ReadAsAsync<List<MstRole>>();
                var Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.rolrecid.ToString(),
                    Text = c.rolname
                });

                ViewBag.Roles = Roles;
            }
        }
        //
        // POST: /WebReports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientFolderMap _clientFolderMap)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}webreports", _uri);

                var result = await client.PostAsJsonAsync(uri, _clientFolderMap);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ClientFolderMap meter = await result.Content.ReadAsAsync<ClientFolderMap>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "WebReports");
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

        //
        // GET: /WebReports/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            await BindDropDown();
            ClientFolderMap clientFolderMap = await GetClientFolderMap(id);
            return View(clientFolderMap);
        }


        private async Task<ClientFolderMap> GetClientFolderMap(int id)
        {
            ClientFolderMap clientFolderMap;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}webreports/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                clientFolderMap = await result.Content.ReadAsAsync<ClientFolderMap>();
            }
            return clientFolderMap;
        }
        //
        // POST: /WebReports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClientFolderMap _clientFolderMap)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}webreports/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _clientFolderMap);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ClientFolderMap clientFolderMap = await result.Content.ReadAsAsync<ClientFolderMap>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "WebReports");
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

        //
        // GET: /WebReports/Delete/5
        [AccessCheck(IdParamName = "WebReports/Index")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            await BindDropDown();
            ClientFolderMap clientFolderMap = await GetClientFolderMap(id);
            return View(clientFolderMap);
        }

        //
        // POST: /WebReports/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ClientFolderMap _clientFolderMap)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}webreports/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ClientFolderMap clientFolderMap = await result.Content.ReadAsAsync<ClientFolderMap>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "WebReports");
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
