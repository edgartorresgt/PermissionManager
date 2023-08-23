using System.Linq.Expressions;

namespace PermissionManager.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T?>> GetAllAsync();
    Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter);
    void Add(T? entity);
    void Update(T? entity);
    void Delete(T? entity);
}
