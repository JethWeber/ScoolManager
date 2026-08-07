namespace ScoolManager.Core.Enums;

/// <summary>
/// Tipo de um <c>DocumentoAluno</c> (aba "Documentação" da View 3 —
/// Detalhes do Aluno, ver SM_Flow.md).
///
/// Novo enum, extraído dos 4 tipos de documento fixos hoje criados em
/// <c>DetalhesAlunoViewModel.PreencherDadosMock</c>
/// ("Certificado / Declaração", "Foto Tipo Passe", "BI / Cédula",
/// "Atestado Médico") — lá esses tipos são texto livre; aqui passam a
/// enum fechado.
/// </summary>
public enum TipoDocumentoAluno
{
    Certificado,
    FotoTipoPasse,
    BiCedula,
    AtestadoMedico
}
