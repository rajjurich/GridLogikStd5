using System.Web.Mvc;

namespace GridLogikViewer.Areas.ILFS
{
    public class ILFSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ILFS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ILFS_default",
                "ILFS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}