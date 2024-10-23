namespace CleanArchMvc.Domain.Interfaces;

public interface IRepositoryBase<T> 
{
    Task<T> CreateAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int? id);
    Task RemoveAsync(T entity);
    Task<T> UpdateAsync(T entity);
}
