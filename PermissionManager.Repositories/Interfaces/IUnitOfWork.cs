using PermissionManager.Models.Entities;

namespace PermissionManager.Repositories.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Employee> Employees { get; set; }
    IGenericRepository<PermissionType> PermissionTypes { get; set; }
    IGenericRepository<EmployeePermission> EmployeePermissions { get; set; }
    Task<int> SaveChangesAsync();
}

