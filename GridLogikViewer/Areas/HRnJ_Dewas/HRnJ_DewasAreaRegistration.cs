using System.Web.Mvc;

namespace GridLogikViewer.Areas.HRnJ_Dewas
{
    public class HRnJ_DewasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HRnJ_Dewas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HRnJ_Dewas_default",
                "HRnJ_Dewas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}