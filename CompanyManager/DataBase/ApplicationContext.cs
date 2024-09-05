using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Database;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<PhotoEmployee> PhotoEmployees { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__F807679CFC71068C");

            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.FkOrganization).HasColumnName("fk_organization");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("middle_name");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("passport");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.FkOrganizationNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkOrganization)
                .HasConstraintName("FK_Employees_Organizations");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.IdEmployeeRole).HasName("PK__Employee__3C87375353037A39");

            entity.ToTable("EmployeeRole");

            entity.Property(e => e.IdEmployeeRole).HasColumnName("id_employeeRole");
            entity.Property(e => e.FkEmployee).HasColumnName("fk_employee");
            entity.Property(e => e.FkRole).HasColumnName("fk_role");

            entity.HasOne(d => d.FkEmployeeNavigation).WithMany(p => p.EmployeeRoles)
                .HasForeignKey(d => d.FkEmployee)
                .HasConstraintName("FK_EmployeeRole_Employee");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.EmployeeRoles)
                .HasForeignKey(d => d.FkRole)
                .HasConstraintName("FK_EmployeeRole_Roles");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.IdOrganization).HasName("PK__Organiza__18F2EEC7EEDDF482");

            entity.Property(e => e.IdOrganization).HasColumnName("id_organization");
            entity.Property(e => e.AddressOrganization)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address_organization");
            entity.Property(e => e.MailOrganization)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mail_organization");
            entity.Property(e => e.NameOrganization)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_organization");
            entity.Property(e => e.PhoneOrganization)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_organization");
        });

        modelBuilder.Entity<PhotoEmployee>(entity =>
        {
            entity.HasKey(e => e.IdPhotoEmployee).HasName("PK__Photo_em__FA410FB095620D14");

            entity.ToTable("Photo_employee");

            entity.Property(e => e.IdPhotoEmployee).HasColumnName("id_photo_employee");
            entity.Property(e => e.FkEmployee).HasColumnName("fk_employee");
            entity.Property(e => e.PhotoEmployee1).HasColumnName("photo_employee");

            entity.HasOne(d => d.FkEmployeeNavigation).WithMany(p => p.PhotoEmployees)
                .HasForeignKey(d => d.FkEmployee)
                .HasConstraintName("FK_Photo_Employees");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__3D48441DC292A6A3");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.NameRole)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
