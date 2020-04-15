using System.Web.Mvc;

namespace GridLogikViewer.Areas.AAIKolkata
{
    public class AAIKolkataAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AAIKolkata";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AAIKolkata_default",
                "AAIKolkata/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}