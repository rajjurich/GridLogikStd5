using System.Web.Mvc;

namespace GridLogikViewer.Areas.ABTMeterView
{
    public class ABTMeterViewAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ABTMeterView";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ABTMeterView_default",
                "ABTMeterView/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}