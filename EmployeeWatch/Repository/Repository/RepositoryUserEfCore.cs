using Domain;
using Repository.ConnectionUtils;
using Repository.DbContexts;

namespace Repository.Repository;

public class RepositoryUserEfCore(IDictionary<string, string> properties)
    : AbstractEfCoreRepository<int, User>(properties), IRepositoryUser
{
    public override User? FindOne(int tid)
    {
        return null;
    }

    public override IEnumerable<User>? FindAll()
    {
        return null;
    }

    public override User? Save(User te)
    {
        return null;
    }

    public override User? Delete(User te)
    {
        return null;
    }

    public override User? Update(User te)
    {
        return null;
    }

    public User? FindUserByCredentials(string username, string password)
    {
        Log.InfoFormat("Finding user with username {0}", username);
        var context = EfCoreDbUtils.GetContext(Properties);

        var user = context.Users.FirstOrDefault(u => u.Username == username);
        Log.InfoFormat("Found user: {0}", user);
        if (user is null)
        {
            Log.Error("The user does not exist.");
            return null;
        }

        Log.Info("Checking if the passed password is correct.");
        if (!CheckPassword(password, user.Password!))
        {
            Log.Error("Password is incorrect.");
            return null;
        }

        Log.Info("Password is correct. Checking the role of the user.");
        return ReturnUserByRole(user);
    }

    public IEnumerable<Employee> GetPresentEmployees()
    {
        Log.Info("Retrieving all the present employees.");
        var context = EfCoreDbUtils.GetContext(Properties);

        var employees = context.Employees.Where(e => e.PresentTime != DateTime.MinValue)
            .ToList();

        Log.InfoFormat("Retrieved {0} present employees.", employees.Count);
        return employees;
    }

    public bool UpdatePresentTimeForEmployee(Employee employee, DateTime presentTime)
    {
        Log.InfoFormat("Updating the present time for employee with username {0} with {1}", employee.Username, presentTime);

        var context = EfCoreDbUtils.GetContext(Properties);
        var employeeToUpdate = context.Users.FirstOrDefault(e => e.Username == employee.Username);

        if (employeeToUpdate is null)
        {
            Log.Error("Employee does not exist.");
            return false;
        }

        if (employeeToUpdate.Role != Role.Employee)
        {
            Log.Error("The user is not an employee.");
            return false;
        }

        var employeePresent = (Employee)employeeToUpdate;
        employeePresent.PresentTime = presentTime;

        context.SaveChanges();
        Log.Info("Present time updated successfully.");
        return true;
    }

    /// <summary>
    /// Returns the user based on its role.
    /// </summary>
    /// <param name="user">Initial user.</param>
    /// <param name="context">Database context.</param>
    /// <returns>The user completed with its next attributes.</returns>
    private User? ReturnUserByRole(User user)
    {
        Log.Info("Checking the role of the user.");

        switch (user.Role)
        {
            case Role.Boss:
                Log.Info("The user is a boss.");

                var boss = (Boss)user;
                return boss;
            case Role.Employee:
                Log.Info("The user is an employee.");
                var employee = (Employee)user;
                return employee;
            default:
                Log.Error("Unknown role.");
                return null;
        }
    }

    private static bool CheckPassword(string expected, string actual) => BCrypt.Net.BCrypt.Verify(expected, actual);
}