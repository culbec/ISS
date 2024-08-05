using Domain;
using log4net;
using Repository.Repository;
using Service.Utils;
using Task = Domain.Task;

namespace Service;

public class Service : IService, IObservable
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(Service));
    public IRepositoryUser? RepositoryUser { get; init; }
    public IRepositoryTask? RepositoryTask { get; init; }
    public List<IObserver> Observers { get; init; } = [];

    public User Login(string username, string password)
    {
        Log.InfoFormat("Logging in user with username {0}", username);

        if (RepositoryUser is null)
        {
            Log.Error("Class is not fully instantiated: RepositoryUser is null.");
            throw new InvalidDataException("RepositoryUser is null.");
        }

        var user = RepositoryUser.FindUserByCredentials(username, password);
        if (user is null)
        {
            Log.Error("The user does not exist.");
            throw new InvalidDataException("The user does not exist.");
        }

        Log.Info("User logged in successfully.");
        return user;
    }

    public bool Logout(User user)
    {
        Log.InfoFormat("Logging out user: {0}", user);

        if (RepositoryUser is null)
        {
            Log.Error("Class is not fully instantiated: RepositoryUser is null.");
            throw new InvalidDataException("RepositoryUser is null.");
        }

        if (user.Role != Role.Employee) return true;

        var updated = RepositoryUser.UpdatePresentTimeForEmployee(user as Employee, DateTime.MinValue);
        if (!updated)
        {
            Log.Error("Couldn't update the present time for the employee.");
            return false;
        }

        Log.Info("User logged out successfully.");
        Notify(new Event<Employee>(EventType.EmployeeLogout, null, user as Employee));
        return true;
    }

    public IEnumerable<Employee> GetPresentEmployees()
    {
        Log.Info(" Retrieving the present employees.");

        if (RepositoryUser is null)
        {
            Log.Error("Class is not fully instantiated: RepositoryUser is null.");
            throw new InvalidDataException("RepositoryUser is null.");
        }

        var employees = RepositoryUser.GetPresentEmployees();
        Log.Info(" Present employees retrieved.");
        return employees;
    }

    public bool UpdatePresentTimeForEmployee(Employee employee, DateTime presentTime)
    {
        Log.InfoFormat("Updating the present time for employee with id {0}", employee.Tid);

        if (RepositoryUser is null)
        {
            Log.Error(" Class is not fully instantiated: RepositoryUser is null.");
            throw new InvalidDataException("RepositoryUser is null.");
        }

        var updated = RepositoryUser.UpdatePresentTimeForEmployee(employee, presentTime);

        if (!updated)
        {
            Log.Error(" Couldn't update the present time for the employee.");
            return false;
        }

        Log.Info(" Present time updated successfully.");
        Notify(new Event<Employee>(EventType.EmployeePresent, null, employee));
        return true;
    }

    public bool SaveTask(Task task)
    {
        Log.InfoFormat("Saving task with id {0}", task.Tid);

        if (RepositoryTask is null)
        {
            Log.Error(" Class is not fully instantiated: RepositoryTask is null.");
            throw new InvalidDataException("RepositoryTask is null.");
        }

        var saved = RepositoryTask.Save(task);

        if (saved is null)
        {
            Log.Error(" Couldn't save the task.");
            return false;
        }

        Log.Info(" Task saved successfully.");
        Notify(new Event<Task>(EventType.TaskSaved, null, saved));
        return true;
    }

    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        Observers.Remove(observer);
    }

    public void Notify<TE>(Event<TE> e)
    {
        Observers.ForEach(o => o.Update(e));
    }
}