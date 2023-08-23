using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PermissionManager.Models.DatabaseContext;
using PermissionManager.Repositories.Interfaces;

namespace PermissionManager.Repositories.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly PermissionsDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(PermissionsDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T?>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _context.Set<T>();

        query = query.Where(filter);

        return await query.ToListAsync();
    }

    public void Add(T? entity) => _dbSet.Add(entity!);

    public void Update(T? entity) => _dbSet.Update(entity!);

    public void Delete(T? entity) => _dbSet.Remove(entity!);
}