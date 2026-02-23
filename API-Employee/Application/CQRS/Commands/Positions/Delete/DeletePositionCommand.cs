using Application.Common;
using MediatR;

namespace Application.CQRS.Commands.Positions.Delete
{
    public class DeletePositionCommand : IRequest<ApiResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
