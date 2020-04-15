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
    public class CommunicationDetailLinkSerialController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /CommunicationDetailLinkSerial/
        [AccessCheck(IdParamName = "CommunicationDetailLinkSerial/Index")]
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
            IEnumerable<CommunicationDetailLinkListModel> communicationDetailLinks;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}CommunicationDetailLink/GetAllListModelSerial", _uri);

                var result = await client.GetAsync(uri);

                communicationDetailLinks = await result.Content.ReadAsAsync<IEnumerable<CommunicationDetailLinkListModel>>();
            }
            return View(communicationDetailLinks);
        }

        //
        // GET: /CommunicationDetailLinkSerial/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CommunicationDetailLinkSerial/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown("Create");
            return View();
        }

        private async Task BindDropDown(string Mode = null, int? id = null)
        {
            IEnumerable<SelectListItem> CommunicationDetails;
            IEnumerable<SelectListItem> Meters;

            using (HttpClient client = new HttpClient())
            {
                if (Mode == "Create")
                {
                    uri = string.Format("{0}communicationdetail/unlinkedserial", _uri);
                }
                else
                {
                    uri = string.Format("{0}communicationdetail/serial", _uri);
                }
                var result = await client.GetAsync(uri);
                var communicationDetails = await result.Content.ReadAsAsync<IEnumerable<CommunicationDetail>>();
                CommunicationDetails = communicationDetails.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.comport
                });



                if (Mode == "Create")
                {
                    uri = string.Format("{0}meter/unlinked", _uri);
                }
                else
                {
                    uri = string.Format("{0}meter/seriallinked/{1}", _uri, id);
                }

                result = await client.GetAsync(uri);

                var meters = await result.Content.ReadAsAsync<IEnumerable<Meter>>();

                Meters = meters.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.MeterName
                });
            }

            ViewBag.CommunicationDetails = CommunicationDetails;
            ViewBag.Meters = Meters;
        }

        //
        // POST: /CommunicationDetailLinkSerial/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CommunicationDetailLinkCreateModel _communicationDetailLinkCreateModel)
        {

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}CommunicationDetailLink", _uri);

                var result = await client.PostAsJsonAsync(uri, _communicationDetailLinkCreateModel);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    CommunicationDetailLink communicationDetailLink = await result.Content.ReadAsAsync<CommunicationDetailLink>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "CommunicationDetailLinkSerial");
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
        // GET: /CommunicationDetailLinkSerial/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel = await GetCommunicationDetailLinkCreateModel(id);

            await BindDropDown(null, id);
            return View(communicationDetailLinkCreateModel);
        }

        private async Task<CommunicationDetailLinkCreateModel> GetCommunicationDetailLinkCreateModel(int id)
        {
            CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}CommunicationDetailLink/GetByConvertorid/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                communicationDetailLinkCreateModel = await result.Content.ReadAsAsync<CommunicationDetailLinkCreateModel>();

            }
            ViewBag.CommunicationDetail = communicationDetailLinkCreateModel;
            return communicationDetailLinkCreateModel;
        }

        //
        // POST: /CommunicationDetailLinkSerial/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CommunicationDetailLinkCreateModel _communicationDetailLinkCreateModel)
        {

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}CommunicationDetailLink/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _communicationDetailLinkCreateModel);
                var contents = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    CommunicationDetailLink communicationDetailLink = await result.Content.ReadAsAsync<CommunicationDetailLink>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "CommunicationDetailLinkSerial");
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
        // GET: /CommunicationDetailLinkSerial/Delete/5
        [AccessCheck(IdParamName = "CommunicationDetailLinkSerial/Index")]
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
            CommunicationDetailLinkCreateModel communicationDetailLinkCreateModel = await GetCommunicationDetailLinkCreateModel(id);
            await BindDropDown();

            return View(communicationDetailLinkCreateModel);
        }

        //
        // POST: /CommunicationDetailLinkSerial/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, CommunicationDetailLinkCreateModel _communicationDetailLinkCreateModel)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}CommunicationDetailLink/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    CommunicationDetailLink communicationDetailLink = await result.Content.ReadAsAsync<CommunicationDetailLink>();
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
                    return View(_communicationDetailLinkCreateModel);
                }
            }
        }
    }
}
