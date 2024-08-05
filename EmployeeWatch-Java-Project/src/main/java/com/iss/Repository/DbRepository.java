package Repository;

import java.util.*;

/**
 * 
 */
public abstract class DbRepository {

    /**
     * Default constructor
     */
    public DbRepository() {
    }

    /**
     * 
     */
    private List<PreparedStatements> statements;

    /**
     * 
     */
    private Connection connection;

    /**
     * @param resultSet
     */
    public abstract void extractFromResultSet(ResultSet resultSet);

}