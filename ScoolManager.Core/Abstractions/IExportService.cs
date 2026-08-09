namespace ScoolManager.Core.Abstractions;

/// <summary>
/// Exportação transversal usada por qualquer módulo que precise de gerar um
/// ficheiro tabular (Alunos: "Exportar PDF"/"Exportar Excel"; Relatórios:
/// idem; Financeiro: "Ver Recibo"/"Imprimir Recibo" — ver SM_Flow.md).
///
/// CORREÇÃO (gap 4): não existia nada disto no Core. Fica como uma única
/// interface transversal em vez de duplicada em cada módulo, porque a
/// operação em si (linhas + colunas → ficheiro) não depende de domínio.
///
/// DECISÃO A CONFIRMAR: <see cref="ExportarParaCsvAsync"/> é um "Excel"
/// interino — um .csv abre normalmente no Excel, mas não é um .xlsx real
/// (sem formatação, sem múltiplas folhas). Gerar .xlsx de verdade exige uma
/// biblioteca própria (ex.: ClosedXML) que ainda não está instalada — só
/// o `QuestPDF` (para PDF) está no .csproj até agora. Troca-se
/// <see cref="ExportarParaCsvAsync"/> por uma implementação com ClosedXML
/// no dia em que precisares de .xlsx real, sem mudar a interface.
/// </summary>
public interface IExportService
{
    /// <summary>Gera um PDF simples: título + tabela (cabeçalho + linhas) + numeração de página.</summary>
    byte[] ExportarParaPdf(string titulo, IReadOnlyList<string> colunas, IReadOnlyList<string[]> linhas);

    /// <summary>Ver nota na documentação da interface — CSV como "Excel" interino.</summary>
    byte[] ExportarParaCsv(IReadOnlyList<string> colunas, IReadOnlyList<string[]> linhas);
}
