using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.Departments.Create
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, ApiResult<DepartmentDTO>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public CreateDepartmentHandler(
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<DepartmentDTO>> Handle(
            CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);
            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();
            var dto = _mapper.Map<DepartmentDTO>(department);
            return ApiResult<DepartmentDTO>.Success(dto);
        }
    }
}
