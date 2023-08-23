namespace PermissionManager.Models.Entities;

public class EmployeePermission
{
    public int EmployeePermissionId { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int PermissionTypeId { get; set; }
    public PermissionType PermissionType { get; set; }
}