using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Queries.Departments.GetAll
{
    public class GetAllDepartmentHandler : IRequestHandler<GetAllDepartmentQuery, List<DepartmentDTO>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetAllDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDTO>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllAsync();
            return _mapper.Map<List<DepartmentDTO>>(departments);
        }
    }
}
