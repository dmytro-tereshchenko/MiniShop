using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MiniShop.Util
{
    public class ShopDbInitializer: DropCreateDatabaseAlways<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category(){Id =1,  Name="Cat food"},
                new Category(){Id =2,  Name="Cat litter"}
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
            List<Good> goods = new List<Good>()
            {
                new Good(){ Id=1, CategoryId=1, Name="Wet food packaging for cats Purina Gourmet Gold", Price=214.2d, Description="Chicken pate 24 pieces 85 g each"},
                new Good(){ Id=2, CategoryId=1, Name="Dry food for adult cats Club 4 Paws", Price=210d, Description="For cats with skin and coat problems. Premium. With Rabbit 2 kg"},
                new Good(){ Id=3, CategoryId=1, Name="Wet food packaging for cats Purina Gourmet Perle Duo", Price=238d, Description="With veal and duck 24 pieces 85 g each"},
                new Good(){ Id=4, CategoryId=2, Name="Cat litter Fluffy", Price=540d, Description="Silica absorbent 6.5 kg (15 L)"},
                new Good(){ Id=5, CategoryId=2, Name="Cat litter Catsan Hygiene plus", Price=199d, Description="Mineral absorbent 4.9 kg (10 l)"},
                new Good(){ Id=6, CategoryId=2, Name="Cat litter Kotix", Price=210d, Description="Silica absorbent 2.2kg (5L)"},
                new Good(){ Id=7, CategoryId=2, Name="Cat litter AnimAll", Price=205d, Description="Green emerald Silica gel absorbent 2.2 kg (5 l)"}
            };
            context.Goods.AddRange(goods);
            context.SaveChanges();
            /*base.Seed(context);*/
        }
    }
}