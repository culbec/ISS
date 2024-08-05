using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;

namespace GUI.Views;

public partial class BossWindow : Window
{
    private readonly BossWindowViewModel _viewModel;
    public BossWindow(BossWindowViewModel bossWindowViewModel)
    {
        InitializeComponent();
        DataContext = _viewModel = bossWindowViewModel;
        _viewModel.RequestClose += Close;
        Closing += (_, _) => _viewModel.LogoutAction();
    }
}