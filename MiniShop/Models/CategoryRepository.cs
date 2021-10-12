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
    public class CategoryRepository : IRepository<Category>
    {
        private ShopContext context;
        public CategoryRepository(ShopContext context)
        {
            this.context = context;

        }
        public int Count => context.Categories.Count();

        public async Task<Category> Add(Category item)
        {
            Category oldCategory = context.Categories.Where(c => c.Name == item.Name).FirstOrDefault();
            if(oldCategory is null)
            {
                context.Categories.Add(item);
                await context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<Category> Delete(int id)
        {
            Category oldCategory = await context.Categories.FindAsync(id);
            if (oldCategory !=null)
            {
                context.Categories.Remove(oldCategory);
                await context.SaveChangesAsync();
            }
            return oldCategory;
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public async Task<Category> GetById(int id)
        {
            return await context.Categories.FindAsync(id);
        }
        public async Task<Category> GetByName(string nameCategory)
        {
            Category Category = null;
            try
            {
                Category = await context.Categories
                .Where(g => g.Name.ToLower().StartsWith(nameCategory.ToLower()) ||
                g.Name.ToLower().EndsWith(nameCategory.ToLower()) ||
                g.Name.ToLower().Contains(nameCategory.ToLower())).FirstAsync();
            }
            catch (Exception) { }
            return Category;
        }
        public async Task<IEnumerable<Category>> GetAllByName(string nameCategory)
        {
            List<Category> categories = null;
            try
            {
                categories = await context.Categories
                .Where(g => g.Name.ToLower().StartsWith(nameCategory.ToLower()) ||
                g.Name.ToLower().EndsWith(nameCategory.ToLower()) ||
                g.Name.ToLower().Contains(nameCategory.ToLower())).ToListAsync();
            }
            catch (Exception) { }
            return categories;
        }

        public async Task<Category> Update(Category item)
        {
            Category oldCategory = await context.Categories.FindAsync(item.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = item.Name;
                context.Entry(oldCategory).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return oldCategory;
        }
    }
}