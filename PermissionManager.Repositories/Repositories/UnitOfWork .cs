using PermissionManager.Models.DatabaseContext;
using PermissionManager.Models.Entities;
using PermissionManager.Repositories.Interfaces;

namespace PermissionManager.Repositories.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly PermissionsDbContext _permissionsDbContext;
    private readonly IGenericRepository<Employee> _employees;
    private readonly IGenericRepository<PermissionType> _permissionTypes;
    private readonly IGenericRepository<EmployeePermission> _employeePermissions;

    public UnitOfWork(IGenericRepository<Employee> employees,
        IGenericRepository<PermissionType> permissionTypes,
        IGenericRepository<EmployeePermission> employeePermissions, 
        PermissionsDbContext permissionsDbContext)
    {
        _employees = employees;
        _permissionTypes = permissionTypes;
        _employeePermissions = employeePermissions;
        _permissionsDbContext = permissionsDbContext;
    }

    public UnitOfWork(PermissionsDbContext permissionsDbContext)
    {
        _permissionsDbContext = permissionsDbContext;
    }

    public IGenericRepository<Employee> Employees { get; set; }
    public IGenericRepository<PermissionType> PermissionTypes { get;  set; }
    public IGenericRepository<EmployeePermission> EmployeePermissions { get;  set; }

    public async Task<int> SaveChangesAsync()
    {
        return await _permissionsDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _permissionsDbContext.Dispose();
    }
}
