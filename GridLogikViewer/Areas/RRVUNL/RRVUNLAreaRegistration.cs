using System.Web.Mvc;

namespace GridLogikViewer.Areas.RRVUNL
{
    public class RRVUNLAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RRVUNL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RRVUNL_default",
                "RRVUNL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}