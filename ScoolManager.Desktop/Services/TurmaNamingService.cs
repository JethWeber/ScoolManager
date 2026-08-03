using System;
using System.Collections.Generic;
using System.Linq;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.Services;

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
/// - Toda Turma tem obrigatoriamente uma Sala e um Ano Lectivo associados.
/// </summary>
public static class TurmaNamingService
{
    private const char PrimeiraLetra = 'A';
    private const char UltimaLetra = 'Z';

    private static bool MesmaCombinacao(TurmaModel t, AnoLectivoModel anoLectivo, ClasseModel classe, CursoModel? curso) =>
        t.AnoLectivo.Id == anoLectivo.Id && t.Classe.Id == classe.Id && t.Curso?.Id == curso?.Id;

    /// <summary>
    /// Determina a próxima letra livre para a combinação Ano Lectivo+Classe+Curso,
    /// respeitando sempre a ordem alfabética.
    /// </summary>
    public static char ProximaLetraDisponivel(IEnumerable<TurmaModel> turmas, AnoLectivoModel anoLectivo, ClasseModel classe, CursoModel? curso)
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

        throw new InvalidOperationException(
            $"Não é possível criar mais turmas para {classe.Nome}: " +
            $"limite de {UltimaLetra - PrimeiraLetra + 1} turmas (A-Z) atingido.");
    }

    /// <summary>
    /// Indica se já é permitido abrir uma nova turma para esta combinação: verdadeiro
    /// se ainda não existir nenhuma (a primeira, "A", pode sempre ser criada) ou se a
    /// última turma (por ordem alfabética) já estiver cheia.
    /// </summary>
    public static bool PodeAbrirNovaTurma(IEnumerable<TurmaModel> turmas, AnoLectivoModel anoLectivo, ClasseModel classe, CursoModel? curso)
    {
        var daCombinacao = turmas
            .Where(t => MesmaCombinacao(t, anoLectivo, classe, curso))
            .OrderBy(t => t.Letra)
            .ToList();

        return daCombinacao.Count == 0 || daCombinacao[^1].EstaCheia;
    }

    /// <summary>
    /// Mensagem explicativa para quando ainda não é permitido abrir uma nova
    /// turma (mostrada na UI se o utilizador tentar antes de tempo).
    /// </summary>
    public static string MotivoBloqueio(IEnumerable<TurmaModel> turmas, AnoLectivoModel anoLectivo, ClasseModel classe, CursoModel? curso)
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
}
