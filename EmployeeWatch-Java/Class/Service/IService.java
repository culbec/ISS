package Service;

import java.util.*;

/**
 * 
 */
public class IService {

    /**
     * Default constructor
     */
    public IService() {
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
     * @param user 
     * @param task
     */
    public void addTask(User user, Task task) {
        // TODO implement here
    }

    /**
     * @param user 
     * @return
     */
    public List<Task> getTasksOf(User user) {
        // TODO implement here
        return null;
    }

    /**
     * @param user
     */
    public void logout(User user) {
        // TODO implement here
    }

}