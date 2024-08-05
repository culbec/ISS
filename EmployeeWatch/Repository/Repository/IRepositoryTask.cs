using Task = Domain.Task;

namespace Repository.Repository;

public interface IRepositoryTask : IRepository<int, Task>
{
}