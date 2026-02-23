using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class EmployeeRepository(DatabaseContext dbContext) : BaseRepository<Employee>(dbContext), IEmployeeReponsitory
    {
        public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
        {
            return await _dbContext.Employees
                .Where(e => e.FullName.Contains(keyword)).ToListAsync();
        }
    }
}
