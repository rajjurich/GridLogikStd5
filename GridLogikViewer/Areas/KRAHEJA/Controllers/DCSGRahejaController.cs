using GridLogikViewer.Areas.ABTScreen;
using GridLogikViewer.Areas.ABTScreen.Models;
using GridLogikViewer.GridLogikViewerModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridLogikViewer.Areas.KRAHEJA.Controllers
{
    public class DCSGRahejaController : Controller
    {
        //
        String url = WebConfigurationManager.AppSettings["APIUrl"];
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
                                dcsg dcsg = new dcsg();
                                dcsg.blockno = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString());
                                dcsg.sgvalue = Convert.ToDecimal(workSheet.Cells[rowIterator, 2].Value.ToString());
                                dcsg.fuelcost = Convert.ToDecimal(workSheet.Cells[rowIterator,3].Value.ToString());
                                //dcsg.dcvalue = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value.ToString());
                                //dcsg.fuelcost = Convert.ToDecimal(0); //Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value.ToString());
                               


                                string startdate = GetFinaldate(formCollection["txtFrmDate"], Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());
                                //string enddate = GetFinaldate(mtrf.enddate, Request.Form["ServerDateformat"].ToString(), Request.Form["Dateformat"].ToString());
                                if (Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString()) == 96)
                                {
                                    dcsg.tstamp = DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(1);// Convert.ToDateTime(startdate); // DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(formCollection["txtFrmDate"]);

                                }
                                else
                                {
                                    dcsg.tstamp = DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Add(tspan);// Convert.ToDateTime(startdate); // DateTime.ParseExact(formCollection["txtFrmDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(formCollection["txtFrmDate"]);

                                }
                                //dcsg.stageid = Convert.ToInt32(formCollection["txtstageid"]);
                                dcsg.stageid = Convert.ToInt32(formCollection["drpstageid"]);
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
                                Jsonstr = client.UploadString(url + "DCSGRevisionWiseAPI", JsonConvert.SerializeObject(list));
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
                return File(FileName, System.Web.MimeMapping.GetMimeMapping(FileName), "DCSGrahejaStages.xlsx");
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
            return dirInfo.FullName + @"\" + "DCSGrahejaStages.xlsx";
        }

        public ActionResult Upload()
        {
            return View("Upload");
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

        public ActionResult Export(String InputString, String InputStr)
        {
            DataTable obj = new DataTable();
            string Lbl = string.Empty;
            string values = string.Empty;
            string[] InputData = InputStr.Split('~');
            string[] labelData = InputString.Split('~');
            try
            {
                Lbl = "Date ~Stage ID ~Revision ID ";
                string id = "/" + InputData[0] + "/" + InputData[1] + "/" + (InputData[2] == "" ? "0" : InputData[2]);
                List<dcsg> dcsgData = new List<dcsg>();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "DCSGRevisionWiseAPI/GetData" + id);
                    obj = ConvertJSONToDataTable(s, "", "");
                }
                if (obj == null || obj.Rows.Count == 0)
                {
                    obj.Columns.Add("Reason");
                    obj.AcceptChanges();
                    DataRow dr1 = obj.NewRow();
                    dr1["Reason"] = "No data Found for Excel Generation";
                    obj.Rows.Add(dr1);
                    obj.AcceptChanges();
                }
            }
            catch
            {
            }
            StringBuilder str = getString(Convert.ToString("DCSG"), Lbl.Split('~'), labelData);
            var grid = new GridView();
            grid.DataSource = obj;
            grid.DataBind();
            StringWriter sw = new StringWriter(str);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=DCSG_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(htw.InnerWriter.ToString().Replace("<div>", "").Replace("</div>", ""));
            grid.Dispose();
            htw.Dispose();
            return File(temp, "application/vnd.ms-excel");
        }
        public static StringBuilder getString(string headerText, string[] paramlabel, string[] paramvalue)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<b>" + headerText + "</b><br><br>");
            if (paramlabel != null && paramlabel.Length > 0)
            {
                for (int i = 0; i < paramlabel.Length; i++)
                {
                    str.Append("<b>" + paramlabel[i] + " :</b> " + paramvalue[i] + "   ");
                }
            }
            str.Append("<br><br>");
            return str;
        }
        protected DataTable ConvertJSONToDataTable(string jsonString, string pprd, string prd)
        {
            DataTable dt = new DataTable();
            //strip out bad characters
            string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

            //hold column names
            List<string> dtColumns = new List<string>();

            //get columns
            foreach (string jp in jsonParts)
            {
                //only loop thru once to get column names
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1);
                        string v = rowData.Substring(idx + 1);
                        if (!dtColumns.Contains(n))
                        {
                            dtColumns.Add(n.Replace("\"", ""));
                        }
                    }
                    catch
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                    }

                }
                break; // TODO: might not be correct. Was : Exit For
            }

            //build dt
            foreach (string c in dtColumns)
            {
                dt.Columns.Add(c);
            }
            //get table data
            foreach (string jp in jsonParts)
            {
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string v = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[n] = v;
                    }
                    catch
                    {
                        continue;
                    }

                }
                dt.Rows.Add(nr);
            }
            RemoveColmnName(ref  dt);
            return dt;
        }
        public void RemoveColmnName(ref DataTable dt)
        {
            try
            {
                string ColumnList = "$id,ContentEncoding,ContentType,Data,result,PLF,JsonRequestBehavior,MaxJsonLength,timestampid,isdeleted,stageid,id,tstamp,revision,dcvalue";
                string[] ColList = ColumnList.Split(',');
                if (dt != null && dt.Columns.Count > 0)
                {
                    for (int i = 0; i < ColList.Length; i++)
                    {

                        if (dt.Columns.Contains(ColList[i]))
                            dt.Columns.Remove(ColList[i]);

                        dt.AcceptChanges();
                    }


                    if (dt.Columns.Contains("blockno"))
                        dt.Columns["blockno"].ColumnName = "Block No";

                    if(dt.Columns.Contains("sgvalue"))
                    dt.Columns["sgvalue"].ColumnName = "SD";

                    if (dt.Columns.Contains("fuelcost"))
                        dt.Columns["fuelcost"].ColumnName = "Unit Cost";
                }
            }
            catch
            {

            }

        }
    }
}