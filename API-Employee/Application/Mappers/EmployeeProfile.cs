using Application.CQRS.Commands.Employees.Create;
using Application.CQRS.Commands.Employees.Update;
using Application.CQRS.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();            
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
