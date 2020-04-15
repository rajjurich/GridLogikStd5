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
    public class MeterGroupController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /MeterGroup/
        [AccessCheck(IdParamName = "MeterGroup/Index")]
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
         
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
            }
            return View(meterGroups);
        }

        //
        // GET: /MeterGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MeterGroup/Create
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
            ViewBag.Meters = Meters;
          
        }
        //
        // POST: /MeterGroup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeterGroup _meterGroup)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metergroup", _uri);

                var result = await client.PostAsJsonAsync(uri, _meterGroup);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterGroup meter = await result.Content.ReadAsAsync<MeterGroup>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "MeterGroup");
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
        // GET: /MeterGroup/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            await BindDropDown();
            MeterGroup meterGroup = await GetMeterGroup(id);
            return View(meterGroup);
        }

        private async Task<MeterGroup> GetMeterGroup(int id)
        {
            MeterGroup meterGroup;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                meterGroup = await result.Content.ReadAsAsync<MeterGroup>();
            }
            return meterGroup;
        }

        //
        // POST: /MeterGroup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MeterGroup _meterGroup)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _meterGroup);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterGroup meterGroup = await result.Content.ReadAsAsync<MeterGroup>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "MeterGroup");
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
        // GET: /MeterGroup/Delete/5
        [AccessCheck(IdParamName = "MeterGroup/Index")]
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
            MeterGroup meterGroup = await GetMeterGroup(id);
            return View(meterGroup);
        }

        //
        // POST: /MeterGroup/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MeterGroup _meterGroup)
        {
            await BindDropDown();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterGroup/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MeterGroup meterGroup = await result.Content.ReadAsAsync<MeterGroup>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "MeterGroup");
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
