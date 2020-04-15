using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogikViewer.Models;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;
using GridLogik.ViewModels;
using Newtonsoft.Json.Linq;
using System.Web.Services.Description;

namespace GridLogikViewer.Controllers
{
    public class GUParametersController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string Message, MessageType = "";
        //
        // GET: /GUParameters/
        public ActionResult Index()
        {
            List<mstmodcostdata> objMstmodcostdata = new List<mstmodcostdata>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "generatorunitparameters");
                objMstmodcostdata = JsonConvert.DeserializeObject<List<mstmodcostdata>>(s);
            }
            return View(objMstmodcostdata);

        }

        //
        // GET: /GUParameters/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /GUParameters/Create
        public ActionResult Create()
        {

            List<MeterDropdown> objMstmodcostdata = new List<MeterDropdown>();
            using (WebClient client = new WebClient())
            {
                //List<mstmodcostdata> objMscostdata = new List<mstmodcostdata>();
                //string s = client.DownloadString(url + "generatorunitparameters");
                //objMscostdata = JsonConvert.DeserializeObject<List<mstmodcostdata>>(s);

                string s1 = client.DownloadString(url + "ModCostLimitAPI/GetMeters");
                objMstmodcostdata = JsonConvert.DeserializeObject<List<MeterDropdown>>(s1);
            }

            List<StateList> stateList = new List<StateList>() { 
            new StateList(){id=0,State="Cold"},
            new StateList(){id=1,State="Hot"},
            new StateList(){id=2,State="Running"}

            };
            ViewBag.StateList = new SelectList(stateList, "id", "State");
            ViewBag.metersCollection = new SelectList(objMstmodcostdata, "id", "metername");
            return View();
        }

        //
        // POST: /GUParameters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(mstmodcostdata Mstmodcostdata)
        {
            try
            {
                using (WebClient client = new WebClient())
                {

                    string Jsonstr;


                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(url + "GeneratorUnitParameters", JsonConvert.SerializeObject(Mstmodcostdata));
                    dynamic dynamicdata = JValue.Parse(Jsonstr);
                    Message = dynamicdata.Data.d;
                    MessageType = dynamicdata.Data.e;
                    return Json(new { d = Message, e = MessageType });
                }

            }
            catch
            {
                return null;
            }
        }

        //
        // GET: /GUParameters/Edit/5
        public ActionResult Edit(int id)
        {
            mstmodcostdata Mstmodcostdata = new mstmodcostdata();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "generatorunitparameters" + "/" + id);
                if (s != null)
                {
                    Mstmodcostdata = JsonConvert.DeserializeObject<mstmodcostdata>(s);
                }
                else
                {
                    return View(new mstmodcostdata());
                }
            }


            List<MeterDropdown> objMstmodcostdata = new List<MeterDropdown>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "ModCostLimitAPI/GetMeters");
                objMstmodcostdata = JsonConvert.DeserializeObject<List<MeterDropdown>>(s);
            }


            List<StateList> stateList = new List<StateList>() { 
            new StateList(){id=0,State="Cold"},
            new StateList(){id=1,State="Hot"},
            new StateList(){id=2,State="Running"}

            };
            ViewBag.StateList = new SelectList(stateList, "id", "State", Mstmodcostdata.mstate);
            ViewBag.metersCollection = new SelectList(objMstmodcostdata, "id", "metername", Mstmodcostdata.mgenid);
            return View(Mstmodcostdata);
        }

        //
        // POST: /GUParameters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(mstmodcostdata Mstmodcostdata)
        {
            try
            {
                
                using (WebClient client = new WebClient())
                {
                    string Jsonstr;


                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(url + "GeneratorUnitParameters/Details" + "/" + Mstmodcostdata.mrecid, "Put", JsonConvert.SerializeObject(Mstmodcostdata));
                   
                    dynamic dynamicdata = JValue.Parse(Jsonstr);
                    Message = dynamicdata.Data.d;
                    MessageType = dynamicdata.Data.e;
                    return Json(new { d = Message, e = MessageType });
                }
            }
            catch
            {
                return Json(new { d = "Error Occur During Updating the record", e = "E" });
            }
        }

        //
        // GET: /GUParameters/Delete/5
        public ActionResult Delete(int id)
        {
            List<MeterDropdown> objMstmodcostdata = new List<MeterDropdown>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "ModCostLimitAPI/GetMeters");
                objMstmodcostdata = JsonConvert.DeserializeObject<List<MeterDropdown>>(s);
            }
            List<StateList> stateList = new List<StateList>() { 
            new StateList(){id=0,State="Cold"},
            new StateList(){id=1,State="Hot"},
            new StateList(){id=2,State="Running"}

            };
            ViewBag.StateList = new SelectList(stateList, "id", "State");
            ViewBag.metersCollection = new SelectList(objMstmodcostdata, "id", "metername");
            mstmodcostdata Mstmodcostdata = new mstmodcostdata();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "generatorunitparameters" + "/" + id);
                if (s != null)
                {
                    Mstmodcostdata = JsonConvert.DeserializeObject<mstmodcostdata>(s);
                }
                else
                {
                    return View(new mstmodcostdata());
                }
            }
            return View(Mstmodcostdata);
        }

        //
        // POST: /GUParameters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(mstmodcostdata mst)
        {
            try
            {
                long id = mst.mrecid;

                string Jsonstr;

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(url + "generatorunitparameters/Delete", "PUT", JsonConvert.SerializeObject(mst));
                    dynamic dynamicdata = JValue.Parse(Jsonstr);
                    Message = dynamicdata.Data.d;
                    MessageType = dynamicdata.Data.e;
                    return Json(new { d = Message, e = MessageType });

                }
            }
            catch
            {
                return null;
            }
        }


    }
    class meterlist
    {

        public int id { get; set; }
        public int MeterName { get; set; }
    }

    class StateList
    {

        public int id { get; set; }
        public string State { get; set; }
    }
}
