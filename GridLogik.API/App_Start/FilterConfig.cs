using GridLogik.API.Filters;
using System.Web;
using System.Web.Mvc;

namespace GridLogik.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());            
        }
    }
}
