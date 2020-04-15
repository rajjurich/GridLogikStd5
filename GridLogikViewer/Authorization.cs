using GridLogik.ViewModels;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using System.Web.Http;
//using System.Web.Mvc;

using System.Net;
using System.Web.Configuration;
using Newtonsoft.Json; 

namespace GridLogikViewer
{
    public class Authorization : AuthorizeAttribute
    {



        protected override bool AuthorizeCore(HttpContextBase httpcontext)
        {

            string url = WebConfigurationManager.AppSettings["APIUrl"];



            if (httpcontext.Request.IsAuthenticated)
            {
                
             //   Login login = new Login();
                string login = string.Empty;
                using (WebClient client = new WebClient())
                {
                    var loggedinuser = httpcontext.User.Identity.Name;

                    string userrole = client.DownloadString(url + "LoginAPI" + "/GetUserRole/" + loggedinuser);
                    if (userrole != null)
                    {

                        login = JsonConvert.DeserializeObject<Login>(userrole).ToString();
                        foreach (string definedrole in this.Roles.Split(','))
                        {


                            foreach (var role in login)
                            {

                                if (definedrole.Equals(role))
                                    return true;
                            }
                        }

                      //  return false;
                    }


                }
            }
            return false;
        }
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
           
                 filterContext.Result = new ViewResult
                  {

                       ViewName = "~/Views/Shared/_LoginPartial.cshtml"
                   };

            //    }
            //    else
            //    {

            //        filterContext.Result = new ViewResult
            //        {
            //            ViewName = "~/Views/Shared/Error.cshtml"
            //        };
            //    }
            }


        }

        

    }


