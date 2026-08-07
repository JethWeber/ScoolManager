using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Escola;

/// <summary>
/// Regras de nomeação e abertura de Turmas para o módulo Escola:
///
/// - Uma Turma nasce da combinação Ano Lectivo + Classe + Curso (quando
///   aplicável) - ex.: "10ª Classe" + "GRSI" em "2025/2026" = "10ª GRSI"; no
///   Primário/Secundário não há Curso.
/// - As Turmas de uma mesma combinação Ano Lectivo+Classe+Curso são
///   identificadas por letras em ordem alfabética (A, B, C, ...): a primeira
///   turma criada é sempre "A", e a numeração reinicia a cada Ano Lectivo.
/// - Só é permitido abrir uma nova turma (ex.: "B") quando a turma anterior
///   ("A") já estiver cheia (matriculados == capacidade). Antes disso, os
///   novos alunos devem entrar na turma existente.
///
/// Migrado quase literalmente de
/// <c>ScoolManager.Desktop.Services.TurmaNamingService</c> — a única
/// mudança de comportamento é que os antigos <c>InvalidOperationException</c>
/// genéricos passam a exceções de domínio específicas (Fase 4).
/// </summary>
public static class TurmaNamingService
{
    private const char PrimeiraLetra = 'A';
    private const char UltimaLetra = 'Z';

    private static bool MesmaCombinacao(Turma t, AnoLectivo anoLectivo, Classe classe, Curso? curso) =>
        t.AnoLectivoId == anoLectivo.Id && t.ClasseId == classe.Id && t.CursoId == curso?.Id;

    /// <summary>
    /// Determina a próxima letra livre para a combinação Ano Lectivo+Classe+Curso,
    /// respeitando sempre a ordem alfabética.
    /// </summary>
    /// <exception cref="LimiteDeLetrasAtingidoException">Quando A-Z já estão todas em uso.</exception>
    public static char ProximaLetraDisponivel(IEnumerable<Turma> turmas, AnoLectivo anoLectivo, Classe classe, Curso? curso)
    {
        var emUso = turmas
            .Where(t => MesmaCombinacao(t, anoLectivo, classe, curso))
            .Select(t => t.Letra)
            .ToHashSet();

        for (var letra = PrimeiraLetra; letra <= UltimaLetra; letra++)
        {
            if (!emUso.Contains(letra))
                return letra;
        }

        throw new LimiteDeLetrasAtingidoException(classe.Numero, UltimaLetra - PrimeiraLetra + 1);
    }

    /// <summary>
    /// Indica se já é permitido abrir uma nova turma para esta combinação: verdadeiro
    /// se ainda não existir nenhuma (a primeira, "A", pode sempre ser criada) ou se a
    /// última turma (por ordem alfabética) já estiver cheia.
    /// </summary>
    public static bool PodeAbrirNovaTurma(IEnumerable<Turma> turmas, AnoLectivo anoLectivo, Classe classe, Curso? curso)
    {
        var daCombinacao = turmas
            .Where(t => MesmaCombinacao(t, anoLectivo, classe, curso))
            .OrderBy(t => t.Letra)
            .ToList();

        return daCombinacao.Count == 0 || daCombinacao[^1].EstaCheia;
    }

    /// <summary>
    /// Mensagem explicativa para quando ainda não é permitido abrir uma nova
    /// turma. Devolve string vazia se ainda não existir nenhuma turma para a
    /// combinação (nesse caso a "A" pode sempre ser criada, não há bloqueio a explicar).
    /// </summary>
    public static string MotivoBloqueio(IEnumerable<Turma> turmas, AnoLectivo anoLectivo, Classe classe, Curso? curso)
    {
        var ultima = turmas
            .Where(t => MesmaCombinacao(t, anoLectivo, classe, curso))
            .OrderByDescending(t => t.Letra)
            .FirstOrDefault();

        if (ultima is null)
            return string.Empty;

        var vagas = ultima.Capacidade - ultima.Matriculados;
        var proximaLetra = (char)(ultima.Letra + 1);

        return $"A turma {ultima.Nome} ainda tem {vagas} vaga(s) disponível(eis). " +
               $"Só é possível abrir a turma \"{proximaLetra}\" quando a \"{ultima.Letra}\" estiver cheia.";
    }

    /// <summary>
    /// Novo (Fase 6): valida a abertura e já lança a exceção pronta, com o
    /// motivo embutido, para <c>EscolaService.CriarTurmaAsync</c> chamar
    /// diretamente sem ter de repetir a lógica de "if não pode, lança".
    /// </summary>
    /// <exception cref="AberturaDeTurmaNaoPermitidaException">Quando ainda não é permitido abrir a próxima letra.</exception>
    public static void ValidarAberturaOuLancar(IEnumerable<Turma> turmas, AnoLectivo anoLectivo, Classe classe, Curso? curso)
    {
        var turmasLista = turmas as ICollection<Turma> ?? turmas.ToList();

        if (!PodeAbrirNovaTurma(turmasLista, anoLectivo, classe, curso))
            throw new AberturaDeTurmaNaoPermitidaException(MotivoBloqueio(turmasLista, anoLectivo, classe, curso));
    }
}
