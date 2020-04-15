using System.Web.Mvc;

namespace GridLogikViewer.Areas.Lnt_Malva
{
    public class Lnt_MalvaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Lnt_Malva";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Lnt_Malva_default",
                "Lnt_Malva/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}