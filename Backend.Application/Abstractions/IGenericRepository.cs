using Backend.Domain.EntityModels;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Backend.Application.Abstractions
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        Task<bool> CreateAsync(T T);
        bool Update(T T);
        bool Remove(T T);
        Task<T> GetByIdAsync(string id,bool tracking = false);
        Task<T> GetSingleByIdAsync(Expression<Func<T,bool>> Expression ,bool tracking = false);
        IQueryable<T> GetAll(bool tracking = false);
        Task<int> SaveAsync();

    }
}
