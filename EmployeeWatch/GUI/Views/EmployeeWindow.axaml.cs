using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;

namespace GUI.Views;

public partial class EmployeeWindow : Window
{
    private readonly EmployeeWindowViewModel _viewModel;

    public EmployeeWindow(EmployeeWindowViewModel employeeWindowViewModel)
    {
        InitializeComponent();
        DataContext = _viewModel = employeeWindowViewModel;
        _viewModel.RequestClose += Close;
        Closing += (_, _) => _viewModel.LogoutAction();
    }
}