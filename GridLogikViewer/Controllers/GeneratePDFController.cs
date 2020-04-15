using GridLogik.ViewModels;
using GridLogikViewer.Models;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Threading;
using Ionic.Zip;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class GeneratePDFController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string msg = string.Empty;
        // GET: /GeneratePDF/
        public ActionResult Index()
        {
            return View();
        }
        //   public void PDF(String MeterString)
        public void PDF(String InputString)
        {
            List<prmglobal> objParam = new List<prmglobal>();
            try
            {
                if (InputString != null && InputString.Contains(','))
                {
                    string[] InputData = InputString.Split(',');
                    //string[] MeterArray = InputData[1].ToString().Split('~');
                    InvoiceMeterData objMeterData = new InvoiceMeterData();
                    objMeterData.CompanyCode = InputData[0];
                    objMeterData.FromDate = InputData[1];
                    objMeterData.ToDate = InputData[2];
                    string FileName = "Bill_" + InputData[3] + "_" + InputData[0] + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".zip"; 
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("Content-Type", "application/json");
                        string s = client.UploadString(url + "InvoicePdfAPI", JsonConvert.SerializeObject(objMeterData));
                        dynamic dynJson1 = JValue.Parse(s);
                        dynamic dynJson = dynJson1.Data.result;
                        if (((Newtonsoft.Json.Linq.JContainer)(dynJson)).HasValues != false)
                        {
                            int count = 0;
                            string[] FileList = new string[dynJson.Count];
                            foreach (var dr in dynJson)
                            {

                                FileList[count] = dr.PdfPath;
                                count++;
                            }

                            using (ZipFile zip = new ZipFile())
                            {
                                zip.AddFiles(FileList, "PDF");
                                Response.ClearContent();
                                Response.ClearHeaders();
                                Response.AppendHeader("content-disposition", "attachment;filename="+FileName);
                                zip.Save(Response.OutputStream);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public void Print(InvoiceMeterData objMeterData, string PPRD, string PRD, dynamic dr, dynamic dynJson, ref string FilePath, string CompanyName)
        {
            try
            {
                string Font_FamilyDetail = "Calibri";
                Document document = new Document(PageSize.A4, 20f, 30f, 10f, 10f);
                Font NormalFont = FontFactory.GetFont(Font_FamilyDetail, 12, Font.NORMAL, Color.BLACK);
               // long id = 0;
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                 
                //using (WebClient client = new WebClient())
                //{
                //client.Headers.Add("Content-Type", "application/json");
                //string s = client.UploadString(url + "InvoicePdfAPI", JsonConvert.SerializeObject(objMeterData));
                //dynamic dynJson1 = JValue.Parse(s);
                //dynamic dynJson = dynJson1.Data.result;
                //if (((Newtonsoft.Json.Linq.JContainer)(dynJson)).HasValues != false)
                //{
                try
                {
                    //foreach (var dr in dynJson)
                    //{
                    string FileName = "bill_" + objMeterData.CompanyCode.Split('~')[0] + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                    FilePath = HostingEnvironment.MapPath("~/PDF/") + FileName;
                    using (System.IO.FileStream memoryStream = new System.IO.FileStream(FilePath, FileMode.Create))
                    {

                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                        document.Open();

                        //Header Table
                        TableObject(ref  table, 2, 500, 0, new float[] { 0.3f, 0.7f }, Element.ALIGN_JUSTIFIED, 5, 5);
                        //Company Logo
                        cell = ImageCell("~/Content/images/northwindlogo.gif", 30f, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);

                        //Company Name and Address
                        phrase = new Phrase();
                        phrase.Add(new Chunk(Convert.ToString(CompanyName + "\n\n"), FontFactory.GetFont(Font_FamilyDetail, 16, Font.BOLD, Color.BLACK)));
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                        cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                        table.AddCell(cell);
                        document.Add(table);


                        SingleLineInfoPrint(cell, ref table, "ELECTRICITY REIMBURSEMENT FOR THE MONTH " + dr.MONTH + "\n\n", "", Font_FamilyDetail, 10, Font.BOLD, 15, 5, PdfPCell.ALIGN_CENTER, Element.ALIGN_CENTER);
                        document.Add(table);

                        TableObject(ref  table, 6, 0, 115, new float[] { 17f, 2f, 35f, 26f, 3f, 26f }, Element.ALIGN_LEFT, 5, 5);

                        upperGrid(cell, ref table, "Allottee Name.", Convert.ToString(dr.ALLOTTE_NAME), "", "", Font_FamilyDetail);
                        upperGrid(cell, ref table, "Ref.", Convert.ToString(dr.REF), "Previous Reading Date", Convert.ToString(dr.PRRD), Font_FamilyDetail);
                        upperGrid(cell, ref table, "Office No.", Convert.ToString(dr.UFFNO), "Present Reading Date", Convert.ToString(dr.PRD), Font_FamilyDetail);
                        upperGrid(cell, ref table, "Meter Sr.No.", Convert.ToString(dr.MSN), "Units Consumed", Convert.ToString(dr.CKHW), Font_FamilyDetail);
                        document.Add(table);

                        WebClient wc = new WebClient();
                        string HTMLString = wc.DownloadString(HostingEnvironment.MapPath("~/PdfData/myHtml.txt"));
                        if (HTMLString != null && HTMLString.Length > 5)
                        {
                            string ResultString = DynamicTableValueFill(HTMLString, dynJson);
                            if (ResultString != null && ResultString.Length > 5)
                            {
                                string[] values = ResultString.Split(new string[] { "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                                List<string> tblRow = new List<string>(values);


                                TableObject(ref  table, 9, 0, 100, new float[] { 9f, 20f, 15f, 6f, 10f, 10f, 14f, 6f, 10f }, Element.ALIGN_CENTER, 5, 5);

                                var count = 0;

                                string ColspanString = wc.DownloadString(HostingEnvironment.MapPath("~/PdfData/InvoiceTableStyle.txt"));

                                object[] ColspanStringArr = ColspanString.Split('~');
                                RemoveUnwantedRow(ref tblRow);
                                if (ColspanStringArr.Length == tblRow.Count)
                                {

                                    foreach (var tblROW in tblRow)
                                    {
                                        string[] valuesTD = tblROW.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);
                                        List<string> tblTDList = new List<string>(valuesTD);
                                        string[] RealcolaspanData = ColspanStringArr[count].ToString().Split('-');
                                        int ColsCount = 0;
                                        RemoveUnwantedRow(ref tblTDList);
                                        foreach (var tblTD in tblTDList)
                                        {

                                            if (count == 0)
                                            {
                                                LoweHeaderTableCell(cell, ref table, tblTD, Font_FamilyDetail, 10, Convert.ToInt32(RealcolaspanData[ColsCount]));
                                            }
                                            else if (count == 1)
                                            {
                                                LoweTableCell(cell, ref table, tblTD, Font_FamilyDetail, 10, Convert.ToInt32(RealcolaspanData[ColsCount]), Font.NORMAL, PdfPCell.ALIGN_CENTER, 5, 5);
                                            }
                                            else if (count == 2)
                                            {
                                                float n;
                                                if (float.TryParse(Convert.ToString(tblTD), out n))
                                                {
                                                    LoweTableCell(cell, ref table, tblTD, Font_FamilyDetail, 10, Convert.ToInt32(RealcolaspanData[ColsCount]), Font.NORMAL, PdfPCell.ALIGN_CENTER, 10, 10);
                                                }
                                                else
                                                {
                                                    LoweTableCell(cell, ref table, tblTD, Font_FamilyDetail, 10, Convert.ToInt32(RealcolaspanData[ColsCount]), Font.BOLD, PdfPCell.ALIGN_CENTER, 10, 10);
                                                }
                                            }
                                            else
                                            {
                                                float n;
                                                if (float.TryParse(Convert.ToString(tblTD), out n))
                                                {
                                                    LoweTableCell(cell, ref table, tblTD, Font_FamilyDetail, 10, Convert.ToInt32(RealcolaspanData[ColsCount]), Font.NORMAL, PdfPCell.ALIGN_CENTER, 5, 5);
                                                }
                                                else
                                                {
                                                    LoweTableCell(cell, ref table, tblTD, Font_FamilyDetail, 10, Convert.ToInt32(RealcolaspanData[ColsCount]), Font.BOLD, PdfPCell.ALIGN_LEFT, 5, 5);
                                                }
                                            }
                                            ColsCount++;
                                        }
                                        count++;
                                    }
                                }
                                else
                                {
                                }
                                document.Add(table);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }

                        //Below Guide Information 
                        SingleLineInfoPrint(cell, ref table, "Remark : ", "", Font_FamilyDetail, 10, Font.BOLD, 15, 5, PdfPCell.ALIGN_LEFT, Element.ALIGN_LEFT);
                        document.Add(table);

                        SingleLineInfoPrint(cell, ref table, "Note: ", "", Font_FamilyDetail, 10, Font.BOLD, 15, 0, PdfPCell.ALIGN_LEFT, Element.ALIGN_LEFT);
                        document.Add(table);

                        string guideInfo = wc.DownloadString(HostingEnvironment.MapPath("~/PdfData/InvoiceTableGuide.txt"));
                        if (guideInfo.Length > 5)
                        {
                            StyleSheet styles = new StyleSheet();
                            styles.LoadTagStyle("li", "face", "garamond");
                            styles.LoadTagStyle("li", "size", "10px");
                            styles.LoadTagStyle("li", "font-family", Font_FamilyDetail);

                            ArrayList objects = HTMLWorker.ParseToList(new StringReader(guideInfo), styles);
                            foreach (IElement element in objects)
                            {

                                List list = new List();
                                list.SetListSymbol("\u2022");
                                list.IndentationLeft = 20f;
                                list.Add(element);
                                if (list.Chunks.Count == 0)
                                {
                                    document.Add(element);
                                }
                                else
                                {
                                    document.Add(list);
                                }
                            }
                        }
                        else
                        {
                        }

                        SingleLineInfoPrint(cell, ref table, "For ", CompanyName, Font_FamilyDetail, 10, Font.NORMAL, 30, 10, PdfPCell.ALIGN_LEFT, Element.ALIGN_LEFT);
                        document.Add(table);

                        document.Close();
                        Thread.Sleep(18000);
                        // byte[] bytes = memoryStream.ToArray();
                        // memoryStream.Close();


                        // PrintPdf(bytes, FileName);

                        //Response.Clear();
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("Content-Disposition", "attachment; filename=bill.pdf");
                        ////Response.ContentType = "application/pdf";
                        //Response.Buffer = true;
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.BinaryWrite(bytes);
                        //Response.Flush();
                        ////   Response.End();
                        //Response.Close();
                    }
                    // }
                }
                catch
                {
                    ErrorPDF(document, table, cell, Font_FamilyDetail, Convert.ToString(objMeterData.MeterGroup), ref  FilePath);
                }
                //}
                //else
                //{
                //    ErrorPDF(document, table, cell, Font_FamilyDetail,Convert.ToString(objMeterData.MeterGroup),ref  FilePath);
                //}
                //}
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public void ErrorPDF(Document document, PdfPTable table, PdfPCell cell, String Font_FamilyDetail, String MeterGroup, ref string FilePath)
        {
            try
            {
                string FileName = "bill_NoData_" + MeterGroup + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                FilePath = HostingEnvironment.MapPath("~/PDF/") + FileName;

                using (System.IO.FileStream memoryStream = new System.IO.FileStream(FilePath, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    SingleLineInfoPrint(cell, ref table, "No data Found for bill Generation\n\n", "", Font_FamilyDetail, 10, Font.BOLD, 15, 5, PdfPCell.ALIGN_CENTER, Element.ALIGN_CENTER);
                    document.Add(table);

                    document.Close();
                    //byte[] bytes = memoryStream.ToArray();
                    //memoryStream.Close();
                    //PrintPdf(bytes, "billnodata.pdf");

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public void PrintPdf(byte[] bytes, string FileName)
        {
            try
            {

                string FilePath = HostingEnvironment.MapPath("~/PDF/") + FileName;
                //Response.ContentType = "application/pdf";;
                //  Response.ClearHeaders();
                Response.Clear();
                Response.BufferOutput = false;
                //   Response.Buffer = false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + FilePath);

                //Response.ContentType = "application/pdf";
                // Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.Flush();

                Response.Close();

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public void RemoveUnwantedRow(ref List<string> tblRow)
        {
            try
            {
                Boolean flag = false;
                foreach (var tblROW in tblRow)
                {
                    if (Convert.ToString(tblROW) == "" || Convert.ToString(tblROW) == "\r\n" || Convert.ToString(tblROW) == "\n" || Convert.ToString(tblROW) == "\r\n\n" || Convert.ToString(tblROW) == " ")
                    {
                        tblRow.Remove(tblROW);
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    RemoveUnwantedRow(ref tblRow);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public string DynamicTableValueFill(string HTMLString, dynamic dr)
        {
            try
            {
                object[] arr = HTMLString.Split('~');
                StringBuilder sb = new StringBuilder();

                Type myType = dr.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                //foreach (var item in arr)
                //{
                //    if (Convert.ToString(item).Contains("InvoiceDtailDATAFORPDF."))
                //    {
                //        foreach (PropertyInfo prop in props)
                //        {
                //            var Key = Convert.ToString(item).Replace("InvoiceDtailDATAFORPDF.", "");
                //            if (prop.Name == Key)
                //            {
                //                var DDD = prop.Name;
                //                object propValue = prop.GetValue(dr, null);
                //                sb.Append(propValue);
                //                break;
                //            }

                //        }
                //    }
                //    else
                //    {
                //        sb.Append(item);
                //    }
                //}
                foreach (var item in arr)
                {
                    if (Convert.ToString(item).Contains("InvoiceDtailDATAFORPDF."))
                    {

                        foreach (JObject content in dr.Children<JObject>())
                        {
                            foreach (JProperty prop in content.Properties())
                            {
                                var Key = Convert.ToString(item).Replace("InvoiceDtailDATAFORPDF.", "");
                                if (prop.Name == Key)
                                {
                                    var DDD = prop.Value;
                                    if (Convert.ToString(DDD) != "" && Convert.ToString(DDD) != null)
                                    {
                                        sb.Append(prop.Value);
                                        break;
                                    }
                                    else
                                    {
                                        sb.Append('-');
                                        break;
                                    }
                                    //  object propValue = prop.GetValue(dr, null);

                                }
                            }
                        }
                    }
                    else
                    {
                        sb.Append(item);
                    }
                }

                HTMLString = sb.ToString();

                return HTMLString;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }
        private static PdfPCell TableHeaderPhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = Color.BLACK;
            cell.VerticalAlignment = PdfCell.ALIGN_CENTER;
            cell.HorizontalAlignment = PdfCell.ALIGN_CENTER;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 3f;
            cell.PaddingLeft = 2f;
            return cell;
        }
        private static PdfPCell TablePhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = Color.BLACK;
            cell.VerticalAlignment = PdfCell.ALIGN_LEFT;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }
        private static PdfPCell ImageCell(string path, float scale, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(path));
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }
        private DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("COMPANY_NAME");
            dt.Columns.Add("MONTH");
            dt.Columns.Add("ALLOTTE_NAME");
            dt.Columns.Add("REF");
            dt.Columns.Add("PRD");
            dt.Columns.Add("PRRD");
            dt.Columns.Add("UFFNO");
            dt.Columns.Add("MSN");
            dt.Columns.Add("UC");

            dt.Columns.Add("METER_NUMBER");
            dt.Columns.Add("METER");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("PF");
            dt.Columns.Add("ORKHW");
            dt.Columns.Add("CRKHW");
            dt.Columns.Add("CKHW");
            dt.Columns.Add("UR");
            dt.Columns.Add("AMOUNT");
            dt.Columns.Add("PLF");
            dt.Columns.Add("TC");
            dt.Columns.Add("TA");
            dt.Columns.Add("ARREAR");
            dt.Columns.Add("TAR");

            dt.AcceptChanges();



            DataRow dr1 = dt.NewRow();
            dr1["COMPANY_NAME"] = "BHARAT DIAMOND BOURSE";
            dr1["MONTH"] = "Dec 2015";
            dr1["ALLOTTE_NAME"] = "AE2012";
            dr1["REF"] = "Ele/Doc/Dec2015/030210020059";
            dr1["PRD"] = "01-12-2015";
            dr1["PRRD"] = "31-12-2015";
            dr1["UFFNO"] = "AE2012";

            dr1["MSN"] = "21001444";
            dr1["UC"] = "213.98 KWh";
            dr1["METER_NUMBER"] = "12";
            dr1["METER"] = "AE2012 ‐ Consumption";
            dr1["DESCRIPTION"] = "3 ‐ Phase";

            dr1["PF"] = "0.873";
            dr1["ORKHW"] = "6315.42";
            dr1["CRKHW"] = "6529.4";
            dr1["CKHW"] = "213.98";
            dr1["UR"] = "1.0";
            dr1["AMOUNT"] = "213.98";
            dr1["PLF"] = "0";
            dr1["TC"] = "213.98";
            dr1["TA"] = "213.98";
            dr1["ARREAR"] = "-";
            dr1["TAR"] = "214";
            dt.Rows.Add(dr1);
            dt.AcceptChanges();
            return dt;
        }
        public void upperGrid(PdfPCell cell, ref PdfPTable table, string lable1, string lbl1Value, string lable2, string lbl2Value, string Font_FamilyDetail)
        {
            try
            {
                cell = PhraseCell(new Phrase(lable1, FontFactory.GetFont(Font_FamilyDetail, 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 1;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                cell = PhraseCell(new Phrase(lable1 == "" ? "" : ":", FontFactory.GetFont(Font_FamilyDetail, 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 1;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                cell = PhraseCell(new Phrase(lbl1Value, FontFactory.GetFont(Font_FamilyDetail, 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 1;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                cell = PhraseCell(new Phrase(lable2, FontFactory.GetFont(Font_FamilyDetail, 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_RIGHT);
                cell.Colspan = 1;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                cell = PhraseCell(new Phrase(lable2 == "" ? "" : ":", FontFactory.GetFont(Font_FamilyDetail, 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 1;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                cell = PhraseCell(new Phrase(lbl2Value, FontFactory.GetFont(Font_FamilyDetail, 10, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 1;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);
            }
            catch
            {
            }
        }
        public void SingleLineInfoPrint(PdfPCell cell, ref PdfPTable table, string lable1, string lbl1Value, string FontFamily, int size, int FontWeight, float PaddingTop, float PaddingBottom, int PdfPCell_ALIGN, int ROW_ALIGN)
        {
            try
            {

                TableObject(ref  table, 1, 500, 100, new float[] { }, ROW_ALIGN, 5, 5);

                cell = PhraseCell(new Phrase(lable1 + " " + lbl1Value, FontFactory.GetFont(Convert.ToString(FontFamily), size, FontWeight, Color.BLACK)), PdfPCell_ALIGN);
                cell.Colspan = 1;
                cell.PaddingTop = PaddingTop;
                cell.PaddingBottom = PaddingBottom;
                table.AddCell(cell);
            }
            catch
            {
            }
        }
        public void LoweHeaderTableCell(PdfPCell cell, ref PdfPTable table, string label, string fontName, int size, int colsPan)
        {
            cell = TableHeaderPhraseCell(new Phrase(label, FontFactory.GetFont(fontName, size, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER);
            cell.Colspan = colsPan;
            table.AddCell(cell);
        }
        public void LoweTableCell(PdfPCell cell, ref PdfPTable table, string label, string fontName, int size, int colsPan, int FontWeight, int PdfPCell_ALIGN, int PaddingTop, int PaddingBottom)
        {
            cell = TablePhraseCell(new Phrase(label, FontFactory.GetFont(fontName, size, FontWeight, Color.BLACK)), PdfPCell_ALIGN);
            cell.Colspan = colsPan;
            cell.PaddingTop = PaddingTop;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);
        }
        public void TableObject(ref PdfPTable table, int colspan, int TotalWidth, int TablePerWidth, float[] ColumnWidth, int HorizonatlAlignment, int SpacingBefore, int SpacingAfter)
        {
            table = new PdfPTable(colspan);
            if (TotalWidth == 0)
            {
                table.WidthPercentage = TablePerWidth;
                table.TotalWidth = 500f;
            }
            else
            {
                table.TotalWidth = TotalWidth;
            }
            //table.LockedWidth = true;
            if (ColumnWidth.Length > 0)
                table.SetWidths(ColumnWidth);
            table.SpacingBefore = SpacingBefore;
            table.SpacingAfter = SpacingAfter;
            table.HorizontalAlignment = HorizonatlAlignment;
        }
    }

}