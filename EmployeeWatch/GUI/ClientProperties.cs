using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using log4net.Config;

namespace GUI;

public static class ClientProperties
{
    public static IDictionary<string, string> GetProperties()
    {
        var fileMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = "client.config"
        };
        var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        XmlConfigurator.Configure(new FileInfo("client.config"));

        var props = new SortedList<string, string>();

        var connectionString = GetConnectionStringByName(configuration, "MariaDBConnectionString");
        if (connectionString is not null)
        {
            props.Add("ConnectionString", connectionString);
        }

        return props;
    }

    private static string? GetConnectionStringByName(Configuration configuration, string name)
    {
        var connectionStringSettings = configuration.ConnectionStrings.ConnectionStrings[name];
        return connectionStringSettings?.ConnectionString;
    }
}