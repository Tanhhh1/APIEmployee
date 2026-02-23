using Application.Common;
using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Commands.Departments.Create
{
    public class CreateDepartmentCommand : IRequest<ApiResult<DepartmentDTO>>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
