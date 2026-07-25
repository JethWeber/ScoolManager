using System;
using System.Collections.Generic;
using System.Linq;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.Services;

/// <summary>
/// Regras de nomeação e abertura de Turmas para o módulo Escola:
///
/// - Uma Turma nasce sempre da combinação Classe + Curso (ex.: "10ª Classe" +
///   "Informática" = "10ª Classe de Informática").
/// - As Turmas de uma mesma combinação Classe+Curso são identificadas por
///   letras em ordem alfabética (A, B, C, ...): a primeira turma criada é
///   sempre "A".
/// - Só é permitido abrir uma nova turma (ex.: "B") quando a turma anterior
///   ("A") já estiver cheia (matriculados == capacidade máxima). Antes disso,
///   os novos alunos devem entrar na turma existente.
/// - Toda Turma tem obrigatoriamente uma Sala associada.
/// </summary>
public static class TurmaNamingService
{
    private const char PrimeiraLetra = 'A';
    private const char UltimaLetra = 'Z';

    /// <summary>
    /// Determina a próxima letra livre para a combinação Classe+Curso,
    /// respeitando sempre a ordem alfabética.
    /// </summary>
    public static char ProximaLetraDisponivel(IEnumerable<TurmaModel> turmas, ClasseModel classe, CursoModel curso)
    {
        var emUso = turmas
            .Where(t => t.Classe.Id == classe.Id && t.Curso.Id == curso.Id)
            .Select(t => t.Letra)
            .ToHashSet();

        for (var letra = PrimeiraLetra; letra <= UltimaLetra; letra++)
        {
            if (!emUso.Contains(letra))
                return letra;
        }

        throw new InvalidOperationException(
            $"Não é possível criar mais turmas para {classe.Nome} - {curso.Nome}: " +
            $"limite de {UltimaLetra - PrimeiraLetra + 1} turmas (A-Z) atingido.");
    }

    /// <summary>
    /// Indica se já é permitido abrir uma nova turma para esta combinação
    /// Classe+Curso: verdadeiro se ainda não existir nenhuma (a primeira, "A",
    /// pode sempre ser criada) ou se a última turma (por ordem alfabética) já
    /// estiver cheia.
    /// </summary>
    public static bool PodeAbrirNovaTurma(IEnumerable<TurmaModel> turmas, ClasseModel classe, CursoModel curso)
    {
        var daCombinacao = turmas
            .Where(t => t.Classe.Id == classe.Id && t.Curso.Id == curso.Id)
            .OrderBy(t => t.Letra)
            .ToList();

        return daCombinacao.Count == 0 || daCombinacao[^1].EstaCheia;
    }

    /// <summary>
    /// Mensagem explicativa para quando ainda não é permitido abrir uma nova
    /// turma (mostrada na UI se o utilizador tentar antes de tempo).
    /// </summary>
    public static string MotivoBloqueio(IEnumerable<TurmaModel> turmas, ClasseModel classe, CursoModel curso)
    {
        var ultima = turmas
            .Where(t => t.Classe.Id == classe.Id && t.Curso.Id == curso.Id)
            .OrderByDescending(t => t.Letra)
            .FirstOrDefault();

        if (ultima is null)
            return string.Empty;

        var vagas = ultima.CapacidadeMaxima - ultima.AlunosMatriculados;
        var proximaLetra = (char)(ultima.Letra + 1);

        return $"A turma {ultima.Codigo} ainda tem {vagas} vaga(s) disponível(eis). " +
               $"Só é possível abrir a turma \"{proximaLetra}\" quando a \"{ultima.Letra}\" estiver cheia.";
    }
}
