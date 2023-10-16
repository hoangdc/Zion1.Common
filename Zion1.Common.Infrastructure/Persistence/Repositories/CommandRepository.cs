using Microsoft.EntityFrameworkCore;
using Zion1.Common.Application.Contracts;

namespace Zion1.Common.Infrastructure.Persistence.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        public readonly DbContext DBContext;
        public CommandRepository(DbContext dbContext) 
        {
            DBContext = dbContext;
        }

        public async Task<T> AddAsync(T item)
        {
            await DBContext.Set<T>().AddAsync(item);
            await DBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            DBContext.Set<T>().Update(item);
            await DBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> DeleteAsync(T item)
        {
            DBContext.Set<T>().Remove(item);
            await DBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DBContext.Set<T>().FindAsync(id);
        }

    }
}
