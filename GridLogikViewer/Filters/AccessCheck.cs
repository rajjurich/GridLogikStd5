using GridLogik.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GridLogikViewer.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AccessCheck : ActionFilterAttribute, IActionFilter
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        public string IdParamName { get; set; }       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = IdParamName;
            MstRoleMenuAccess mstRoleMenuAccess = new MstRoleMenuAccess();
            var userID = filterContext.HttpContext.User.Identity.Name;
            //string parameter = "CommunicationDetail/Index";
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}menuaccess/accesscheck/{1}/{2}", _uri, id, userID);

                var result = client.GetAsync(uri).Result;

                mstRoleMenuAccess = result.Content.ReadAsAsync<MstRoleMenuAccess>().Result;

            }


            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(url + "MenuAccessAPI/Get/" + id + "/" + userID);
            //    obj = JsonConvert.DeserializeObject<mstRoleMenuAccess>(s);
            //}
            filterContext.Controller.ViewData.Model = mstRoleMenuAccess;
        }
    }
}