using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Abstractions.Services;

public interface IArmazenamentoArquivosService
{
    /// <summary>
    /// Salva o arquivo em Documents/ScoolManager/Dados/{anoLectivo}/Alunos/{codigoAluno}/
    /// com nome padronizado: {Tipo}_{CodigoAluno}_{PrimeiroNome}_{Sobrenome}.{ext}
    /// Devolve apenas o NOME do arquivo gravado, para persistir em DocumentoAluno.NomeArquivo.
    /// </summary>
    Task<string> SalvarDocumentoAlunoAsync(
        string anoLectivo,
        string codigoAluno,
        string nomeCompletoAluno,
        TipoDocumentoAluno tipo,
        string extensaoOriginal,   // ex: ".png", ".pdf" — só a extensão importa
        Stream conteudo,
        CancellationToken ct = default);

    string ObterCaminhoDocumentoAluno(string anoLectivo, string codigoAluno, string nomeArquivo);
    void RemoverDocumentoAluno(string anoLectivo, string codigoAluno, string nomeArquivo);

    string ObterPastaBackups();
    string ObterPastaExportacoes();
    string ObterPastaLogs();
}