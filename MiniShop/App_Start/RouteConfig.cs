﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MiniShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Home", action = "Index", category = (string)null, page = 1 }
                );
            routes.MapRoute(
                name: null,
                url: "Admin",
                defaults: new { controller = "Goods", action = "Index", category = (string)null, page = 1 }
                );
            routes.MapRoute(
                null,
                url: "Admin/Page{page}",
                defaults: new { controller = "Goods", action = "Index", category = (string)null },
                constraints: new { page = @"\d+" });
            routes.MapRoute(null,
                "Admin/{category}",
                new { controller = "Goods", action = "Index", page = 1 });
            routes.MapRoute(
                name: null,
                url: "Admin/{category}/Page{page}/{searchTemplate}",
                defaults: new { controller = "Goods", action = "Index", searchTemplate = UrlParameter.Optional },
                constraints: new { page = @"\d+" });
            routes.MapRoute(null,
                "Show/Good/{id}",
                new { controller = "Home", action = "ShowGood", id = 1 },
                constraints: new { id = @"\d+" });
            routes.MapRoute(null,
                "Admin/Search/Good/{goodName}",
                new { controller = "Goods", action = "SearchGood", goodName = UrlParameter.Optional });
            routes.MapRoute(null,
                "Search/Good/{goodName}",
                new { controller = "Home", action = "SearchGood", goodName = UrlParameter.Optional });
            routes.MapRoute(
                null,
                url: "Page{page}",
                defaults: new { controller = "Home", action = "Index", category = (string)null },
                constraints: new { page = @"\d+" });
            routes.MapRoute(null,
                "{category}",
                new { controller = "Home", action = "Index", page = 1 });
            routes.MapRoute(
                name: null,
                url: "{category}/Page{page}/{searchTemplate}",
                defaults: new { controller = "Home", action = "Index", searchTemplate = UrlParameter.Optional },
                constraints: new { page = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
