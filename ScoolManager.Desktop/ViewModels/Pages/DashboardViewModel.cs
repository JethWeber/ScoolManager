using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>
/// ViewModel do Dashboard. Os KPIs, o resumo financeiro do dia e a lista de
/// devedores vêm de <see cref="IDashboardService"/>, carregados em
/// <see cref="InitializeAsync"/> (o construtor não pode ser assíncrono —
/// ver DashboardView.axaml.cs, que chama InitializeAsync quando a View é
/// anexada).
///
/// O gráfico "Receita por Trimestre" é desenhado à mão (Path/SVG, sem
/// biblioteca de gráficos externa — ver nota em
/// <see cref="CarregarGrafico"/>) e é filtrado por Ano Letivo + Trimestre
/// (com os meses respectivos). Os valores continuam mock: o Core ainda não
/// expõe uma série mensal de receita.
/// </summary>
public partial class DashboardViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IDashboardService _dashboardService;

    public string Title { get; } = "Dashboard";

    // ---------------------------------------------------------------
    // Cabeçalho (Top App Bar)
    // ---------------------------------------------------------------

    [ObservableProperty]
    private string _nomeUtilizador = "Maura Rerreira"; // TODO: vir da sessão autenticada (ver LoginViewModel)

    [ObservableProperty]
    private string _dataAtualLabel = string.Empty;

    [ObservableProperty]
    private string _diaHoraLabel = string.Empty;

    [ObservableProperty]
    private int _numeroNotificacoes = 3;

    [ObservableProperty]
    private bool _isLoading;

    public string SaudacaoLabel => $"Olá, {NomeUtilizador}! 👋";

    // ---------------------------------------------------------------
    // KPIs
    // ---------------------------------------------------------------

    public ObservableCollection<KpiCardModel> KpiCards { get; } = new();

    // ---------------------------------------------------------------
    // Gráfico "Receita por Trimestre" — desenhado à mão (Path/SVG via
    // Viewbox, viewBox lógico 800x250), sem dependência de bibliotecas de
    // gráficos externas.
    // ---------------------------------------------------------------

    [ObservableProperty]
    private string _chartFillPathData = string.Empty;

    [ObservableProperty]
    private string _chartStrokePathData = string.Empty;

    public ObservableCollection<ChartMarkerPoint> ChartMarkers { get; } = new();

    public ObservableCollection<string> ChartMonthLabels { get; } = new();

    public ObservableCollection<string> AnosLetivos { get; } = new() { "2024/2025", "2025/2026" };

    [ObservableProperty]
    private string _anoLetivoSelecionado = "2025/2026";

    /// <summary>
    /// Trimestres do calendário letivo angolano (Out–Dez, Jan–Mar, Abr–Jun).
    /// Ajustar os meses aqui se o calendário letivo da instituição for diferente.
    /// </summary>
    public ObservableCollection<TrimestreOption> Trimestres { get; } = new()
    {
        new TrimestreOption("1º Trimestre", new[] { "Out", "Nov", "Dez" }),
        new TrimestreOption("2º Trimestre", new[] { "Jan", "Fev", "Mar" }),
        new TrimestreOption("3º Trimestre", new[] { "Abr", "Mai", "Jun" }),
    };

    [ObservableProperty]
    private TrimestreOption _trimestreSelecionado;

    partial void OnAnoLetivoSelecionadoChanged(string value) => CarregarGrafico();

    partial void OnTrimestreSelecionadoChanged(TrimestreOption value) => CarregarGrafico();

    // ---------------------------------------------------------------
    // Top 5 Devedores
    // ---------------------------------------------------------------

    public ObservableCollection<DevedorModel> TopDevedores { get; } = new();

    [RelayCommand]
    private void VerTodosDevedores()
    {
        // TODO: navegar para Alunos com filtro "em atraso" aplicado.
    }

    // ---------------------------------------------------------------
    // Resumo Financeiro do Dia
    // ---------------------------------------------------------------

    [ObservableProperty]
    private string _entradasHoje = "0";

    [ObservableProperty]
    private string _saidasHoje = "0";

    [ObservableProperty]
    private string _saldoHoje = "0";

    [RelayCommand]
    private void FecharDia()
    {
        // TODO: abrir modal "Fechar Caixa" (ver VIEW 4 - FINANCEIRO, aba Caixa).
    }

    // ---------------------------------------------------------------
    // Rodapé
    // ---------------------------------------------------------------

    public string VersaoLabel { get; } = "School Manager v1.0.0 | Todos os direitos reservados";

    [ObservableProperty]
    private bool _backupOk = true;

    // ---------------------------------------------------------------

    public DashboardViewModel(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
        _trimestreSelecionado = Trimestres[0];

        AtualizarDataHora();  // síncrono, não depende do Core
        CarregarGrafico();    // mock, estruturado por ano letivo/trimestre
        // NADA de dados do Core aqui — vai para InitializeAsync
    }

    public async Task InitializeAsync()
    {
        IsLoading = true;
        try
        {
            var resumo = await _dashboardService.ObterResumoAsync(DateTime.Now);

            KpiCards.Clear();
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.AccountGroup,
                Label = "Alunos Ativos",
                Value = resumo.TotalAlunos.ToString(),
                TrendText = string.Empty,
                TrendIsPositive = true
            });
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.CashMultiple,
                Label = "Receita do Mês",
                Value = resumo.PropinasPagas.ToString("N0"),
                Suffix = "Kz",
                TrendText = string.Empty,
                TrendIsPositive = true
            });
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.CreditCardOff,
                Label = "Em Dívida",
                Value = resumo.PropinasEmAtraso.ToString("N0"),
                Suffix = "Kz",
                TrendText = string.Empty,
                TrendIsPositive = false,
                IsAlert = resumo.PropinasEmAtraso > 0
            });
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.Bank,
                Label = "Recebido Hoje",
                Value = resumo.Entradas.ToString("N0"),
                Suffix = "Kz",
                TrendText = string.Empty,
                TrendIsPositive = true
            });

            // TODO: ResumoDashboardDto ainda não expõe "top devedores" —
            // ligar aqui (ex.: resumo.TopDevedores) quando o Core o expuser.
            // Por agora a lista fica vazia em vez de mostrar dados fictícios.
            TopDevedores.Clear();

            EntradasHoje = resumo.Entradas.ToString("N0");
            SaidasHoje = resumo.Saidas.ToString("N0");
            SaldoHoje = resumo.SaldoCaixa.ToString("N0");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void AtualizarDataHora()
    {
        var agora = DateTime.Now;

        string[] meses =
        {
            "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
            "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
        };
        string[] diasSemana =
        {
            "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira",
            "Quinta-feira", "Sexta-feira", "Sábado"
        };

        DataAtualLabel = $"{agora.Day} de {meses[agora.Month - 1]}, {agora.Year}";
        DiaHoraLabel = $"{diasSemana[(int)agora.DayOfWeek]}, {agora:HH:mm}";
    }

    /// <summary>
    /// Reconstrói o gráfico (path SVG + marcadores + rótulos dos meses) com
    /// base no ano letivo e trimestre selecionados. Os valores são mock
    /// (determinísticos por trimestre) até o Core expor uma série mensal
    /// real — por exemplo, um futuro
    /// IDashboardService.ObterReceitaPorMesAsync(anoLetivo, meses), que
    /// deve substituir os 3 valores abaixo por dados reais (a lógica de
    /// desenho do path continua igual).
    /// </summary>
    private void CarregarGrafico()
    {
        var trimestre = TrimestreSelecionado ?? Trimestres[0];

        double[] valoresMock = trimestre.Label switch
        {
            "1º Trimestre" => new double[] { 420_000, 460_000, 510_000 },
            "2º Trimestre" => new double[] { 480_000, 530_000, 560_000 },
            _ => new double[] { 500_000, 540_000, 610_000 },
        };

        // viewBox lógico 800x250 (mesmo usado no Viewbox do XAML).
        const double largura = 800;
        const double altura = 250;
        const double margemSuperior = 30;
        const double margemInferior = 30;

        double[] xs = { 0, largura / 2, largura };

        double minValor = valoresMock.Min();
        double maxValor = valoresMock.Max();
        double amplitude = maxValor - minValor;
        if (amplitude == 0) amplitude = 1; // evita divisão por zero quando os 3 valores são iguais

        double[] ys = valoresMock
            .Select(v => altura - margemInferior - (v - minValor) / amplitude * (altura - margemSuperior - margemInferior))
            .ToArray();

        var invariante = System.Globalization.CultureInfo.InvariantCulture;
        string P(double x, double y) => $"{x.ToString(invariante)},{y.ToString(invariante)}";

        ChartStrokePathData = $"M{P(xs[0], ys[0])} L{P(xs[1], ys[1])} L{P(xs[2], ys[2])}";
        ChartFillPathData = $"M{P(xs[0], ys[0])} L{P(xs[1], ys[1])} L{P(xs[2], ys[2])} L{P(xs[2], altura)} L{P(xs[0], altura)} Z";

        ChartMarkers.Clear();
        for (int i = 0; i < xs.Length; i++)
        {
            // Deslocado em -4 (raio do círculo) para o Ellipse 8x8 ficar
            // centrado exatamente sobre o ponto do path.
            ChartMarkers.Add(new ChartMarkerPoint { X = xs[i] - 4, Y = ys[i] - 4, MesLabel = trimestre.Meses[i] });
        }

        ChartMonthLabels.Clear();
        foreach (var mes in trimestre.Meses)
            ChartMonthLabels.Add(mes);
    }
}

/// <summary>
/// Opção de trimestre para o filtro do gráfico de receita, com os meses
/// respectivos (calendário letivo angolano: Out–Dez, Jan–Mar, Abr–Jun).
/// </summary>
public sealed class TrimestreOption
{
    public string Label { get; }
    public string[] Meses { get; }

    public TrimestreOption(string label, string[] meses)
    {
        Label = label;
        Meses = meses;
    }

    public override string ToString() => Label;
}
