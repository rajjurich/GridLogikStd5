using System.Web.Mvc;

namespace GridLogikViewer.Areas.SAIL_DSP
{
    public class SAIL_DSPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SAIL_DSP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SAIL_DSP_default",
                "SAIL_DSP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}