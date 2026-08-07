using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Alunos;

/// <summary>
/// Documento anexado ao processo de um Aluno (aba "Documentação" da View 3
/// — Detalhes do Aluno, ver SM_Flow.md).
///
/// Migrado de <c>DocumentoAlunoItem</c> (classe interna de
/// <c>DetalhesAlunoViewModel</c>). <c>TemArquivo</c>/<c>DataUploadLabel</c>
/// (propriedades de apresentação) não sobem — ficam na UI.
/// </summary>
public class DocumentoAluno
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    public TipoDocumentoAluno Tipo { get; set; }
    public string? NomeArquivo { get; set; }
    public DateTime? DataUpload { get; set; }
}
