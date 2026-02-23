using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Phone)
                   .HasMaxLength(20);

            builder.Property(x => x.DateOfBirth)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(x => x.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(x => x.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Position)
                   .WithMany(p => p.Employees)
                   .HasForeignKey(x => x.PositionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Attendances)
                   .WithOne(a => a.Employees)
                   .HasForeignKey(a => a.EmployeeId);

            builder.HasData(
                new Employee
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    FullName = "Nguyen Van A",
                    Email = "a.nguyen@company.com",
                    Phone = "0909123456",
                    DateOfBirth = new DateTime(1995, 5, 10),
                    DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    PositionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                },
                new Employee
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    FullName = "Tran Thi B",
                    Email = "b.tran@company.com",
                    Phone = "0912345678",
                    DateOfBirth = new DateTime(1993, 8, 20),
                    DepartmentId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    PositionId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                }
            );

        }
    }
}
