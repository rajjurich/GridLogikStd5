using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Web.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using GridLogikViewer.GridLogikViewerModels;
using System.Globalization;
using System.Threading.Tasks;
using GridLogik.ViewModels;
using GridLogikViewer.Utilities;
using System.Reflection;


namespace GridLogikViewer.Controllers
{
    public class HomeController : BaseController
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        string Message, MessageType = "";

        int firstNumber = (new Random()).Next(10, 100);
        int secondNumber = (new Random()).Next(1, 10);
        string oprtr = string.Empty;
        List<string> operators = new List<string>() { "+", "-" };
        //
        // GET: /Home/
        public ActionResult Login()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string versiontrimed = version.Substring(0, 4);
            HttpContext.Session["Version_display"] = "Version " + versiontrimed;
            int randomSkip = (new Random()).Next(-1, 2);
            ViewBag.FirstNumber = firstNumber;
            ViewBag.SecondNumber = secondNumber;
            oprtr = operators.Skip(randomSkip).Take(1).FirstOrDefault();
            ViewBag.Operators = oprtr;
            return View(new UserLogin());
        }
        [HttpPost]
        
        public async Task<ActionResult> Login(UserLogin login)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string versiontrimed = version.Substring(0, 4);
            int captchaResult = login.oprtr == "+" ? login.firstNumber + login.secondNumber : login.firstNumber - login.secondNumber;
            login.verionsofwebapp = versiontrimed;
            HttpContext.Session["Version_display"] = "Version " + login.verionsofwebapp;
            if (login.captchaResult == captchaResult.ToString())
            {
                if (ModelState.IsValid)
                {
                    login.usrpassword = login.usrpassword.EncryptPass();
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}Authentication/login", _uri);
                        var result = await client.PostAsJsonAsync(uri, login);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            var user = await result.Content.ReadAsAsync<MstUser>();
                            FormsAuthentication.SetAuthCookie(user.usrrecid.ToString(), false);
                            HttpContext.Session["userid"] = login.usrid;
                            HttpContext.Session["mnutype"] = -1;
                            HttpContext.Session["usrrecid"] = user.usrrecid;
                            HttpContext.Session["usrroleid"] = user.usrroleid;

                            return Json(new { d = "Success", e = "S" });
                        }
                        else
                        {
                            Message = contents;
                            MessageType = "M";
                            return Json(new { d = Message, e = MessageType });
                        }
                    }
                }
                else
                {
                    Message = "User Id and Password is required";
                    MessageType = "M";
                    return Json(new { d = Message, e = MessageType });
                }
            }
            else
            {
                Message = "Invalid Captcha";
                MessageType = "M";
                return Json(new { d = Message, e = MessageType });
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            HttpContext.Session["userid"] = null;
            HttpContext.Session["mnutype"] = null;
            return RedirectToAction("Login", "Home");
        }

    }
}