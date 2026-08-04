using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;

namespace ScoolManager.Desktop.ViewModels.Pages;

// ============================================================================
// Modelos de apoio à View 6 - Relatórios (ver SM_Flow.md). Vivem ao lado da
// RelatoriosViewModel, no mesmo espírito de AbaEscolaItem/TurnoOpcao
// (EscolaViewModel) e PagamentoItem/MovimentoItem (FinanceiroViewModel):
// são modelos "de UI", não entidades de domínio/BD.
// ============================================================================

/// <summary>
/// Os 7 relatórios previstos no SM_Flow.md (View 6 > Relatórios).
/// </summary>
public enum RelatorioTipo
{
    Matriculas,
    ListaAlunos,
    PropinasPagas,
    PropinasAtraso,
    Entradas,
    Saidas,
    FluxoCaixa
}

/// <summary>
/// Um cartão da galeria principal da view (Fase 2). Dados estáticos,
/// por isso não precisa de ser ObservableObject.
/// </summary>
public class RelatorioTipoItem
{
    public RelatorioTipo Tipo { get; }
    public string Titulo { get; }
    public string Descricao { get; }
    public MaterialIconKind Icon { get; }

    public RelatorioTipoItem(RelatorioTipo tipo, string titulo, string descricao, MaterialIconKind icon)
    {
        Tipo = tipo;
        Titulo = titulo;
        Descricao = descricao;
        Icon = icon;
    }
}

/// <summary>
/// Filtros do modal "Configurar Relatório" (Fase 3). Um único objeto
/// partilhado entre todos os tipos de relatório - cada formulário de
/// configuração mostra apenas os campos relevantes para o RelatorioTipo
/// selecionado (ex.: "Fluxo de Caixa" só precisa de Periodo/DataInicio/DataFim).
/// </summary>
public partial class RelatorioFiltro : ObservableObject
{
    [ObservableProperty] private string? _periodo;
    [ObservableProperty] private DateTimeOffset? _dataInicio;
    [ObservableProperty] private DateTimeOffset? _dataFim;
    [ObservableProperty] private string? _anoLectivo;
    [ObservableProperty] private string? _turma;
    [ObservableProperty] private string? _classe;
    [ObservableProperty] private string? _metodoPagamento;

    /// <summary>Repõe os filtros sempre que um novo relatório é aberto na galeria.</summary>
    public void Limpar()
    {
        Periodo = null;
        DataInicio = null;
        DataFim = null;
        AnoLectivo = null;
        Turma = null;
        Classe = null;
        MetodoPagamento = null;
    }
}

// ---- Linhas de resultado da Pré-Visualização (populadas na Fase 4) -------

public class MatriculaRelatorioItem
{
    public string Aluno { get; set; } = string.Empty;
    public string NumeroMatricula { get; set; } = string.Empty;
    public string Turma { get; set; } = string.Empty;
    public string Classe { get; set; } = string.Empty;
    public string DataMatricula { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
}

public class AlunoRelatorioItem
{
    public string Nome { get; set; } = string.Empty;
    public string NumeroMatricula { get; set; } = string.Empty;
    public string Classe { get; set; } = string.Empty;
    public string Turma { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
    public string Contacto { get; set; } = string.Empty;
}

/// <summary>
/// Usado tanto por "Propinas Pagas" como por "Propinas em Atraso" - o campo
/// Estado (e o preenchimento de DataPagamento) é que distingue os dois.
/// </summary>
public class PropinaRelatorioItem
{
    public string Aluno { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
    public string DataVencimento { get; set; } = string.Empty;
    public string DataPagamento { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty; // "Pago" ou "Em Atraso"
}

/// <summary>
/// Usado tanto por "Entradas" como por "Saídas" - o campo Tipo distingue
/// os dois relatórios, tal como o MovimentoItem faz no FinanceiroViewModel.
/// </summary>
public class RelatorioMovimentoItem
{
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // "Entrada" ou "Saida"
}

public class FluxoCaixaRelatorioItem
{
    public string Periodo { get; set; } = string.Empty;
    public string SaldoInicial { get; set; } = string.Empty;
    public string TotalEntradas { get; set; } = string.Empty;
    public string TotalSaidas { get; set; } = string.Empty;
    public string SaldoFinal { get; set; } = string.Empty;
}
