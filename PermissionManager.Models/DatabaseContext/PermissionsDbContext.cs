using Microsoft.EntityFrameworkCore;
using PermissionManager.Models.Entities;

namespace PermissionManager.Models.DatabaseContext;

public class PermissionsDbContext : DbContext
{
    public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }
    public DbSet<EmployeePermission> EmployeePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeePermission>()
            .HasKey(ep => ep.EmployeePermissionId)
            .HasName("PK_EmployeePermissions"); 

        modelBuilder.Entity<EmployeePermission>()
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeePermissions)
            .HasForeignKey(ep => ep.EmployeeId)
            .HasConstraintName("FK_EmployeePermissions_Employees_EmployeeId");

        modelBuilder.Entity<EmployeePermission>()
            .HasOne(ep => ep.PermissionType)
            .WithMany(p => p.EmployeePermissions)
            .HasForeignKey(ep => ep.PermissionTypeId)
            .HasConstraintName("FK_EmployeePermissions_PermissionTypes_PermissionTypeId");
    }
}