using System;
using Avalonia.Controls;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();

        var viewModel = new LoginViewModel();
        viewModel.LoginSucceeded += OnLoginSucceeded;
        DataContext = viewModel;
    }

    private void OnLoginSucceeded(object? sender, EventArgs e)
    {
        var mainWindow = new MainWindow
        {
            DataContext = new MainWindowViewModel(),
        };

        mainWindow.Show();
        Close();
    }
}
