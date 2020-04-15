using GridLogik.ViewModels;
using GridLogikViewer.Filters;
using GridLogikViewer.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Controllers
{
    [Authorize]
    public class ConsumerController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        // GET: /Consumer/        
        [AccessCheck(IdParamName = "Consumer/Index")]
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
            IEnumerable<Consumer> Consumers;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumer", _uri);

                var result = await client.GetAsync(uri);

                Consumers = await result.Content.ReadAsAsync<IEnumerable<Consumer>>();
            }

            return View(Consumers);
        }

        //
        // GET: /Consumer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Consumer/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View("Create");
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}prmglobal/GetValuesOnModuleUnitAndIdentifier/Global/Gender/Gender", _uri);
                var result = await client.GetAsync(uri);
                var genders = await result.Content.ReadAsAsync<List<prmglobal>>();
                var Genders = genders.Select(c => new SelectListItem
                {
                    Value = c.prmrecid.ToString(),
                    Text = c.prmvalue
                });

                uri = string.Format("{0}prmglobal/GetValuesOnModuleUnitAndIdentifier/Global/MaritalStatus/MaritalStatus", _uri);
                result = await client.GetAsync(uri);
                var maritalStatus = await result.Content.ReadAsAsync<List<prmglobal>>();
                var MaritalStatus = maritalStatus.Select(c => new SelectListItem
                {
                    Value = c.prmrecid.ToString(),
                    Text = c.prmvalue
                });

                uri = string.Format("{0}consumercategory", _uri);
                result = await client.GetAsync(uri);
                var categories = await result.Content.ReadAsAsync<List<ConsumerCategory>>();
                var Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.categoryrecid.ToString(),
                    Text = c.categorydescription
                });

                ViewBag.Categories = Categories;
                ViewBag.Genders = Genders;
                ViewBag.MaritalStatus = MaritalStatus;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Consumer objConsumer)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumer", _uri);

                var result = await client.PostAsJsonAsync(uri, objConsumer);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    Consumer consumer = await result.Content.ReadAsAsync<Consumer>();
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
                    await BindDropDown();
                    return View();
                }
            }
        }

        //
        // GET: /Consumer/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            Consumer consumer = await GetConsumer(Convert.ToInt32(id));
            await BindDropDown();
            return View(consumer);
        }
        private async Task<Consumer> GetConsumer(int id)
        {
            Consumer consumer;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumer/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                consumer = await result.Content.ReadAsAsync<Consumer>();

            }

            return consumer;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Consumer objConsumer)
        {
            using (HttpClient client = new HttpClient())
            {

                uri = string.Format("{0}consumer/{1}", _uri, id);

                var result = await client.PutAsJsonAsync(uri, objConsumer);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    Consumer consumer = await result.Content.ReadAsAsync<Consumer>();
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
                    await BindDropDown();
                    return View();
                }
            }
        }

        //
        // GET: /Consumer/Delete/5
        [AccessCheck(IdParamName = "Consumer/Index")]
        public async Task<ActionResult> Delete(long id)
        {
            var data = ViewData.Model as MstRoleMenuAccess;
            if (data.rmacreateaccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.rmadeleteaccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.rmaupdateaccess == 0)
                ViewBag.EditAccess = "False";
            Consumer consumer = await GetConsumer(Convert.ToInt32(id));
            await BindDropDown();
            return View(consumer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Consumer objConsumer)
        {
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}consumer/{1}", _uri, id);

                var result = await client.DeleteAsync(uri);
                var contents = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    Consumer consumer = await result.Content.ReadAsAsync<Consumer>();
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
                    await BindDropDown();
                    return View();
                }
            }
        }
    }
}