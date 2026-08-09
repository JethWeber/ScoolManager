using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ScoolManager.Desktop.ViewModels.Pages;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.Views.Pages;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        CloseButton.Click += (_, _) => Close();

        // A LoginViewModel já não é criada aqui com `new` — App.axaml.cs
        // (composition root) resolve-a via DI e atribui a DataContext antes
        // de dar Show() nesta janela. Só precisamos de ligar o evento assim
        // que isso acontecer.
        DataContextChanged += (_, _) =>
        {
            if (DataContext is LoginViewModel viewModel)
                viewModel.LoginSucceeded += OnLoginSucceeded;
        };
    }

    private void OnLoginSucceeded(object? sender, EventArgs e)
    {
        var mainWindow = new MainWindow
        {
            DataContext = App.Services.GetRequiredService<MainWindowViewModel>(),
        };

        mainWindow.Show();
        Close();
    }
}
