using System;
using System.Collections.ObjectModel;
using Domain;
using log4net;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using ReactiveUI.Fody.Helpers;
using Service;
using Service.Utils;

namespace GUI.ViewModels;

public class EmployeeWindowViewModel(IService service, Employee employee) : ViewModelBase, IObserver
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoginWindowViewModel));

    [Reactive] public ObservableCollection<Task> Tasks { get; set; } = [];
    private Employee Employee => employee;

    public event Action? RequestClose;
    private bool LoggedOut { get; set; }

    public string WelcomeMessage => $"Welcome, {employee.Name}!";

    public async void LogoutAction()
    {
        if (LoggedOut) return;
        Log.Info("Logging out.");
        var loggedOut = service.Logout(employee);

        IMsBox<ButtonResult>? messageBox;
        if (!loggedOut)
        {
            Log.Error("Couldn't log out.");
            messageBox = MessageBoxManager.GetMessageBoxStandard("Error", "Couldn't log out.", ButtonEnum.Ok);
            await messageBox.ShowAsync();
            return;
        }

        LoggedOut = true;
        RequestClose?.Invoke();
        messageBox = MessageBoxManager.GetMessageBoxStandard("Success", "Logged out!", ButtonEnum.Ok);
        await messageBox.ShowAsync();
    }

    public async void AnnouncePresenceAction()
    {
        IMsBox<ButtonResult>? messageBox;
        if (employee.PresentTime != DateTime.MinValue)
        {
            messageBox = MessageBoxManager.GetMessageBoxStandard("Error", "Already declared as present.");
            await messageBox.ShowAsync();
            return;
        }
        Log.InfoFormat("Announcing the presence of {0}.", employee);

        var updated = service.UpdatePresentTimeForEmployee(employee, DateTime.Now);
        if (!updated)
        {
            Log.Error("Couldn't announce the presence.");
            messageBox = MessageBoxManager.GetMessageBoxStandard("Error", "Couldn't announce the presence.", ButtonEnum.Ok);
            await messageBox.ShowAsync();
            return;
        }

        messageBox = MessageBoxManager.GetMessageBoxStandard("Success", "Announced presence!", ButtonEnum.Ok);
        await messageBox.ShowAsync();
    }

    public void Update<TE>(Event<TE> e)
    {
        if (e.EventType != EventType.TaskSaved) return;
        var task = e.NewData as Task ?? null;
        if (task != null && task.Username == Employee.Username) Tasks.Add(task);
    }
}