using Application.CQRS.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Queries.Positions.GetAll
{
    public class GetAllPositionHandler : IRequestHandler<GetAllPositionQuery, List<PositionDTO>>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;
        public GetAllPositionHandler(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }
        public async Task<List<PositionDTO>> Handle(GetAllPositionQuery request, CancellationToken cancellationToken)
        {
            var positions = await _positionRepository.GetAllAsync();
            return _mapper.Map<List<PositionDTO>>(positions);
        }
    }
}
