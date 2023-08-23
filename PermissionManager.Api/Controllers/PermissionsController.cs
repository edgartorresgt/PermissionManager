using Microsoft.AspNetCore.Mvc;
using PermissionManager.Core.Interfaces;

namespace PermissionManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;
    private readonly ILogger<PermissionsController> _logger;
    public PermissionsController(IPermissionService permissionService, ILogger<PermissionsController> logger)
    {
        _permissionService = permissionService;
        _logger = logger;
    }

    [HttpPost("request")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestPermission(int employeeId, int permissionTypeId)
    {
        _logger.LogInformation($"Executing RequestPermission operation for employee ID {employeeId} and permission Type {permissionTypeId}");
        var result = await _permissionService.RequestPermission(employeeId, permissionTypeId);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpPut("modify/{permissionId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ModifyPermission(int permissionId, int newPermissionTypeId)
    {
        _logger.LogInformation($"Executing RequestPermission operation for permission ID {permissionId} and permission Type {newPermissionTypeId}");
        var result = await _permissionService.ModifyPermission(permissionId, newPermissionTypeId);
        if (result) return Ok();
        return NotFound();
    }

    [HttpGet("get/{employeeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPermissions(int employeeId)
    {
        _logger.LogInformation($"Executing GetPermissions operation for employee ID {employeeId}");
        var permissions = await _permissionService.GetPermissions(employeeId);
        if (permissions.Any())
            return Ok(permissions);
        return NoContent();
    }
}
