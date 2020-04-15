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
    public class OPCServerTagController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        [AccessCheck(IdParamName = "OPCServerTag/Index")]
        //
        // GET: /OPCServerTag/
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

            IEnumerable<OPCServerTag> OPCServerTags;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag", _uri);

                var result = await client.GetAsync(uri);

                OPCServerTags = await result.Content.ReadAsAsync<IEnumerable<OPCServerTag>>();
                
            }

            return View(OPCServerTags);
        }  
       
        //
        // GET: /OPCServerTag/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /OPCServerTag/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        private async Task BindDropDown()
        {
            IEnumerable<SelectListItem> Meters;
           

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}Meter", _uri);
                var result = await client.GetAsync(uri);
                var meters = await result.Content.ReadAsAsync<IEnumerable<Meter>>();
                Meters = meters.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.MeterName
                });
            }

            var Datatype = new List<SelectListItem>();
            Datatype.Add(new SelectListItem() { Text = "Int16", Value = "Int16" });
            Datatype.Add(new SelectListItem() { Text = "Int32", Value = "Int32" });
            Datatype.Add(new SelectListItem() { Text = "Int64", Value = "Int64" });            

            ViewBag.Datatype = Datatype;
            ViewBag.Meters = Meters;
        }
        //
        // POST: /OPCServerTag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OPCServerTag collection)
        {
            collection.istag = collection.is_tag ? 1 : 0;
            collection.priority = collection._priority;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag", _uri);
                var result = await client.PostAsJsonAsync(uri, collection);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    OPCServerTag OPCServerTag = await result.Content.ReadAsAsync<OPCServerTag>();
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
                    await BindDropDown();
                    return View();
                }
            }
        }

        //
        // GET: /OPCServerTag/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            await BindDropDown();
            OPCServerTag OPCServerTag = await GetOPCServerTag(id);
            return View(OPCServerTag);
        }
        private async Task<OPCServerTag> GetOPCServerTag(int id)
        {
            OPCServerTag OPCServerTag;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                OPCServerTag = await result.Content.ReadAsAsync<OPCServerTag>();
            }
            return OPCServerTag;
        }
        //
        // POST: /OPCServerTag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OPCServerTag collection)
        {
            collection.istag = collection.is_tag ? 1 : 0;
            collection.priority = collection._priority;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, collection);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    OPCServerTag OPCServerTag = await result.Content.ReadAsAsync<OPCServerTag>();
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
                    await BindDropDown();
                    return View();
                }
            }
        }

        //
        // GET: /OPCServerTag/Delete/5
        [AccessCheck(IdParamName = "OPCServerTag/Index")]
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
            OPCServerTag OPCServerTag = await GetOPCServerTag(id);
            return View(OPCServerTag);
        }

        //
        // POST: /OPCServerTag/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, OPCServerTag collection)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    OPCServerTag OPCServerTag = await result.Content.ReadAsAsync<OPCServerTag>();
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
                    await BindDropDown();
                    return View();
                }
            }
        }
    }
}
