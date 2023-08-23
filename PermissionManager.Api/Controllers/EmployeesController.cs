using Microsoft.AspNetCore.Mvc;
using PermissionManager.Core.Interfaces;
using PermissionManager.Models.Entities;

namespace PermissionManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;
    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Executing GetAll operation for employees.");
        return Ok(await _employeeService.GetAllEmployeesAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"Executing Get operation for employee ID: {id}");
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            _logger.LogWarning($"Employee not found for ID: {id}");
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(Employee employee)
    {
        _logger.LogInformation("Executing Add operation for employee.");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for Add operation.");
            return BadRequest(ModelState);
        }
        await _employeeService.AddEmployeeAsync(employee);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        _logger.LogInformation($"Executing Update operation for employee ID: {id}");

        if (id != employee.EmployeeId) return BadRequest();

        var success = await _employeeService.UpdateEmployeeAsync(employee);
        if (!success)
        {
            _logger.LogWarning($"Failed to update employee with ID: {id}");
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation($"Executing Delete operation for employee ID: {id}");

        var success = await _employeeService.DeleteEmployeeAsync(id);
        if (!success)
        {
            _logger.LogWarning($"Failed to delete employee with ID: {id}");
            return NotFound();
        }

        return NoContent();
    }
}