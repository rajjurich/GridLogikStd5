using GridLogik.ViewModels;
using GridLogikViewer.Areas.ABTScreen;
using GridLogikViewer.Areas.ABTScreen.Models;
using GridLogikViewer.GridLogikViewerModels;
using GridLogikViewer.Models;
using GridLogikViewer.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EToolsViewer.Areas.ABTScreen.Controllers
{
    [Authorize]
    public class UirateStagedController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;

        [HttpPost]
        public async Task<ActionResult> Upload(FormCollection formcollection)
        {
            try
            {
                var dateSplit = formcollection.GetValues("txtFrmDate")[0].Split('/');
                var app_uidate = dateSplit[2] + "-" + dateSplit[1] + "-" + dateSplit[0] + " 00:00:00";
                var stageid = formcollection.GetValues("drpstageid")[0];
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["Uploadfile"];
                    List<UiRateDatewise> list = new List<UiRateDatewise>();
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
                                UiRateDatewise uirate = new UiRateDatewise();
                                uirate.frequency = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value.ToString());
                                uirate.positive = Convert.ToDecimal(workSheet.Cells[rowIterator, 2].Value.ToString());
                                uirate.negative = Convert.ToDecimal(workSheet.Cells[rowIterator, 3].Value.ToString());
                                uirate.UiRateDate = Convert.ToDateTime(app_uidate);
                                uirate.stageid = Convert.ToInt32(stageid);
                                uirate.tstamp = Convert.ToDateTime(app_uidate);
                                uirate.revision = Convert.ToInt32(formcollection["txtrevisionid"]);
                                list.Add(uirate);
                            }
                        }
                        if (list.Count > 0 && list != null)
                        {
                            

                            using (HttpClient client = new HttpClient())
                            {
                                //client.Headers.Add("Content-Type", "application/json");
                                //Jsonstr = client.UploadString(url + "UIRateStageAPI", JsonConvert.SerializeObject(list));

                                uri = string.Format("{0}UIRate", url);

                                var Jsonstr2 = await client.PostAsJsonAsync(uri, list);
                                var contents = await Jsonstr2.Content.ReadAsStringAsync();

                                TempData["Message"] = MessageConfig.htmlSuccessString;
                                TempData["Status"] = "Success";
                                TempData["InnerMessage"] = "";
                                ViewBag.result = "Record Inserted Successfully!";
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
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        public FileResult Download(string id)
        {
            try
            {
                string FileName = GetFileName();
                return File(FileName, System.Web.MimeMapping.GetMimeMapping(FileName), "UIRate.xlsx");
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
            return dirInfo.FullName + @"\" + "UIRate.xlsx";
        }



        public async Task<ActionResult> Export(String InputString, String InputStr)
        {
            DataTable obj = new DataTable();
            string Lbl = string.Empty;
            string values = string.Empty;
            string[] InputData = InputStr.Split('~');
            string[] labelData = InputString.Split('~');

            Lbl = "Date ~Stage ID ~Revision ID ";
            string id = "/" + InputData[0] + "/" + InputData[1] + "/" + (InputData[2] == "" ? "0" : InputData[2]);
            List<DCSGFuelModel> dcsgData = new List<DCSGFuelModel>();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}UIRate/GetData/{1}", url, id);

                var Jsonstr2 = await client.GetAsync(uri);
                string s = await Jsonstr2.Content.ReadAsStringAsync();
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

            StringBuilder str = getString(Convert.ToString("UIRATE"), Lbl.Split('~'), labelData);
            var grid = new GridView();
            grid.DataSource = obj;
            grid.DataBind();
            StringWriter sw = new StringWriter(str);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Uirate_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + ".xls");
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

                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
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

            string ColumnList = "$id,ContentEncoding,ContentType,Data,result,PLF,JsonRequestBehavior,MaxJsonLength,timestampid,isdeleted,stageid,id,tstamp,revision";
            string[] ColList = ColumnList.Split(',');
            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < ColList.Length; i++)
                {

                    if (dt.Columns.Contains(ColList[i]))
                        dt.Columns.Remove(ColList[i]);

                    dt.AcceptChanges();
                }
            }


        }

    }
}
