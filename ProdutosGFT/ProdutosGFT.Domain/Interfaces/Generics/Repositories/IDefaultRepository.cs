using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdutosGFT.Domain.Interfaces.Generics.Repositories
{
    public interface IDefaultRepository<T> where T : class
    {
        Task<List<T>> SelectAllAsync(Func<T, Object> orderByFilter, bool asc = true);
        Task<List<T>> SelectByConditionAsync(Func<T, bool> condition, Func<T, Object> orderByFilter, bool asc = true);
        Task<T> SelectByIdAsync(long id, bool onlyAtivos = false);
        Task<T> SelectByIdAsNoTrackingAsync(long id, bool onlyAtivos = false);
        Task<T> SaveAsync(T entity);//Update e insert
        Task DeleteByIdAsync(long id);
    }
}