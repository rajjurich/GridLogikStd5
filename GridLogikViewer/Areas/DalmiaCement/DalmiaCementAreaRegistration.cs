using System.Web.Mvc;

namespace GridLogikViewer.Areas.DalmiaCement
{
    public class DalmiaCementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DalmiaCement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DalmiaCement_default",
                "DalmiaCement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}