using Domain;
using Repository.DbContexts;

namespace Repository.Repository;

public interface IRepositoryUser : IRepository<int, User>
{
    /// <summary>
    ///     Find a user by its username and password.
    /// </summary>
    /// <param name="username">The username of the user to find</param>
    /// <param name="password">The password of the user to find</param>
    /// <returns>
    ///     The user with the given username and password or null if there is no user with the given username and
    ///     password.
    /// </returns>
    User? FindUserByCredentials(string username, string password);

    /// <summary>
    /// Get all the present employees.
    /// </summary>
    /// <returns>An IEnumerable containing all the present employees.</returns>
    IEnumerable<Employee> GetPresentEmployees();

    /// <summary>
    /// Updates the present time of an employee.
    /// </summary>
    /// <param name="employee">Employee to update its present time.</param>
    /// <param name="presentTime">The moment in time when the employee announced its presence.</param>
    /// <returns>True if the present time was updated, false otherwise.</returns>
    bool UpdatePresentTimeForEmployee(Employee employee, DateTime presentTime);
}