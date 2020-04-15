using GridLogik.ViewModels;
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
    public class HeatRateUploadController : Controller
    {
        //
        // GET: /HeatRateUpload/
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection formCollection)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["Uploadfile"];
                    List<HeatRate> list = new List<HeatRate>();
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

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
                                HeatRate dcsg = new HeatRate();

                                dcsg.mgmw = Convert.ToDouble(workSheet.Cells[rowIterator, 1].Value.ToString());
                                dcsg.mheatrate = Convert.ToDouble(workSheet.Cells[rowIterator, 2].Value.ToString());
                                dcsg.mgenid = Convert.ToInt32(formCollection["drpstageid"]);
                                list.Add(dcsg);
                            }
                        }
                        if (list.Count > 0 && list != null)
                        {
                            list = list.OrderBy(x => x.mgmw).ToList();
                            string Jsonstr;
                            if (GetHeatRateValidation(list))
                            {
                                using (WebClient client = new WebClient())
                                {
                                    client.Headers.Add("Content-Type", "application/json");
                                    Jsonstr = client.UploadString(url + "HeatRUploadAPI", JsonConvert.SerializeObject(list));
                                    dynamic dynamicDCSG = JValue.Parse(Jsonstr);

                                    TempData["Msg"] = dynamicDCSG.Data.d;
                                    TempData["MsgType"] = dynamicDCSG.Data.e;
                                    return View("Index");

                                }
                            }
                            else
                            {
                                TempData["Msg"] = "The Product of MW & HeatRate value Should be in Incremental Order";
                                TempData["MsgType"] = "M";
                                return View("Index");
                            }
                        }
                        else
                        {

                            //MessageList objMsg = MessageRepository.GetMessage("13021", null);
                            TempData["Msg"] = "No valid Record Found for Uploading";
                            TempData["MsgType"] = "M";
                            return View("Index");
                        }
                    }
                    else
                    {
                        //MessageList objMsg = MessageRepository.GetMessage("13021", null);
                        TempData["Msg"] = "Invalid File Format";
                        TempData["MsgType"] = "E";
                        return View("Index");
                    }

                }
                else
                {
                    //MessageList objMsg = MessageRepository.GetMessage("13022", null);
                    TempData["Msg"] = "An Internal error occured. Please contact the System Administrator";
                    TempData["MsgType"] = "E";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                //MessageList objMsg = MessageRepository.GetMessage("13023", null);
                //TempData["Msg"] = objMsg.Msg_Text;
                //TempData["MsgType"] = objMsg.Msg_Type;
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return View("Index");
            }

        }
        public Boolean GetHeatRateValidation(List<HeatRate> list)
        {
            Boolean result = false;
            Double AMVAlue = 0;
            Double PReAMVAlue = 0;
            int count = 0;
            try
            {
                foreach (HeatRate drs in list)
                {


                    AMVAlue = Convert.ToDouble(drs.mheatrate) * Convert.ToDouble(drs.mgmw);
                    if (count == 0)
                    {
                        PReAMVAlue = AMVAlue;
                    }
                    else
                    {
                        if (PReAMVAlue > AMVAlue)
                        {
                            return false;
                        }
                        else
                        {
                            PReAMVAlue = AMVAlue;
                        }

                    }
                    count++;

                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }
            return result;
        }
        public FileResult Download(string id)
        {
            try
            {
                string FileName = GetFileName();
                return File(FileName, System.Web.MimeMapping.GetMimeMapping(FileName), "HeatRateUpload.xlsx");
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
            return dirInfo.FullName + @"\" + "HeatRateUpload.xlsx";
        }
    }
}