using GridLogik.ViewModels;
using GridLogikViewer.Filters;
using GridLogikViewer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    public class GridLogikCalculationController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        [AccessCheck(IdParamName = "GridLogikCalculation/Index")]
        //
        // GET: /GridLogikCalculation/
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

            IEnumerable<GridLogikCalculation> gridLogikCalculations;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/ForCalculation", _uri);

                var result = await client.GetAsync(uri);

                gridLogikCalculations = await result.Content.ReadAsAsync<IEnumerable<GridLogikCalculation>>();
            }

            return View(gridLogikCalculations);
        }

        //
        // GET: /GridLogikCalculation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /GridLogikCalculation/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GridLogikCalculation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GridLogikCalculation collection)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag", _uri);                
                var result = await client.PostAsJsonAsync(uri, collection);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    GridLogikCalculation gridLogikCalculation = await result.Content.ReadAsAsync<GridLogikCalculation>();
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
                    return View();
                }
            }
        }

        //
        // GET: /GridLogikCalculation/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            GridLogikCalculation gridLogikCalculation = await GetGridLogikCalculation(id);
            return View(gridLogikCalculation);
        }
        private async Task<GridLogikCalculation> GetGridLogikCalculation(int id)
        {
            GridLogikCalculation gridLogikCalculation;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                gridLogikCalculation = await result.Content.ReadAsAsync<GridLogikCalculation>();
            }
            return gridLogikCalculation;
        }
        //
        // POST: /GridLogikCalculation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, GridLogikCalculation collection)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, collection);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    GridLogikCalculation gridLogikCalculation = await result.Content.ReadAsAsync<GridLogikCalculation>();
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
                    return View();
                }
            }
        }

        //
        // GET: /GridLogikCalculation/Delete/5
        [AccessCheck(IdParamName = "GridLogikCalculation/Index")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            GridLogikCalculation gridLogikCalculation = await GetGridLogikCalculation(id);
            return View(gridLogikCalculation);
        }

        //
        // POST: /GridLogikCalculation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, GridLogikCalculation collection)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}OPCServerTag/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    GridLogikCalculation gridLogikCalculation = await result.Content.ReadAsAsync<GridLogikCalculation>();
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
                    return View();
                }
            }
        }
    }
}
