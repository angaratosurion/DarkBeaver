using System.Web;
using System.Web.Mvc;

namespace DarkBeaver
{
    public class FilterConfig : BlackCogs.Application.FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
