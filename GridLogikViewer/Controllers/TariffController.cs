using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Web.Configuration;
using System.Net;
using Newtonsoft.Json;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;


namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class TariffController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /Tariff/
        [AccessCheck(IdParamName = "Tariff/Index")]
        public ActionResult Index()
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            return View();
        }

        //
        // GET: /Tariff/Details/5
        public ActionResult Details(long id)
        {
            return View();
        }

        //
        // GET: /Tariff/Create
        public ActionResult Create()
        {
            List<MstUtility> lstUtility = FillUtilities();
            ViewBag.UtilityID = new SelectList(lstUtility, "utilid", "utilname");
            return View();
        }

        //
        // POST: /Tariff/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            List<MstUtility> lstUtility = FillUtilities();
            ViewBag.UtilityID = new SelectList(lstUtility, "utilid", "utilname");
            return View();
        }

        //
        // GET: /Tariff/Edit/5
        public ActionResult Edit(int id)
        {
            List<MstUtility> lstUtility = FillUtilities();
            ViewBag.UtilityID = new SelectList(lstUtility, "utilid", "utilname");
            return View();

        }

        //
        // POST: /Tariff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tariff/Delete/5
        public ActionResult Delete(long id)
        {
            List<MstUtility> lstUtility = FillUtilities();
            ViewBag.UtilityID = new SelectList(lstUtility, "utilid", "utilname");
            return View();
        }

        //
        // POST: /Tariff/Delete/5
        //[HttpPut]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        [HttpPost]
        public ActionResult Delete(int ID, GridLogikViewer.Models.MstTariff sa)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    web.Headers.Add("Content-Type", "application/json");
                    //string status = web.UploadString(url + "StandardAlarmAPI/Delete", JsonConvert.SerializeObject(ID));

                    string s = web.UploadString(url + "Tariff/Delete" + "/" + ID, "DELETE", JsonConvert.SerializeObject(sa));
                    //string statusdelete = web.DownloadString(url + "StandardAlarmAPI/Delete/" + ID);

                    return Json(new { Message = "Records deleted successfully" }, JsonRequestBehavior.AllowGet);
                }

                //return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Json(new { Message = "Unable to proceed. Please try again later." }, JsonRequestBehavior.AllowGet);
                //return View("Index");
            }




        }


        private List<MstUtility> FillUtilities()
        {
            List<MstUtility> lstUtility = new List<MstUtility>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("utility").Result;
            if (response.IsSuccessStatusCode)
            {
                var utilities = response.Content.ReadAsStringAsync().Result;

                dynamic objUtilities = JValue.Parse(utilities);
                if (objUtilities.Data.result != null)
                {
                    foreach (dynamic div in objUtilities.Data.result)
                    {
                        lstUtility.Add(new MstUtility() { utilid = div.utilid, utilname = div.utilname });
                    }
                }


            }
            return lstUtility;
        }
    }
}
