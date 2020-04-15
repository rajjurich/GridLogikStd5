using System.Web.Mvc;

namespace GridLogikViewer.Areas.BPCL
{
    public class BPCLAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BPCL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BPCL_default",
                "BPCL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}