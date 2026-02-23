using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Commands.Employees.Update
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, ApiResult<EmployeeDTO>>
    {
        private readonly IEmployeeReponsitory _empReponsitory;
        private readonly IMapper _mapper;

        public UpdateEmployeeHandler(IEmployeeReponsitory empReponsitory, IMapper mapper)
        {
            _empReponsitory = empReponsitory;
            _mapper = mapper;
        }

        public async Task<ApiResult<EmployeeDTO>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _empReponsitory.GetByIdAsync(request.Id);
            if (employee == null)
                return ApiResult<EmployeeDTO>.Failure(new[] { "Employee not found" });

            _mapper.Map(request, employee);
            _empReponsitory.Update(employee);
            await _empReponsitory.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDTO>(employee);
            return ApiResult<EmployeeDTO>.Success(dto);
        }
    }
}
