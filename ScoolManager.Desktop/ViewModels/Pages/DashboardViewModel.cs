using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Dashboard;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels.Pages;

public partial class DashboardViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IDashboardService _dashboardService;
    private IReadOnlyList<ReceitaMensalDto> _receitas = Array.Empty<ReceitaMensalDto>();

    public string Title { get; } = "Dashboard";

    [ObservableProperty] private string _nomeUtilizador = "Maura Rerreira";
    [ObservableProperty] private string _dataAtualLabel = string.Empty;
    [ObservableProperty] private string _diaHoraLabel = string.Empty;
    [ObservableProperty] private int _numeroNotificacoes = 3;
    [ObservableProperty] private bool _isLoading;

    public string SaudacaoLabel => $"Olá, {NomeUtilizador}! 👋";

    public ObservableCollection<KpiCardModel> KpiCards { get; } = new();

    public ObservableCollection<string> AnosLetivos { get; } = new() { "2024/2025", "2025/2026" };

    [ObservableProperty]
    private string _anoLetivoSelecionado = "2025/2026";

    public ObservableCollection<TrimestreOption> Trimestres { get; } = new()
    {
        new TrimestreOption("1º Trimestre", new[] { "Out", "Nov", "Dez" }),
        new TrimestreOption("2º Trimestre", new[] { "Jan", "Fev", "Mar" }),
        new TrimestreOption("3º Trimestre", new[] { "Abr", "Mai", "Jun" }),
    };

    [ObservableProperty]
    private TrimestreOption _trimestreSelecionado;

    public ObservableCollection<DevedorModel> TopDevedores { get; } = new();

    [ObservableProperty] private string _entradasHoje = "0";
    [ObservableProperty] private string _saidasHoje = "0";
    [ObservableProperty] private string _saldoHoje = "0";

    public string VersaoLabel { get; } = "School Manager v1.0.0 | Todos os direitos reservados";

    [ObservableProperty] private bool _backupOk = true;

    public IReadOnlyList<double> ChartValues { get; private set; } = Array.Empty<double>();
    public IReadOnlyList<string> ChartLabels { get; private set; } = Array.Empty<string>();

    public event Action? ChartAtualizado;

    partial void OnAnoLetivoSelecionadoChanged(string value) => CarregarGrafico();
    partial void OnTrimestreSelecionadoChanged(TrimestreOption value) => CarregarGrafico();

    public DashboardViewModel(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
        _trimestreSelecionado = Trimestres[0];
        AtualizarDataHora();
        CarregarGrafico();
    }

    public async Task InitializeAsync()
    {
        IsLoading = true;
        try
        {
            var resumo = await _dashboardService.ObterResumoAsync(DateTime.Now);
            _receitas = resumo.ReceitaPorMes;

            KpiCards.Clear();
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.AccountGroup,
                Label = "Alunos Ativos",
                Value = resumo.TotalAlunos.ToString(),
                TrendIsPositive = true
            });
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.CashMultiple,
                Label = "Receita do Mês",
                Value = resumo.PropinasPagas.ToString("N0", CultureInfo.GetCultureInfo("pt-PT")),
                Suffix = "Kz",
                TrendIsPositive = true
            });
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.CreditCardOff,
                Label = "Em Dívida",
                Value = resumo.TotalEmDivida.ToString("N0", CultureInfo.GetCultureInfo("pt-PT")),
                Suffix = "Kz",
                TrendIsPositive = false,
                IsAlert = resumo.TotalEmDivida > 0
            });
            KpiCards.Add(new KpiCardModel
            {
                Icon = MaterialIconKind.Bank,
                Label = "Recebido Hoje",
                Value = resumo.Entradas.ToString("N0", CultureInfo.GetCultureInfo("pt-PT")),
                Suffix = "Kz",
                TrendIsPositive = true
            });

            TopDevedores.Clear();
            var rank = 1;
            foreach (var devedor in resumo.TopDevedores)
            {
                TopDevedores.Add(new DevedorModel
                {
                    Rank = rank++,
                    Nome = devedor.Nome,
                    Turma = devedor.Turma,
                    ValorEmDivida = devedor.ValorEmDivida.ToString("N0", CultureInfo.GetCultureInfo("pt-PT")),
                    Iniciais = ObterIniciais(devedor.Nome)
                });
            }

            EntradasHoje = resumo.Entradas.ToString("N0", CultureInfo.GetCultureInfo("pt-PT"));
            SaidasHoje = resumo.Saidas.ToString("N0", CultureInfo.GetCultureInfo("pt-PT"));
            SaldoHoje = resumo.SaldoCaixa.ToString("N0", CultureInfo.GetCultureInfo("pt-PT"));

            CarregarGrafico();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void VerTodosDevedores()
    {
    }

    [RelayCommand]
    private void FecharDia()
    {
    }

    private void CarregarGrafico()
    {
        var trimestre = TrimestreSelecionado ?? Trimestres[0];
        var anoInicial = ObterAnoInicial(AnoLetivoSelecionado);

        var valores = new double[3];
        var labels = trimestre.Meses.ToArray();

        for (var i = 0; i < 3; i++)
        {
            var mes = ObterMesNumero(trimestre.Meses[i]);
            var ano = mes >= 9 ? anoInicial : anoInicial + 1;

            valores[i] = (double)(_receitas
                .FirstOrDefault(r => r.Mes.Year == ano && r.Mes.Month == mes)?.Valor ?? 0m);
        }

        ChartValues = valores;
        ChartLabels = labels;
        OnPropertyChanged(nameof(ChartValues));
        OnPropertyChanged(nameof(ChartLabels));
        ChartAtualizado?.Invoke();
    }

    private static int ObterAnoInicial(string? anoLetivo)
    {
        if (!string.IsNullOrWhiteSpace(anoLetivo) &&
            int.TryParse(anoLetivo.Split('/')[0], out var ano))
            return ano;

        return DateTime.Now.Month >= 9 ? DateTime.Now.Year : DateTime.Now.Year - 1;
    }

    private static int ObterMesNumero(string mes) => mes switch
    {
        "Jan" => 1, "Fev" => 2, "Mar" => 3, "Abr" => 4, "Mai" => 5, "Jun" => 6,
        "Jul" => 7, "Ago" => 8, "Set" => 9, "Out" => 10, "Nov" => 11, "Dez" => 12,
        _ => 1
    };

    private static string ObterIniciais(string nome)
    {
        var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (partes.Length == 0) return "?";
        if (partes.Length == 1) return partes[0][..Math.Min(2, partes[0].Length)].ToUpperInvariant();
        return $"{partes[0][0]}{partes[^1][0]}".ToUpperInvariant();
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
}

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
