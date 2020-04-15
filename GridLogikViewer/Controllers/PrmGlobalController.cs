using GridLogik.ViewModels;
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
    public class PrmGlobalController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /PrmGlobal/
        public async Task<ActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];
            IEnumerable<prmglobal> prmGlobal;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal", _uri);

                var result = await client.GetAsync(uri);

                prmGlobal = await result.Content.ReadAsAsync<IEnumerable<prmglobal>>();
            }
            return View(prmGlobal);
        }


        //
        // GET: /PrmGlobal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PrmGlobal/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PrmGlobal/Create
        [HttpPost]
        public async Task<ActionResult> Create(prmglobal _prmglobal)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal", _uri);

                var result = await client.PostAsJsonAsync(uri, _prmglobal);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    prmglobal prmglobal = await result.Content.ReadAsAsync<prmglobal>();
                    TempData["Message"] = MessageConfig.htmlSuccessString;
                    TempData["Status"] = "Success";
                    TempData["InnerMessage"] = "";
                    return RedirectToAction("Index", "PrmGlobal");
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
        // GET: /PrmGlobal/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            prmglobal prmglobal = await Getprmglobal(id);
            return View(prmglobal);
        }

        private async Task<prmglobal> Getprmglobal(int id)
        {
            prmglobal prmglobal;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                prmglobal = await result.Content.ReadAsAsync<prmglobal>();

            }            

            return prmglobal;
        }

        //
        // POST: /PrmGlobal/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, prmglobal _prmglobal)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, _prmglobal);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    prmglobal prmglobal = await result.Content.ReadAsAsync<prmglobal>();
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
                    return View(_prmglobal);
                }
            }
        }

        //
        // GET: /PrmGlobal/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            prmglobal prmglobal = await Getprmglobal(id);
            return View(prmglobal);
        }

        //
        // POST: /PrmGlobal/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, prmglobal _prmglobal)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    prmglobal prmglobal = await result.Content.ReadAsAsync<prmglobal>();
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

                    return View(_prmglobal);
                }
            }
        }
    }
}
