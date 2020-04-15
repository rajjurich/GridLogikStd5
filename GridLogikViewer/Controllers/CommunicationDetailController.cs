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
    public class CommunicationDetailController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /CommunicationDetail/
        [AccessCheck(IdParamName = "CommunicationDetail/Index")]
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
            IEnumerable<CommunicationDetail> communicationDetails;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}communicationdetail", _uri);

                var result = await client.GetAsync(uri);

                communicationDetails = await result.Content.ReadAsAsync<IEnumerable<CommunicationDetail>>();
            }
            return View(communicationDetails);
        }

        //
        // GET: /CommunicationDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CommunicationDetail/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        private async Task BindDropDown()
        {
            IEnumerable<SelectListItem> CommunicationTypes;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/GetIdentifiersOnModuleNew/CommunicationType", _uri);
                var result = await client.GetAsync(uri);
                var communicationTypes = await result.Content.ReadAsAsync<IEnumerable<prmglobal>>();
                CommunicationTypes = communicationTypes.Select(c => new SelectListItem
                {
                    Value = c.prmvalue.ToString(),
                    Text = c.prmidentifier
                });
            }
            ViewBag.CommunicationTypes = CommunicationTypes;
        }

        //
        // POST: /CommunicationDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CommunicationDetail _communicationDetail)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}communicationdetail", _uri);

                var result = await client.PostAsJsonAsync(uri, _communicationDetail);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    CommunicationDetail communicationDetail = await result.Content.ReadAsAsync<CommunicationDetail>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "CommunicationDetail");
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
        // GET: /CommunicationDetail/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CommunicationDetail communicationDetail = await GetCommunicationDetail(id);
            await BindDropDown();

            return View(communicationDetail);
        }


        private async Task<CommunicationDetail> GetCommunicationDetail(int id)
        {
            CommunicationDetail communicationDetail;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}communicationdetail/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                communicationDetail = await result.Content.ReadAsAsync<CommunicationDetail>();

            }



            return communicationDetail;
        }

        //
        // POST: /CommunicationDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CommunicationDetail _communicationDetail)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}communicationdetail/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _communicationDetail);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    CommunicationDetail communicationDetail = await result.Content.ReadAsAsync<CommunicationDetail>();
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
                    return View(_communicationDetail);
                }
            }
        }

        //
        // GET: /CommunicationDetail/Delete/5
        [AccessCheck(IdParamName = "CommunicationDetail/Index")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            //return FillMeterData(id);
            CommunicationDetail communicationDetail = await GetCommunicationDetail(id);
            await BindDropDown();

            return View(communicationDetail);
        }

        //
        // POST: /CommunicationDetail/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, CommunicationDetail _communicationDetail)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}communicationdetail/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    CommunicationDetail communicationDetail = await result.Content.ReadAsAsync<CommunicationDetail>();
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
                    return View(_communicationDetail);
                }
            }
        }
    }
}
