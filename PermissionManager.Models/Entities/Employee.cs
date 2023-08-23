namespace PermissionManager.Models.Entities;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<EmployeePermission> EmployeePermissions { get; set; }
}