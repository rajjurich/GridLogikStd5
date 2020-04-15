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
    public class MeterModelController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
                //
        // GET: /MeterType/
        [AccessCheck(IdParamName = "MeterModel/Index")]
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
            IEnumerable<MeterModel> meterModels;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metermodel", _uri);

                var result = await client.GetAsync(uri);

                meterModels = await result.Content.ReadAsAsync<IEnumerable<MeterModel>>();

            }

            //using (WebClient client = new WebClient())
            //{

            //    string s = client.DownloadString(url + "MeterModel");
            //    MeterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            //    MeterModel.RemoveAll(item => item == null);
            //}
            return View(meterModels);
        }

        //
        // GET: /MeterType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MeterType/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MeterType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeterModel _metermodel)
        {

            //string Jsonstr;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metermodel", _uri);

                var result = await client.PostAsJsonAsync(uri, _metermodel);
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


            //using (WebClient client = new WebClient())
            //{
            //    client.Headers.Add("Content-Type", "application/json");
            //    Jsonstr = client.UploadString(_uri + "metermodelapi", "POST", JsonConvert.SerializeObject(metermodel));
            //    dynamic dynamicdata = JValue.Parse(Jsonstr);
            //    Message = dynamicdata.Data.d;
            //    MessageType = dynamicdata.Data.e;
            //    return Json(new { d = Message, e = MessageType });
            //}



        }

        //
        // GET: /MeterType/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            MeterModel meterModel = await GetMeterModel(id);

           
            return View(meterModel);

        }
        private async Task<MeterModel> GetMeterModel(int id)
        {
            MeterModel meterModel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metermodel/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                meterModel = await result.Content.ReadAsAsync<MeterModel>();
            }
            return meterModel;
        }

        //
        // POST: /MeterType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MeterModel _metermodel)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metermodel/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _metermodel);
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

            //string Jsonstr;

            //using (WebClient client = new WebClient())
            //{
            //    client.Headers.Add("Content-Type", "application/json");
            //    Jsonstr = client.UploadString(_uri + "metermodelapi" + "/" + id, "PUT", JsonConvert.SerializeObject(metermodel));
            //    dynamic dynamicdata = JValue.Parse(Jsonstr);
            //    Message = dynamicdata.Data.d;
            //    MessageType = dynamicdata.Data.e;
            //    return Json(new { d = Message, e = MessageType });

            //}



        }

        //
        // GET: /MeterType/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            MeterModel meterModel = await GetMeterModel(id);

            return View(meterModel);
            //GridLogikViewer.Utilities.Utilities util = new Utilities.Utilities();
            //try
            //{
            //    MeterModel metermodel = new MeterModel();
            //    using (WebClient client = new WebClient())
            //    {
            //        string s = client.DownloadString(_uri + "MeterModelAPI/GetMeterDetailsList" + "/" + id);
            //        if (s != null)
            //        {
            //            if (util.IsValidJson(s))
            //            {
            //                metermodel = JsonConvert.DeserializeObject<MeterModel>(s);
            //            }
            //            else
            //            {
            //                string ErrorMessage = util.ErrorMessages(s, HttpContext.Server.MapPath("~/App_Data/ErrorCode.xml"));

            //                ViewBag.ErrorMessage = ErrorMessage;
            //                return View(new MeterModel());
            //            }
            //        }
            //        else
            //        {
            //            return View(new MeterModel());
            //        }
            //    }
            //    return View(metermodel);
            //}
            //catch
            //{
            //    return View(new MeterModel());
            //}
        }

        //
        // POST: /MeterType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MeterModel _metermodel)
        {

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metermodel/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
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
            //string Jsonstr;

            //using (WebClient client = new WebClient())
            //{
            //    client.Headers.Add("Content-Type", "application/json");
            //    Jsonstr = client.UploadString(_uri + "metermodelapi/Delete" + "/" + id, "PUT", JsonConvert.SerializeObject(metermodel));
            //    dynamic dynamicdata = JValue.Parse(Jsonstr);
            //    Message = dynamicdata.Data.d;
            //    MessageType = dynamicdata.Data.e;
            //    return Json(new { d = Message, e = MessageType });

            //}



        }



    }
}
