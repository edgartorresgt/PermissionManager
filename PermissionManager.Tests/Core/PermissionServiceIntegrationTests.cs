using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services;
using PermissionManager.Models.DatabaseContext;
using PermissionManager.Models.Entities;
using PermissionManager.Repositories.Interfaces;
using PermissionManager.Repositories.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PermissionManager.Tests.Core;

public class PermissionServiceIntegrationTests : IDisposable
{
    private readonly PermissionService _service;
    private readonly DbContextOptions<PermissionsDbContext> _options;

    public PermissionServiceIntegrationTests()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _options = new DbContextOptionsBuilder<PermissionsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name to avoid sharing state between tests
            .UseLoggerFactory(loggerFactory)
            .Options;

        var context = new PermissionsDbContext(_options);
        context.Database.EnsureCreated(); 

        IUnitOfWork unitOfWork = new UnitOfWork(context);
        unitOfWork.EmployeePermissions = new GenericRepository<EmployeePermission>(context);
        Mock<ILogger<PermissionService>> loggerMock = new();
        Mock<IElasticSearchService> elasticSearchMock = new();
        Mock<IKafkaProducerService> kafkaProducerMock = new();
 
        _service = new PermissionService(unitOfWork, elasticSearchMock.Object, kafkaProducerMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task RequestPermission_ShouldAddPermissionToDatabase()
    {
        // Arrange
        var testEmployeeId = 1;
        var testPermissionTypeId = 1;

        // Act
        var result = await _service.RequestPermission(testEmployeeId, testPermissionTypeId);

        // Assert
        Assert.True(result);

        await using var context = new PermissionsDbContext(_options);
        Assert.Single(context.EmployeePermissions);
    }

    public void Dispose()
    {
        using var context = new PermissionsDbContext(_options);
        context.Database.EnsureDeleted(); // Delete database after test
    }
}
