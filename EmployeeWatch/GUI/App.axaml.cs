using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;
using GUI.Views;
using log4net;
using Repository;
using Repository.Repository;

namespace GUI;

public partial class App : Application
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(App));
    private LoginWindowViewModel? _loginWindowViewModel;

    public override void Initialize()
    {
        Log.Info("Initializing the app.");
        AvaloniaXamlLoader.Load(this);

        Log.Info("Initializing the properties of the app.");
        var props = ClientProperties.GetProperties();
        Log.Info("Properties initialized.");

        Log.Info("Initializing the repositories.");
        var userRepository = new RepositoryUserEfCore(props);
        var taskRepository = new RepositoryTaskEfCore(props);
        Log.Info("Repositories initialized.");

        Log.Info("Initializing the service.");
        var service = new Service.Service
        {
            RepositoryUser = userRepository,
            RepositoryTask = taskRepository
        };
        Log.Info("Service initialized.");

        Log.Info("Initializing the login window.");
        _loginWindowViewModel = new LoginWindowViewModel
        {
            Service = service
        };
        Log.Info("Login window initialized.");

        Log.Info("App initialized.");
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new LoginWindow
            {
                DataContext = _loginWindowViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}