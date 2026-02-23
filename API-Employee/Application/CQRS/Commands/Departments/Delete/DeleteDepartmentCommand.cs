using Application.Common;
using MediatR;

namespace Application.CQRS.Commands.Departments.Delete
{
    public class DeleteDepartmentCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
