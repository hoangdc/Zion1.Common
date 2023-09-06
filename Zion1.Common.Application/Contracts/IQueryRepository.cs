namespace Zion1.Common.Application.Contracts
{
    public interface IQueryRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
