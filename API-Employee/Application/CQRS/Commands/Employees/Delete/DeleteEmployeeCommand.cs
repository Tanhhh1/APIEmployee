using Application.Common;
using MediatR;

namespace Application.CQRS.Commands.Employees.Delete
{
    public class DeleteEmployeeCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
