using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Application.CQRS.Queries.Departments.GetById
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDTO>
    {
        private readonly IDepartmentRepository _departmentResponsitory;
        private readonly IMapper _mapper;

        public GetDepartmentByIdHandler(IDepartmentRepository departmentResponsitory, IMapper mapper)
        {
            _departmentResponsitory = departmentResponsitory;
            _mapper = mapper;
        }

        public async Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentResponsitory.GetByIdAsync(request.Id);
            if (department == null)
                return null!;
            var departmentDto = _mapper.Map<DepartmentDTO>(department);
            return departmentDto;
        }
    }
}
