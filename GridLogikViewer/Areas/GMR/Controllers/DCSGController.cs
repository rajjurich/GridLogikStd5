using GridLogikViewer.Areas.ABTScreen;
using GridLogikViewer.Areas.ABTScreen.Models;
using GridLogikViewer.GridLogikViewerModels;
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

namespace GridLogikViewer.Areas.GMR.Controllers
{
    public class DCSGController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //string Message, MessageType = "";


        //////[HttpPost]
        //////public ActionResult CheckUpload(FormCollection formCollection)
        //////{
        //////    string Jsonstr;
        //////    using (WebClient client = new WebClient())
        //////    {
        //////        client.Headers.Add("Content-Type", "application/json");

        //////        Jsonstr = client.UploadString(url + "DCSGAPI/CheckUpload", JsonConvert.SerializeObject(Convert.ToDateTime(formCollection[2])));
        //////        dynamic dynamicDCSG = JValue.Parse(Jsonstr);

        //////        TempData["Msg"] = dynamicDCSG.Data.d;
        //////        TempData["MsgType"] = dynamicDCSG.Data.e;
        //////        return View();

        //////    }
        //////    return View();
        //////}

        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["Uploadfile"];
                    List<dcsg> list = new List<dcsg>();
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

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                dcsg dcsg = new dcsg();
                                dcsg.blockno = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString());
                                dcsg.sgvalue = Convert.ToDecimal(workSheet.Cells[rowIterator, 2].Value.ToString());
                                dcsg.dcvalue = Convert.ToDecimal(workSheet.Cells[rowIterator, 3].Value.ToString());
                                dcsg.fuelcost = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value.ToString());

                                string startdate = GetFinaldate(formCollection["txtFrmDate"], Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());
                                //string enddate = GetFinaldate(mtrf.enddate, Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());

                                dcsg.tstamp = DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(startdate); // DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(formCollection["txtFrmDate"]);
                                dcsg.stageid = Convert.ToInt32(formCollection["txtstageid"]);
                                dcsg.revision = Convert.ToInt32(formCollection["txtrevisionid"]);
                                list.Add(dcsg);
                            }
                        }
                        if (list.Count > 0 && list != null)
                        {
                            string Jsonstr;

                            using (WebClient client = new WebClient())
                            {
                                client.Headers.Add("Content-Type", "application/json");
                                Jsonstr = client.UploadString(url + "DCSGAPI", JsonConvert.SerializeObject(list));
                                dynamic dynamicDCSG = JValue.Parse(Jsonstr);

                                TempData["Msg"] = dynamicDCSG.Data.d;
                                TempData["MsgType"] = dynamicDCSG.Data.e;
                                return View();

                            }
                        }
                        else
                        {

                            MessageList objMsg = MessageRepository.GetMessage("13021", null);
                            TempData["Msg"] = objMsg.Msg_Text;
                            TempData["MsgType"] = objMsg.Msg_Type;
                            return View();
                        }
                    }
                    else
                    {
                        MessageList objMsg = MessageRepository.GetMessage("13021", null);
                        TempData["Msg"] = objMsg.Msg_Text;
                        TempData["MsgType"] = objMsg.Msg_Type;
                        return View();
                    }

                }
                else
                {
                    MessageList objMsg = MessageRepository.GetMessage("13022", null);
                    TempData["Msg"] = objMsg.Msg_Text;
                    TempData["MsgType"] = objMsg.Msg_Type;
                    return View();
                }
            }
            catch (Exception ex)
            {
                MessageList objMsg = MessageRepository.GetMessage("13023", null);
                TempData["Msg"] = objMsg.Msg_Text;
                TempData["MsgType"] = objMsg.Msg_Type;
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return View();
            }

        }
        public FileResult Download(string id)
        {
            try
            {
                string FileName = GetFileName();
                return File(FileName, System.Web.MimeMapping.GetMimeMapping(FileName), "DCSG.xlsx");
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
            return dirInfo.FullName + @"\" + "DCSG.xlsx";
        }

        public ActionResult Upload()
        {
            return View();
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
