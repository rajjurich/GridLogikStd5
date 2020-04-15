using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class MeterTariffController : Controller
    {
        //
        String url = WebConfigurationManager.AppSettings["APIUrl"];
        // GET: /MeterTariff/
        [AccessCheck(IdParamName = "MeterTariff/Index")]
        public ActionResult Index()
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
           // List<MstConsumerCategoryTariff> meterTariff = new List<MstConsumerCategoryTariff>();
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterTariffAPI");
            //    meterTariff = JsonConvert.DeserializeObject<List<MeterTariff>>(s);
            //}
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsumerCategoryTariff mtrTariff)
        {
            try
            {
                mtrTariff.isdeleted = 0;
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(mtrTariff);
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "ConsumerCategoryTariffAPI", JsonConvert.SerializeObject(mtrTariff));
                }
                return RedirectToAction("Index", "MeterTariff");
            }
            catch
            {
                return View(mtrTariff);
            }
        }


        public ActionResult Create()
        {
            //MeterTariff model= new MeterTariff();
            //List<Meter> mtr = new List<Meter>();
            //List<MstTariff> Tariff = new List<MstTariff>();

            //using (WebClient client = new WebClient())
            //{
            //    string mt = client.DownloadString(url + "MeterAPI");
            //    string trf = client.DownloadString(url + "Tariff");

            //    //Tariff = JsonConvert.DeserializeObject<List<MstTariff>>(trf);
            //    mtr = JsonConvert.DeserializeObject<List<Meter>>(mt);
            //    mtr.RemoveAll(item => item == null);
            //}

            //MstTariff objmt1 = new MstTariff();
            //objmt1.trfid = "1";
            //objmt1.trfschemename="ABC";
            //Tariff.Add(objmt1);

            //MstTariff objmt2 = new MstTariff();
            //objmt2.trfid = "2";
            //objmt2.trfschemename = "XYZ";
            //Tariff.Add(objmt2);



            //ViewBag.ModelID = new SelectList(mtr, "ID", "MeterName");
            //ViewBag.Tariff = new SelectList(Tariff, "ID", "trfschemename");
            return View("Create");

        }

        public ActionResult Edit(int id)
        {
            //List<Meter> meter = new List<Meter>();
            //MeterTariff s = new MeterTariff();
            //List<MstTariff> Tariff = new List<MstTariff>();

            //List<SelectListItem> Tariffitems = new List<SelectListItem>();


            //using (WebClient client = new WebClient())
            //{

            //    string m = client.DownloadString(url + "MeterAPI");
            //    meter = JsonConvert.DeserializeObject<List<Meter>>(m);
            //    meter.RemoveAll(item => item == null);
            //}
            ////ViewBag.ID = new SelectList(meter, "Id", "MeterName");

            //using (WebClient web = new WebClient())
            //{
            //    string sd = web.DownloadString(url + "MeterTariffAPI/" + id);

            //    string trf = web.DownloadString(url + "Tariff");
            //    //Tariff = JsonConvert.DeserializeObject<List<MstTariff>>(trf);

            //    //dynamic parsing

            //    //var EmployeeDetails = response.Content.ReadAsStringAsync().Result;

            //    //dynamic zones = JValue.Parse(EmployeeDetails);
            //    //foreach (dynamic zone in zones.Data.result)
            //    //{
            //    //    items.Add(new SelectListItem { Text = zone.znname, Value = zone.znid });
            //    //}

            //    //ViewBag.ZonesList = new SelectList(items, "Value", "Text");
            //    //ends here

            //    dynamic TariffMasterData = JValue.Parse(trf);
            //    foreach (dynamic tariffmaster in TariffMasterData.Data.result)
            //    {
            //        Tariffitems.Add(new SelectListItem { Text = tariffmaster.trfschemename, Value = tariffmaster.trfid });
            //    }

            //    s = JsonConvert.DeserializeObject<MeterTariff>(sd);
            //    //ViewBag.Tariff = new SelectList(Tariff, "ID", "trfschemename");
            //    ViewBag.Tariff = Tariffitems;

            //    //meter = meter.Select(x => x).Where(x => x.ID == s.meterid).ToList();
                
            //    //s.MeterSelectionList = new SelectList(meter, "Id", "MeterName");

            //    ViewBag.ModelID = new SelectList(meter, "ID", "MeterName");
            //}


            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MeterTariff mtrf)
        {
            try
            {
                mtrf.isdeleted = 0;

                string startdate = GetFinaldate(mtrf.startdate, Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());
                string enddate = GetFinaldate(mtrf.enddate, Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());

                mtrf.startdate = startdate;
                mtrf.enddate = enddate;

                using (WebClient we = new WebClient())
                {
                    we.Headers.Add("Content-Type", "application/json");
                    string sd = we.UploadString(url + "MeterTariffAPI/" + id, "Put", JsonConvert.SerializeObject(mtrf));
                }
                return RedirectToAction("Index", "MeterTariff");

            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult Delete(long ID, ConsumerCategoryTariff sa)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ID, ConsumerCategoryTariff sa)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    web.Headers.Add("Content-Type", "application/json");
                    //string status = web.UploadString(url + "StandardAlarmAPI/Delete", JsonConvert.SerializeObject(ID));

                    string s = web.UploadString(url + "ConsumerCategoryTariffAPI/Delete" + "/" + ID, "DELETE", JsonConvert.SerializeObject(sa));
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


        protected string GetFinaldate(string textdate, string DBDateFormat, string Dateformat)
        {
            string[] dateAr = textdate.Split('/');
            var finalDate = textdate;
            if (DBDateFormat == "dd/mm/yy" && Dateformat == "mm/dd/yy")
            {
                finalDate = dateAr[1] + '-' + dateAr[0] + '-' + dateAr[2];
            }
            else if (DBDateFormat == "mm/dd/yy" && Dateformat == "dd/mm/yy")
            {
                finalDate = dateAr[1] + '-' + dateAr[0] + '-' + dateAr[2];
            }
            return finalDate;
        }

        protected string GetClientdate(string textdate, string DBDateFormat)
        {
            var finalDate = textdate;

            if (textdate != null && textdate != "")
            {
                var dateAr = textdate.Split('-');

                if (DBDateFormat == "dd/mm/yy")
                {
                    finalDate = dateAr[2] + '-' + dateAr[1] + '-' + dateAr[0];
                }
                else if (DBDateFormat == "mm/dd/yy")
                {
                    finalDate = dateAr[1] + '-' + dateAr[2] + '-' + dateAr[0];
                }
            }
            return finalDate;
        }


	}
}