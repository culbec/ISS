using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Threading;
using Domain;
using log4net;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using ReactiveUI.Fody.Helpers;
using Service;
using Service.Utils;

namespace GUI.ViewModels;

public class BossWindowViewModel : ViewModelBase, IObserver
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoginWindowViewModel));
    private readonly IService _service;


    [Reactive] public ObservableCollection<Employee> PresentEmployees { get; set; } = [];
    private readonly Boss _boss;

    public event Action? RequestClose;
    private bool LoggedOut { get; set; }

    public string WelcomeMessage => $"Welcome, {_boss.Name}!";
    [Reactive] public Employee? SelectedEmployee { get; set; }
    [Reactive] public string? TaskTitle { get; set; }
    [Reactive] public string? TaskDescription { get; set; }

    private void Initialize()
    {
        Dispatcher.UIThread.Post(() =>
        {
            PresentEmployees = new ObservableCollection<Employee>(_service.GetPresentEmployees());
        }, DispatcherPriority.Normal);
    }

    public BossWindowViewModel(IService service, Boss boss)
    {
        _service = service;
        _boss = boss;
        Initialize();
    }

    public async void LogoutAction()
    {
        if (LoggedOut) return;
        Log.Info("Logging out.");
        var loggedOut = _service.Logout(_boss);

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

    public async void AssignTaskAction()
    {
        var errors = "";

        if (SelectedEmployee is null) errors += "Please select an employee.\n";
        if (string.IsNullOrWhiteSpace(TaskTitle)) errors += "Please enter a title.\n";
        if (string.IsNullOrWhiteSpace(TaskDescription)) errors += "Please enter a description.\n";

        IMsBox<ButtonResult>? messageBox;
        if (!string.IsNullOrWhiteSpace(errors))
        {
            messageBox = MessageBoxManager.GetMessageBoxStandard("Error", errors);
            await messageBox.ShowAsync();
            return;
        }

        var task = new Task
        {
            Title = TaskTitle!, Description = TaskDescription!, StartTime = DateTime.Now,
            Username = SelectedEmployee!.Username
        };
        var saved = _service.SaveTask(task);
        if (!saved)
        {
            messageBox = MessageBoxManager.GetMessageBoxStandard("Error", "Couldn't save the task.");
            await messageBox.ShowAsync();
            return;
        }

        messageBox = MessageBoxManager.GetMessageBoxStandard("Succes", "Task assigned!");
        await messageBox.ShowAsync();
    }

    public void Update<TE>(Event<TE> e)
    {
        Employee? employee;
        switch (e.EventType)
        {
            case EventType.EmployeePresent:
                employee = e.NewData as Employee ?? null;
                if (employee != null) PresentEmployees.Add(employee);
                break;
            case EventType.EmployeeLogout:
                employee = e.NewData as Employee ?? null;
                if (employee != null)
                    PresentEmployees =
                        new ObservableCollection<Employee>(PresentEmployees.Where(e => e.Username != employee.Username)
                            .ToList());
                break;
        }
    }
}