using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Queries.Positions.GetById
{
    public class GetPositionByIdQuery : IRequest<PositionDTO>
    {
        public Guid Id { get; set; }

        public GetPositionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
