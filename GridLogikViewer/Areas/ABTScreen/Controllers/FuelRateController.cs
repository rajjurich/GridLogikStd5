using GridLogik.ViewModels;
using GridLogikViewer.Areas.ABTScreen.Models;
using GridLogikViewer.GridLogikViewerModels;
using GridLogikViewer.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Areas.ABTScreen.Controllers
{
    [Authorize]
    public class FuelRateController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string Message, MessageType = "";

        // GET: /FuelRate/
       // [AccessCheck(IdParamName = "FuelRate/Index")]
        public ActionResult Index()
        {
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
            List<FuelRate> model = new List<FuelRate>();
            using (WebClient client = new WebClient())
            {

                string s = client.DownloadString(url + "FuelRateAPI");
                model = JsonConvert.DeserializeObject<List<FuelRate>>(s);
                model.RemoveAll(item => item == null);
            }
            return View(model);
        }

        // GET: /FuelRate/Create
        public ActionResult Create()
        {
            return View(new FuelRate());
        }

        //
        // POST: /FuelRate/Create
        [HttpPost]
        public ActionResult Create(FuelRate model)
        {
            try
            {

                string Jsonstr;

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(url + "FuelRateAPI", "POST", JsonConvert.SerializeObject(model));
                    dynamic dynamicdata = JValue.Parse(Jsonstr);
                    Message = dynamicdata.Data.d;
                    MessageType = dynamicdata.Data.e;
                    return Json(new { d = Message, e = MessageType });

                }


            }
            catch (Exception ex)
            {
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return null;
            }
        }

        // GET: /FuelRate/Edit/5
        public ActionResult Edit(int id)
        {
            GridLogikViewer.Utilities.Utilities util = new Utilities.Utilities();
            try
            {
                FuelRate metermodel = new FuelRate();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "FuelRateAPI/GetFuelRateDetailsList" + "/" + id);
                    if (s != null)
                    {
                        if (util.IsValidJson(s))
                        {
                            metermodel = JsonConvert.DeserializeObject<FuelRate>(s);
                        }
                        else
                        {
                            string ErrorMessage = util.ErrorMessages(s, HttpContext.Server.MapPath("~/App_Data/ErrorCode.xml"));

                            ViewBag.ErrorMessage = ErrorMessage;
                            return View(new FuelRate());
                        }
                    }
                    else
                    {
                        return View(new FuelRate());
                    }
                }
                return View(metermodel);
            }
            catch
            {
                return View(new FuelRate());
            }
        }

        // POST: /fuelrate/Edit/5
        [HttpPost]
        public JsonResult Edit(int id, FuelRate model)
        {
            try
            {

                string Jsonstr;

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(url + "FuelRateAPI" + "/" + id, "PUT", JsonConvert.SerializeObject(model));
                    dynamic dynamicdata = JValue.Parse(Jsonstr);
                    Message = dynamicdata.Data.d;
                    MessageType = dynamicdata.Data.e;
                    return Json(new { d = Message, e = MessageType });

                }


            }
            catch (Exception ex)
            {
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return null;
            }
        }

        // GET: /MeterType/Delete/5
        public ActionResult Delete(int id)
        {
            GridLogikViewer.Utilities.Utilities util = new Utilities.Utilities();
            try
            {
                FuelRate model = new FuelRate();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "FuelRateAPI/GetFuelRateDetailsList" + "/" + id);
                    if (s != null)
                    {
                        if (util.IsValidJson(s))
                        {
                            model = JsonConvert.DeserializeObject<FuelRate>(s);
                        }
                        else
                        {
                            string ErrorMessage = util.ErrorMessages(s, HttpContext.Server.MapPath("~/App_Data/ErrorCode.xml"));

                            ViewBag.ErrorMessage = ErrorMessage;
                            return View(new FuelRate());
                        }
                    }
                    else
                    {
                        return View(new FuelRate());
                    }
                }
                return View(model);
            }
            catch
            {
                return View(new FuelRate());
            }
        }

        //
        // POST: /MeterType/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FuelRate model)
        {
            try
            {

                string Jsonstr;

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(url + "FuelRateapi/Delete" + "/" + id, "PUT", JsonConvert.SerializeObject(model));
                    dynamic dynamicdata = JValue.Parse(Jsonstr);
                    Message = dynamicdata.Data.d;
                    MessageType = dynamicdata.Data.e;
                    return Json(new { d = Message, e = MessageType });

                }


            }
            catch (Exception ex)
            {
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return null;
            }
        }
    }
}