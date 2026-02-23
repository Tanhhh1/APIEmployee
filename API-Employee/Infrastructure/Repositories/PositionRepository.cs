using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;


namespace Infrastructure.Repositories
{
    public class PositionRepository(DatabaseContext dbContext) : BaseRepository<Position>(dbContext), IPositionRepository
    {
    }
}
