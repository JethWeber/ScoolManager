using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Escola;

/// <summary>
/// Uma Turma é a combinação concreta de Ano Lectivo + Classe + Curso
/// (quando aplicável) + Letra (ex.: "10ª GRSI A"), sempre associada a uma
/// Sala e a um Turno. A Letra segue ordem alfabética dentro da mesma
/// combinação Classe+Curso: a turma "B" só nasce quando a "A" enche. Ver
/// <c>Services.Escola.TurmaNamingService</c> para a regra completa.
///
/// Migrado de <c>ScoolManager.Desktop.Models.TurmaModel</c>. As referências
/// a Ano Lectivo/Classe/Curso/Sala, que eram objeto direto no Desktop
/// (ObservableCollection partilhada), passam a FK + navegação (para o EF
/// Core), mas mantêm o mesmo comportamento de leitura.
///
/// DECISÃO TOMADA (ver roteiro, Secção 2): <see cref="Nome"/> continua a
/// devolver a string já composta ("10ª GRSI A"), em vez de só as partes
/// soltas, para não alterar comportamento visual sem necessidade.
/// </summary>
public class Turma
{
    public int Id { get; set; }

    public int AnoLectivoId { get; set; }
    public AnoLectivo? AnoLectivo { get; set; }

    public int ClasseId { get; set; }
    public Classe? Classe { get; set; }

    /// <summary>Curso da turma; null quando não aplicável (Primário/Secundário).</summary>
    public int? CursoId { get; set; }
    public Curso? Curso { get; set; }

    /// <summary>Letra da turma dentro da combinação Classe+Curso (A, B, C, ...).</summary>
    public char Letra { get; set; }

    public int SalaId { get; set; }
    public Sala? Sala { get; set; }

    public TurnoLetivo Turno { get; set; }
    public int Capacidade { get; set; }
    public int Matriculados { get; set; }

    /// <summary>Nome gerado automaticamente (ex.: "10ª GRSI A" ou, sem curso, "7ª A").</summary>
    public string Nome => Curso is null || string.IsNullOrWhiteSpace(Curso.Sigla)
        ? $"{Classe?.Numero}ª {Letra}"
        : $"{Classe?.Numero}ª {Curso.Sigla} {Letra}";

    public double OcupacaoPercentual =>
        Capacidade <= 0 ? 0 : Math.Clamp(Matriculados / (double)Capacidade * 100, 0, 100);

    /// <summary>Verdadeiro quando a turma atingiu a capacidade máxima (gatilho para abrir a próxima letra).</summary>
    public bool EstaCheia => Capacidade > 0 && Matriculados >= Capacidade;
}
