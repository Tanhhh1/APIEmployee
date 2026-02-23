using Application.Common;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Commands.Employees.Delete
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, ApiResult<bool>>
    {
        private readonly IEmployeeReponsitory _empReponsitory;
        public DeleteEmployeeHandler(IEmployeeReponsitory empReponsitory)
        {
            _empReponsitory = empReponsitory;
        }

        public async Task<ApiResult<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _empReponsitory.GetByIdAsync(request.Id);
            if (employee == null)
                return null!;

            _empReponsitory.Delete(employee);
            await _empReponsitory.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }
    }
}
