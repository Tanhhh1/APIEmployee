using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Application.CQRS.Commands.Positions.Update
{
    public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand, ApiResult<PositionDTO>>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public UpdatePositionHandler(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<PositionDTO>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await _positionRepository.GetByIdAsync(request.Id);
            if (position == null)
                return ApiResult<PositionDTO>.Failure(new[] { "Position not found" });
            _mapper.Map(request, position);
            _positionRepository.Update(position);
            await _positionRepository.SaveChangesAsync();
            var dto = _mapper.Map<PositionDTO>(position);
            return ApiResult<PositionDTO>.Success(dto);
        }
    }
}
