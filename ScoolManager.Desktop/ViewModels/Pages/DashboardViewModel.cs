using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>
/// ViewModel do Dashboard. Os dados abaixo são "mock" (estáticos), exatamente
/// como no protótipo dashBoard.html. Quando os serviços reais existirem
/// (AlunosService, FinanceiroService, etc.) basta substituir o conteúdo do
/// construtor / dos métodos Carregar* por chamadas assíncronas a esses serviços.
/// </summary>
public partial class DashboardViewModel : ViewModelBase
{
    public string Title { get; } = "Dashboard";

    // ---------------------------------------------------------------
    // Cabeçalho (Top App Bar)
    // ---------------------------------------------------------------

    [ObservableProperty]
    private string _nomeUtilizador = "Maura Rerreira";

    [ObservableProperty]
    private string _dataAtualLabel = string.Empty;

    [ObservableProperty]
    private string _diaHoraLabel = string.Empty;

    [ObservableProperty]
    private int _numeroNotificacoes = 3;

    public string SaudacaoLabel => $"Olá, {NomeUtilizador}! 👋";

    // ---------------------------------------------------------------
    // KPIs
    // ---------------------------------------------------------------

    public ObservableCollection<KpiCardModel> KpiCards { get; } = new();

    // ---------------------------------------------------------------
    // Gráfico "Receita dos Últimos 6 Meses"
    // ---------------------------------------------------------------

    /// <summary>Área de preenchimento sob a linha (mesmo path do mock em HTML).</summary>
    public string ChartFillPathData { get; } =
        "M0,180 L160,160 L320,170 L480,120 L640,130 L800,80 L800,250 L0,250 Z";

    /// <summary>Linha do gráfico.</summary>
    public string ChartStrokePathData { get; } =
        "M0,180 L160,160 L320,170 L480,120 L640,130 L800,80";

    public ObservableCollection<ChartMarkerPoint> ChartMarkers { get; } = new();

    public ObservableCollection<string> ChartMonthLabels { get; } =
        new() { "Dez", "Jan", "Fev", "Mar", "Abr", "Mai" };

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
    // Atalhos Rápidos
    // ---------------------------------------------------------------

    public ObservableCollection<AtalhoRapidoModel> AtalhosRapidos { get; } = new();

    [RelayCommand]
    private void ExecutarAtalho(AtalhoRapidoModel? atalho)
    {
        if (atalho is null) return;

        // TODO: abrir o modal correspondente (Nova Matrícula, Novo Pagamento,
        // Emitir Recibo, Nova Entrada, Configurar Relatório), conforme
        // School_Manager_Fluxo_Navegacao.txt.
    }

    // ---------------------------------------------------------------
    // Resumo Financeiro do Dia
    // ---------------------------------------------------------------

    [ObservableProperty]
    private string _entradasHoje = "65.000";

    [ObservableProperty]
    private string _saidasHoje = "12.500";

    [ObservableProperty]
    private string _saldoHoje = "52.500";

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
    public DashboardViewModel()
    {
        AtualizarDataHora();
        CarregarKpis();
        CarregarGrafico();
        CarregarDevedores();
        CarregarAtalhos();
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

    private void CarregarKpis()
    {
        KpiCards.Add(new KpiCardModel
        {
            Icon = MaterialIconKind.AccountGroup,
            Label = "Alunos Ativos",
            Value = "78",
            TrendText = "+8 este mês",
            TrendIsPositive = true
        });

        KpiCards.Add(new KpiCardModel
        {
            Icon = MaterialIconKind.CashMultiple,
            Label = "Receita do Mês",
            Value = "1.200.000",
            Suffix = "Kz",
            TrendText = "+12.5%",
            TrendIsPositive = true
        });

        KpiCards.Add(new KpiCardModel
        {
            Icon = MaterialIconKind.CreditCardOff,
            Label = "Em Dívida",
            Value = "850.000",
            Suffix = "Kz",
            TrendText = "-3.2%",
            TrendIsPositive = false,
            IsAlert = true
        });

        KpiCards.Add(new KpiCardModel
        {
            Icon = MaterialIconKind.Bank,
            Label = "Recebido Hoje",
            Value = "65.000",
            Suffix = "Kz",
            TrendText = "+15.8%",
            TrendIsPositive = true
        });
    }

    private void CarregarGrafico()
    {
        // X/Y já vêm deslocados em -4 (raio do círculo) para que o Ellipse
        // de 8x8 fique centrado exatamente sobre o ponto do path do gráfico.
        ChartMarkers.Add(new ChartMarkerPoint { X = 0 - 4, Y = 180 - 4, MesLabel = "Dez" });
        ChartMarkers.Add(new ChartMarkerPoint { X = 160 - 4, Y = 160 - 4, MesLabel = "Jan" });
        ChartMarkers.Add(new ChartMarkerPoint { X = 320 - 4, Y = 170 - 4, MesLabel = "Fev" });
        ChartMarkers.Add(new ChartMarkerPoint { X = 480 - 4, Y = 120 - 4, MesLabel = "Mar" });
        ChartMarkers.Add(new ChartMarkerPoint { X = 640 - 4, Y = 130 - 4, MesLabel = "Abr" });
        ChartMarkers.Add(new ChartMarkerPoint { X = 800 - 4, Y = 80 - 4, MesLabel = "Mai" });
    }

    private void CarregarDevedores()
    {
        TopDevedores.Add(new DevedorModel { Rank = 1, Nome = "João Pedro da Silva", Turma = "8ª Classe A", ValorEmDivida = "120.000", Iniciais = "JP" });
        TopDevedores.Add(new DevedorModel { Rank = 2, Nome = "Maria Luísa Alberto", Turma = "9ª Classe B", ValorEmDivida = "95.000", Iniciais = "ML" });
        TopDevedores.Add(new DevedorModel { Rank = 3, Nome = "Manuel Francisco", Turma = "7ª Classe A", ValorEmDivida = "80.000", Iniciais = "MF" });
        TopDevedores.Add(new DevedorModel { Rank = 4, Nome = "Ana Paula Domingos", Turma = "10ª Classe A", ValorEmDivida = "75.000", Iniciais = "AP" });
        TopDevedores.Add(new DevedorModel { Rank = 5, Nome = "Carlos Manuel", Turma = "6ª Classe B", ValorEmDivida = "65.000", Iniciais = "CM" });
    }

    private void CarregarAtalhos()
    {
        AtalhosRapidos.Add(new AtalhoRapidoModel { Icon = MaterialIconKind.AccountPlus, Label = "Novo Aluno" });
        AtalhosRapidos.Add(new AtalhoRapidoModel { Icon = MaterialIconKind.CashRegister, Label = "Novo Pagamento" });
        AtalhosRapidos.Add(new AtalhoRapidoModel { Icon = MaterialIconKind.FileDocumentOutline, Label = "Emitir Recibo" });
        AtalhosRapidos.Add(new AtalhoRapidoModel { Icon = MaterialIconKind.Import, Label = "Nova Entrada" });
        AtalhosRapidos.Add(new AtalhoRapidoModel { Icon = MaterialIconKind.ChartBar, Label = "Relatório Diário" });
    }
}
