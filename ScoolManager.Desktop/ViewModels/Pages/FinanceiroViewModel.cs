using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ScoolManager.Desktop.ViewModels.Pages
{

/// <summary>
/// ViewModel da view "Financeiro" (View 4 da Secretaria Escolar).
///
/// 4 abas internas: Recebimentos, Entradas, Saídas, Caixa. Segue o mesmo
/// padrão de AlunosViewModel/DetalhesAlunoViewModel: dados locais/mock,
/// modais em overlay, sem dependência do ScoolManager.Core.
/// </summary>
public partial class FinanceiroViewModel : ViewModelBase
{
    private readonly IFinanceiroService _financeiro;
    private readonly ICaixaService _caixa;
    private readonly ISessaoAtualService _sessaoAtual;
    [ObservableProperty] private string _erroFinanceiro = string.Empty;
    public bool TemErroFinanceiro => !string.IsNullOrWhiteSpace(ErroFinanceiro);
    partial void OnErroFinanceiroChanged(string value) => OnPropertyChanged(nameof(TemErroFinanceiro));
    [RelayCommand] private void FecharErroFinanceiro() => ErroFinanceiro = string.Empty;

    private static string Kz(decimal value) => value.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("pt-PT")) + " Kz";
    private static bool TryValor(string value, out decimal result)
    {
        var raw = (value ?? string.Empty).Replace("Kz", "").Trim().Replace(".", "").Replace(",", ".");
        return decimal.TryParse(raw, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out result);
    }
    public enum Aba
    {
        Recebimentos,
        Entradas,
        Saidas,
        Caixa
    }

    // ================================================================
    // Abas
    // ================================================================
    [ObservableProperty] private Aba _abaSelecionada = Aba.Recebimentos;

    public bool AbaRecebimentosAtiva => AbaSelecionada == Aba.Recebimentos;
    public bool AbaEntradasAtiva => AbaSelecionada == Aba.Entradas;
    public bool AbaSaidasAtiva => AbaSelecionada == Aba.Saidas;
    public bool AbaCaixaAtiva => AbaSelecionada == Aba.Caixa;

    partial void OnAbaSelecionadaChanged(Aba value)
    {
        OnPropertyChanged(nameof(AbaRecebimentosAtiva));
        OnPropertyChanged(nameof(AbaEntradasAtiva));
        OnPropertyChanged(nameof(AbaSaidasAtiva));
        OnPropertyChanged(nameof(AbaCaixaAtiva));
    }

    [RelayCommand] private void AbrirAbaRecebimentos() => AbaSelecionada = Aba.Recebimentos;
    [RelayCommand] private void AbrirAbaEntradas() => AbaSelecionada = Aba.Entradas;
    [RelayCommand] private void AbrirAbaSaidas() => AbaSelecionada = Aba.Saidas;
    [RelayCommand] private void AbrirAbaCaixa() => AbaSelecionada = Aba.Caixa;

    // ================================================================
    // Aba "Recebimentos" (SÓ CONSULTA — ver nota no cabeçalho da classe)
    // ================================================================
    private readonly List<PagamentoItem> _todosPagamentos;
    public ObservableCollection<PagamentoItem> Pagamentos { get; } = new();

    [ObservableProperty] private string _pesquisaPagamento = string.Empty;

    // ---- Filtros rápidos (Período / Método de Pagamento / Tipo de Cobrança) ----
    public IReadOnlyList<string> OpcoesPeriodo { get; } =
        new[] { "Todos", "Hoje", "Esta semana", "Este mês", "Ano letivo", "Intervalo personalizado" };

    public IReadOnlyList<string> OpcoesMetodoPagamento { get; } =
        new[] { "Todos", "Dinheiro", "Transferência Bancária", "TPA / Multicaixa" };

    public IReadOnlyList<string> OpcoesTipoCobranca { get; } =
        new[] { "Todos", "Matrícula", "Propina", "Confirmação", "Uniforme", "Cartão Escolar", "Declaração", "Certificado", "Outros" };

    [ObservableProperty] private string _filtroPeriodo = "Todos";
    [ObservableProperty] private string _filtroMetodoPagamento = "Todos";
    [ObservableProperty] private string _filtroTipoCobranca = "Todos";

    public bool MostrarIntervaloPersonalizado => FiltroPeriodo == "Intervalo personalizado";

    // Intervalo personalizado (só relevante quando FiltroPeriodo == "Intervalo personalizado")
    [ObservableProperty] private DateTimeOffset? _filtroDataInicio;
    [ObservableProperty] private DateTimeOffset? _filtroDataFim;

    partial void OnPesquisaPagamentoChanged(string value) => AplicarFiltroPagamentos();

    partial void OnFiltroPeriodoChanged(string value)
    {
        OnPropertyChanged(nameof(MostrarIntervaloPersonalizado));
        AplicarFiltroPagamentos();
    }
    partial void OnFiltroMetodoPagamentoChanged(string value) => AplicarFiltroPagamentos();
    partial void OnFiltroTipoCobrancaChanged(string value) => AplicarFiltroPagamentos();
    partial void OnFiltroDataInicioChanged(DateTimeOffset? value) => AplicarFiltroPagamentos();
    partial void OnFiltroDataFimChanged(DateTimeOffset? value) => AplicarFiltroPagamentos();

    private void AplicarFiltroPagamentos()
    {
        IEnumerable<PagamentoItem> query = _todosPagamentos;

        if (!string.IsNullOrWhiteSpace(PesquisaPagamento))
        {
            var termo = PesquisaPagamento.Trim();
            query = query.Where(p =>
                p.Aluno.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                p.Referencia.Contains(termo, StringComparison.OrdinalIgnoreCase));
        }

        if (FiltroMetodoPagamento != "Todos")
            query = query.Where(p => p.Metodo == FiltroMetodoPagamento);

        if (FiltroTipoCobranca != "Todos")
            query = query.Where(p => p.TipoCobranca == FiltroTipoCobranca);

        if (FiltroPeriodo != "Todos")
            query = query.Where(p => DentroDoPeriodo(p.Data, FiltroPeriodo, FiltroDataInicio, FiltroDataFim));

        Pagamentos.Clear();
        foreach (var p in query) Pagamentos.Add(p);
    }

    private static bool DentroDoPeriodo(string dataTexto, string periodo, DateTimeOffset? inicio, DateTimeOffset? fim)
    {
        if (!DateTime.TryParseExact(dataTexto, "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out var data))
            return true;

        var hoje = DateTime.Now.Date;
        return periodo switch
        {
            "Hoje" => data.Date == hoje,
            "Esta semana" => data.Date >= hoje.AddDays(-(int)hoje.DayOfWeek) && data.Date <= hoje,
            "Este mês" => data.Year == hoje.Year && data.Month == hoje.Month,
            "Ano letivo" => data >= AnoLetivoInicio(hoje),
            "Intervalo personalizado" =>
                (!inicio.HasValue || data.Date >= inicio.Value.Date) &&
                (!fim.HasValue || data.Date <= fim.Value.Date),
            _ => true
        };
    }

    /// <summary>Início do ano letivo: 1 de setembro do ano corrente (ou anterior, se ainda não chegou setembro).</summary>
    private static DateTime AnoLetivoInicio(DateTime hoje)
    {
        var inicioEsteAno = new DateTime(hoje.Year, 9, 1);
        return hoje >= inicioEsteAno ? inicioEsteAno : new DateTime(hoje.Year - 1, 9, 1);
    }

    [ObservableProperty] private PagamentoItem? _pagamentoSelecionado;

    // Detalhes do Recebimento / Ver Recibo (mesmo modal, troca de conteúdo)
    [ObservableProperty] private bool _isDetalhesPagamentoAberto;
    [ObservableProperty] private bool _mostrandoRecibo;

    [RelayCommand]
    private void AbrirDetalhesPagamento(PagamentoItem? pagamento)
    {
        if (pagamento is null) return;
        PagamentoSelecionado = pagamento;
        MostrandoRecibo = false;
        IsDetalhesPagamentoAberto = true;
    }

    [RelayCommand] private void AbrirVerRecibo() => MostrandoRecibo = true;
    [RelayCommand] private void VoltarDetalhesPagamento() => MostrandoRecibo = false;

    // TODO: gerar PDF/impressão real do recibo quando existir o serviço.
    [RelayCommand] private void ImprimirRecibo() { }

    // ---- Anular pagamento (mediante autorização) ----
    [ObservableProperty] private bool _isAnularPagamentoAberto;
    [ObservableProperty] private string _motivoAnulacao = string.Empty;

    [RelayCommand]
    private void AbrirAnularPagamento()
    {
        if (PagamentoSelecionado is null) return;
        MotivoAnulacao = string.Empty;
        IsDetalhesPagamentoAberto = false;
        IsAnularPagamentoAberto = true;
    }

    // TODO: exigir autorização (perfil/permissão) real antes de confirmar,
    // e propagar a anulação para o módulo Alunos quando existir o serviço.
    [RelayCommand]
    private async Task ConfirmarAnularPagamento()
    {
        if (PagamentoSelecionado is null || PagamentoSelecionado.Id <= 0 || string.IsNullOrWhiteSpace(MotivoAnulacao))
        {
            ErroFinanceiro = "Informe o motivo da anulação.";
            return;
        }
        try
        {
            await _financeiro.AnularPagamentoAsync(PagamentoSelecionado.Id, MotivoAnulacao.Trim());
            FecharModal();
            await CarregarFinanceiroAsync();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }

    // ---- Exportação de listagens ----
    // TODO: gerar ficheiro real (PDF/Excel) quando existir o serviço de exportação.
    [RelayCommand] private void ExportarPdf() { }
    [RelayCommand] private void ExportarExcel() { }

    // ---- Numeração sequencial de recibos (REC-AAAA-NNNNNN) ----
    private int _proximoNumeroRecibo = 1;

    private string GerarNumeroRecibo() => $"REC-{DateTime.Now.Year}-{_proximoNumeroRecibo++:D6}";

    // ================================================================
    // Abas "Entradas" e "Saídas" (mesma forma, coleções separadas)
    // ================================================================
    public ObservableCollection<MovimentoItem> Entradas { get; } = new();
    public ObservableCollection<MovimentoItem> Saidas { get; } = new();

    /// <summary>"Entrada" ou "Saída" - identifica o contexto do modal partilhado.</summary>
    [ObservableProperty] private string _movimentoTipoModal = "Entrada";

    [ObservableProperty] private MovimentoItem? _movimentoSelecionado;

    [ObservableProperty] private bool _isNovoMovimentoAberto;
    [ObservableProperty] private bool _isEditarMovimentoAberto;
    [ObservableProperty] private bool _isDetalhesMovimentoAberto;

    // Campos do formulário partilhado "Nova Entrada" / "Nova Saída"
    [ObservableProperty] private string _novoMovimentoDescricao = string.Empty;
    [ObservableProperty] private string _novoMovimentoCategoria = string.Empty;
    [ObservableProperty] private string _novoMovimentoValor = string.Empty;

    [RelayCommand]
    private void AbrirNovaEntrada()
    {
        MovimentoTipoModal = "Entrada";
        LimparFormularioMovimento();
        IsNovoMovimentoAberto = true;
    }

    [RelayCommand]
    private void AbrirNovaSaida()
    {
        MovimentoTipoModal = "Saída";
        LimparFormularioMovimento();
        IsNovoMovimentoAberto = true;
    }

    private void LimparFormularioMovimento()
    {
        NovoMovimentoDescricao = string.Empty;
        NovoMovimentoCategoria = string.Empty;
        NovoMovimentoValor = string.Empty;
    }

    [RelayCommand]
    private void AbrirDetalhesMovimento(MovimentoItem? movimento)
    {
        if (movimento is null) return;
        MovimentoSelecionado = movimento;
        MovimentoTipoModal = Entradas.Contains(movimento) ? "Entrada" : "Saída";
        IsDetalhesMovimentoAberto = true;
    }

    [RelayCommand]
    private void AbrirEditarMovimento()
    {
        if (MovimentoSelecionado is null) return;

        // Reaproveita os mesmos campos do formulário "Novo Movimento", já
        // pré-preenchidos com os dados atuais do item selecionado.
        NovoMovimentoDescricao = MovimentoSelecionado.Descricao;
        NovoMovimentoCategoria = MovimentoSelecionado.Categoria;
        NovoMovimentoValor = MovimentoSelecionado.Valor;

        IsDetalhesMovimentoAberto = false;
        IsEditarMovimentoAberto = true;
    }

    [RelayCommand]
    private async Task ConfirmarNovoMovimento()
    {
        if (string.IsNullOrWhiteSpace(NovoMovimentoDescricao) ||
            !TryValor(NovoMovimentoValor, out var valor) || valor <= 0)
        {
            ErroFinanceiro = "Informe descrição e um valor válido.";
            return;
        }
        try
        {
            var movimento = new MovimentoCaixa
            {
                Descricao = NovoMovimentoDescricao.Trim(),
                Categoria = string.IsNullOrWhiteSpace(NovoMovimentoCategoria) ? "Outro" : NovoMovimentoCategoria.Trim(),
                Valor = valor,
                Data = DateTime.Now,
                Tipo = MovimentoTipoModal == "Entrada" ? TipoMovimentoCaixa.Entrada : TipoMovimentoCaixa.Saida
            };
            await _financeiro.RegistarMovimentoAsync(movimento);
            FecharModal();
            await CarregarFinanceiroAsync();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }

    [RelayCommand]
    private async Task ConfirmarEditarMovimento()
    {
        if (MovimentoSelecionado is null || MovimentoSelecionado.Id <= 0 ||
            string.IsNullOrWhiteSpace(NovoMovimentoDescricao) ||
            !TryValor(NovoMovimentoValor, out var valor) || valor <= 0)
        {
            ErroFinanceiro = "Informe descrição e um valor válido.";
            return;
        }
        try
        {
            var movimento = await _financeiro.ObterMovimentoPorIdAsync(MovimentoSelecionado.Id);
            movimento.Descricao = NovoMovimentoDescricao.Trim();
            movimento.Categoria = string.IsNullOrWhiteSpace(NovoMovimentoCategoria) ? "Outro" : NovoMovimentoCategoria.Trim();
            movimento.Valor = valor;
            await _financeiro.AtualizarMovimentoAsync(movimento);
            FecharModal();
            await CarregarFinanceiroAsync();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }

    // ================================================================
    // Aba "Caixa"
    // ================================================================
    [ObservableProperty] private bool _caixaAberto;
    [ObservableProperty] private string _saldoInicialLabel = "0,00 Kz";
    [ObservableProperty] private string _saldoAtualLabel = "0,00 Kz";

    public ObservableCollection<SessaoCaixaItem> HistoricoCaixa { get; } = new();

    [ObservableProperty] private bool _isAbrirCaixaAberto;
    [ObservableProperty] private bool _isFecharCaixaAberto;
    [ObservableProperty] private bool _isReabrirCaixaAberto;

    // Campo do formulário "Abrir Caixa"
    [ObservableProperty] private string _novoSaldoInicialCaixa = string.Empty;

    [RelayCommand]
    private void AbrirAbrirCaixaModal()
    {
        NovoSaldoInicialCaixa = string.Empty;
        IsAbrirCaixaAberto = true;
    }

    [RelayCommand] private void AbrirFecharCaixaModal() => IsFecharCaixaAberto = true;
    [RelayCommand] private void AbrirReabrirCaixaModal() => IsReabrirCaixaAberto = true;

    // ================================================================
    // Fecho unificado de todos os modais desta view
    // ================================================================
    public bool AlgumModalAberto =>
        IsDetalhesPagamentoAberto || IsAnularPagamentoAberto ||
        IsNovoMovimentoAberto || IsEditarMovimentoAberto || IsDetalhesMovimentoAberto ||
        IsAbrirCaixaAberto || IsFecharCaixaAberto || IsReabrirCaixaAberto;

    partial void OnIsDetalhesPagamentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsAnularPagamentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsNovoMovimentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsEditarMovimentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsDetalhesMovimentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsAbrirCaixaAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsFecharCaixaAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsReabrirCaixaAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));

    [RelayCommand]
    private void FecharModal()
    {
        IsDetalhesPagamentoAberto = false;
        MostrandoRecibo = false;
        IsAnularPagamentoAberto = false;
        MotivoAnulacao = string.Empty;
        IsNovoMovimentoAberto = false;
        IsEditarMovimentoAberto = false;
        IsDetalhesMovimentoAberto = false;
        IsAbrirCaixaAberto = false;
        IsFecharCaixaAberto = false;
        IsReabrirCaixaAberto = false;
    }

    // ================================================================
    // Dashboard Financeiro (indicadores resumidos — secção 5 do relatório)
    // ================================================================
    [ObservableProperty] private string _recebimentosMesAtualLabel = "0 Kz";
    [ObservableProperty] private string _recebimentosMesAnteriorLabel = "0 Kz";
    [ObservableProperty] private string _totalEntradasMesLabel = "0 Kz";
    [ObservableProperty] private string _totalSaidasMesLabel = "0 Kz";
    // Saldo atual do caixa reutiliza SaldoAtualLabel (aba "Caixa").

    private void AtualizarIndicadoresDashboard()
    {
        var hoje = DateTime.Now;
        var mesAnterior = hoje.AddMonths(-1);

        var recebimentosValidos = _todosPagamentos.Where(p => !p.Anulado).Select(p => (p.Data, p.Valor));

        RecebimentosMesAtualLabel = FormatarKz(SomaValoresDoMes(recebimentosValidos, hoje.Year, hoje.Month));
        RecebimentosMesAnteriorLabel = FormatarKz(SomaValoresDoMes(recebimentosValidos, mesAnterior.Year, mesAnterior.Month));
        TotalEntradasMesLabel = FormatarKz(SomaValoresDoMes(Entradas.Select(e => (e.Data, e.Valor)), hoje.Year, hoje.Month));
        TotalSaidasMesLabel = FormatarKz(SomaValoresDoMes(Saidas.Select(s => (s.Data, s.Valor)), hoje.Year, hoje.Month));
    }

    private static decimal SomaValoresDoMes(IEnumerable<(string Data, string Valor)> itens, int ano, int mes)
    {
        decimal total = 0m;
        foreach (var (dataTexto, valorTexto) in itens)
        {
            if (DateTime.TryParseExact(dataTexto, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out var data) &&
                data.Year == ano && data.Month == mes)
            {
                total += ParseValorKz(valorTexto);
            }
        }
        return total;
    }

    private static decimal ParseValorKz(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor)) return 0m;
        var limpo = valor.Replace("Kz", string.Empty).Trim()
            .Replace(".", string.Empty)   // remove separador de milhar
            .Replace(",", ".");           // vírgula decimal -> ponto
        return decimal.TryParse(limpo, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : 0m;
    }

    private static string FormatarKz(decimal valor) =>
        $"{valor.ToString("N0", System.Globalization.CultureInfo.InvariantCulture).Replace(",", ".")} Kz";

    // ================================================================
    // Dados mock
    // ================================================================
    public FinanceiroViewModel(IFinanceiroService financeiro, ICaixaService caixa, ISessaoAtualService sessaoAtual)
    {
        _financeiro = financeiro;
        _caixa = caixa;
        _sessaoAtual = sessaoAtual;
        _todosPagamentos = new List<PagamentoItem>();
        _ = CarregarFinanceiroAsync();
    }

    private int UtilizadorAtualId()
        => _sessaoAtual.UtilizadorAtual?.Id
            ?? throw new InvalidOperationException("Não existe uma sessão de utilizador autenticada.");

    private async Task CarregarFinanceiroAsync()
    {
        try
        {
            ErroFinanceiro = string.Empty;
            var agora = DateTime.Now;
            var inicio = agora.AddYears(-1);

            _todosPagamentos.Clear();
            var pagamentos = await _financeiro.ObterPagamentosAsync(inicio, agora);
            foreach (var p in pagamentos.OrderByDescending(x => x.DataPagamento ?? x.DataVencimento))
                _todosPagamentos.Add(new PagamentoItem(
                    p.Aluno?.Nome ?? "Aluno #" + p.AlunoId, p.NumeroRecibo, Kz(p.Valor),
                    (p.DataPagamento ?? p.DataVencimento).ToString("dd/MM/yyyy"),
                    p.MetodoPagamento ?? "Não informado", p.NumeroRecibo, p.Tipo.ToString(),
                    p.Anulado ? "Anulado" : "Confirmado", p.Id));
            AplicarFiltroPagamentos();

            Entradas.Clear();
            Saidas.Clear();
            var movimentos = await _financeiro.ObterMovimentosAsync(inicio, agora);
            foreach (var m in movimentos.OrderByDescending(x => x.Data))
            {
                var item = new MovimentoItem(m.Descricao, m.Categoria, Kz(m.Valor), m.Data.ToString("dd/MM/yyyy"), m.Id);
                if (m.Tipo == TipoMovimentoCaixa.Entrada) Entradas.Add(item);
                else Saidas.Add(item);
            }

            await AtualizarEstadoCaixaAsync();
            AtualizarIndicadoresDashboard();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }

    private async Task AtualizarEstadoCaixaAsync()
    {
        var sessao = await _caixa.ObterSessaoAtualAsync();
        CaixaAberto = sessao is not null;
        if (sessao is null)
        {
            SaldoInicialLabel = "0,00 Kz";
            SaldoAtualLabel = "0,00 Kz";
            HistoricoCaixa.Clear();
            return;
        }

        SaldoInicialLabel = Kz(sessao.SaldoInicial);
        var movimentos = await _financeiro.ObterMovimentosAsync(sessao.DataAbertura, DateTime.Now);
        var entradas = movimentos.Where(m => m.SessaoCaixaId == sessao.Id && m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
        var saidas = movimentos.Where(m => m.SessaoCaixaId == sessao.Id && m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);
        var pagamentos = (await _financeiro.ObterPagamentosAsync(sessao.DataAbertura, DateTime.Now))
            .Where(p => p.SessaoCaixaId == sessao.Id && !p.Anulado).Sum(p => p.Valor);
        SaldoAtualLabel = Kz(sessao.SaldoInicial + entradas + pagamentos - saidas);

        HistoricoCaixa.Clear();
        var historico = await _caixa.ObterHistoricoAsync(DateTime.Now.AddYears(-2), DateTime.Now);
        foreach (var s in historico.OrderByDescending(x => x.DataAbertura))
        {
            HistoricoCaixa.Add(new SessaoCaixaItem(
                s.DataAbertura.ToString("dd/MM/yyyy HH:mm"),
                s.DataFechamento?.ToString("dd/MM/yyyy HH:mm"),
                Kz(s.SaldoInicial),
                s.SaldoFinal.HasValue ? Kz(s.SaldoFinal.Value) : null,
                s.Estado == EstadoCaixa.Aberta ? "Aberto" : "Fechado",
                s.Id));
        }
    }

    [RelayCommand]
    private async Task ConfirmarAbrirCaixa()
    {
        if (!TryValor(NovoSaldoInicialCaixa, out var saldo) || saldo < 0)
        {
            ErroFinanceiro = "Informe um saldo inicial válido.";
            return;
        }
        try
        {
            ErroFinanceiro = string.Empty;
            var sessao = await _caixa.AbrirCaixaAsync(UtilizadorAtualId(), saldo);
            CaixaAberto = true;
            SaldoInicialLabel = Kz(sessao.SaldoInicial);
            SaldoAtualLabel = Kz(sessao.SaldoInicial);
            FecharModal();
            await CarregarFinanceiroAsync();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }

    [RelayCommand]
    private async Task ConfirmarFecharCaixa()
    {
        try
        {
            ErroFinanceiro = string.Empty;
            await _caixa.FecharCaixaAsync(await UtilizadorAtualIdAsync());
            FecharModal();
            await CarregarFinanceiroAsync();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }

    [RelayCommand]
    private async Task ConfirmarReabrirCaixa()
    {
        try
        {
            ErroFinanceiro = string.Empty;
            await _caixa.ReabrirCaixaAsync(await UtilizadorAtualIdAsync());
            FecharModal();
            await CarregarFinanceiroAsync();
        }
        catch (Exception ex) { ErroFinanceiro = ex.Message; }
    }
}

/// <summary>Linha da aba "Recebimentos" (consulta de pagamentos vindos do módulo Alunos).</summary>
public sealed record PagamentoItem(
    string Aluno,
    string Referencia,
    string Valor,
    string Data,
    string Metodo,
    string NumeroRecibo,
    string TipoCobranca,
    string Estado = "Confirmado",
    int Id = 0)
{
    public bool Anulado => Estado == "Anulado";
}

/// <summary>Linha partilhada pelas abas "Entradas" e "Saídas".</summary>
public sealed class MovimentoItem
{
    public string Descricao { get; }
    public string Categoria { get; }
    public string Valor { get; }
    public string Data { get; }
    public int Id { get; }

    public MovimentoItem(string descricao, string categoria, string valor, string data, int id = 0)
    {
        Descricao = descricao;
        Categoria = categoria;
        Valor = valor;
        Data = data;
        Id = id;
    }
}

/// <summary>Sessão de caixa exibida no histórico da aba "Caixa".</summary>
public sealed record SessaoCaixaItem(string DataAbertura, string? DataFecho, string SaldoInicial, string? SaldoFinal, string Estado, int Id = 0);

} // fim namespace ScoolManager.Desktop.ViewModels.Pages
