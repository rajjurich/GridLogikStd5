using System.Web.Mvc;

namespace GridLogikViewer.Areas.RKM
{
    public class RKMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RKM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RKM_default",
                "RKM/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}