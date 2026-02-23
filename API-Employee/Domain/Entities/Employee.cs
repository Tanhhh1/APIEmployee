using Domain.Common;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public Department Department { get; set; } = default!;
        public Position Position { get; set; } = default!;
        public ICollection<EmployeeAttendance> Attendances { get; set; }
    }
}
