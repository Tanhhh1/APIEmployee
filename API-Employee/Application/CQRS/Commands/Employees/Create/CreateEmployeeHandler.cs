using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.Employees.Create
{
    public class CreateEmployeeHandler
        : IRequestHandler<CreateEmployeeCommand, ApiResult<EmployeeDTO>>
    {
        private readonly IEmployeeReponsitory _empReponsitory;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            IEmployeeReponsitory empReponsitory,
            IMapper mapper)
        {
            _empReponsitory = empReponsitory;
            _mapper = mapper;
        }

        public async Task<ApiResult<EmployeeDTO>> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);

            await _empReponsitory.AddAsync(employee);
            await _empReponsitory.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDTO>(employee);

            return ApiResult<EmployeeDTO>.Success(dto);
        }
    }
}
