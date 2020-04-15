using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using GridLogik.ViewModels;
using System.Net;
using Newtonsoft.Json;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class CommunicationTypeController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /CommunicationType/
        public ActionResult Index()
        {
            List<CommunicationType> communicationType = new List<CommunicationType>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "communicationTypeAPI");
                communicationType = JsonConvert.DeserializeObject<List<CommunicationType>>(s);
                communicationType.RemoveAll(item => item == null);
            }
            return View(communicationType);
        }

        //
        // GET: /CommunicationType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CommunicationType/Create
        public ActionResult Create()
        {
            return View(new CommunicationType());
        }

        //
        // POST: /CommunicationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommunicationType communicationType)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(communicationType);
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "communicationTypeAPI", JsonConvert.SerializeObject(communicationType));
                }
                return RedirectToAction("Index", "CommunicationType");
            }
            catch
            {
                return View(communicationType);
            }
        }

        //
        // GET: /CommunicationType/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                CommunicationType communicationType = new CommunicationType();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "communicationTypeAPI" + "/" + id);
                    if (s != null)
                    {
                        communicationType = JsonConvert.DeserializeObject<CommunicationType>(s);
                    }
                    else
                    {
                        return View(new CommunicationType());
                    }
                }
                return View(communicationType);
            }
            catch
            {
                return View(new CommunicationType());
            }
        }

        //
        // POST: /CommunicationType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommunicationType communicationType)
        {
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    return View(communicationType);
                }
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "communicationTypeAPI" + "/" + id, "PUT", JsonConvert.SerializeObject(communicationType));
                }
                return RedirectToAction("Index", "CommunicationType");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CommunicationType/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                CommunicationType communicationType = new CommunicationType();
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "communicationTypeAPI" + "/" + id);
                    if (s != null)
                    {
                        communicationType = JsonConvert.DeserializeObject<CommunicationType>(s);
                    }
                    else
                    {
                        return View(new CommunicationType());
                    }
                }
                return View(communicationType);
            }
            catch
            {
                return View(new CommunicationType());
            }
        }

        //
        // POST: /CommunicationType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CommunicationType communicationType)
        {
            try
            {
                // TODO: Add delete logic here
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    string s = client.UploadString(url + "communicationTypeAPI" + "/" + id, "DELETE", JsonConvert.SerializeObject(communicationType));
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(communicationType);
            }
        }
    }
}
