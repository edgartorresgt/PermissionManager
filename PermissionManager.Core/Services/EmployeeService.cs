using PermissionManager.Core.Interfaces;
using PermissionManager.Models.Entities;
using PermissionManager.Repositories.Interfaces;

namespace PermissionManager.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return (await _unitOfWork.Employees.GetAllAsync())!;
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return (await _unitOfWork.Employees.GetByIdAsync(id))!;
    }

    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
         _unitOfWork.Employees.Add(employee);
         return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        _unitOfWork.Employees.Update(employee);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null) return false;

        _unitOfWork.Employees.Delete(employee);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}