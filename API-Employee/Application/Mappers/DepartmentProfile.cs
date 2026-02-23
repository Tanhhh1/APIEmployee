using Application.CQRS.Commands.Departments.Create;
using Application.CQRS.Commands.Departments.Update;
using Application.CQRS.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<UpdateDepartmentCommand, Department>();
        }
    }
}
