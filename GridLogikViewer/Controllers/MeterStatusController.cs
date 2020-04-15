using GridLogik.ViewModels;
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
    public class MeterStatusController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /MeterStatus/
        public async Task<ActionResult> Index()
        {
            IEnumerable<MeterStatus> meterStatus;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}MeterStatus/GetMeterStatus", _uri);

                var result = await client.GetAsync(uri);

                meterStatus = await result.Content.ReadAsAsync<IEnumerable<MeterStatus>>();
            }
            return View(meterStatus);
        }

        //
        // GET: /MeterStatus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MeterStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MeterStatus/Create
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
        // GET: /MeterStatus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MeterStatus/Edit/5
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
        // GET: /MeterStatus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MeterStatus/Delete/5
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
    }
}
