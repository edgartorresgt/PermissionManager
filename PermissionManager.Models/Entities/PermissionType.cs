namespace PermissionManager.Models.Entities;

public class PermissionType
{
    public int PermissionTypeId { get; set; }
    public string Description { get; set; }
    public List<EmployeePermission> EmployeePermissions { get; set; }
}