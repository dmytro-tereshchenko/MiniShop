using Autofac;
using Autofac.Integration.Mvc;
using MiniShop.Interfaces;
using MiniShop.Models;
using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniShop.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<GoodRepository>().As<IRepository<Good>>()
                .WithParameter("context", new ShopContext());
            builder.RegisterType<CategoryRepository>().As<IRepository<Category>>()
                .WithParameter("context", new ShopContext());
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}