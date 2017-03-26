using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlackCogs.Application;

namespace DarkBeaver
{
    public class MvcApplication : Application
    {
        protected void Application_Start()
        {

            try
            {
                base.Application_Start();

                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                //RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                BootStrap();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
        protected void Application_Error()
        {

            base.Application_Error();

        }
    }
}
