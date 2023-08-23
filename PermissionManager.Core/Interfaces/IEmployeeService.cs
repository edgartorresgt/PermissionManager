using PermissionManager.Models.Entities;

namespace PermissionManager.Core.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<bool> AddEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(int id);
}