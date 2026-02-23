using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Queries.Positions.GetAll
{
    public class GetAllPositionQuery : IRequest<List<PositionDTO>>
    {
    }
}
