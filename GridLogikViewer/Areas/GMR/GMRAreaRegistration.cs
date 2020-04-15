using System.Web.Mvc;

namespace GridLogikViewer.Areas.GMR
{
    public class GMRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GMR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GMR_default",
                "GMR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}