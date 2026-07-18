
namespace ScoolManager.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // Página atual exibida no "body". O ViewLocator (App.axaml) resolve
    // automaticamente a View correspondente a esta ViewModel.
    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private NavigationItemViewModel? _selectedNavigationItem;

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; }

    // Dados do utilizador autenticado - placeholder por agora,
    // depois ligamos à sessão/serviço de autenticação real.
    public string UserName { get; } = "Secretaria";
    public string UserRole { get; } = "Administrador";

    public MainWindowViewModel()
    {
        // 6 views principais, conforme School_Manager_Fluxo_Navegacao.txt.
        // Por enquanto todos os itens apontam para o DashboardViewModel (placeholder).
        // Quando cada view real for criada, basta trocar a lambda do PageFactory
        // (ex.: () => new AlunosViewModel()) - nada mais muda.
        NavigationItems = new ObservableCollection<NavigationItemViewModel>
        {
            new(MaterialIconKind.ViewDashboard, "Dashboard",      () => new DashboardViewModel()),
            new(MaterialIconKind.AccountGroup,  "Alunos",         () => new AlunosViewModel()), // TODO: AlunosViewModel
            new(MaterialIconKind.CashMultiple,  "Financeiro",     () => new DashboardViewModel()), // TODO: FinanceiroViewModel
            new(MaterialIconKind.ChartLine,     "Relatórios",     () => new DashboardViewModel()), // TODO: RelatoriosViewModel
            new(MaterialIconKind.Domain,        "Escola",         () => new DashboardViewModel()), // TODO: EscolaViewModel
            new(MaterialIconKind.Cog,           "Configurações",  () => new DashboardViewModel()), // TODO: ConfiguracoesViewModel
        };

        _selectedNavigationItem = NavigationItems[0];
        _currentPage = _selectedNavigationItem.PageFactory();
    }

    partial void OnSelectedNavigationItemChanged(NavigationItemViewModel? value)
    {
        if (value is null) return;
        CurrentPage = value.PageFactory();
    }

    [RelayCommand]
    private void Logout()
    {
        // TODO: encerrar sessão / voltar ao ecrã de login
    }
}
