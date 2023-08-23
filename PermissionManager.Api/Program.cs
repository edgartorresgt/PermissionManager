using PermissionManager.API.Configuration;
using PermissionManager.Models.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);
if (!string.IsNullOrEmpty(builder.Environment.EnvironmentName))
{
    builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
}
builder.Services.ConfigureLogServices();
builder.Services.ConfigureDatabaseServices(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.Services.EnsureMigrationOfContext<PermissionsDbContext>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
