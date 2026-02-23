using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Commands.Departments.Update
{
    public class UpdateDepartmentHandler
        : IRequestHandler<UpdateDepartmentCommand, ApiResult<DepartmentDTO>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public UpdateDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<DepartmentDTO>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);
            if (department == null)
                return ApiResult<DepartmentDTO>.Failure(new[] { "Department not found" });

            _mapper.Map(request, department);
            _departmentRepository.Update(department);
            await _departmentRepository.SaveChangesAsync();

            var dto = _mapper.Map<DepartmentDTO>(department);
            return ApiResult<DepartmentDTO>.Success(dto);
        }
    }
}
