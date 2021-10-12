using MiniShop.Interfaces;
using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MiniShop.Models
{
    public class GoodRepository : IRepository<Good>
    {
        private ShopContext context;
        public GoodRepository(ShopContext context)
        {
            this.context = context;
        }
        public int Count => context.Goods.ToList().Count();

        public async Task<Good> Add(Good item)
        {
            Good oldGood = context.Goods
                .Where(c => c.Name == item.Name &&
                c.Description == item.Description &&
                c.Price == item.Price)
                .FirstOrDefault();
            if (oldGood is null)
            {
                context.Goods.Add(item);
                await context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<Good> Delete(int id)
        {
            Good oldGood = await context.Goods.FindAsync(id);
            if (oldGood != null)
            {
                context.Goods.Remove(oldGood);
                await context.SaveChangesAsync();
            }
            return oldGood;
        }

        public List<Good> GetAll()
        {
            return context.Goods.Include(g => g.Category).ToList();
        }

        public async Task<Good> GetById(int id)
        {
            return await context.Goods.Include(g => g.Category).Where(g => g.Id == id).FirstAsync();
        }
        public async Task<Good> GetByName(string nameGood)
        {
            Good good = null;
            try
            {
                good = await context.Goods.Include(g => g.Category)
                .Where(g => g.Name.ToLower().StartsWith(nameGood.ToLower()) ||
                g.Name.ToLower().EndsWith(nameGood.ToLower()) ||
                g.Name.ToLower().Contains(nameGood.ToLower())).FirstAsync();
            }
            catch (Exception){  }
            return good;
        }
        public async Task<IEnumerable<Good>> GetAllByName(string nameGood)
        {
            List<Good> goods = null;
            try
            {
                goods = await context.Goods.Include(g => g.Category)
                .Where(g => g.Name.ToLower().StartsWith(nameGood.ToLower()) ||
                g.Name.ToLower().EndsWith(nameGood.ToLower()) ||
                g.Name.ToLower().Contains(nameGood.ToLower())).ToListAsync();
            }
            catch (Exception) { }
            return goods;
        }

        public async Task<Good> Update(Good item)
        {
            Good oldGood = await context.Goods.FindAsync(item.Id);
            if (oldGood != null)
            {
                oldGood.Name = item.Name;
                context.Entry(oldGood).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return oldGood;
        }
    }
}