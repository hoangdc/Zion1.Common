namespace Zion1.Common.Application.Contracts
{
    public interface ICommandRepository<T>
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(T item);
        Task<T> GetByIdAsync(int id);
    }
}
