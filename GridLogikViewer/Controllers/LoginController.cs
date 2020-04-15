using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Net;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace GridLogikViewer.Controllers
{
   // [Authorize]
    public class LoginController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /Login/
        public ActionResult Index()
        {
            List<Login> login = new List<Login>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "loginAPI");
                login = JsonConvert.DeserializeObject<List<Login>>(s);
            }
            return View(login);
        }

        //
        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }      

        //
        // GET: /Login/Create
        public ActionResult Create()
        {

            List<Role> role = new List<Role>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "roleAPI");
                role = JsonConvert.DeserializeObject<List<Role>>(s);
            }
            //ViewBag.RoleId = role;
            ViewBag.RoleId = new SelectList(role, "Id", "Roles");
            return View();
        }

        //
        // POST: /Login/Create
        [HttpPost]
        
        public ActionResult Create(Login login)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    List<Role> role = new List<Role>();
                    using (WebClient client = new WebClient())
                    {
                        string s = client.DownloadString(url + "roleAPI");
                        role = JsonConvert.DeserializeObject<List<Role>>(s);
                    }
                    ViewBag.RoleId = new SelectList(role, "Id", "Roles");
                    return View(login);
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "loginAPI", JsonConvert.SerializeObject(login));
                }
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                List<Role> role = new List<Role>();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "roleAPI");
                    role = JsonConvert.DeserializeObject<List<Role>>(s);
                }
                ViewBag.RoleId = new SelectList(role, "Id", "Roles");
                return View(login);
            }
        }

        //
        // GET: /Login/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Login login = new Login();
                List<Role> role = new List<Role>();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "loginAPI" + "/" + id);
                    string p = client.DownloadString(url + "roleAPI");
                    if (s != null)
                    {
                        login = JsonConvert.DeserializeObject<Login>(s);
                        role = JsonConvert.DeserializeObject<List<Role>>(p);
                        ViewBag.RoleIdList = new SelectList(role, "Id", "Roles", login.RoleId);
                    }
                    else
                    {
                        return View(new Login());
                    }
                }
                return View(login);
            }
            catch
            {
                return View(new Login());
            }
        }

        //
        // POST: /Login/Edit/5
        [HttpPost]
        
        public ActionResult Edit(int id, Login login)
        {
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    return View(login);
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "loginAPI" + "/" + id, "PUT", JsonConvert.SerializeObject(login));
                }
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                Login login = new Login();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "loginAPI" + "/" + id);
                    if (s != null)
                    {
                        login = JsonConvert.DeserializeObject<Login>(s);
                    }
                    else
                    {
                        return View(new Login());
                    }
                }
                return View(login);
            }
            catch
            {
                return View(new Login());
            }
        }

        //
        // POST: /Login/Delete/5
        [HttpPost]
        
        public ActionResult Delete(int id, Login login)
        {
            try
            {
                // TODO: Add delete logic here
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "loginAPI" + "/" + id, "DELETE", JsonConvert.SerializeObject(login));
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(login);
            }
        }

        [HttpGet]
        public JsonResult KeepSessionAlive()
        {
            //return Json(null);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public DateTime GetCurrentTime()
        {
            //return Json(null);
            return DateTime.Now;
        }
    }
}
