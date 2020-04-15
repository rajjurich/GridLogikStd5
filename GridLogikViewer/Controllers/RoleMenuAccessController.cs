using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Configuration;
using GridLogik.ViewModels;
using System.Net.Http;

namespace GridLogikViewer.Controllers
{
     [Authorize]
    public class RoleMenuAccessController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        public async Task<ActionResult> Index()
        {
            await BindDropDown();
            return View();
        }

        private async Task BindDropDown()
        {
            IEnumerable<MstRole> roles;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}role", _uri);

                var result = await client.GetAsync(uri);

                roles = await result.Content.ReadAsAsync<IEnumerable<MstRole>>();
                var Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.rolrecid.ToString(),
                    Text = c.rolname
                });

                ViewBag.Roles = Roles;
            }

            

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstRoleMenuAccess mstRoleMenuAccess)
        {
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View();
        }

        [HttpPut]
        public ActionResult Edit(MstRoleMenuAccess mstRoleMenuAccess)
        {
            return View();
        }
        public ActionResult Delete(long id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MstRoleMenuAccess mstRoleMenuAccess)
        {
            return View();
        }
	}
}