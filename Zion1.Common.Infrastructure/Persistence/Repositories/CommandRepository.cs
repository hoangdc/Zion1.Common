using Microsoft.EntityFrameworkCore;
using Zion1.Common.Application.Contracts;

namespace Zion1.Common.Infrastructure.Persistence.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly DbContext _commonDBContext;
        public CommandRepository(DbContext commonDBContext) 
        {
            _commonDBContext = commonDBContext;
        }

        public async Task<T> AddAsync(T item)
        {
            await _commonDBContext.Set<T>().AddAsync(item);
            await _commonDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _commonDBContext.Set<T>().Update(item);
            await _commonDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> DeleteAsync(T item)
        {
            _commonDBContext.Set<T>().Remove(item);
            await _commonDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _commonDBContext.Set<T>().FindAsync(id);
        }

    }
}
