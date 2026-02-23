using Application.CQRS.DTOs;
using Application.CQRS.Queries.Employees.GetById;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Queries.Positions.GetById
{
    public class GetPositionByIdHandler : IRequestHandler<GetPositionByIdQuery, PositionDTO>
    {
        private readonly IPositionRepository _positionResponsitory;
        private readonly IMapper _mapper;

        public GetPositionByIdHandler(IPositionRepository positionResponsitory, IMapper mapper)
        {
            _positionResponsitory = positionResponsitory;
            _mapper = mapper;
        }

        public async Task<PositionDTO> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var position = await _positionResponsitory.GetByIdAsync(request.Id);
            if (position == null)
                return null!;

            var positionDto = _mapper.Map<PositionDTO>(position);
            return positionDto;
        }
    }
}
