using Application.Common;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Application.CQRS.Commands.Positions.Delete
{
    public class DeletePositionHandler : IRequestHandler<DeletePositionCommand, ApiResult<bool>>
    {
        private readonly IPositionRepository _positionRepository;
        public DeletePositionHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<ApiResult<bool>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await _positionRepository.GetByIdAsync(request.Id);
            if (position == null)
                return null!;
            _positionRepository.Delete(position);
            await _positionRepository.SaveChangesAsync();
            return ApiResult<bool>.Success(true);
        }
    }
}
