using Microsoft.EntityFrameworkCore;
using PermissionManager.Models.DatabaseContext;
using PermissionManager.Repositories.Interfaces;
using PermissionManager.Repositories.Repositories;

namespace PermissionManager.API.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabaseServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<PermissionsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, 
                optionsBuilder => optionsBuilder.MigrationsAssembly("PermissionManager.Models")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void EnsureMigrationOfContext<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        using var serviceScope = serviceProvider.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<T>();
        context!.Database.Migrate();
    }
}