using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>
/// View 6 do SM_Flow.md, fluxo completo:
///   Fase 0-1: galeria fixa dos 7 relatórios + estado dos modais.
///   Fase 2:   View (RelatoriosView.axaml).
///   Fase 3:   formulário de filtros (RelatorioFiltro, modal "Configurar Relatório") -
///             cada tipo de relatório mostra só os campos relevantes (ver MostrarXxx).
///   Fase 4:   GerarPreVisualizacao popula o ResultadoXxx do tipo selecionado.
///   Fase 5-7: ExportarPdf / ExportarExcel / Imprimir - por agora placeholders
///             (sem geração de ficheiro real; mensagem no modal de Exportação).
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

    // --- Que campos de filtro / que tabela mostrar, consoante o tipo
    //     selecionado. A View (Configurar + Pré-Visualizar) liga-se a estas. ---
    public bool MostrarMatriculas => RelatorioSelecionado?.Tipo == RelatorioTipo.Matriculas;
    public bool MostrarAlunos => RelatorioSelecionado?.Tipo == RelatorioTipo.ListaAlunos;
    public bool MostrarPropinas => RelatorioSelecionado?.Tipo is RelatorioTipo.PropinasPagas or RelatorioTipo.PropinasAtraso;
    public bool MostrarMovimentos => RelatorioSelecionado?.Tipo is RelatorioTipo.Entradas or RelatorioTipo.Saidas;
    public bool MostrarFluxoCaixa => RelatorioSelecionado?.Tipo == RelatorioTipo.FluxoCaixa;

    // Campos de filtro partilhados por vários tipos.
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

    /// <summary>
    /// Fase 4: usa RelatorioSelecionado.Tipo + FiltroAtual para popular o
    /// ResultadoXxx correspondente. Dados de exemplo (mock), no mesmo espírito
    /// do Design.DataContext do Financeiro/Escola - a Fase 8 substitui isto
    /// por dados reais dos repositórios de Alunos e Financeiro.
    /// </summary>
    [RelayCommand]
    private void GerarPreVisualizacao()
    {
        if (RelatorioSelecionado is null)
            return;

        // Limpa todos os resultados - só um tipo fica populado de cada vez.
        ResultadoMatriculas.Clear();
        ResultadoAlunos.Clear();
        ResultadoPropinas.Clear();
        ResultadoMovimentos.Clear();
        ResultadoFluxoCaixa.Clear();

        var inicio = FiltroAtual.DataInicio?.Date ?? DateTime.Today.AddMonths(-1);
        var fim = FiltroAtual.DataFim?.Date ?? DateTime.Today;

        switch (RelatorioSelecionado.Tipo)
        {
            case RelatorioTipo.Matriculas:
                foreach (var item in GerarMatriculasExemplo(inicio, fim))
                    ResultadoMatriculas.Add(item);
                break;

            case RelatorioTipo.ListaAlunos:
                foreach (var item in GerarAlunosExemplo())
                    ResultadoAlunos.Add(item);
                break;

            case RelatorioTipo.PropinasPagas:
                foreach (var item in GerarPropinasExemplo(inicio, fim, pago: true))
                    ResultadoPropinas.Add(item);
                break;

            case RelatorioTipo.PropinasAtraso:
                foreach (var item in GerarPropinasExemplo(inicio, fim, pago: false))
                    ResultadoPropinas.Add(item);
                break;

            case RelatorioTipo.Entradas:
                foreach (var item in GerarMovimentosExemplo(inicio, fim, entrada: true))
                    ResultadoMovimentos.Add(item);
                break;

            case RelatorioTipo.Saidas:
                foreach (var item in GerarMovimentosExemplo(inicio, fim, entrada: false))
                    ResultadoMovimentos.Add(item);
                break;

            case RelatorioTipo.FluxoCaixa:
                foreach (var item in GerarFluxoCaixaExemplo(inicio, fim))
                    ResultadoFluxoCaixa.Add(item);
                break;
        }

        ModalConfigurarVisivel = false;
        ModalPreVisualizarVisivel = true;
    }

    // Fase 5: exportação real de PDF fica para uma próxima atualização -
    // por agora só confirma a ação no modal de Exportação.
    [RelayCommand]
    private void ExportarPdf()
    {
        MostrarMensagemExportacao("A exportação para PDF ainda não está disponível nesta versão. " +
                                   "Esta funcionalidade chega numa próxima atualização.");
    }

    // Fase 6: idem, para Excel/CSV.
    [RelayCommand]
    private void ExportarExcel()
    {
        MostrarMensagemExportacao("A exportação para Excel ainda não está disponível nesta versão. " +
                                   "Esta funcionalidade chega numa próxima atualização.");
    }

    // Fase 7: idem, para impressão.
    [RelayCommand]
    private void Imprimir()
    {
        MostrarMensagemExportacao("A impressão ainda não está disponível nesta versão. " +
                                   "Esta funcionalidade chega numa próxima atualização.");
    }

    private void MostrarMensagemExportacao(string mensagem)
    {
        MensagemExportacao = mensagem;
        ModalPreVisualizarVisivel = false;
        ModalExportacaoVisivel = true;
    }

    // --- Geradores de dados de exemplo (só para a Fase 4; substituídos na Fase 8) ---
    // Os campos do RelatoriosModels.cs são strings já formatadas para exibição
    // direta na tabela (sem conversores na View), por isso a formatação
    // (datas, "Kz") acontece aqui.

    private static IEnumerable<MatriculaRelatorioItem> GerarMatriculasExemplo(DateTime inicio, DateTime fim)
    {
        (string Aluno, string Classe, string Turma)[] alunos =
        {
            ("Beatriz Manuel", "10ª", "GRSI A"),
            ("Domingos Sanjambo", "10ª", "GRSI B"),
            ("Elsa Puna", "11ª", "GRSI A"),
            ("Fábio Necongo", "10ª", "GRH A"),
        };

        for (var i = 0; i < alunos.Length; i++)
        {
            var (aluno, classe, turma) = alunos[i];
            var data = inicio.AddDays((fim - inicio).TotalDays * i / Math.Max(alunos.Length - 1, 1));
            yield return new MatriculaRelatorioItem
            {
                Aluno = aluno,
                NumeroMatricula = $"MAT-{2600 + i}",
                Classe = classe,
                Turma = turma,
                DataMatricula = data.ToString("dd/MM/yyyy"),
                Estado = "Ativo",
            };
        }
    }

    private static IEnumerable<AlunoRelatorioItem> GerarAlunosExemplo()
    {
        (string Nome, string Classe, string Turma, string Situacao, string Contacto)[] alunos =
        {
            ("Beatriz Manuel", "10ª", "GRSI A", "Ativo", "923 111 222"),
            ("Domingos Sanjambo", "10ª", "GRSI B", "Ativo", "923 222 333"),
            ("Elsa Puna", "11ª", "GRSI A", "Ativo", "923 333 444"),
            ("Fábio Necongo", "10ª", "GRH A", "Transferido", "923 444 555"),
            ("Graça Ndozi", "13ª", "GE A", "Ativo", "923 555 666"),
        };

        for (var i = 0; i < alunos.Length; i++)
        {
            var (nome, classe, turma, situacao, contacto) = alunos[i];
            yield return new AlunoRelatorioItem
            {
                Nome = nome,
                NumeroMatricula = $"MAT-{2500 + i}",
                Classe = classe,
                Turma = turma,
                Situacao = situacao,
                Contacto = contacto,
            };
        }
    }

    private static IEnumerable<PropinaRelatorioItem> GerarPropinasExemplo(DateTime inicio, DateTime fim, bool pago)
    {
        (string Aluno, decimal Valor)[] propinas =
        {
            ("Beatriz Manuel", 25000m),
            ("Domingos Sanjambo", 25000m),
            ("Elsa Puna", 30000m),
        };

        for (var i = 0; i < propinas.Length; i++)
        {
            var (aluno, valor) = propinas[i];
            var vencimento = inicio.AddDays((fim - inicio).TotalDays * i / Math.Max(propinas.Length - 1, 1));
            yield return new PropinaRelatorioItem
            {
                Aluno = aluno,
                Referencia = $"REF-{4100 + i}",
                Valor = $"{valor:N0} Kz",
                DataVencimento = vencimento.ToString("dd/MM/yyyy"),
                DataPagamento = pago ? vencimento.AddDays(1).ToString("dd/MM/yyyy") : string.Empty,
                Estado = pago ? "Pago" : "Em Atraso",
            };
        }
    }

    private static IEnumerable<RelatorioMovimentoItem> GerarMovimentosExemplo(DateTime inicio, DateTime fim, bool entrada)
    {
        var descricoes = entrada
            ? new[] { "Propinas do mês", "Venda de material escolar", "Taxa de matrícula" }
            : new[] { "Salários", "Manutenção", "Material de escritório" };
        var categoria = entrada ? "Receita" : "Despesa";
        var tipo = entrada ? "Entrada" : "Saida";

        for (var i = 0; i < descricoes.Length; i++)
        {
            var data = inicio.AddDays((fim - inicio).TotalDays * i / Math.Max(descricoes.Length - 1, 1));
            var valor = 15000m + i * 5000m;
            yield return new RelatorioMovimentoItem
            {
                Data = data.ToString("dd/MM/yyyy"),
                Descricao = descricoes[i],
                Categoria = categoria,
                Valor = $"{valor:N0} Kz",
                Tipo = tipo,
            };
        }
    }

    private static IEnumerable<FluxoCaixaRelatorioItem> GerarFluxoCaixaExemplo(DateTime inicio, DateTime fim)
    {
        var meses = Math.Max(1, ((fim.Year - inicio.Year) * 12 + fim.Month - inicio.Month) + 1);
        decimal saldo = 0;

        for (var i = 0; i < meses; i++)
        {
            var mes = inicio.AddMonths(i);
            var saldoInicial = saldo;
            var entradas = 80000m + i * 4000m;
            var saidas = 55000m + i * 2500m;
            saldo += entradas - saidas;
            yield return new FluxoCaixaRelatorioItem
            {
                Periodo = mes.ToString("MMMM yyyy"),
                SaldoInicial = $"{saldoInicial:N0} Kz",
                TotalEntradas = $"{entradas:N0} Kz",
                TotalSaidas = $"{saidas:N0} Kz",
                SaldoFinal = $"{saldo:N0} Kz",
            };
        }
    }
}
