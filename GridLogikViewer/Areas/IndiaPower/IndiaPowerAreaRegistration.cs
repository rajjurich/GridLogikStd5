using System.Web.Mvc;

namespace GridLogikViewer.Areas.IndiaPower
{
    public class IndiaPowerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IndiaPower";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IndiaPower_default",
                "IndiaPower/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}