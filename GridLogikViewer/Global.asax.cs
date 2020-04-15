using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
//using DataAccess;
namespace GridLogikViewer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
      //      BundleTable.EnableOptimizations = true;

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
                        BundleTable.EnableOptimizations = false;
#endif
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                Session.Timeout = 30;
                string CookieHeaders = Convert.ToString(Session["userid"]);
                if (CookieHeaders == null || CookieHeaders == "")
                {
                    Response.RedirectToRoute("Default");
                }
            }
            catch
            {
            }

        }

        //protected void Application_AuthenticateRequest(object sender, EventArgs args)
        //{
        //    string url = WebConfigurationManager.AppSettings["APIUrl"];
        //    if (Context.User != null)
        //    {

        //        List<Role> role = new List<Role>();
        //        using (WebClient client = new WebClient())
        //        {
        //            string s = client.DownloadString(url + "roleAPI");
        //            role = JsonConvert.DeserializeObject<List<Role>>(s);
        //        }

        //        string[] rolesArray = new string[role.Count()];
        //        for (int i = 0; i < role.Count(); i++)
        //        {
        //            rolesArray[i] = role.ElementAt(i).Roles;
        //        }
        //        GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, rolesArray);
        //        Context.User = gp;
        //    }

        //}


        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    if (FormsAuthentication.CookiesSupported == true && Request.IsAuthenticated == true)
        //    {
        //        if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
        //        {
        //            try
        //            {
        //                //let us take out the username now                
        //                string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
        //                string roles = string.Empty;

        //                using (EMSEntities db = new EMSEntities())
        //                {
        //                    User user = db.Users.SingleOrDefault(u => u.Username == username);

        //                    roles = user.Role;
        //                }
        //                //let us extract the roles from our own custom cookie
        //                //Let us set the Pricipal with our user specific details
        //                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
        //                  new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
        //            }
        //            catch (Exception)
        //            {
        //                //something went wrong
        //            }
        //        }
        //    }
        //}   
    }
}
