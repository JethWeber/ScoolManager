using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Responsável por salvar/remover arquivos físicos da aplicação no disco do
/// utilizador (Documents/ScoolManager/...), de forma idêntica em Windows e Linux
/// (usa Environment.SpecialFolder.MyDocuments, que resolve corretamente nos dois SOs).
/// </summary>
public interface IArmazenamentoArquivosService
{
    /// <summary>
    /// Salva o arquivo em Documents/ScoolManager/Dados/{anoLectivo}/Alunos/{codigoAluno}/
    /// com nome padronizado: {Tipo}_{CodigoAluno}_{PrimeiroNome}_{Sobrenome}.{ext}
    /// Devolve apenas o NOME do arquivo gravado (sem caminho), para persistir em DocumentoAluno.NomeArquivo.
    /// </summary>
    Task<string> SalvarDocumentoAlunoAsync(
        string anoLectivo,
        string codigoAluno,
        string nomeCompletoAluno,
        TipoDocumentoAluno tipo,
        string extensaoOriginal,
        Stream conteudo,
        CancellationToken ct = default);

    /// <summary>Recompõe o caminho completo a partir dos dados já persistidos no banco.</summary>
    string ObterCaminhoDocumentoAluno(string anoLectivo, string codigoAluno, string nomeArquivo);

    /// <summary>Remove o arquivo físico do disco (usado, por exemplo, em rollback de cadastro).</summary>
    void RemoverDocumentoAluno(string anoLectivo, string codigoAluno, string nomeArquivo);

    string ObterPastaBackups();
    string ObterPastaExportacoes();
    string ObterPastaLogs();
}
