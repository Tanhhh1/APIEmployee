using Application.Common;
using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Commands.Employees.Update
{
    public class UpdateEmployeeCommand : IRequest<ApiResult<EmployeeDTO>>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
    }
}
