using System.Web.Mvc;

namespace GridLogikViewer.Areas.Gadarwara
{
    public class GadarwaraAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Gadarwara";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Gadarwara_default",
                "Gadarwara/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}