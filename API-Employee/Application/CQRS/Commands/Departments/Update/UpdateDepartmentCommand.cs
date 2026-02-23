using Application.Common;
using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Commands.Departments.Update
{
    public class UpdateDepartmentCommand : IRequest<ApiResult<DepartmentDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
