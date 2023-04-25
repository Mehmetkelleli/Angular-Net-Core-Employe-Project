using Backend.Application.Abstractions;
using Backend.Domain.EntityModels;
using Backend.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Persistence.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
        private DataContext _DataContext;

        public GenericRepository(DataContext dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<bool> CreateAsync(T T)
        {
            T.CreatedTime= DateTime.Now;
            var result = await _DataContext.Set<T>().AddAsync(T);
            return result.State == Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public IQueryable<T> GetAll(bool tracking = false)
        {
            var list = _DataContext.Set<T>().AsQueryable();
            if (!tracking)
            {
                list = list.AsNoTracking();
            }
            return list;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = false)
        {
            var list = _DataContext.Set<T>().AsQueryable();
            if (!tracking)
            {
                list = list.AsNoTracking();
            }
            return await list.FirstOrDefaultAsync(i=>i.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleByIdAsync(Expression<Func<T, bool>> Expression, bool tracking = false)
        {
            var list = _DataContext.Set<T>().AsQueryable();
            if (!tracking)
            {
                list = list.AsNoTracking();
            }
            return await list.FirstOrDefaultAsync(Expression);
        }

        public bool Remove(T T)
        {
            var result = _DataContext.Set<T>().Remove(T);
            return result.State == EntityState.Deleted;
        }

        public async Task<int> SaveAsync()
        {
            return await _DataContext.SaveChangesAsync();
        }

        public bool Update(T T)
        {
            var result = _DataContext.Set<T>().Update(T);
            return result.State == EntityState.Modified;
        }
    }
}
