using Domain;
using Task = Domain.Task;

namespace Service;

public interface IService
{
    /// <summary>
    /// Find a user by its username and password.
    /// </summary>
    /// <param name="username">Username of the user.</param>
    /// <param name="password">Password of the user.</param>
    /// <returns>The user with the passed credentials.</returns>
    /// <exception cref="InvalidDataException">If the passed credentials couldn't identify a user.</exception>
    User Login(string username, string password);

    /// <summary>
    /// Logs out a user.
    /// </summary>
    /// <param name="user">User that wants to log out.</param>
    /// <returns>True if the user logged out, false otherwise.</returns>
    bool Logout(User user);

    /// <summary>
    /// Get all the present employees.
    /// </summary>
    /// <returns>An IEnumerable containing all the present employees.</returns>
    IEnumerable<Employee> GetPresentEmployees();

    /// <summary>
    /// Updates the present time of an employee.
    /// </summary>
    /// <param name="employee">Employee that declared itself as present.</param>
    /// <param name="presentTime">The time when the employee declared itself as present.</param>
    /// <returns>True if the present time was updated, false otherwise.</returns>
    bool UpdatePresentTimeForEmployee(Employee employee, DateTime presentTime);

    /// <summary>
    /// Saves a task.
    /// </summary>
    /// <param name="task">The task to be saved.</param>
    /// <returns>True if the task was saved, false otherwise.</returns>
    bool SaveTask(Task task);
}