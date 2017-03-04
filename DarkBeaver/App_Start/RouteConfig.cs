using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlackCogs.Interfaces;

namespace DarkBeaver
{
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 999)]
    public class RouteConfig : IRouteRegistrar
    {
        public void RegisterIgnoreRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
        public void RegisterRoutes(RouteCollection routes)
        {

            //base.RegisterRoutes(routes);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Projects", action = "Index", id = UrlParameter.Optional }
            );
            //MultiPlex Wiki
            routes.MapRoute(
                "History",
                "{wikiname}/{id}/{slug}/v{version}",
                new { controller = "HomeWiki", action = "ViewContentVersion" },
                new { id = @"\d+", version = @"\d+" }
                );

            routes.MapRoute(
                "Source",
                "{wikiname}/{id}/{slug}/source/v{version}",
                new { controller = "HomeWiki", action = "GetWikiSource" },
                new { id = @"\d+", version = @"\d+" }
                );

            routes.MapRoute(
                "Act",
                "{id}/{slug}/{action}",
                new { controller = "HomeWiki", action = "ViewContent" },
                new { id = @"\d+", action = @"\w+" }
                );
        }

    }
}
