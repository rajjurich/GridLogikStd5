using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using GridLogikViewer.GridLogikViewerModels;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;
using System.Threading.Tasks;
using GridLogikViewer.Utilities;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class MeterController : BaseController
    {
        
        // GET: /Meter/
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        
               //
        // GET: /Meter/
        [AccessCheck(IdParamName = "Meter/Index")]
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
            // List<clsMeterVMS> meter = new List<clsMeterVMS>();

            //List<clsMeterVMS> meter = GetMeterList();

            IEnumerable<Meter> meters;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}meter/GetMeterDetails/{1}", _uri, Convert.ToInt64(HttpContext.Session["usrrecid"]));

                var result = await client.GetAsync(uri);

                meters = await result.Content.ReadAsAsync<IEnumerable<Meter>>();                
            }



            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MeterAPI/GetIndexMeterList");
            //    meter = JsonConvert.DeserializeObject<List<clsMeterVMS>>(s);
            //    meter.RemoveAll(item => item == null);
            //}
            return View(meters);
        }

        //public List<clsMeterVMS> GetMeterList()
        //{
        //    List<clsMeterVMS> meterListfinal = new List<clsMeterVMS>();
        //    try
        //    {

        //        using (WebClient client = new WebClient())
        //        {
        //            string s = client.DownloadString(url + "MeterAPI/GetIndexMeterList?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
        //            List<clsMeterVMS> meterList = null;
        //            meterList = JsonConvert.DeserializeObject<List<clsMeterVMS>>(s);
        //            meterList.RemoveAll(item => item == null);

        //            foreach (var item in meterList)
        //            {
        //                clsMeterVMS meter = new clsMeterVMS();
        //                meter.id = item.id;
        //                meter.metername = item.metername;
        //                meter.meterno = item.meterno;
        //                meter.metertype = item.metertype;
        //                meter.serialno = item.serialno;
        //                meter.model = item.model;

        //                List<prmglobal> Prmlocation = GetGlobalValuesLocationByuserid(); //GetGlobalValuesLocation();
        //                if (Prmlocation!=null)
        //                {
        //                    var Locationname = Prmlocation.Where(m => m.prmvalue == item.location).Select(m=>m.prmidentifier).FirstOrDefault();
        //                    meter.location = Locationname;
        //                }
        //                else
        //                {
        //                    meter.location = item.location;
        //                }
        //                meterListfinal.Add(meter);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {                
        //        throw;
        //    }
        //    return meterListfinal;
        //}


        //
        // GET: /Meter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Meter/Create
        public async Task<ActionResult> Create()
        {

            //List<prmglobal> prmglobal = new List<prmglobal>();
            //List<MeterGroup> metergroup = new List<MeterGroup>();
            //List<MeterType> meterType = new List<MeterType>();
            //List<MeterModel> meterModel = new List<MeterModel>();
            //List<CommunicationType> communicationType = new List<CommunicationType>();
            //List<prmglobal> factor = new List<prmglobal>();
            //using (WebClient client = new WebClient())
            //{
            //    string prm = client.DownloadString(url + "prmglobal/GetPhaseValues");
            //    string mg = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
            //    string mt = client.DownloadString(url + "meterTypeAPI");
            //    string mm = client.DownloadString(url + "meterModelAPI");

            //    string mf = client.DownloadString(url + "MeterAPI/getmultiplication");
            //    // string ct = client.DownloadString(url + "communicationTypeAPI");
            //    prmglobal = JsonConvert.DeserializeObject<List<prmglobal>>(prm);
            //    metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mg);
            //    meterType = JsonConvert.DeserializeObject<List<MeterType>>(mt);
            //    meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(mm);
            //    factor = JsonConvert.DeserializeObject<List<prmglobal>>(mf);
            //    //  communicationType = JsonConvert.DeserializeObject<List<CommunicationType>>(ct);
            //}

            //ViewBag.GroupId = new SelectList(metergroup, "Id", "GroupName");
            //ViewBag.Phase = new SelectList(prmglobal, "prmrecid", "prmvalue");
            //ViewBag.MeterTypeID = new SelectList(meterType, "ID", "MeterTypeName");
            //ViewBag.ModelID = new SelectList(meterModel, "ID", "ModelName");
            //ViewBag.mf = factor;


            await BindDropDown();



            //List<prmglobal> prmglobal = GetGlobalValues();
            //ViewBag.parameter = new SelectList(prmglobal, "prmidentifier", "prmvalue");

            //List<prmglobal> Prmlocation = GetGlobalValuesLocationByuserid();// GetGlobalValuesLocation();
            //ViewBag.locationfill = new SelectList(Prmlocation, "prmvalue", "prmidentifier");



            return View();
        }

        private async Task BindDropDown()
        {
            IEnumerable<SelectListItem> MeterModel;
            IEnumerable<SelectListItem> ConsumptionTypes;
            IEnumerable<SelectListItem> LocationsFilter;
            IEnumerable<SelectListItem> Phase;
            IEnumerable<SelectListItem> MeterType;
            IEnumerable<prmglobal> multiplicationFactor;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/GetIdentifiersOnModuleNew/ConsumptionType", _uri);
                var result = await client.GetAsync(uri);
                var consumptionTypes = await result.Content.ReadAsAsync<IEnumerable<prmglobal>>();
                ConsumptionTypes = consumptionTypes.Select(c => new SelectListItem
                {
                    Value = c.prmrecid.ToString(),
                    Text = c.prmvalue
                });

                uri = string.Format("{0}prmglobal/GetIdentifiersOnModuleNew/Locationfilter", _uri);
                result = await client.GetAsync(uri);
                var locations = await result.Content.ReadAsAsync<IEnumerable<prmglobal>>();
                LocationsFilter = locations.Select(c => new SelectListItem
                {
                    Value = c.prmrecid.ToString(),
                    Text = c.prmvalue
                });

                uri = string.Format("{0}prmglobal/GetValuesOnModuleUnitAndIdentifier/{1}/{2}/{3}", _uri, "Global", "Phase", "Phase");
                result = await client.GetAsync(uri);
                var phase = await result.Content.ReadAsAsync<IEnumerable<prmglobal>>();
                Phase = phase.Select(c => new SelectListItem
                {
                    Value = c.prmrecid.ToString(),
                    Text = c.prmvalue
                });

                uri = string.Format("{0}metertype", _uri);
                result = await client.GetAsync(uri);
                var meterType = await result.Content.ReadAsAsync<IEnumerable<MeterType>>();
                MeterType = meterType.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.MeterTypeName
                });

                uri = string.Format("{0}metermodel", _uri);
                result = await client.GetAsync(uri);
                var meterModel = await result.Content.ReadAsAsync<IEnumerable<MeterModel>>();
                MeterModel = meterModel.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.ModelName
                });

                uri = string.Format("{0}prmglobal/GetIdentifiersOnUnit/{1}", _uri, "Scaling");
                result = await client.GetAsync(uri);
                multiplicationFactor = await result.Content.ReadAsAsync<IEnumerable<prmglobal>>();

               
            }
            ViewBag.ConsumptionTypes = ConsumptionTypes;
            ViewBag.LocFill = LocationsFilter;
            ViewBag.Phases = Phase;
            ViewBag.FillMeterTypes = MeterType;
            ViewBag.FillMeterModels = MeterModel;
            ViewBag.MFs = multiplicationFactor;
            ViewBag.ServiceCalculationFlag = new SelectList(new[] { new { ID = 1, Name = "evt_loadsurvey" }, new { ID = 2, Name = "query" } }, "ID", "Name").ToList();
        }
        //private List<prmglobal> GetGlobalValues()
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = client.GetAsync("prmglobal/GetTablesIdentifiers/ConsumptionType").Result;
        //    List<prmglobal> lstGlobal = null;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var objResponse = response.Content.ReadAsStringAsync().Result;
        //        lstGlobal = new List<prmglobal>();
        //        dynamic objPrmGlobal = JValue.Parse(objResponse);
        //        foreach (dynamic prm in objPrmGlobal.Data.result)
        //        {
        //            if (prm.prmmodule.ToString().ToLower() != "global")
        //            {
        //                prmglobal obj = new prmglobal();
        //                obj.prmidentifier = prm.prmidentifier.ToString();
        //                obj.prmmodule = prm.prmmodule.ToString();
        //                obj.prmrecid = Convert.ToInt16(prm.prmrecid.ToString());
        //                obj.prmunit = (prm.prmunit.ToString().IndexOf('.') > 0 ? prm.prmunit.ToString().Substring(prm.prmunit.ToString().IndexOf('.'), (prm.prmunit.ToString().Length) - (prm.prmunit.ToString().IndexOf('.'))).ToString().Replace(".", "") : prm.prmunit.ToString());
        //                obj.prmvalue = prm.prmvalue.ToString();
        //                obj.rfu1 = prm.rfu1.ToString();
        //                obj.rfu2 = prm.rfu2.ToString();
        //                lstGlobal.Add(obj);
        //            }
        //        }
        //    }
        //    return lstGlobal;

        //}
        //private List<prmglobal> GetGlobalValuesLocation()
        //{
        //    HttpClient ObjHttpclient = new HttpClient();
        //    ObjHttpclient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
        //    ObjHttpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = ObjHttpclient.GetAsync("prmglobal/GetTablesIdentifiers/Locationfilter").Result;
        //    List<prmglobal> lstGlobal = null;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var objResponse = response.Content.ReadAsStringAsync().Result;
        //        lstGlobal = new List<prmglobal>();
        //        dynamic objPrmGlobal = JValue.Parse(objResponse);
        //        foreach (dynamic prm in objPrmGlobal.Data.result)
        //        {
        //            if (prm.prmmodule.ToString().ToLower() != "global")
        //            {
        //                prmglobal obj = new prmglobal();
        //                obj.prmidentifier = prm.prmidentifier.ToString();
        //                obj.prmvalue = prm.prmvalue.ToString();
        //                lstGlobal.Add(obj);
        //            }
        //        }
        //    }
        //    return lstGlobal;

        //}
        //private List<prmglobal> GetGlobalValuesLocationByuserid()
        //{
        //    HttpClient ObjHttpclient = new HttpClient();
        //    ObjHttpclient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
        //    ObjHttpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = ObjHttpclient.GetAsync("prmglobal/GetTablesIdentifiers/Locationfilter/" + Convert.ToString(HttpContext.Session["usrrecid"])).Result;
        //    List<prmglobal> lstGlobal = null;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var objResponse = response.Content.ReadAsStringAsync().Result;
        //        lstGlobal = new List<prmglobal>();
        //        dynamic objPrmGlobal = JValue.Parse(objResponse);
        //        foreach (dynamic prm in objPrmGlobal.Data.result)
        //        {
        //            if (prm.prmmodule.ToString().ToLower() != "global")
        //            {
        //                prmglobal obj = new prmglobal();
        //                obj.prmidentifier = prm.prmidentifier.ToString();
        //                obj.prmvalue = prm.prmvalue.ToString();
        //                lstGlobal.Add(obj);
        //            }
        //        }
        //    }
        //    return lstGlobal;

        //}
        //private ActionResult FillData()
        //{
        //    List<prmglobal> prmglobal = new List<prmglobal>();
        //    List<MeterGroup> metergroup = new List<MeterGroup>();
        //    List<MeterType> meterType = new List<MeterType>();
        //    List<MeterModel> meterModel = new List<MeterModel>();
        //    List<CommunicationType> communicationType = new List<CommunicationType>();
        //    List<prmglobal> factor = new List<prmglobal>();
        //    using (WebClient client = new WebClient())
        //    {
        //        string prm = client.DownloadString(url + "prmglobal/GetPhaseValues");
        //        string mg = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
        //        string mt = client.DownloadString(url + "meterTypeAPI");
        //        string mm = client.DownloadString(url + "meterModelAPI");

        //        string mf = client.DownloadString(url + "MeterAPI/getmultiplication");
        //        // string ct = client.DownloadString(url + "communicationTypeAPI");
        //        prmglobal = JsonConvert.DeserializeObject<List<prmglobal>>(prm);
        //        metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mg);
        //        meterType = JsonConvert.DeserializeObject<List<MeterType>>(mt);
        //        meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(mm);
        //        factor = JsonConvert.DeserializeObject<List<prmglobal>>(mf);
        //        //  communicationType = JsonConvert.DeserializeObject<List<CommunicationType>>(ct);
        //    }

        //    ViewBag.GroupId = new SelectList(metergroup, "Id", "GroupName");
        //    ViewBag.Phase = new SelectList(prmglobal, "prmrecid", "prmvalue");
        //    ViewBag.MeterTypeID = new SelectList(meterType, "ID", "MeterTypeName");
        //    ViewBag.ModelID = new SelectList(meterModel, "ID", "ModelName");
        //    ViewBag.mf = factor;
        //    // ViewBag.CommunicationTypeId = new SelectList(communicationType, "ID", "CommunicationType1");
        //    return View(new Meter());
        //}

        //
        // POST: /Meter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Meter _meter)
        {

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}meter", _uri);

                var result = await client.PostAsJsonAsync(uri, _meter);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    Meter meter = await result.Content.ReadAsAsync<Meter>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "Meter");
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = contents;
                    return View();
                }
            }

            //try
            //{

            //    string Jsonstr;

            //    using (WebClient client = new WebClient())
            //    {
            //        client.Headers.Add("Content-Type", "application/json");
            //        Jsonstr = client.UploadString(url + "meterapi", "POST", JsonConvert.SerializeObject(meter));
            //        dynamic dynamicdata = JValue.Parse(Jsonstr);
            //        Message = dynamicdata.Data.d;
            //        MessageType = dynamicdata.Data.e;
            //        return Json(new { d = Message, e = MessageType });

            //    }


            //}
            //catch (Exception ex)
            //{
            //    new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
            //    return null;
            //}
        }

        //
        // GET: /Meter/Edit/5
        public async Task<ActionResult> Edit(int id)
        {            

            Meter meter = await GetMeter(id);
            await BindDropDown();

            //await GetParameterMFValuesForMeter(id);

            return View(meter);

        }
       

        private async Task<Meter> GetMeter(int id)
        {
            Meter meter;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}meter/GetMeterWithParameterValues/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                meter = await result.Content.ReadAsAsync<Meter>();               
               
            }

            if (meter.replacedby != null)
            {
                if (meter.replacedby != 0)
                {
                    meter.tagforsubmeter = true;
                    meter.parentcmrmeterid = Convert.ToString(meter.replacedby);
                }
                else
                {
                    meter.tagforsubmeter = false;
                }
            }
            else
            {
                meter.tagforsubmeter = false;
            }

            return meter;
        }
        //private ActionResult FillMeterData(int id)
        //{
        //    try
        //    {
        //        Meter meter = new Meter();
        //        List<prmglobal> prmglobal1 = GetGlobalValues();
        //        List<prmglobal> prmglobal = new List<prmglobal>();
        //        List<MeterGroup> metergroup = new List<MeterGroup>();
        //        List<MeterType> meterType = new List<MeterType>();
        //        List<MeterModel> meterModel = new List<MeterModel>();
        //        List<CommunicationType> communicationType = new List<CommunicationType>();

        //        List<ParameterMF> factor = new List<ParameterMF>();
        //        using (WebClient client = new WebClient())
        //        {
        //            string prm = client.DownloadString(url + "prmglobal/GetPhaseValues");
        //            string mg = client.DownloadString(url + "MeterGroupAPI?Userid=" + Convert.ToString(HttpContext.Session["usrrecid"]));
        //            string mt = client.DownloadString(url + "meterTypeAPI");
        //            string mm = client.DownloadString(url + "meterModelAPI");
        //            string mf = client.DownloadString(url + "MeterAPI/getmultiplication/" + id);
        //            // string ct = client.DownloadString(url + "communicationTypeAPI");
        //            metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mg);
        //            prmglobal = JsonConvert.DeserializeObject<List<prmglobal>>(prm);
        //            meterType = JsonConvert.DeserializeObject<List<MeterType>>(mt);
        //            meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(mm);
        //            factor = JsonConvert.DeserializeObject<List<ParameterMF>>(mf);
        //            //communicationType = JsonConvert.DeserializeObject<List<CommunicationType>>(ct);
        //            string s = client.DownloadString(url + "MeterAPI" + "/" + id);
        //            if (s != null)
        //            {
        //                meter = JsonConvert.DeserializeObject<Meter>(s);
        //                if (meter.replacedby != null)
        //                {
        //                    if (meter.replacedby != 0)
        //                    {
        //                        meter.tagforsubmeter = true;
        //                        meter.parentcmrmeterid = Convert.ToString(meter.replacedby);
        //                    }
        //                    else
        //                    {
        //                        meter.tagforsubmeter = false;
        //                    }
        //                }
        //                else
        //                    meter.tagforsubmeter = false;
        //            }
        //            else
        //            {
        //                return View(new Meter());
        //            }
        //            //ViewBag.GroupId = new SelectList(metergroup, "Id", "GroupName", meter.GroupId);
        //            ViewBag.PhaseVal = new SelectList(prmglobal, "prmrecid", "prmvalue");
        //            ViewBag.MeterType = new SelectList(meterType, "ID", "MeterTypeName");
        //            ViewBag.Model = new SelectList(meterModel, "ID", "ModelName");
        //            ViewBag.ConsumptionType = new SelectList(prmglobal1, "prmidentifier", "prmvalue");
        //            ViewBag.mf = factor;

        //            List<prmglobal> Prmlocation = GetGlobalValuesLocationByuserid(); //GetGlobalValuesLocation();                   

        //            FillNewMeters(meter.ID);
        //            //ViewBag.CommunicationType = new SelectList(communicationType, "ID", "CommunicationType1");
        //            if (Prmlocation != null)
        //            {
        //                meter.LocationDp = Convert.ToInt32(meter.Location);
        //                ViewBag.fillLocation = new SelectList(Prmlocation, "prmvalue", "prmidentifier", Convert.ToInt32(meter.Location));
        //            }
        //            else
        //            {
        //                ViewBag.fillLocation = null;
        //            }
        //            ViewBag.ServiceCalculationFlag = new SelectList(new[] { new { ID = 1, Name = "evt_loadsurvey" }, new { ID = 2, Name = "query" } }, "ID", "Name", meter.caltype).ToList();

        //        }
        //        return View(meter);
        //    }
        //    catch (Exception ex)
        //    {
        //        new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
        //        return View(new Meter());
        //    }
        //}

        //private List<Meter> FillNewMeters(long MeterID)
        //{

        //    List<Meter> lstMeter = new List<Meter>();
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = client.GetAsync("meterapi/GetCustomerIDForTheMeter/" + MeterID).Result;
        //    string CsmrID = "";
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var data = response.Content.ReadAsStringAsync().Result;
        //        dynamic objID = JValue.Parse(data);
        //        CsmrID = Convert.ToString(objID.Data.result);

        //    }

        //    client = new HttpClient();
        //    client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIUrl"]);
        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));
        //    response = client.GetAsync("ConsumerMeterRelation/GetParentMeter/" + CsmrID + "/" + MeterID).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var meters = response.Content.ReadAsStringAsync().Result;
        //        dynamic objmeters = JValue.Parse(meters);
        //        if (objmeters.Data.result != null)
        //        {
        //            foreach (dynamic mt in objmeters.Data.result)
        //            {
        //                lstMeter.Add(new Meter() { ID = mt.id, MeterName = mt.serialno });
        //            }
        //        }
        //    }

        //    ViewBag.NewMeterID = new SelectList(lstMeter, "ID", "MeterName");
        //    return lstMeter;
        //}

        // POST: /Meter/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Meter _meter)
        {

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}meter/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _meter);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    Meter meter = await result.Content.ReadAsAsync<Meter>();
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
                    return View(_meter);
                }
            }
            //try
            //{

            //    string Jsonstr;

            //    using (WebClient client = new WebClient())
            //    {
            //        client.Headers.Add("Content-Type", "application/json");
            //        Jsonstr = client.UploadString(url + "meterapi" + "/" + id, "PUT", JsonConvert.SerializeObject(meter));
            //        dynamic dynamicdata = JValue.Parse(Jsonstr);
            //        Message = dynamicdata.Data.d;
            //        MessageType = dynamicdata.Data.e;
            //        return Json(new { d = Message, e = MessageType });

            //    }


            //}
            //catch (Exception ex)
            //{
            //    new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
            //    return null;
            //}
        }

        //
        // GET: /Meter/Delete/5
        [AccessCheck(IdParamName = "Meter/Index")]
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
            Meter meter = await GetMeter(id);
            await BindDropDown();           

            return View(meter);
        }

        //
        // POST: /Meter/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Meter _meter)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}meter/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    Meter meter = await result.Content.ReadAsAsync<Meter>();
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
                    return View(_meter);
                }
            }
            //try
            //{

            //    string Jsonstr;

            //    using (WebClient client = new WebClient())
            //    {
            //        client.Headers.Add("Content-Type", "application/json");
            //        Jsonstr = client.UploadString(url + "meterapi/Delete", "PUT", JsonConvert.SerializeObject(meter));
            //        dynamic dynamicdata = JValue.Parse(Jsonstr);
            //        Message = dynamicdata.Data.d;
            //        MessageType = dynamicdata.Data.e;
            //        return Json(new { d = Message, e = MessageType });

            //    }


            //}
            //catch (Exception ex)
            //{
            //    new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
            //    return null;
            //}
        }
        public ActionResult History()
        {
            return View();
        }
    }
}