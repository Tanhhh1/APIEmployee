using Application.Common;
using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Commands.Positions.Update
{
    public class UpdatePositionCommand : IRequest<ApiResult<PositionDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}

