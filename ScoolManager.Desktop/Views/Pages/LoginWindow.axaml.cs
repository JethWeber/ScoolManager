using System;
using Avalonia.Controls;
using ScoolManager.Desktop.ViewModels.Pages;
using ScoolManager.Desktop.ViewModels;


namespace ScoolManager.Desktop.Views.Pages;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        CloseButton.Click += (_, _) => Close();
        
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
