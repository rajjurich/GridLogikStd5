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
    [Authorize]
    public class EmployeeController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /Employee/
        [AccessCheck(IdParamName = "Employee/Index")]
        public ActionResult Index()
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
            return View();
        }

        //
        // GET: /Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstEmployee MstEmployee)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}employee", _uri);

                var result = await client.PostAsJsonAsync(uri, MstEmployee);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstEmployee mstEmployee = await result.Content.ReadAsAsync<MstEmployee>();
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
        // GET: /Employee/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            MstEmployee mstEmployee = await GetMstEmployee(id);


            return View(mstEmployee);

        }

        private async Task<MstEmployee> GetMstEmployee(int id)
        {
            MstEmployee mstEmployee;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}employee/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstEmployee = await result.Content.ReadAsAsync<MstEmployee>();
            }
            mstEmployee.isactive = mstEmployee.empisactive == 1 ? false : true;
            return mstEmployee;
        }

        //
        // POST: /Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MstEmployee MstEmployee)
        {
            MstEmployee.empisactive = Convert.ToInt16(MstEmployee.isactive ? 0 : 1);
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}employee/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, MstEmployee);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstEmployee mstEmployee = await result.Content.ReadAsAsync<MstEmployee>();
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
        // GET: /Employee/Delete/5
        [AccessCheck(IdParamName = "Employee/Index")]
        public async Task<ActionResult> Delete(int id)
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

            MstEmployee mstEmployee = await GetMstEmployee(id);


            return View(mstEmployee);
        }

        //
        // POST: /Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MstEmployee MstEmployee)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}employee/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstEmployee mstEmployee = await result.Content.ReadAsAsync<MstEmployee>();
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
