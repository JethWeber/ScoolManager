using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Microsoft.Extensions.DependencyInjection;
using ScoolManager.Core.Abstractions.Services;
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
    // TODO: LoginViewModel já recebe o Utilizador de AutenticarAsync — falta
    // propagar essa sessão até aqui (ex.: um ISessaoAtualService no Core).
    public string UserName { get; } = "Secretaria";
    public string UserRole { get; } = "Administrador";

    public MainWindowViewModel()
    {
        // 6 views principais, conforme SM_Flow.md. As páginas passam a ser
        // resolvidas pelo container de DI (App.Services) em vez de `new`,
        // porque agora recebem Services do Core no construtor.
        NavigationItems = new ObservableCollection<NavigationItemViewModel>
        {
            new(MaterialIconKind.ViewDashboard, "Dashboard",     () => App.Services.GetRequiredService<DashboardViewModel>()),
            new(MaterialIconKind.AccountGroup,  "Alunos",        CriarPaginaAlunos),
            new(MaterialIconKind.CashMultiple,  "Financeiro",    () => App.Services.GetRequiredService<FinanceiroViewModel>()),
            new(MaterialIconKind.ChartLine,     "Relatórios",    () => App.Services.GetRequiredService<RelatoriosViewModel>()),
            new(MaterialIconKind.Domain,        "Escola",        () => App.Services.GetRequiredService<EscolaViewModel>()),
            new(MaterialIconKind.Cog,           "Configurações", () => App.Services.GetRequiredService<ConfiguracoesViewModel>()),
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
        var alunosViewModel = App.Services.GetRequiredService<AlunosViewModel>();
        alunosViewModel.DetalhesAlunoSolicitado += (_, aluno) => AbrirDetalhesAluno(aluno);
        return alunosViewModel;
    }

    /// <summary>
    /// Troca o CurrentPage para os Detalhes do aluno selecionado. Note que
    /// SelectedNavigationItem NÃO muda aqui de propósito: a sidebar continua
    /// a mostrar "Alunos" como ativo enquanto o utilizador vê os detalhes,
    /// já que Detalhes do Aluno é uma sub-página de Alunos.
    /// </summary>
    private async void AbrirDetalhesAluno(AlunoListItemModel aluno)
    {
        var detalhes = new DetalhesAlunoViewModel(
            aluno,
            App.Services.GetRequiredService<IAlunoService>(),
            App.Services.GetRequiredService<IEscolaService>());

        detalhes.VoltarParaAlunosSolicitado += (_, _) => CurrentPage = CriarPaginaAlunos();
        detalhes.ExclusaoConfirmada += (_, _) => CurrentPage = CriarPaginaAlunos();

        CurrentPage = detalhes;

        // Carrega dados completos do Core (inclui as opções de Ano Lectivo/Classe do pagamento)
        if (detalhes is IAsyncInitializable init)
            await init.InitializeAsync();
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
