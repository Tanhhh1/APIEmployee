using Application.Common;
using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Queries.Employees.GetAll
{
    public class GetAllEmployeeQuery : IRequest<PageList<EmployeeDTO>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
