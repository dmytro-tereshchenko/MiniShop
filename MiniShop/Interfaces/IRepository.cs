using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Interfaces
{
    public interface IRepository<T>
    {
        int Count { get; }
        List<T> GetAll();
        Task<T> GetById(int id);
        Task<T> GetByName(string nameCategory);
        Task<IEnumerable<T>> GetAllByName(string nameCategory);
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<T> Delete(int id);
    }
}
