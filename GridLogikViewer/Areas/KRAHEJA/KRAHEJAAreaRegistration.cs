using System.Web.Mvc;

namespace GridLogikViewer.Areas.KRAHEJA
{
    public class KRAHEJAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KRAHEJA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KRAHEJA_default",
                "KRAHEJA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}