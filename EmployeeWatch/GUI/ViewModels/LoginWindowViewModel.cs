using System;
using Domain;
using GUI.Views;
using log4net;
using MsBox.Avalonia;
using ReactiveUI;
using Service;
using Service.Utils;
using Task = System.Threading.Tasks.Task;

namespace GUI.ViewModels;

public class LoginWindowViewModel : ViewModelBase
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoginWindowViewModel));
    public IService? Service { get; init; }

    private string? _username;

    public string? Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    private string? _password;

    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public async Task LoginAction()
    {
        Log.Info("Verifying credentials...");

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            Log.Error("Username or password is empty.");
            ErrorMessage = "Please enter both username and password.";
            return;
        }

        Log.Info("Credentials are valid.");
        ErrorMessage = "";

        try
        {
            Log.Info("Trying to log in...");

            if (Service is null)
            {
                Log.Error("Class not fully instantiated: Service is null.");
                ErrorMessage = "Internal error. Please try again.";
                return;
            }

            var user = Service.Login(Username, Password);
            Log.InfoFormat("Login successful! User: {0}", user);

            switch (user.Role)
            {
                case Role.Employee:
                {
                    Log.Info("Initializing the Employee window.");
                    var employeeWindowViewModel = new EmployeeWindowViewModel(Service, (Employee) user);
                    var employeeWindow = new EmployeeWindow(employeeWindowViewModel);
                    ((IObservable)Service).Attach(employeeWindowViewModel);

                    Log.Info("Showing the Employee window.");
                    employeeWindow.Show();
                    break;
                }
                case Role.Boss:
                {
                    Log.Info("Initializing the Boss window.");
                    var bossWindowViewModel = new BossWindowViewModel(Service, (Boss) user);
                    var bossWindow = new BossWindow(bossWindowViewModel);
                    ((IObservable)Service).Attach(bossWindowViewModel);

                    Log.Info("Showing the Boss window.");
                    bossWindow.Show();
                    break;
                }
                default:
                    Log.Error("Unknown role.");
                    await MessageBoxManager.GetMessageBoxStandard("Error", "Unknown role!").ShowAsync();
                    break;
            }

            var messageBox = MessageBoxManager.GetMessageBoxStandard("Success", "Login successful!");
            await messageBox.ShowAsync();
        }
        catch (Exception e)
        {
            Log.ErrorFormat("Login failed: {0}", e.Message);
            var messageBox = MessageBoxManager.GetMessageBoxStandard("Error", e.Message);
            await messageBox.ShowAsync();
        }
    }
}