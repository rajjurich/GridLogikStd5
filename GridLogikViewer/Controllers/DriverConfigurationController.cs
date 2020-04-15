using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Reflection;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class DriverConfigurationController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        #region to fill data in IMapping dropdown
        private List<PropertyInfo> InstaceDataList()
        {
            InstanceData instaceData = new InstanceData();
            System.Reflection.PropertyInfo[] array = instaceData.GetType().GetProperties();
            List<PropertyInfo> list = array.ToList();
            list.RemoveRange(0, 5);
            return list;
        }
        #endregion

        #region to fill data in Meter Model dropdown
        private List<MeterModel> MeterModelList()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            return meterModel;
        }
        #endregion

        #region this is list for DataType dropdown
        private IEnumerable<SelectListItem> DataTypeList()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "UNIT16", Text = "UNIT16" },
                new { ID = "INT16", Text = "INT16" },
                new { ID = "UNIT32", Text = "UNIT32" },
                 new { ID = "INT32", Text = "INT32" },
                   new { ID = "FLOAT32", Text = "FLOAT32" },
            }, "ID", "Text");
            return list;
        }
        #endregion

        #region this method uset to fill form data into model list
        private Tuple<List<MemoryMap_Addressdetails>, bool> AddressList(FormCollection form, List<MemoryMap_Addressdetails> addressList)
        {
            int counter = 0;
            int numerOfExistingRecords = addressList.Count;
            if (numerOfExistingRecords == 0)
                //Gives number of rows added while adding
                counter = ((form.Count) - 13) / 5;
            else
                //gives number of row excluding existion rows while editing
                counter = ((form.Count - 15) - (numerOfExistingRecords * 7)) / 5;
            bool errorFlag = false;

            //putting all rows into the list
            for (int loop = 1; loop <= counter; loop++)
            {
                MemoryMap_Addressdetails model = new MemoryMap_Addressdetails();
                if (numerOfExistingRecords == 0)
                    model.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                else
                    model.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                if (Request.Form["name" + (loop + numerOfExistingRecords)].Count() != 0)//to check all rows are filled or not
                    model.Address = Request.Form["name" + (loop + numerOfExistingRecords)];
                else
                {
                    errorFlag = true;
                    break;
                }
                if (Request.Form["parameter" + (loop + numerOfExistingRecords)].Count() != 0)
                    model.ParameterName = Request.Form["parameter" + (loop + numerOfExistingRecords)];
                else
                {
                    errorFlag = true;
                    break;
                }
                if (Request.Form["datatype" + (loop + numerOfExistingRecords)].Count() != 0)
                    model.DataType = Request.Form["datatype" + (loop + numerOfExistingRecords)];
                else
                {
                    errorFlag = true;
                    break;
                }
                if (Request.Form["instancedata" + (loop + numerOfExistingRecords)].Count() != 0)
                    model.InstanceDataMapping = Request.Form["instancedata" + (loop + numerOfExistingRecords)];
                else
                {
                    errorFlag = true;
                    break;
                }
                if (Request.Form["multification" + (loop + numerOfExistingRecords)].Count() != 0)
                    model.MultiplicationFactor = Convert.ToDouble(Request.Form["multification" + (loop + numerOfExistingRecords)]);
                else
                {
                    errorFlag = true;
                    break;
                }


                addressList.Add(model);
            }
            return new Tuple<List<MemoryMap_Addressdetails>, bool>(addressList, errorFlag);
        }
        #endregion

        // GET: /MeterModel/
        public ActionResult Index()
        {
            List<MeterModel> meterModel = new List<MeterModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "meterModelAPI");
                meterModel = JsonConvert.DeserializeObject<List<MeterModel>>(s);
            }
            return View(meterModel);
        }

        #region this method is used for Get details on ADD,DELETE and View Action
        public ActionResult GetMeterDetails(Int32? id, string actionName)
        {
            MemoryMap_Addressdetails model = new MemoryMap_Addressdetails();

            //Drowpdown data for IMapping
            ViewBag.InstanceParameterID = new SelectList(InstaceDataList(), "Name", "Name");

            //Drowpdown data for MeterModel
            model.ListMeterModel = MeterModelList();
            if (id != 0 || id != null)
                model.ModelID = Convert.ToInt32(id);

            //Drowpdown data for DataType
            ViewBag.DataTypelist = DataTypeList();

            if (actionName == "Add" || actionName == "" || actionName == null)
                return View("AddDriverDetails", model);

            //list of drivers details by id
            using (WebClient client = new WebClient())
            {
                // string s = client.DownloadString(url + "MemoryMap_AddressdetailsAPI/GetMemmoryMapAddress" + "/" + id);
              //  string s = client.DownloadString(url + "MemoryMap_AddressdetailsAPI/GetList" + "/" + id);
                //string s = client.DownloadString(url + "MemoryMap_AddressdetailsAPI" + "/" + id);
                string s1 = client.DownloadString(url + "MemoryMap_AddressdetailsAPI/GetMemmoryMapAddress" + "/" + id);
                string s2 = client.DownloadString(url + "MemoryMap_AddressdetailsAPI/GetMemmoryAddress" + "/" + id);
                if (s1 != null)
                {
                    model.ListAddressDetails1 = JsonConvert.DeserializeObject<List<MemoryMap_Addressdetails>>(s1);
                }

                if (s2 != null)
                {
                    model.ListAddressDetails2 = JsonConvert.DeserializeObject<List<MemoryMap_Addressdetails>>(s2);


                }
                else
                {
                    return View(new MemoryMap_Addressdetails());
                }
            }

            if (actionName == "Edit")
                return View("EditDriverDetails", model);
            else if (actionName == "Read")
                return View("ReadDriverDetails", model);
            else
                return View("DeleteDriverDetails", model);
        }
        #endregion

        #region this method is for get values on EDIT action
        public ActionResult GetMeterDetailsForEdit(Int32? id)
        {
            MemoryMap_Addressdetails model = new MemoryMap_Addressdetails();
       

            //Drowpdown data for IMapping
            ViewBag.InstanceParameterID = new SelectList(InstaceDataList(), "Name", "Name");

            //Drowpdown data for MeterModel
            model.ListMeterModel = MeterModelList();
            if (id != 0 || id != null)
                model.ModelID = Convert.ToInt32(id);

            //Drowpdown data for DataType
            ViewBag.DataTypelist = DataTypeList();

            //list of drivers details by id
            using (WebClient client = new WebClient())
            {
                string s1 = client.DownloadString(url + "MemoryMap_AddressdetailsAPI/GetMemmoryMapAddress" + "/" + id);
                string s2 = client.DownloadString(url + "MemoryMap_AddressdetailsAPI/GetMemmoryAddress" + "/" + id);
                if (s1 != null)
                {
                    model.ListAddressDetails1 = JsonConvert.DeserializeObject<List<MemoryMap_Addressdetails>>(s1);
                }

                if (s2 != null)
                {
                    model.ListAddressDetails2 = JsonConvert.DeserializeObject<List<MemoryMap_Addressdetails>>(s2);
                    
                    
                }
                else
                {
                    return View(new MemoryMap_Addressdetails());
                }
            }
            return View("EditDriverDetails", model);
        }
        #endregion

        #region this method is used for Saving data on ADD action
        [HttpPost]
        public ActionResult SaveDriverDetails(FormCollection form)
        {
            try
            {
                 MemoryMap_Addressdetails model = new MemoryMap_Addressdetails();
                 List<MemoryMap_Addressdetails> addressList1 = new List<MemoryMap_Addressdetails>();
                List<MemoryMap_Addressdetails> addressList = new List<MemoryMap_Addressdetails>();
                //convert form data into model list and check for null entry
                
               
               Tuple<List<MemoryMap_Addressdetails>, bool> addressDetails = AddressList(form, addressList);
               

               MemoryMap_Range m = new MemoryMap_Range();
               m.StartAddress = form["Day"];
               m.EndAddress = form["Sec"];
               m.CountAddress = Convert.ToString(Convert.ToInt32(form["Sec"]) - Convert.ToInt32(form["Day"]));
               m.RangeType = "Static";
               m.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                
             
               if (addressDetails.Item2)
               {
                   var jsonData = new
                   {
                       error = true,
                       message = "Please fill all details",
                   };
                   return Json(jsonData);
               }

               using (WebClient client = new WebClient())
               {
                  
                   client.Headers.Add("Content-Type", "application/json");



                   string memorymap = client.UploadString(url + "MemoryMap_RangeAPI/PostMemoryRange", JsonConvert.SerializeObject(m));

                   MemoryMap_Range range = JsonConvert.DeserializeObject<MemoryMap_Range>(memorymap);
                   MemoryMap_Addressdetails Day = new MemoryMap_Addressdetails();
                   Day.Address = form["Day"];
                   Day.DataType = "UNIT16";
                   Day.InstanceDataMapping = "Date_Time";
                   Day.MultiplicationFactor = 1;
                   Day.ParameterName = "DateTime";
                   Day.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                   Day.RangeId = range.ID;



                   addressList1.Add(Day); //********


                   MemoryMap_Addressdetails Month = new MemoryMap_Addressdetails();
                   Month.Address = form["Month"];
                   Month.DataType = "UNIT16";
                   Month.InstanceDataMapping = "Date_Time";
                   Month.MultiplicationFactor = 1;
                   Month.ParameterName = "DateTime";
                   Month.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                   Month.RangeId = range.ID;
                   addressList1.Add(Month); //********



                   MemoryMap_Addressdetails Year = new MemoryMap_Addressdetails();
                   Year.Address = form["Year"];
                   Year.DataType = "UNIT16";
                   Year.InstanceDataMapping = "Date_Time";
                   Year.MultiplicationFactor = 1;
                   Year.ParameterName = "DateTime";
                   Year.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                   Year.RangeId = range.ID;
                   addressList1.Add(Year);



                   MemoryMap_Addressdetails Hr = new MemoryMap_Addressdetails();
                   Hr.Address = form["Hr"];
                   Hr.DataType = "UNIT16";
                   Hr.InstanceDataMapping = "Date_Time";
                   Hr.MultiplicationFactor = 1;
                   Hr.ParameterName = "DateTime";
                   Hr.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                   Hr.RangeId = range.ID;
                   addressList1.Add(Hr);




                   MemoryMap_Addressdetails Min = new MemoryMap_Addressdetails();
                   Min.Address = form["Min"];
                   Min.DataType = "UNIT16";
                   Min.InstanceDataMapping = "Date_Time";
                   Min.MultiplicationFactor = 1;
                   Min.ParameterName = "DateTime";
                   Min.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                   Min.RangeId = range.ID;
                   addressList1.Add(Min);



                   MemoryMap_Addressdetails Sec = new MemoryMap_Addressdetails();
                   Sec.Address = form["Sec"];
                   Sec.DataType = "UNIT16";
                   Sec.InstanceDataMapping = "Date_Time";
                   Sec.MultiplicationFactor = 1;
                   Sec.ParameterName = "DateTime";
                   Sec.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                   Sec.RangeId = range.ID;
                   addressList1.Add(Sec);

               }

              
              

                   using (WebClient client = new WebClient())
                   {
                       client.Headers.Add("Content-Type", "application/json");
                     //  string s = client.UploadString(url + "MemoryMap_RmoangeAPI", JsonConvert.SerializeObject(addressDetails.Item1));
                       string memorymap1 = client.UploadString(url + "MemoryMap_AddressdetailsAPI/PostMemoryMapAddressDetails", JsonConvert.SerializeObject(addressList1.ToArray()));
                      
                   }
          
                 

                   using (WebClient client = new WebClient())
                   {
                       client.Headers.Add("Content-Type", "application/json");



                       string s = client.UploadString(url + "MemoryMap_RangeAPI", JsonConvert.SerializeObject(addressDetails.Item1));


                   }



               
                var jsonDataSuceess = new
                {
                    success = true,
                    message = "Data saved successfully",
                };
                return Json(jsonDataSuceess);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

       
        #endregion

        #region this method is used for saving data on EDIT action
        [HttpPost]
        public ActionResult EditDriverDetails(MemoryMap_Addressdetails model, FormCollection form)
        {

            MemoryMap_Addressdetails model1 = new MemoryMap_Addressdetails();
            List<MemoryMap_Addressdetails> addressList1 = new List<MemoryMap_Addressdetails>();
            try
            {
                Tuple<List<MemoryMap_Addressdetails>, bool> addressDetails = AddressList(form, model.ListAddressDetails1);



                MemoryMap_Range m = new MemoryMap_Range();
                m.StartAddress = form["ListAddressDetails2[0].Address"];
                m.EndAddress = form["ListAddressDetails2[1].Address"];
                m.CountAddress = Convert.ToString(Convert.ToInt32(form["ListAddressDetails2[1].Address"]) - Convert.ToInt32(form["ListAddressDetails2[0].Address"]));
                m.RangeType = "Static";
               // m.ModelID = Convert.ToInt32(Request.Form["metermodel.modelname"]);
                m.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                m.ID = Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                if (addressDetails.Item2)
                {
                    var jsonData = new
                    {
                        error = true,
                        message = "Please fill all details",
                    };
                    return Json(jsonData);
                }

                using (WebClient client = new WebClient())
                {

                    client.Headers.Add("Content-Type", "application/json");



                    string memorymap = client.UploadString(url + "MemoryMap_RangeAPI/" + m.ID, "PUT", JsonConvert.SerializeObject(m));

                   // MemoryMap_Range range = JsonConvert.DeserializeObject<MemoryMap_Range>(memorymap);
                    MemoryMap_Addressdetails Day = new MemoryMap_Addressdetails();
                    Day.Address = form["ListAddressDetails2[0].Address"];
                    Day.DataType = "UNIT16";
                    Day.InstanceDataMapping = "Date_Time";
                    Day.MultiplicationFactor = 1;
                    Day.ParameterName = "DateTime";
                    Day.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                    //Day.RangeId = range.ID;
                   Day.RangeId= Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                    Day.ID = Convert.ToInt32(form["ListAddressDetails2[0].ID"]);




                    addressList1.Add(Day); //********


                    MemoryMap_Addressdetails Month = new MemoryMap_Addressdetails();
                    Month.Address = form["ListAddressDetails2[1].Address"];
                    Month.DataType = "UNIT16";
                    Month.InstanceDataMapping = "Date_Time";
                    Month.MultiplicationFactor = 1;
                    Month.ParameterName = "DateTime";
                    Month.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                     Month.RangeId = Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                    addressList1.Add(Month); //********
                    Month.ID = Convert.ToInt32(form["ListAddressDetails2[1].ID"]);



                    MemoryMap_Addressdetails Year = new MemoryMap_Addressdetails();
                    Year.Address = form["ListAddressDetails2[2].Address"];
                    Year.DataType = "UNIT16";
                    Year.InstanceDataMapping = "Date_Time";
                    Year.MultiplicationFactor = 1;
                    Year.ParameterName = "DateTime";
                    Year.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                    Year.RangeId =Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                    Year.ID = Convert.ToInt32(form["ListAddressDetails2[2].ID"]);
                    addressList1.Add(Year);



                    MemoryMap_Addressdetails Hr = new MemoryMap_Addressdetails();
                    Hr.Address = form["ListAddressDetails2[3].Address"];
                    Hr.DataType = "UNIT16";
                    Hr.InstanceDataMapping = "Date_Time";
                    Hr.MultiplicationFactor = 1;
                    Hr.ParameterName = "DateTime";
                    Hr.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                     Hr.RangeId = Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                    Hr.ID = Convert.ToInt32(form["ListAddressDetails2[3].ID"]);
                    addressList1.Add(Hr);




                    MemoryMap_Addressdetails Min = new MemoryMap_Addressdetails();
                    Min.Address = form["ListAddressDetails2[4].Address"];
                    Min.DataType = "UNIT16";
                    Min.InstanceDataMapping = "Date_Time";
                    Min.MultiplicationFactor = 1;
                    Min.ParameterName = "DateTime";
                    Min.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                     Min.RangeId = Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                    Min.ID = Convert.ToInt32(form["ListAddressDetails2[4].ID"]);
                    addressList1.Add(Min);



                    MemoryMap_Addressdetails Sec = new MemoryMap_Addressdetails();
                    Sec.Address = form["ListAddressDetails2[5].Address"];
                    Sec.DataType = "UNIT16";
                    Sec.InstanceDataMapping = "Date_Time";
                    Sec.MultiplicationFactor = 1;
                    Sec.ParameterName = "DateTime";
                    Sec.ModelID = Convert.ToInt32(Request.Form["ModelId"]);
                    Sec.ID = Convert.ToInt32(form["ListAddressDetails2[5].ID"]);
                    Sec.RangeId = Convert.ToInt32(form["ListAddressDetails2[0].MemoryMap_Range.ID"]);
                    addressList1.Add(Sec);

                }

              
              
               


                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    //  string s = client.UploadString(url + "MemoryMap_RmoangeAPI", JsonConvert.SerializeObject(addressDetails.Item1));
                    string memorymap1 = client.UploadString(url + "MemoryMap_AddressdetailsAPI/0","PUT", JsonConvert.SerializeObject(addressList1.ToArray()));

                }



                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");



                    string s = client.UploadString(url + "MemoryMap_RangeAPI", JsonConvert.SerializeObject(addressDetails.Item1));


                }



                var jsonDataSuceess = new
                {
                    success = true,
                    message = "Data saved successfully",
                };
                return Json(jsonDataSuceess);
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        #endregion

        #region this is metho is used to delete data
        [HttpPost]
        public ActionResult DeleteDriverDetails(int id)
        {
            try
            {
                MemoryMap_Range metertype = new MemoryMap_Range();
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "MemoryMap_RangeAPI" + "/" + id, "DELETE", JsonConvert.SerializeObject(metertype));
                    if (s != null)
                    {
                        metertype = JsonConvert.DeserializeObject<MemoryMap_Range>(s);
                        var jsonDataSuceess = new
                        {
                            success = true,
                            message = "Data Deleted successfully",
                        };
                        return Json(jsonDataSuceess);
                    }

                      
                    else
                    {
                        return View(new MemoryMap_Range());
                    }
                }

              
                //return RedirectToAction("Index");
            }
            catch
            {
                return View(new MemoryMap_Range());
            }
        }
        #endregion
    }
}