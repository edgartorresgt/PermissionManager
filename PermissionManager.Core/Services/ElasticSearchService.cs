using Microsoft.Extensions.Options;
using Nest;
using PermissionManager.Core.Interfaces;
using PermissionManager.Models.Entities;

namespace PermissionManager.Core.Services;

public class ElasticSearchService : IElasticSearchService
{
    private readonly ElasticClient _client;

    public ElasticSearchService(IOptions<ElasticSearchService> settings)
    {
        var connectionSettings = new ConnectionSettings(new Uri(settings.Value.ToString()!))
            .DefaultIndex("permissions");

        _client = new ElasticClient(connectionSettings);
    }

    public async Task IndexAsync(EmployeePermission permission)
    {
        await _client.IndexDocumentAsync(permission);
    }
}