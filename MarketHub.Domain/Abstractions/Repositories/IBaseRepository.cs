using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Repository
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);
        Task Delete(int id);
        Task<List<T>?> GetAll();
        Task<T?> GetOne(int id);
        Task Update(T entity);
    }
}
