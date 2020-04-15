using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridLogikViewer.Filters;
using GridLogik.ViewModels;
using System.Threading.Tasks;
using System.Net.Http;
using GridLogikViewer.Utilities;
using System.Web.Configuration;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ConsumerCategoryController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /MstConsumerCategory/
        [AccessCheck(IdParamName = "consumercategory/index")]
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

            IEnumerable<ConsumerCategory> ConsumerCategories;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumercategory", _uri);

                var result = await client.GetAsync(uri);

                ConsumerCategories = await result.Content.ReadAsAsync<IEnumerable<ConsumerCategory>>();
            }

            return View(ConsumerCategories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConsumerCategory objConsumerCategory)
        {
            objConsumerCategory.categoryfixedstatus = objConsumerCategory.checkcategoryfixedstatus ? 1 : 0;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumercategory", _uri);

                var result = await client.PostAsJsonAsync(uri, objConsumerCategory);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ConsumerCategory consumerCategory = await result.Content.ReadAsAsync<ConsumerCategory>();
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
        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            ConsumerCategory consumerCategory = await GetConsumerCategory(Convert.ToInt32(id));
            consumerCategory.checkcategoryfixedstatus = consumerCategory.categoryfixedstatus == 0 ? false : true;
            return View(consumerCategory);
        }

        private async Task<ConsumerCategory> GetConsumerCategory(int id)
        {
            ConsumerCategory consumerCategory;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumercategory/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                consumerCategory = await result.Content.ReadAsAsync<ConsumerCategory>();

            }

            return consumerCategory;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ConsumerCategory objConsumerCategory)
        {
            objConsumerCategory.categoryfixedstatus = objConsumerCategory.checkcategoryfixedstatus ? 1 : 0;
            using (HttpClient client = new HttpClient())
            {

                uri = string.Format("{0}consumercategory/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objConsumerCategory);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ConsumerCategory consumerCategory = await result.Content.ReadAsAsync<ConsumerCategory>();
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
        [AccessCheck(IdParamName = "consumercategory/Index")]
        public async Task<ActionResult> Delete(long id)
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            ConsumerCategory consumerCategory = await GetConsumerCategory(Convert.ToInt32(id));
            consumerCategory.checkcategoryfixedstatus = consumerCategory.categoryfixedstatus == 0 ? false : true;
            return View(consumerCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ConsumerCategory objConsumerCategory)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumercategory/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    ConsumerCategory consumerCategory = await result.Content.ReadAsAsync<ConsumerCategory>();
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