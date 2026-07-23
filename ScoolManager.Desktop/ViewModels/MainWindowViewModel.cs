using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.ViewModels.Pages;

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
            new(MaterialIconKind.AccountGroup,  "Alunos",         CriarPaginaAlunos),
            new(MaterialIconKind.CashMultiple,  "Financeiro",     () => new DashboardViewModel()), // TODO: FinanceiroViewModel
            new(MaterialIconKind.ChartLine,     "Relatórios",     () => new DashboardViewModel()), // TODO: RelatoriosViewModel
            new(MaterialIconKind.Domain,        "Escola",         () => new DashboardViewModel()), // TODO: EscolaViewModel
            new(MaterialIconKind.Cog,           "Configurações",  () => new DashboardViewModel()), // TODO: ConfiguracoesViewModel
        };

        _selectedNavigationItem = NavigationItems[0];
        _currentPage = _selectedNavigationItem.PageFactory();
    }

    /// <summary>
    /// Cria a AlunosViewModel já com a navegação para "Detalhes do Aluno" ligada:
    /// ao clicar numa linha (evento DetalhesAlunoSolicitado), trocamos o
    /// CurrentPage para a DetalhesAlunoViewModel correspondente.
    /// </summary>
    private ViewModelBase CriarPaginaAlunos()
    {
        var alunosViewModel = new AlunosViewModel();
        alunosViewModel.DetalhesAlunoSolicitado += (_, aluno) => AbrirDetalhesAluno(aluno);
        return alunosViewModel;
    }

    /// <summary>
    /// Troca o CurrentPage para os Detalhes do aluno selecionado. Note que
    /// SelectedNavigationItem NÃO muda aqui de propósito: a sidebar continua
    /// a mostrar "Alunos" como ativo enquanto o utilizador vê os detalhes,
    /// já que Detalhes do Aluno é uma sub-página de Alunos.
    /// </summary>
    private void AbrirDetalhesAluno(AlunoListItemModel aluno)
    {
        var detalhes = new DetalhesAlunoViewModel(aluno);

        // "Voltar para Alunos" e "Excluir Aluno" (dentro de Detalhes) levam
        // os dois de volta para uma lista de Alunos "fresca".
        detalhes.VoltarParaAlunosSolicitado += (_, _) => CurrentPage = CriarPaginaAlunos();
        detalhes.ExclusaoConfirmada += (_, _) => CurrentPage = CriarPaginaAlunos();

        CurrentPage = detalhes;
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