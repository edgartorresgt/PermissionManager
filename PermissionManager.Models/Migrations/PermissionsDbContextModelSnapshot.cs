﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PermissionManager.Models.DatabaseContext;

#nullable disable

namespace PermissionManager.Models.Migrations
{
    [DbContext(typeof(PermissionsDbContext))]
    partial class PermissionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PermissionManager.Models.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PermissionManager.Models.Entities.EmployeePermission", b =>
                {
                    b.Property<int>("EmployeePermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeePermissionId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionTypeId")
                        .HasColumnType("int");

                    b.HasKey("EmployeePermissionId")
                        .HasName("PK_EmployeePermissions");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PermissionTypeId");

                    b.ToTable("EmployeePermissions");
                });

            modelBuilder.Entity("PermissionManager.Models.Entities.PermissionType", b =>
                {
                    b.Property<int>("PermissionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionTypeId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionTypeId");

                    b.ToTable("PermissionTypes");
                });

            modelBuilder.Entity("PermissionManager.Models.Entities.EmployeePermission", b =>
                {
                    b.HasOne("PermissionManager.Models.Entities.Employee", "Employee")
                        .WithMany("EmployeePermissions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EmployeePermissions_Employees_EmployeeId");

                    b.HasOne("PermissionManager.Models.Entities.PermissionType", "PermissionType")
                        .WithMany("EmployeePermissions")
                        .HasForeignKey("PermissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EmployeePermissions_PermissionTypes_PermissionTypeId");

                    b.Navigation("Employee");

                    b.Navigation("PermissionType");
                });

            modelBuilder.Entity("PermissionManager.Models.Entities.Employee", b =>
                {
                    b.Navigation("EmployeePermissions");
                });

            modelBuilder.Entity("PermissionManager.Models.Entities.PermissionType", b =>
                {
                    b.Navigation("EmployeePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}