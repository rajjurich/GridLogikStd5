using GridLogikViewer.GridLogikViewerModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    public class GroupCalIntimationController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        // GET: /ServiceIntimationCal/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ServiceIntimationCal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ServiceIntimationCal/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ServiceIntimationCal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ServiceIntimationCal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ServiceIntimationCal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ServiceIntimationCal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ServiceIntimationCal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public FileResult Download(long id)
        {
            DataTable objMstmodcostdata = new DataTable();
            string FilePath = "";
            string FileNames = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "GroupCalIntimationAPI/Getdata/" + id);
                    objMstmodcostdata = JsonConvert.DeserializeObject<DataTable>(s);
                }
                if (objMstmodcostdata != null && objMstmodcostdata.Rows.Count == 1)
                {
                    FilePath = Convert.ToString(objMstmodcostdata.Rows[0]["filepath"]);
                   // FileNames = Convert.ToString(objMstmodcostdata.Rows[0]["filename"]);
                    if (!FilePath.EndsWith("\\"))
                    {
                        FilePath = FilePath + "\\";
                    }
                    FileNames = FilePath + Convert.ToString(objMstmodcostdata.Rows[0]["filename"]);
                }
                //return File(FilePath, System.Web.MimeMapping.GetMimeMapping(FileNames), FileNames);
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(FileNames, contentType, Path.GetFileName(FileNames));
            }
            catch (Exception ex)
            {
                new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, this.ControllerContext.RouteData.Values["controller"].ToString());
                return null;
            }
        }
        public FileResult Download_old(string id, string name)
        {
            //id-file path  name=fileName

            if (!id.EndsWith("\\"))
            {
                id = id + "\\";
            }
            string FileNames = id + name;
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(FileNames, contentType, Path.GetFileName(FileNames));
        }

    }
}
