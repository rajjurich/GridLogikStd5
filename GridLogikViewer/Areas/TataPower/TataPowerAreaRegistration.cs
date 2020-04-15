using System.Web.Mvc;

namespace GridLogikViewer.Areas.TataPower
{
    public class TataPowerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TataPower";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TataPower_default",
                "TataPower/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}