using Domain.Common;

namespace Domain.Entities
{
    public class EmployeeAttendance : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public decimal TotalHours { get; set; }
        public Employee Employees { get; private set; } = default!;
    }
}
