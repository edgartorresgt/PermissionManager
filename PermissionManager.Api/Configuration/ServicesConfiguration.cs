using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services;
using PermissionManager.Models.Configuration;

namespace PermissionManager.API.Configuration;

public static class ServicesConfiguration
{
    public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPermissionTypeService, PermissionTypeService>();
        services.Configure<ElasticSearchSettings>(configuration.GetSection("ElasticSearch"));
        services.AddSingleton<IElasticSearchService, ElasticSearchService>();
        services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
    }
 
}