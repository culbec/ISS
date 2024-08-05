using Repository.ConnectionUtils;
using Task = Domain.Task;

namespace Repository.Repository;

public class RepositoryTaskEfCore(IDictionary<string, string> properties)
    : AbstractEfCoreRepository<int, Task>(properties), IRepositoryTask
{
    public override Task? FindOne(int tid)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Task>? FindAll()
    {
        throw new NotImplementedException();
    }

    public override Task? Save(Task te)
    {
        Log.InfoFormat("Saving task with id {0}", te.Tid);
        var context = EfCoreDbUtils.GetContext(Properties);

        try
        {
            context.Tasks.Add(te);
            Log.Info("Task saved.");

            context.SaveChanges();
            Log.Info("Changes saved to the database.");
            return te;
        }
        catch (Exception e)
        {
            Log.ErrorFormat("Error saving task: {0}", e.Message);
            return null;
        }

    }

    public override Task? Delete(Task te)
    {
        throw new NotImplementedException();
    }

    public override Task? Update(Task te)
    {
        throw new NotImplementedException();
    }
}