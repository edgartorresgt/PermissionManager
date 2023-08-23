using Microsoft.AspNetCore.Mvc;
using PermissionManager.Core.Interfaces;
using PermissionManager.Models.Entities;

namespace PermissionManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionTypesController:ControllerBase
{
    private readonly IPermissionTypeService _permissionTypeService;
    private readonly ILogger<PermissionTypesController> _logger;

    public PermissionTypesController(IPermissionTypeService permissionTypeService, ILogger<PermissionTypesController> logger)
    {
        _permissionTypeService = permissionTypeService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Executing GetAll operation for permission types.");
        return Ok(await _permissionTypeService.GetAllPermissionTypesAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"Executing Get operation for permission type ID: {id}");
        var permissionType = await _permissionTypeService.GetPermissionTypeByIdAsync(id);
        if (permissionType == null)
        {
            _logger.LogWarning($"Employee not found for ID: {id}");
            return NotFound();
        }
        return Ok(permissionType);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(PermissionType permissionType)
    {
        _logger.LogInformation("Executing Add operation for permission type.");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for Add operation permission type.");
            return BadRequest(ModelState);
        }
        await _permissionTypeService.AddPermissionTypeAsync(permissionType);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, PermissionType permissionType)
    {
        _logger.LogInformation($"Executing Update operation for permission type ID: {id}");

        if (id != permissionType.PermissionTypeId) return BadRequest();

        var success = await _permissionTypeService.UpdatePermissionTypeAsync(permissionType);
        if (!success)
        {
            _logger.LogWarning($"Failed to update permission type with ID: {id}");
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _permissionTypeService.DeletePermissionTypeAsync(id);
        if (!success)
        {
            _logger.LogWarning($"Failed to delete permission type with ID: {id}");
            return NotFound();
        }

        return NoContent();
    }
}