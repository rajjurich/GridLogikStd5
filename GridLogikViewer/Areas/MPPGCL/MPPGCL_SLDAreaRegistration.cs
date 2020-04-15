using System.Web.Mvc;

namespace GridLogikViewer.Areas.MPPGCL
{
    public class MPPGCLAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MPPGCL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MPPGCL_default",
                "MPPGCL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}