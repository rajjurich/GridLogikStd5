using System.Web.Mvc;

namespace GridLogikViewer.Areas.JPLAvantha
{
    public class JPLAvanthaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JPLAvantha";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JPLAvantha_default",
                "JPLAvantha/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}