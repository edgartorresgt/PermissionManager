using Microsoft.Extensions.Logging;
using Moq;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services;
using PermissionManager.Models.Entities;
using PermissionManager.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace PermissionManager.Tests.Core;

public class PermissionServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IElasticSearchService> _elasticSearchServiceMock = new();
    private readonly Mock<IKafkaProducerService> _kafkaProducerServiceMock = new();
    private readonly Mock<ILogger<PermissionService>> _loggerMock = new();
    private readonly PermissionService _service;
    public PermissionServiceTests()
    {
        _service = new PermissionService(
            _unitOfWorkMock.Object,
            _elasticSearchServiceMock.Object,
            _kafkaProducerServiceMock.Object,
            _loggerMock.Object);

        _unitOfWorkMock.Setup(x => x.EmployeePermissions.Add(It.IsAny<EmployeePermission>()))
            .Callback<EmployeePermission>(_ => { });
    }

    [Fact]
    public async Task RequestPermission_AddsPermissionSuccessfully()
    {
        // Arrange
        var employeeId = 1;
        var permissionTypeId = 100;
        _unitOfWorkMock.Setup(x => x.EmployeePermissions.Add(It.IsAny<EmployeePermission>()))
            .Callback<EmployeePermission>(_ => { });
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1); // Indicate a change occurred

        // Act
        var result = await _service.RequestPermission(employeeId, permissionTypeId);

        // Assert
        Assert.True(result);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task RequestPermission_FailsToAddPermission()
    {
        // Arrange
        var employeeId = 1;
        var permissionTypeId = 100;


        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(0); // No changes occurred

        // Act
        var result = await _service.RequestPermission(employeeId, permissionTypeId);

        // Assert
        Assert.False(result);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ModifyPermission_ModifiesSuccessfully()
    {
        // Arrange
        var permissionId = 1;
        var newPermissionTypeId = 101;

        var existingPermission = new EmployeePermission
        {
            PermissionTypeId = 100
        };

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1); // Indicate a change occurred
        _unitOfWorkMock.Setup(u => u.EmployeePermissions.GetByIdAsync(permissionId)).ReturnsAsync(existingPermission);

        // Act
        var result = await _service.ModifyPermission(permissionId, newPermissionTypeId);

        // Assert
        Assert.True(result);
        Assert.Equal(newPermissionTypeId, existingPermission.PermissionTypeId);
    }

    [Fact]
    public async Task ModifyPermission_FailsWhenPermissionNotFound()
    {
        // Arrange
        var permissionId = 1;
        var newPermissionTypeId = 101;

        _unitOfWorkMock.Setup(u => u.EmployeePermissions.GetByIdAsync(permissionId)).ReturnsAsync((EmployeePermission)null!);

        // Act
        var result = await _service.ModifyPermission(permissionId, newPermissionTypeId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetPermissions_WithPermissions_ReturnsListOfPermissions()
    {
        // Arrange
        var testEmployeeId = 1;
        var permissionsList = new List<EmployeePermission>
        {
            new() { EmployeeId = testEmployeeId, PermissionTypeId = 1 },
            new() { EmployeeId = testEmployeeId, PermissionTypeId = 2 }
        };

        _unitOfWorkMock.Setup(uow => uow.EmployeePermissions.GetAllAsync(It.IsAny<Expression<Func<EmployeePermission, bool>>>()))
            .ReturnsAsync(permissionsList);

        // Act
        var result = await _service.GetPermissions(testEmployeeId);

        // Assert
        Assert.Equal(permissionsList.Count, result.Count());

    }

    [Fact]
    public async Task GetPermissions_NoPermissions_ReturnsEmptyList()
    {
        // Arrange
        var testEmployeeId = 1;
        _unitOfWorkMock.Setup(uow => uow.EmployeePermissions.GetAllAsync(It.IsAny<Expression<Func<EmployeePermission, bool>>>()))
            .ReturnsAsync(new List<EmployeePermission>());

        // Act
        var result = await _service.GetPermissions(testEmployeeId);

        // Assert
        Assert.Empty(result);
    }

}