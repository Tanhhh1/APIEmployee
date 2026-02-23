using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;


namespace Infrastructure.Repositories
{
    public class DepartmentRepository(DatabaseContext dbContext) : BaseRepository<Department>(dbContext), IDepartmentRepository
    {
    }
}
