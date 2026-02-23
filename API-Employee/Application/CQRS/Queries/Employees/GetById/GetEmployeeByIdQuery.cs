using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Queries.Employees.GetById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
    {
        public Guid Id { get; set; }

        public GetEmployeeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
