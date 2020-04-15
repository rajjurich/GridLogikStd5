using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

using System.Net;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GridLogikViewer.Filters;
using Newtonsoft.Json.Linq;
using GridLogikViewer.GridLogikViewerModels;
using GridLogik.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class GroupConfigController : BaseController
    {

        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        string Message, MessageType = "";
        private List<PropertyInfo> InstaceDataList()
        {
            InstanceData instaceData = new InstanceData();
            System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 10);
            list.RemoveAt(list.Count - 1);
            return list;
        }
        //
        // GET: /GroupConfig/
        [AccessCheck(IdParamName = "GroupConfig/Index")]
        public async Task<ActionResult> Index()
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
            IEnumerable<MeterGroup> meterGroups;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}metergroup/GetMeterGroupConfiguration/", _uri);

                var result = await client.GetAsync(uri);

                meterGroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();

            }

            //GetMeterGroupConfiguration
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "GroupConfigAPI");
            //    groupconfig = JsonConvert.DeserializeObject<List<MeterGroup>>(s);
            //}
            return View(meterGroups);
        }

        //
        // GET: /GroupConfig/
        public async Task<ActionResult> Create()
        {
            IEnumerable<SelectListItem> _metergroups;
            IEnumerable<SelectListItem> _meters;

            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}meter", _uri);

                var result = await client.GetAsync(uri);

                var meters = await result.Content.ReadAsAsync<IEnumerable<Meter>>();
                _meters = meters.Select(m => new SelectListItem
                {
                    Value = m.ID.ToString(),
                    Text = m.MeterName
                });

                uri = string.Format("{0}metergroup/GetMeterGroupByUserid/{1}", _uri, Convert.ToInt64(HttpContext.Session["usrrecid"]));
                result = await client.GetAsync(uri);
                var metergroups = await result.Content.ReadAsAsync<IEnumerable<MeterGroup>>();
                _metergroups = metergroups.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.GroupName
                });
                ViewBag.ID = _meters;
                ViewBag.GrpID = _metergroups;
            }

            //using (WebClient client = new WebClient())
            //{

            //    //string m = client.DownloadString(url + "MeterAPI");
            //    string m = client.DownloadString(_uri + "MeterAPI/GetMeterList");
            //    string mgrp = client.DownloadString(_uri + "MeterGroupAPI?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"]));
            //   // meter = JsonConvert.DeserializeObject<List<Meter>>(m);

            //    dynamic objMeter = JValue.Parse(m);
            //    if (objMeter.Data.result != null)
            //    {
            //        foreach (dynamic objmeter in objMeter.Data.result)
            //        {
            //            meter.Add(new Meter { ID = objmeter.id, MeterName = objmeter.metername });
            //        }
            //    }
            //    metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mgrp);
            //}
            
           
            ViewBag.InstanceParameterID = new SelectList(InstaceDataList(), "Name", "Name");
            return View();
        }


        //
        // POST: /GroupConfig/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupConfiguration groupconfig)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    //List<MeterGroup> metergroup = new List<MeterGroup>();
                    //List<Meter> meter = new List<Meter>();
                    //using (WebClient client = new WebClient())
                    //{
                    //    string m = client.DownloadString(url + "MeterAPI");
                    //    string mgrp = client.DownloadString(url + "MeterGroupAPI");
                    //    meter = JsonConvert.DeserializeObject<List<Meter>>(m);
                    //    metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mgrp);
                    //}
                    //ViewBag.ID = new SelectList(meter, "Id", "MeterName");
                    //ViewBag.GrpID = new SelectList(metergroup, "Id", "GroupName");
                    //ViewBag.InstanceParameterID = new SelectList(InstaceDataList(), "Name", "Name");
                    //return RedirectToAction("Index");


                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("Content-Type", "application/json");
                        string s = client.UploadString(_uri + "GroupConfigAPI", JsonConvert.SerializeObject(groupconfig));
                        TempData["Success"] = "Record Added Successfully!";
                    }
                }
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "groupconfiguration");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /GroupConfig/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                GroupConfiguration model = new GroupConfiguration();
                List<Meter> selectedMeterList = new List<Meter>();
                //List<MeterGroup> metergroup = new List<MeterGroup>();
                List<Meter> meter = new List<Meter>();
                List<PropertyInfo> selectedparameterList = new List<PropertyInfo>();
                InstanceData instance = new InstanceData();
                List<PropertyInfo> parameterList = new List<PropertyInfo>();

                List<GroupConfiguration> grpconfig = new List<GroupConfiguration>();

                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(_uri + "GroupConfigAPI" + "/" + id);
                    string m = client.DownloadString(_uri + "MeterAPI/GetMeterList");
                    string mgrp = client.DownloadString(_uri + "MeterGroupAPI?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"]));
                    if (s != null)
                    {
                        grpconfig = JsonConvert.DeserializeObject<List<GroupConfiguration>>(s);
                       // meter = JsonConvert.DeserializeObject<List<Meter>>(m);

                        dynamic objMeter = JValue.Parse(m);
                        if (objMeter.Data.result != null)
                        {
                            foreach (dynamic objmeter in objMeter.Data.result)
                            {
                                meter.Add(new Meter { ID = objmeter.id, MeterName = objmeter.metername });
                            }
                        }

                        parameterList = InstaceDataList();
                        // metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mgrp);
                        foreach (var item in grpconfig)
                        {
                            Meter selectedMeter = new Meter();
                            selectedMeter.ID = Convert.ToInt16(item.MeterId);
                            selectedMeter.MeterName = item.MeterName;
                            if (!selectedMeterList.Any(mo => mo.MeterName == item.MeterName))
                            {
                                selectedMeterList.Add(selectedMeter);
                            }
                            PropertyInfo propertyInfo = instance.GetType().GetProperty(item.Parameter);
                            selectedparameterList.Add(propertyInfo);
                            if (meter.Any(mo => mo.MeterName == item.MeterName))
                            {
                                var obj = meter.First(x => x.ID == item.MeterId);
                                meter.Remove(obj);
                            }

                            // meter.Skip().Equals(selectedMeter);
                            model.GroupName = item.GroupName;
                            model.Id = item.Id;
                            parameterList.Remove(propertyInfo);
                        }
                        ViewBag.SelectedParameter = new SelectList(selectedparameterList.Distinct(), "Name", "Name");
                        ViewBag.SelectedMeters = new SelectList(selectedMeterList.Distinct(), "Id", "MeterName");
                        ViewBag.ID = new SelectList(meter, "Id", "MeterName");
                        //ViewBag.GrpID = new SelectList(metergroup, "Id", "GroupName");
                        ViewBag.InstanceParameterID = new SelectList(parameterList, "Name", "Name");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // return RedirectToAction("Index");
                throw ex;
            }
        }

        //
        // POST: /GroupConfig/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GroupConfiguration groupconfig)
        {
            try
            {
                // TODO: Add update logic here
                //if (!ModelState.IsValid)
                //{
                //    return View(groupconfig);
                //}
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(_uri + "GroupConfigAPI" + "/" + id, "PUT", JsonConvert.SerializeObject(groupconfig));
                    TempData["Success"] = "Record Updated Successfully!";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }




        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                GroupConfiguration model = new GroupConfiguration();
                List<Meter> selectedMeterList = new List<Meter>();
                //List<MeterGroup> metergroup = new List<MeterGroup>();
                List<Meter> meter = new List<Meter>();
                List<PropertyInfo> selectedparameterList = new List<PropertyInfo>();
                InstanceData instance = new InstanceData();
                List<PropertyInfo> parameterList = new List<PropertyInfo>();

                List<GroupConfiguration> grpconfig = new List<GroupConfiguration>();

                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(_uri + "GroupConfigAPI" + "/" + id);
                    string m = client.DownloadString(_uri + "MeterAPI/GetMeterList");
                    string mgrp = client.DownloadString(_uri + "MeterGroupAPI?Userid=" + Convert.ToInt64(HttpContext.Session["usrrecid"]));
                    if (s != null)
                    {
                        grpconfig = JsonConvert.DeserializeObject<List<GroupConfiguration>>(s);
                        //meter = JsonConvert.DeserializeObject<List<Meter>>(m);

                        dynamic objMeter = JValue.Parse(m);
                        if (objMeter.Data.result != null)
                        {
                            foreach (dynamic objmeter in objMeter.Data.result)
                            {
                                meter.Add(new Meter { ID = objmeter.id, MeterName = objmeter.metername });
                            }
                        }


                        parameterList = InstaceDataList();
                        // metergroup = JsonConvert.DeserializeObject<List<MeterGroup>>(mgrp);
                        foreach (var item in grpconfig)
                        {
                            Meter selectedMeter = new Meter();
                            selectedMeter.ID = Convert.ToInt16(item.MeterId);
                            selectedMeter.MeterName = item.MeterName;
                            if (!selectedMeterList.Any(mo => mo.MeterName == item.MeterName))
                            {
                                selectedMeterList.Add(selectedMeter);
                            }
                            PropertyInfo propertyInfo = instance.GetType().GetProperty(item.Parameter);
                            selectedparameterList.Add(propertyInfo);
                            if (meter.Contains(selectedMeter))
                            {
                                var obj = meter.First(x => x.ID == item.MeterId);
                                meter.Remove(obj);
                            }
                            // meter.Skip().Equals(selectedMeter);
                            model.GroupName = item.GroupName;
                            model.Id = item.Id;
                            parameterList.Remove(propertyInfo);
                        }
                        ViewBag.SelectedParameter = new SelectList(selectedparameterList.Distinct(), "Name", "Name");
                        ViewBag.SelectedMeters = new SelectList(selectedMeterList.Distinct(), "Id", "MeterName");
                        ViewBag.ID = new SelectList(meter, "Id", "MeterName");
                        //ViewBag.GrpID = new SelectList(metergroup, "Id", "GroupName");
                        ViewBag.InstanceParameterID = new SelectList(parameterList, "Name", "Name");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }





        // POST: /GroupConfig/Delete/5
        // [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, GroupConfiguration gc)
        {
            GroupConfiguration groupconfig = new GroupConfiguration();
            try
            {
                string Jsonstr;
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.UploadString(_uri + "GroupConfigAPI/Delete" + "/" + id, "DELETE", JsonConvert.SerializeObject(groupconfig));
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