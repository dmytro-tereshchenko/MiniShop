using MiniShop.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MiniShop.Models.Bd
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Good> Goods { get; set; }
        public ShopContext() : base("ShopContext")
        {
            Database.SetInitializer(new ShopDbInitializer());
        }
    }
}