using Domain.Common;

namespace Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();
    }
}
