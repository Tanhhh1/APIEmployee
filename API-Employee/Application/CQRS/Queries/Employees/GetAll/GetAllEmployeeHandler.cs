using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Queries.Employees.GetAll
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, PageList<EmployeeDTO>>
    {
        private readonly IEmployeeReponsitory _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeeHandler(IEmployeeReponsitory repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageList<EmployeeDTO>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var queryable = _repository.GetByCondition(e => string.IsNullOrEmpty(request.Search) || e.FullName.Contains(request.Search));
            var pagedEmployees = await PageList<Employee>.ToPagedListAsync(queryable, request.PageIndex, request.PageSize);

            var mappedItems = _mapper.Map<List<EmployeeDTO>>(pagedEmployees.Items);
            return new PageList<EmployeeDTO>(mappedItems, pagedEmployees.TotalCount, pagedEmployees.PageIndex, pagedEmployees.PageSize
            );
        }
    }

}
