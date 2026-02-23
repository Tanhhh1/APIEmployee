using Application.Common;
using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.Positions.Create
{
    public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, ApiResult<PositionDTO>>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public CreatePositionHandler(
            IPositionRepository positionRepository,
            IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<PositionDTO>> Handle(
            CreatePositionCommand request,
            CancellationToken cancellationToken)
        {
            var position = _mapper.Map<Position>(request);
            await _positionRepository.AddAsync(position);
            await _positionRepository.SaveChangesAsync();
            var dto = _mapper.Map<PositionDTO>(position);
            return ApiResult<PositionDTO>.Success(dto);
        }
    }
}
