using MiniShop.Interfaces;
using MiniShop.Models.Bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniShop.Models
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private IRepository<Category> categories;
        private IRepository<Good> goods;
        private ShopContext context = new ShopContext();
        private bool disposed = false;
        public IRepository<Category> Categories
        {
            get
            {
                if(categories is null)
                {
                    categories = new CategoryRepository(context);
                }
                return categories;
            }
        }

        public IRepository<Good> Goods
        {
            get
            {
                if (goods is null)
                {
                    goods = new GoodRepository(context);
                }
                return goods;
            }
        }

        public async void Save()
        {
            await context.SaveChangesAsync();
        }
        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}