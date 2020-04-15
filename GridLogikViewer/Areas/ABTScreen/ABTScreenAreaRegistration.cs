using System.Web.Mvc;

namespace GridLogikViewer.Areas.ABTScreen
{
    public class ABTScreenAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ABTScreen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ABTScreen_default",
                "ABTScreen/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}