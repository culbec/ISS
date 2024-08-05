using Repository.DbContexts;

namespace Repository.ConnectionUtils;

public static class EfCoreDbUtils
{
    private static AppDbContext? _instance;

    public static AppDbContext GetContext(IDictionary<string, string> props)
    {
        if (_instance != null && _instance.Database.CanConnect()) return _instance;
        _instance = GetNewContext(props);
        _instance.Database.EnsureCreated();

        return _instance;
    }

    private static AppDbContext GetNewContext(IDictionary<string, string> props)
    {
        var context = new AppDbContext {Properties = props};
        return context;
    }
}