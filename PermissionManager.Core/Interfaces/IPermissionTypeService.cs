using PermissionManager.Models.Entities;

namespace PermissionManager.Core.Interfaces;

public interface IPermissionTypeService
{
    Task<IEnumerable<PermissionType>> GetAllPermissionTypesAsync();
    Task<PermissionType> GetPermissionTypeByIdAsync(int id);
    Task<bool> AddPermissionTypeAsync(PermissionType type);
    Task<bool> UpdatePermissionTypeAsync(PermissionType type);
    Task<bool> DeletePermissionTypeAsync(int id);
}