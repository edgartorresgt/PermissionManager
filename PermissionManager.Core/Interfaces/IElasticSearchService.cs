using PermissionManager.Models.Entities;

namespace PermissionManager.Core.Interfaces;

public interface IElasticSearchService
{
    Task IndexAsync(EmployeePermission permission);
}