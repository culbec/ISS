package Repository;

import java.util.*;

/**
 * 
 */
public class IUserRepository extends IRepository {

    /**
     * Default constructor
     */
    public IUserRepository() {
    }

    /**
     * @param expected 
     * @param actual 
     * @return
     */
    public Boolean checkPassword(String expected, String actual) {
        // TODO implement here
        return null;
    }

    /**
     * @param username 
     * @param password 
     * @return
     */
    public User login(String username, String password) {
        // TODO implement here
        return null;
    }

    /**
     * @param resultSet 
     * @return
     */
    public Employee extractEmployee(ResultSet resultSet) {
        // TODO implement here
        return null;
    }

    /**
     * @param resultSet 
     * @return
     */
    public Boss extractBoss(ResultSet resultSet) {
        // TODO implement here
        return null;
    }

    /**
     * @param person
     */
    public void logout(User person) {
        // TODO implement here
    }

}