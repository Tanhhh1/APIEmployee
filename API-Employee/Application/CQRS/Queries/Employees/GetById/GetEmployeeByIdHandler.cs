using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Queries.Employees.GetById
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        private readonly IEmployeeReponsitory _empResponsitory;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(IEmployeeReponsitory empResponsitory, IMapper mapper)
        {
            _empResponsitory = empResponsitory;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _empResponsitory.GetByIdAsync(request.Id);
            if (employee == null)
                return null!;

            var employeeDto = _mapper.Map<EmployeeDTO>(employee);
            return employeeDto;
        }
    }
}
