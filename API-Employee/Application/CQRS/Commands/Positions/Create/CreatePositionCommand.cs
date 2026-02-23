using Application.Common;
using Application.CQRS.DTOs;
using MediatR;


namespace Application.CQRS.Commands.Positions.Create
{
    public class CreatePositionCommand : IRequest<ApiResult<PositionDTO>>
    {
        public string Name { get; set; } = null!;
    }
}
