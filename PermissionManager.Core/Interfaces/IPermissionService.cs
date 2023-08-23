using PermissionManager.Models.Entities;

namespace PermissionManager.Core.Interfaces;

public interface IPermissionService
{
    Task<bool> RequestPermission(int employeeId, int permissionTypeId);
    Task<bool> ModifyPermission(int permissionId, int newPermissionTypeId);
    Task<IEnumerable<EmployeePermission>> GetPermissions(int employeeId);
}
