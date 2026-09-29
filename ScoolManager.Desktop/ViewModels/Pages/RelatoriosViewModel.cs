using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Relatorios;
using ScoolManager.Desktop.Services;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.ViewModels.Pages;

public partial class RelatoriosViewModel : ViewModelBase
{
    private readonly IRelatorioService _relatorios;
    private readonly IExportacaoArquivoService _exportacao;

    public ObservableCollection<RelatorioTipoItem> RelatoriosDisponiveis { get; }

    [ObservableProperty] private RelatorioTipoItem? _relatorioSelecionado;
    public RelatorioFiltro FiltroAtual { get; } = new();

    [ObservableProperty] private bool _modalConfigurarVisivel;
    [ObservableProperty] private bool _modalPreVisualizarVisivel;
    [ObservableProperty] private bool _modalExportacaoVisivel;
    [ObservableProperty] private string _mensagemExportacao = string.Empty;
    [ObservableProperty] private string _erroRelatorios = string.Empty;
    [ObservableProperty] private bool _gerando;

    public bool AlgumModalAberto => ModalConfigurarVisivel || ModalPreVisualizarVisivel || ModalExportacaoVisivel;

    partial void OnModalConfigurarVisivelChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnModalPreVisualizarVisivelChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnModalExportacaoVisivelChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

    public bool MostrarMatriculas => RelatorioSelecionado?.Tipo == RelatorioTipo.Matriculas;
    public bool MostrarAlunos => RelatorioSelecionado?.Tipo == RelatorioTipo.ListaAlunos;
    public bool MostrarPropinas => RelatorioSelecionado?.Tipo is RelatorioTipo.PropinasPagas or RelatorioTipo.PropinasAtraso;
    public bool MostrarMovimentos => RelatorioSelecionado?.Tipo is RelatorioTipo.Entradas or RelatorioTipo.Saidas;
    public bool MostrarFluxoCaixa => RelatorioSelecionado?.Tipo == RelatorioTipo.FluxoCaixa;
    public bool MostrarFiltroPeriodo => RelatorioSelecionado?.Tipo != RelatorioTipo.ListaAlunos;
    public bool MostrarFiltroTurmaClasse => MostrarMatriculas || MostrarAlunos;
    public bool MostrarFiltroAnoLectivo => MostrarMatriculas;
    public bool MostrarFiltroMetodoPagamento => RelatorioSelecionado?.Tipo == RelatorioTipo.PropinasPagas;

    partial void OnRelatorioSelecionadoChanged(RelatorioTipoItem? value)
    {
        OnPropertyChanged(nameof(MostrarMatriculas));
        OnPropertyChanged(nameof(MostrarAlunos));
        OnPropertyChanged(nameof(MostrarPropinas));
        OnPropertyChanged(nameof(MostrarMovimentos));
        OnPropertyChanged(nameof(MostrarFluxoCaixa));
        OnPropertyChanged(nameof(MostrarFiltroPeriodo));
        OnPropertyChanged(nameof(MostrarFiltroTurmaClasse));
        OnPropertyChanged(nameof(MostrarFiltroAnoLectivo));
        OnPropertyChanged(nameof(MostrarFiltroMetodoPagamento));
    }

    public ObservableCollection<MatriculaRelatorioItem> ResultadoMatriculas { get; } = new();
    public ObservableCollection<AlunoRelatorioItem> ResultadoAlunos { get; } = new();
    public ObservableCollection<PropinaRelatorioItem> ResultadoPropinas { get; } = new();
    public ObservableCollection<RelatorioMovimentoItem> ResultadoMovimentos { get; } = new();
    public ObservableCollection<FluxoCaixaRelatorioItem> ResultadoFluxoCaixa { get; } = new();

    public RelatoriosViewModel(IRelatorioService relatorios, IExportacaoArquivoService exportacao)
    {
        _relatorios = relatorios;
        _exportacao = exportacao;

        RelatoriosDisponiveis = new ObservableCollection<RelatorioTipoItem>
        {
            new(RelatorioTipo.Matriculas, "Matrículas", "Novas matrículas efetuadas no período.", MaterialIconKind.AccountPlus),
            new(RelatorioTipo.ListaAlunos, "Lista de Alunos", "Listagem completa de alunos e a sua situação.", MaterialIconKind.AccountGroup),
            new(RelatorioTipo.PropinasPagas, "Propinas Pagas", "Pagamentos de propinas confirmados.", MaterialIconKind.CashCheck),
            new(RelatorioTipo.PropinasAtraso, "Propinas em Atraso", "Propinas por regularizar.", MaterialIconKind.CashRemove),
            new(RelatorioTipo.Entradas, "Entradas", "Entradas de caixa registadas.", MaterialIconKind.TrendingUp),
            new(RelatorioTipo.Saidas, "Saídas", "Saídas de caixa registadas.", MaterialIconKind.TrendingDown),
            new(RelatorioTipo.FluxoCaixa, "Fluxo de Caixa", "Evolução do saldo de caixa por período.", MaterialIconKind.ChartLine),
        };
    }

    [RelayCommand]
    private void AbrirConfigurarRelatorio(RelatorioTipoItem item)
    {
        RelatorioSelecionado = item;
        FiltroAtual.Limpar();
        ErroRelatorios = string.Empty;
        ModalConfigurarVisivel = true;
    }

    [RelayCommand]
    private void FecharModal()
    {
        ModalConfigurarVisivel = false;
        ModalPreVisualizarVisivel = false;
        ModalExportacaoVisivel = false;
    }

    [RelayCommand]
    private void VoltarConfigurar()
    {
        ModalPreVisualizarVisivel = false;
        ModalConfigurarVisivel = true;
    }

    [RelayCommand]
    private async Task GerarPreVisualizacao()
    {
        if (RelatorioSelecionado is null) return;

        try
        {
            Gerando = true;
            ErroRelatorios = string.Empty;
            LimparResultados();

            var filtro = new FiltroRelatorioDto
            {
                Periodo = FiltroAtual.Periodo,
                DataInicio = FiltroAtual.DataInicio?.Date,
                DataFim = FiltroAtual.DataFim?.Date.AddDays(1).AddTicks(-1),
                AnoLectivo = FiltroAtual.AnoLectivo,
                Turma = FiltroAtual.Turma,
                Classe = FiltroAtual.Classe,
                MetodoPagamento = FiltroAtual.MetodoPagamento
            };

            switch (RelatorioSelecionado.Tipo)
            {
                case RelatorioTipo.Matriculas:
                    foreach (var x in await _relatorios.GerarMatriculasAsync(filtro)) ResultadoMatriculas.Add(new()
                    {
                        Aluno=x.Aluno, NumeroMatricula=x.NumeroMatricula, Classe=x.Classe, Turma=x.Turma,
                        DataMatricula=x.DataMatricula.ToString("dd/MM/yyyy"), Estado=x.Estado
                    });
                    break;
                case RelatorioTipo.ListaAlunos:
                    foreach (var x in await _relatorios.GerarListaAlunosAsync(filtro)) ResultadoAlunos.Add(new()
                    {
                        Nome=x.Nome, NumeroMatricula=x.NumeroMatricula, Classe=x.Classe, Turma=x.Turma,
                        Situacao=x.Situacao, Contacto=x.Contacto
                    });
                    break;
                case RelatorioTipo.PropinasPagas:
                    foreach (var x in await _relatorios.GerarPropinasPagasAsync(filtro)) ResultadoPropinas.Add(MapPropina(x));
                    break;
                case RelatorioTipo.PropinasAtraso:
                    foreach (var x in await _relatorios.GerarPropinasAtrasoAsync(filtro)) ResultadoPropinas.Add(MapPropina(x));
                    break;
                case RelatorioTipo.Entradas:
                    foreach (var x in await _relatorios.GerarEntradasAsync(filtro)) ResultadoMovimentos.Add(MapMovimento(x));
                    break;
                case RelatorioTipo.Saidas:
                    foreach (var x in await _relatorios.GerarSaidasAsync(filtro)) ResultadoMovimentos.Add(MapMovimento(x));
                    break;
                case RelatorioTipo.FluxoCaixa:
                    foreach (var x in await _relatorios.GerarFluxoCaixaAsync(filtro)) ResultadoFluxoCaixa.Add(new()
                    {
                        Periodo=x.Periodo, SaldoInicial=Kz(x.SaldoInicial), TotalEntradas=Kz(x.TotalEntradas),
                        TotalSaidas=Kz(x.TotalSaidas), SaldoFinal=Kz(x.SaldoFinal)
                    });
                    break;
            }

            ModalConfigurarVisivel = false;
            ModalPreVisualizarVisivel = true;
        }
        catch (Exception ex) { ErroRelatorios = ex.Message; }
        finally { Gerando = false; }
    }

    [RelayCommand]
    private async Task ExportarPdf()
    {
        if (RelatorioSelecionado is null) return;

        try
        {
            var (headers, rows) = ConstruirLinhasPdf();
            var caminho = await _exportacao.ExportarPdfAsync(
                RelatorioSelecionado.Titulo,
                $"ScoolManager_{RelatorioSelecionado.Titulo.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.pdf",
                headers,
                rows,
                $"Período: {FiltroAtual.DataInicio?.Date:dd/MM/yyyy} — {FiltroAtual.DataFim?.Date:dd/MM/yyyy}");

            if (caminho is not null)
                MostrarMensagemExportacao($"PDF gerado com sucesso em:\n{caminho}");
        }
        catch (Exception ex) { ErroRelatorios = ex.Message; }
    }

    [RelayCommand]
    private async Task ExportarExcel()
    {
        if (RelatorioSelecionado is null) return;

        try
        {
            var (headers, rows) = ConstruirLinhasPdf();
            var caminho = await _exportacao.ExportarExcelAsync(
                $"ScoolManager_{RelatorioSelecionado.Titulo.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                headers,
                rows,
                RelatorioSelecionado.Titulo.Length > 25 ? "Relatorio" : RelatorioSelecionado.Titulo);

            if (caminho is not null)
                MostrarMensagemExportacao($"Excel gerado com sucesso em:\n{caminho}");
        }
        catch (Exception ex) { ErroRelatorios = ex.Message; }
    }

    [RelayCommand]
    private void Imprimir() => MostrarMensagemExportacao("O relatório PDF já está pronto para impressão.");

    private void MostrarMensagemExportacao(string mensagem)
    {
        MensagemExportacao = mensagem;
        ModalPreVisualizarVisivel = false;
        ModalExportacaoVisivel = true;
    }

    private (IReadOnlyList<string>, IReadOnlyList<IReadOnlyList<string>>) ConstruirLinhasPdf()
    {
        if (RelatorioSelecionado?.Tipo == RelatorioTipo.Matriculas)
            return (new[]{"Aluno","Matrícula","Classe","Turma","Data","Estado"}, ResultadoMatriculas.Select(x=>(IReadOnlyList<string>)new[]{x.Aluno,x.NumeroMatricula,x.Classe,x.Turma,x.DataMatricula,x.Estado}).ToList());
        if (RelatorioSelecionado?.Tipo == RelatorioTipo.ListaAlunos)
            return (new[]{"Aluno","Matrícula","Classe","Turma","Situação","Contacto"}, ResultadoAlunos.Select(x=>(IReadOnlyList<string>)new[]{x.Nome,x.NumeroMatricula,x.Classe,x.Turma,x.Situacao,x.Contacto}).ToList());
        if (RelatorioSelecionado?.Tipo is RelatorioTipo.PropinasPagas or RelatorioTipo.PropinasAtraso)
            return (new[]{"Aluno","Referência","Valor","Vencimento","Pagamento","Estado"}, ResultadoPropinas.Select(x=>(IReadOnlyList<string>)new[]{x.Aluno,x.Referencia,x.Valor,x.DataVencimento,x.DataPagamento,x.Estado}).ToList());
        if (RelatorioSelecionado?.Tipo is RelatorioTipo.Entradas or RelatorioTipo.Saidas)
            return (new[]{"Data","Descrição","Categoria","Valor","Tipo"}, ResultadoMovimentos.Select(x=>(IReadOnlyList<string>)new[]{x.Data,x.Descricao,x.Categoria,x.Valor,x.Tipo}).ToList());
        return (new[]{"Período","Saldo inicial","Entradas","Saídas","Saldo final"}, ResultadoFluxoCaixa.Select(x=>(IReadOnlyList<string>)new[]{x.Periodo,x.SaldoInicial,x.TotalEntradas,x.TotalSaidas,x.SaldoFinal}).ToList());
    }

    private void LimparResultados()
    {
        ResultadoMatriculas.Clear(); ResultadoAlunos.Clear(); ResultadoPropinas.Clear();
        ResultadoMovimentos.Clear(); ResultadoFluxoCaixa.Clear();
    }

    private static PropinaRelatorioItem MapPropina(PropinaRelatorioDto x) => new()
    {
        Aluno=x.Aluno, Referencia=x.Referencia, Valor=Kz(x.Valor),
        DataVencimento=x.DataVencimento.ToString("dd/MM/yyyy"),
        DataPagamento=x.DataPagamento?.ToString("dd/MM/yyyy") ?? string.Empty, Estado=x.Estado
    };

    private static RelatorioMovimentoItem MapMovimento(RelatorioMovimentoDto x) => new()
    {
        Data=x.Data.ToString("dd/MM/yyyy"), Descricao=x.Descricao, Categoria=x.Categoria,
        Valor=Kz(x.Valor), Tipo=x.Tipo
    };

    private static string Kz(decimal value) => $"{value:N0} Kz";
}
