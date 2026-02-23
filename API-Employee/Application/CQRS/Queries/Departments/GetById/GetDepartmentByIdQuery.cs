using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Queries.Departments.GetById
{
    public class GetDepartmentByIdQuery : IRequest<DepartmentDTO>
    {
        public Guid Id { get; set; }

        public GetDepartmentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
