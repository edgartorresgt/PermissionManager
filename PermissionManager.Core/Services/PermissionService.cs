using Microsoft.Extensions.Logging;
using PermissionManager.Core.Extensions;
using PermissionManager.Core.Interfaces;
using PermissionManager.Models.Configuration;
using PermissionManager.Models.Entities;
using PermissionManager.Models.Models;
using PermissionManager.Repositories.Interfaces;

namespace PermissionManager.Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticSearchService _elasticSearchService;
    private readonly IKafkaProducerService _kafkaProducerService;
    private readonly ILogger<PermissionService> _logger;
    public PermissionService(IUnitOfWork unitOfWork, IElasticSearchService elasticSearchService, IKafkaProducerService kafkaProducerService, ILogger<PermissionService> logger)
    {
        _unitOfWork = unitOfWork;
        _elasticSearchService = elasticSearchService;
        _kafkaProducerService = kafkaProducerService;
        _logger = logger;
    }

    public async Task<bool> RequestPermission(int employeeId, int permissionTypeId)
    {
        _logger.LogInformation($"Requesting permission for employee {employeeId} with permission type {permissionTypeId}");

        var permission = new EmployeePermission
        {
            EmployeeId = employeeId,
            PermissionTypeId = permissionTypeId
        };

        _unitOfWork.EmployeePermissions.Add(permission);

        var result = await _unitOfWork.SaveChangesAsync();
        await _elasticSearchService.IndexAsync(permission);
        await _kafkaProducerService.ProduceMessage(new OperationDto { NameOperation = NameOperation.Request.GetDescription() });

        _logger.LogInformation($"Permission requested successfully for employee {employeeId}");
        return result > 0;
    }

    public async Task<bool> ModifyPermission(int permissionId, int newPermissionTypeId)
    {
        _logger.LogInformation($"Modifying permission {permissionId} to permission type {newPermissionTypeId}");
        var permission = await _unitOfWork.EmployeePermissions.GetByIdAsync(permissionId);
        if (permission == null)
        {
            _logger.LogWarning($"Permission with ID {permissionId} not found");
            return false;
        }

        permission.PermissionTypeId = newPermissionTypeId;
        _unitOfWork.EmployeePermissions.Update(permission);

        var result = await _unitOfWork.SaveChangesAsync();
        await _kafkaProducerService.ProduceMessage(new OperationDto { NameOperation = NameOperation.Modify.GetDescription() });

        _logger.LogInformation($"Permission {permissionId} modified successfully");
        return result > 0;
    }

    public async Task<IEnumerable<EmployeePermission>> GetPermissions(int employeeId)
    {
        _logger.LogInformation($"Fetching permissions for employee {employeeId}");

        var permissions = (await _unitOfWork.EmployeePermissions.GetAllAsync(p => p.EmployeeId == employeeId))!;
        await _kafkaProducerService.ProduceMessage(new OperationDto { NameOperation = NameOperation.Get.GetDescription() });

        _logger.LogInformation($"Fetched {permissions?.Count()} permissions for employee {employeeId}");
        return permissions!;
    }
}
