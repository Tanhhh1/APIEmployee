using Application.CQRS.Commands.Positions.Create;
using Application.CQRS.Commands.Positions.Update;
using Application.CQRS.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionCommand, Position>();
            CreateMap<UpdatePositionCommand, Position>();            
            CreateMap<Position, PositionDTO>();
        }
    }
}
