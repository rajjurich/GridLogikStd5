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
    public class ChangePasswordController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        //
        // GET: /CahngePassword/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(MstUser User1)
        {
            User1.usrpassword = User1.usrpassword.EncryptPass();
            User1.Old_Password = User1.Old_Password.EncryptPass();
            
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    var uri = url + "ChangePassword/ResetingPassword/"+User1.Old_Password;
                    var result = await client.PostAsJsonAsync(uri, User1);
                    if (result.IsSuccessStatusCode)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.InnerMessage = result.Content;
                        ViewBag.Status = "Success";
                        return Redirect(Url.Action("index", "Dashboard"));
                    }
                    else
                    {   // need to handled exception 
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.InnerMessage = "Old Password Doesnt Match!";
                        ViewBag.Status = "Failed";
                        return View("Index");
                    }
                }

            }
            else
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.InnerMessage = "The user name or password provided is incorrect.";
                ViewBag.Status = "Failed";
                //return View("Index");


                ModelState.AddModelError("CustomError", "The user name or password provided is incorrect.");

                return View("Index");
            }
        }
	}
}