
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
    public class UserController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /User/
        [AccessCheck(IdParamName = "user/index")]
        public ActionResult Index()
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
            return View();
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown(true);
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MstUser _mstUser)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}user", _uri);
                _mstUser.usrpassword = _mstUser.usrpassword.EncryptPass();
                var result = await client.PostAsJsonAsync(uri, _mstUser);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstUser mstUser = await result.Content.ReadAsAsync<MstUser>();
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

        private async Task BindDropDown(bool create)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role", _uri);
                var result = await client.GetAsync(uri);
                var roles = await result.Content.ReadAsAsync<List<MstRole>>();
                var Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.rolrecid.ToString(),
                    Text = c.rolname
                });

                if (create)
                {
                    uri = string.Format("{0}employee/getunusedemployee/", _uri);
                }
                else
                {
                    uri = string.Format("{0}employee", _uri);
                }
                result = await client.GetAsync(uri);
                var employees = await result.Content.ReadAsAsync<List<MstEmployee>>();
                var Employees = employees.Select(c => new SelectListItem
                {
                    Value = c.emprecid.ToString(),
                    Text = string.Format("{0} {1}", c.empfirstname, c.emplastname)
                });

                ViewBag.Employees = Employees;
                ViewBag.Roles = Roles;
            }
        }

        //
        // GET: /User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            MstUser mstUser = await GetUser(id);

            await BindDropDown(false);
            return View(mstUser);
        }

        private async Task<MstUser> GetUser(int id)
        {
            MstUser mstUser;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}user/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                mstUser = await result.Content.ReadAsAsync<MstUser>();
            }
            mstUser.isactive = mstUser.usrisactive == null || mstUser.usrisactive == 0 ? true : false;
            return mstUser;
        }


        //
        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MstUser _mstuser)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}user/{1}", _uri, id);
                _mstuser.usrisactive = Convert.ToInt16(_mstuser.isactive == true ? 0 : 1);
                _mstuser.usrpassword = _mstuser.usrpassword.EncryptPass();
                var result = await client.PutAsJsonAsync(uri, _mstuser);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstUser mstusers = await result.Content.ReadAsAsync<MstUser>();
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
                    await BindDropDown(false);
                    return View();
                }
            }
        }

        //
        // GET: /User/Delete/5
        [AccessCheck(IdParamName = "user/index")]
        public async Task<ActionResult> Delete(int id)
        {
            MstUser mstUser = await GetUser(id);

            await BindDropDown(false);

            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            return View(mstUser);
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MstUser MstUser)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}user/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    MstUser mstUser = await result.Content.ReadAsAsync<MstUser>();
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
                    await BindDropDown(false);
                    return View();
                }
            }
        }
    }
}
