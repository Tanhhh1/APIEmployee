using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EmployeeAttendanceConfiguration : IEntityTypeConfiguration<EmployeeAttendance>
    {
        public void Configure(EntityTypeBuilder<EmployeeAttendance> builder)
        {
            builder.ToTable("EmployeeAttendances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WorkDate)
                   .IsRequired();

            builder.Property(x => x.CheckIn)
                   .IsRequired();

            builder.Property(x => x.CheckOut);

            builder.Property(x => x.TotalHours)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.HasIndex(x => new { x.EmployeeId, x.WorkDate })
                   .IsUnique();

            builder.HasOne(x => x.Employees)
                   .WithMany(e => e.Attendances)
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new EmployeeAttendance
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    EmployeeId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    WorkDate = new DateTime(2025, 1, 5),
                    CheckIn = new TimeSpan(8, 0, 0),
                    CheckOut = new TimeSpan(17, 0, 0),
                    TotalHours = 8.0m,
                    CreatedAt = new DateTime(2025, 1, 5),
                    UpdatedAt = new DateTime(2025, 1, 5)
                },
                new EmployeeAttendance
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    EmployeeId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    WorkDate = new DateTime(2025, 1, 5),
                    CheckIn = new TimeSpan(8, 30, 0),
                    CheckOut = new TimeSpan(17, 30, 0),
                    TotalHours = 8.0m,
                    CreatedAt = new DateTime(2025, 1, 5),
                    UpdatedAt = new DateTime(2025, 1, 5)
                }
            );

        }
    }
}
