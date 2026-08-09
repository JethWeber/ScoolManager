using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Desktop.ViewModels.Pages;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthService _authService;

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

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
    }

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
            // TODO: guardar o Utilizador devolvido (ex.: numa sessão/serviço
            // partilhado) assim que MainWindowViewModel precisar de
            // UserName/UserRole reais em vez dos placeholders atuais.
            await _authService.AutenticarAsync(Phone.Trim(), Password);

            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }
        catch (CredenciaisInvalidasException ex)
        {
            ErrorMessage = ex.Message; // "Telefone ou senha inválidos."
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
