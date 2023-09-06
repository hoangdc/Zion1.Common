using Microsoft.EntityFrameworkCore;
using Zion1.Common.Application.Contracts;

namespace Zion1.Common.Infrastructure.Persistence.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly DbContext _commonDBContext;

        public QueryRepository(DbContext commonDBContext) 
        {
            _commonDBContext = commonDBContext;
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _commonDBContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _commonDBContext.Set<T>().FindAsync(id);
        }
        
    }
}
