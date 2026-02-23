using Application.Interfaces.Repositories.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IEmployeeReponsitory : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> SearchAsync(string keyword);
    }
}
