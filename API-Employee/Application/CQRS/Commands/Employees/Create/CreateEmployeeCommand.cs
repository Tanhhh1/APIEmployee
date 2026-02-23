using MediatR;
using Application.Common;
using Application.CQRS.DTOs;

namespace Application.CQRS.Commands.Employees.Create
{
    public class CreateEmployeeCommand : IRequest<ApiResult<EmployeeDTO>>
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
    }
}
