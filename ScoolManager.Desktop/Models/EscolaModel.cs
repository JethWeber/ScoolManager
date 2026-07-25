using System;

namespace ScoolManager.Desktop.Models;

// =====================================================================
// Enums
// =====================================================================

/// <summary>
/// Nível de ensino ao qual uma <see cref="ClasseModel"/> pertence, conforme o
/// sistema educativo angolano: Ensino Primário (1ª-6ª classe), Ensino
/// Secundário / I Ciclo (7ª-9ª classe) e Ensino Médio / II Ciclo (10ª-13ª
/// classe, onde surgem os Cursos/áreas de formação).
/// </summary>
public enum NivelEnsino
{
    Primario,
    Secundario,
    Medio
}

public static class NivelEnsinoExtensions
{
    public static string ParaLabel(this NivelEnsino nivel) => nivel switch
    {
        NivelEnsino.Primario => "Ensino Primário",
        NivelEnsino.Secundario => "Ensino Secundário",
        NivelEnsino.Medio => "Ensino Médio",
        _ => nivel.ToString()
    };
}

/// <summary>Período em que uma Turma funciona.</summary>
public enum PeriodoLetivo
{
    Manha,
    Tarde,
    Noite
}

public static class PeriodoLetivoExtensions
{
    public static string ParaLabel(this PeriodoLetivo periodo) => periodo switch
    {
        PeriodoLetivo.Manha => "Manhã",
        PeriodoLetivo.Tarde => "Tarde",
        PeriodoLetivo.Noite => "Noite",
        _ => periodo.ToString()
    };

    /// <summary>
    /// Conversão inversa (label -> enum), usada porque os ComboBox dos
    /// formulários trabalham com o label em texto (mais simples de vincular
    /// em bindings compilados do que o enum diretamente).
    /// </summary>
    public static PeriodoLetivo? DeLabel(string? label) => label switch
    {
        "Manhã" => PeriodoLetivo.Manha,
        "Tarde" => PeriodoLetivo.Tarde,
        "Noite" => PeriodoLetivo.Noite,
        _ => null
    };
}

// =====================================================================
// Entidades de referência (Curso, Classe, Sala, Ano Lectivo)
// =====================================================================

/// <summary>
/// Curso / área de formação (ex.: Informática, Ciências Físicas e Biológicas).
/// No Ensino Médio, cada Classe pode ter vários Cursos em paralelo (é isso que
/// faz nascer turmas diferentes, ex.: "10ª Classe de Informática" e "10ª
/// Classe de Ciências"). No Primário e no Secundário usa-se um curso genérico
/// ("Formação Geral") porque não há especialização por área nesses níveis.
/// </summary>
public class CursoModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    /// <summary>Nível de ensino a que este curso pertence.</summary>
    public NivelEnsino Nivel { get; set; }
}

/// <summary>
/// Uma Classe representa o ano/grau de escolaridade (7ª, 10ª, 12ª, ...).
/// A Classe, por si só, não define o Curso: é a combinação Classe + Curso que
/// dá origem às Turmas (ex.: "10ª Classe" + "Informática" = "10ª Classe de
/// Informática", que pode ter turmas A, B, C...).
/// </summary>
public class ClasseModel
{
    public int Id { get; set; }

    /// <summary>Número da classe (7, 8, 9, 10, 11, 12, 13, ...).</summary>
    public int Numero { get; set; }

    public NivelEnsino Nivel { get; set; }

    /// <summary>Texto curto exibido como subtítulo (ex.: "Ensino Médio Regular").</summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>Nome de apresentação (ex.: "10ª Classe").</summary>
    public string Nome => $"{Numero}ª Classe";
}

/// <summary>Sala/espaço físico onde uma Turma funciona.</summary>
public class SalaModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }
}

/// <summary>Ano lectivo (ex.: "2025/2026").</summary>
public class AnoLectivoModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}

// =====================================================================
// Turma
// =====================================================================

/// <summary>
/// Uma Turma é a combinação concreta de Classe + Curso + Letra (ex.: "10ª A -
/// Informática"), sempre associada a uma Sala. A Letra segue ordem alfabética
/// dentro da mesma combinação Classe+Curso: a turma "B" só nasce quando a "A"
/// enche. Ver <see cref="Services.TurmaNamingService"/> para a regra completa.
/// </summary>
public class TurmaModel
{
    public int Id { get; set; }
    public required ClasseModel Classe { get; set; }
    public required CursoModel Curso { get; set; }

    /// <summary>Letra da turma dentro da combinação Classe+Curso (A, B, C, ...).</summary>
    public char Letra { get; set; }

    public required SalaModel Sala { get; set; }
    public PeriodoLetivo Periodo { get; set; }
    public int CapacidadeMaxima { get; set; }
    public int AlunosMatriculados { get; set; }

    /// <summary>Código curto usado na listagem (ex.: "T10-A").</summary>
    public string Codigo => $"T{Classe.Numero}-{Letra}";

    /// <summary>Nome completo (ex.: "10ª Classe A - Informática").</summary>
    public string NomeCompleto => $"{Classe.Nome} {Letra} - {Curso.Nome}";

    public double OcupacaoPercentual =>
        CapacidadeMaxima <= 0 ? 0 : Math.Clamp(AlunosMatriculados / (double)CapacidadeMaxima * 100, 0, 100);

    /// <summary>Verdadeiro quando a turma atingiu a capacidade máxima (gatilho para abrir a próxima letra).</summary>
    public bool EstaCheia => CapacidadeMaxima > 0 && AlunosMatriculados >= CapacidadeMaxima;

    public string PeriodoLabel => Periodo.ParaLabel();
}

// =====================================================================
// DTOs de apresentação
// =====================================================================

/// <summary>
/// DTO de apresentação para o ecrã "Escola &gt; Classes": agrega todas as
/// Turmas de uma combinação Classe + Curso (matriculados, capacidade,
/// ocupação), para mostrar um cartão por combinação, agrupado por Nível e
/// depois por Curso.
/// </summary>
public class ClasseCardModel
{
    public required ClasseModel Classe { get; init; }
    public required CursoModel Curso { get; init; }
    public int Matriculados { get; init; }
    public int Capacidade { get; init; }
    public int NumeroDeTurmas { get; init; }

    public double OcupacaoPercentual =>
        Capacidade <= 0 ? 0 : Math.Clamp(Matriculados / (double)Capacidade * 100, 0, 100);

    /// <summary>Ocupação alta (perto do limite) - realçado na UI para sinalizar que uma nova turma pode ser necessária em breve.</summary>
    public bool OcupacaoAlta => OcupacaoPercentual >= 90;

    public string OcupacaoLabel => $"{OcupacaoPercentual:0}% Ocupado";

    public string MatriculadosCapacidadeLabel => $"{Matriculados}/{Capacidade}";

    public string NumeroDeTurmasLabel => NumeroDeTurmas == 1 ? "1 turma" : $"{NumeroDeTurmas} turmas";
}
