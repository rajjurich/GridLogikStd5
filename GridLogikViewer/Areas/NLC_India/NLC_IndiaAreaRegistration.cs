using System.Web.Mvc;

namespace GridLogikViewer.Areas.NLC_India
{
    public class NLC_IndiaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NLC_India";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NLC_India_default",
                "NLC_India/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}