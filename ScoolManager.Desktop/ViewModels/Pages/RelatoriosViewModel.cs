using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>
/// View 6 do SM_Flow.md. Este ficheiro cobre apenas a Fase 0-1 do roadmap:
/// galeria fixa dos 7 relatórios + estado dos modais. A View (Fase 2), os
/// formulários de filtro (Fase 3), a geração da pré-visualização (Fase 4) e
/// as exportações (Fase 5-7) ficam para as próximas fases - por isso os
/// comandos abaixo estão marcados com TODO.
///
/// Tal como o Financeiro/Escola fazem hoje via Design.DataContext, os dados
/// aqui são de exemplo; a Fase 8 do roadmap trata da ligação a serviços/
/// repositórios reais de Alunos e Financeiro.
/// </summary>
public partial class RelatoriosViewModel : ViewModelBase
{
    // Galeria fixa dos 7 relatórios (SM_Flow.md > View 6 > Relatórios).
    public ObservableCollection<RelatorioTipoItem> RelatoriosDisponiveis { get; }

    // Relatório escolhido na galeria - define o conteúdo dos modais.
    [ObservableProperty]
    private RelatorioTipoItem? _relatorioSelecionado;

    // Filtros do modal "Configurar Relatório" (Fase 3), partilhados entre
    // todos os tipos de relatório.
    public RelatorioFiltro FiltroAtual { get; } = new();

    // --- Estado dos modais (SM_Flow.md > View 6 > Modais) ---
    [ObservableProperty] private bool _modalConfigurarVisivel;
    [ObservableProperty] private bool _modalPreVisualizarVisivel;
    [ObservableProperty] private bool _modalExportacaoVisivel;

    // Mensagem de feedback do modal de exportação/impressão (Fase 5-7).
    [ObservableProperty] private string _mensagemExportacao = string.Empty;

    /// <summary>Usado pelo overlay único dos modais, tal como AlgumModalAberto no Financeiro.</summary>
    public bool AlgumModalAberto =>
        ModalConfigurarVisivel || ModalPreVisualizarVisivel || ModalExportacaoVisivel;

    partial void OnModalConfigurarVisivelChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnModalPreVisualizarVisivelChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnModalExportacaoVisivelChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

    // --- Resultados da pré-visualização (populados na Fase 4) ---
    public ObservableCollection<MatriculaRelatorioItem> ResultadoMatriculas { get; } = new();
    public ObservableCollection<AlunoRelatorioItem> ResultadoAlunos { get; } = new();
    public ObservableCollection<PropinaRelatorioItem> ResultadoPropinas { get; } = new();
    public ObservableCollection<RelatorioMovimentoItem> ResultadoMovimentos { get; } = new();
    public ObservableCollection<FluxoCaixaRelatorioItem> ResultadoFluxoCaixa { get; } = new();

    public RelatoriosViewModel()
    {
        RelatoriosDisponiveis = new ObservableCollection<RelatorioTipoItem>
        {
            new(RelatorioTipo.Matriculas, "Matrículas",
                "Novas matrículas efetuadas no período.", MaterialIconKind.AccountPlus),
            new(RelatorioTipo.ListaAlunos, "Lista de Alunos",
                "Listagem completa de alunos e a sua situação.", MaterialIconKind.AccountGroup),
            new(RelatorioTipo.PropinasPagas, "Propinas Pagas",
                "Pagamentos de propinas confirmados.", MaterialIconKind.CashCheck),
            new(RelatorioTipo.PropinasAtraso, "Propinas em Atraso",
                "Propinas por regularizar.", MaterialIconKind.CashRemove),
            new(RelatorioTipo.Entradas, "Entradas",
                "Entradas de caixa registadas.", MaterialIconKind.TrendingUp),
            new(RelatorioTipo.Saidas, "Saídas",
                "Saídas de caixa registadas.", MaterialIconKind.TrendingDown),
            new(RelatorioTipo.FluxoCaixa, "Fluxo de Caixa",
                "Evolução do saldo de caixa por período.", MaterialIconKind.ChartLine),
        };
    }

    /// <summary>Abre "Configurar Relatório" (Fase 3) para o cartão clicado na galeria.</summary>
    [RelayCommand]
    private void AbrirConfigurarRelatorio(RelatorioTipoItem item)
    {
        RelatorioSelecionado = item;
        FiltroAtual.Limpar();
        ModalConfigurarVisivel = true;
    }

    [RelayCommand]
    private void FecharModal()
    {
        ModalConfigurarVisivel = false;
        ModalPreVisualizarVisivel = false;
        ModalExportacaoVisivel = false;
    }

    /// <summary>Volta de "Pré-Visualizar" para "Configurar Relatório" para ajustar filtros.</summary>
    [RelayCommand]
    private void VoltarConfigurar()
    {
        ModalPreVisualizarVisivel = false;
        ModalConfigurarVisivel = true;
    }

    // TODO (Fase 4): usar RelatorioSelecionado.Tipo + FiltroAtual para popular
    // o ResultadoXxx correspondente (dados de exemplo), depois
    // ModalConfigurarVisivel = false; ModalPreVisualizarVisivel = true;
    [RelayCommand]
    private void GerarPreVisualizacao()
    {
    }

    // TODO (Fase 5): gerar PDF real a partir do ResultadoXxx ativo.
    [RelayCommand]
    private void ExportarPdf()
    {
    }

    // TODO (Fase 6): gerar Excel/CSV real a partir do ResultadoXxx ativo.
    [RelayCommand]
    private void ExportarExcel()
    {
    }

    // TODO (Fase 7): enviar para impressão.
    [RelayCommand]
    private void Imprimir()
    {
    }
}
