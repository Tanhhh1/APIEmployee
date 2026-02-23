using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Queries.Departments.GetAll
{
    public class GetAllDepartmentQuery : IRequest<List<DepartmentDTO>>
    {
    }
}
