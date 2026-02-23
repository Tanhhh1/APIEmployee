using Application.Common;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Commands.Departments.Delete
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, ApiResult<bool>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ApiResult<bool>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);
            if (department == null)
                return null!;

            _departmentRepository.Delete(department);
            await _departmentRepository.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }
    }
}
