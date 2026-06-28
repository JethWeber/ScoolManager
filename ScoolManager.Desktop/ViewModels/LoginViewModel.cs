using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ScoolManager.Desktop.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty]
    private string phone = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool rememberMe;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    public event EventHandler? LoginSucceeded;

    [RelayCommand(CanExecute = nameof(CanExecuteLogin))]
    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Phone) || Phone.Trim().Length < 9)
        {
            ErrorMessage = "Informe um telefone válido.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "A palavra-passe não pode ficar vazia.";
            return;
        }

        IsBusy = true;
        LoginCommand.NotifyCanExecuteChanged();

        try
        {
            await Task.Delay(600);

            if (Phone.Trim().Length < 9 || Password.Trim().Length < 4)
            {
                ErrorMessage = "Telefone ou senha inválidos.";
                return;
            }

            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }
        finally
        {
            IsBusy = false;
            LoginCommand.NotifyCanExecuteChanged();
        }
    }

    private bool CanExecuteLogin()
        => !IsBusy;
}
