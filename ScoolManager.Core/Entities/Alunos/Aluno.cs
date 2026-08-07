using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Entities.Alunos;

/// <summary>
/// Entidade completa do Aluno — consolida <c>AlunoListItemModel</c> (a linha
/// da tabela) com os campos vistos em <c>DetalhesAlunoViewModel</c> (aba
/// "Dados Pessoais" + dados de matrícula).
///
/// Diferenças em relação ao Desktop: <c>Classe</c>/<c>Curso</c>/<c>Sala</c>
/// deixam de ser <c>string</c> — passam a ser resolvidos via
/// <see cref="Turma"/> (uma Turma já carrega Classe/Curso/Sala). Campos
/// puramente visuais (Iniciais, EstadoTexto, brushes de estado) não sobem.
/// </summary>
public class Aluno
{
    public int Id { get; set; }

    /// <summary>Código/identificador do aluno (ex.: "2026/0842").</summary>
    public string Codigo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public DateTime? DataNascimento { get; set; }
    public string? Genero { get; set; }
    public string? Nacionalidade { get; set; }
    public string? NumeroBiCedula { get; set; }
    public string? Endereco { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? FotografiaCaminho { get; set; }

    public bool Ativo { get; set; } = true;

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public DateTime? DataMatricula { get; set; }
    public int? AnoLectivoId { get; set; }
    public AnoLectivo? AnoLectivo { get; set; }

    public List<Encarregado> Encarregados { get; set; } = new();
    public List<DocumentoAluno> Documentos { get; set; } = new();
}
