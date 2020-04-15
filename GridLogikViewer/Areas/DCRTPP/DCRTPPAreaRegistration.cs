using System.Web.Mvc;

namespace GridLogikViewer.Areas.DCRTPP
{
    public class DCRTPPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DCRTPP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DCRTPP_default",
                "DCRTPP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}