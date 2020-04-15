using GridLogikViewer.GridLogikViewerModels;
using GridLogikViewer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    public class ModCostLimitController : Controller
    {
        //
        // GET: /ModCostLimit/
        //public ActionResult Index()
        //{
        //    return View();
        //}
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["Uploadfile"];
                    List<modlimit> list = new List<modlimit>();
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        Boolean validationFlag = true;
                        string Messgae = string.Empty;
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            TimeSpan tspan = new TimeSpan(00, 00, 00);
                            int i = 1;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                tspan = tspan.Add(new TimeSpan(00, 15, 00));

                                if (i == 96)
                                {
                                    tspan = new TimeSpan(00, 00, 00);
                                }
                                i++;
                                modlimit dcsg = new modlimit();
                                dcsg.mblockno = Convert.ToInt64(workSheet.Cells[rowIterator, 1].Value.ToString());
                                dcsg.moperation = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value.ToString());
                                dcsg.mminsch = Convert.ToDouble(workSheet.Cells[rowIterator, 3].Value.ToString());
                                dcsg.mmaxsch = Convert.ToDouble(workSheet.Cells[rowIterator, 4].Value.ToString());
                                //dcsg.fuelcost = Convert.ToDecimal(0); //Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value.ToString());

                                //string startdate = GetFinaldate(formCollection["txtFrmDate"], Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());
                                //string enddate = GetFinaldate(mtrf.enddate, Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());

                                //dcsg.tstamp = DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//.Add(tspan) ;// Convert.ToDateTime(startdate); // DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(formCollection["txtFrmDate"]);
                                //dcsg.stageid = Convert.ToInt32(formCollection["txtstageid"]);
                                dcsg.mgenid = Convert.ToInt32(formCollection["drpstageid"]);
                                //dcsg.revision = Convert.ToInt32(formCollection["txtrevisionid"]);

                                if (dcsg.moperation == 0 || dcsg.moperation == 1)
                                {
                                    if (dcsg.mminsch < 0 || dcsg.mmaxsch < 0)
                                    {
                                        validationFlag = false;
                                        Messgae = "Max schedule and Min Schedule cannot be Negative.";
                                        break;
                                    }
                                    if (dcsg.mminsch > dcsg.mmaxsch)
                                    {
                                        validationFlag = false;
                                        Messgae = "Max schedule cannot be lower than Min Schedule.";
                                        break;
                                    }
                                    if (dcsg.mminsch == 0 && dcsg.mmaxsch == 0 && dcsg.moperation == 1)
                                    {
                                        validationFlag = false;
                                        Messgae = "if Block is in operation then max and min can not be 0.";
                                        break;
                                    }
                                }
                                else
                                {
                                    validationFlag = false;
                                    Messgae = "Invalid Operation Mode. Please Enter either 0 or 1.";
                                    break;
                                }
                                list.Add(dcsg);
                            }
                        }
                        if (list.Count > 0 && list != null && validationFlag)
                        {
                            string Jsonstr;

                            using (WebClient client = new WebClient())
                            {
                                client.Headers.Add("Content-Type", "application/json");
                                Jsonstr = client.UploadString(url + "ModCostLimitAPI", JsonConvert.SerializeObject(list));
                                dynamic dynamicDCSG = JValue.Parse(Jsonstr);

                                TempData["Msg"] = dynamicDCSG.Data.d;
                                TempData["MsgType"] = dynamicDCSG.Data.e;
                                return View();

                            }
                        }
                        else
                        {
                            if (!validationFlag)
                            {
                                // MessageList objMsg = MessageRepository.GetMessage("13021", null);
                                TempData["Msg"] = Messgae;
                                TempData["MsgType"] = "E";
                                return View();
                            }
                            else
                            {

                                TempData["Msg"] = "No Valid Record found for Uploading....!!!!";
                                TempData["MsgType"] = "M";
                                return View();
                            }
                        }
                    }
                    else
                    {
                        TempData["Msg"] = "Please use Valid File format for Uploading";
                        TempData["MsgType"] = "E";
                        return View();
                    }

                }
                else
                {
                    TempData["Msg"] = "Error occur During  Uploading.Please Try again Later";
                    TempData["MsgType"] = "E";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Error occur During  Uploading.Please Try again Later";
                TempData["MsgType"] = "E";
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return View();
            }

        }
        public FileResult Download(string id)
        {
            try
            {
                string FileName = GetFileName();
                return File(FileName, System.Web.MimeMapping.GetMimeMapping(FileName), "limitUpload.xlsx");
            }
            catch (Exception ex)
            {
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return null;
            }
        }
        public string GetFileName()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/Template"));
            return dirInfo.FullName + @"\" + "limitUpload.xlsx";
        }
        public ActionResult Upload()
        {
            return View("Upload");
        }
    }
}