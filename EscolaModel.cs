using System;
using System.Globalization;

namespace ScoolManager.Desktop.Models;

// =====================================================================
// Enums
// =====================================================================

/// <summary>
/// Nível de ensino de uma <see cref="ClasseModel"/>, conforme o sistema
/// educativo angolano: Ensino Primário (1ª-6ª classe), Ensino Secundário / I
/// Ciclo (7ª-9ª classe) e Ensino Médio / II Ciclo (10ª-13ª classe, onde surge
/// o Curso). A Classe é um catálogo interno do sistema (ver
/// <see cref="ClasseModel"/>) - não tem CRUD próprio.
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

/// <summary>Turno em que uma Turma funciona.</summary>
public enum TurnoLetivo
{
    Manha,
    Tarde,
    Noite
}

public static class TurnoLetivoExtensions
{
    public static string ParaLabel(this TurnoLetivo turno) => turno switch
    {
        TurnoLetivo.Manha => "Manhã",
        TurnoLetivo.Tarde => "Tarde",
        TurnoLetivo.Noite => "Noite",
        _ => turno.ToString()
    };

    /// <summary>
    /// Conversão inversa (label -> enum), usada porque os ComboBox dos
    /// formulários trabalham com o label em texto.
    /// </summary>
    public static TurnoLetivo? DeLabel(string? label) => label switch
    {
        "Manhã" => TurnoLetivo.Manha,
        "Tarde" => TurnoLetivo.Tarde,
        "Noite" => TurnoLetivo.Noite,
        _ => null
    };
}

/// <summary>Estado de um Ano Lectivo.</summary>
public enum EstadoAnoLectivo
{
    Aberto,
    Encerrado
}

public static class EstadoAnoLectivoExtensions
{
    public static string ParaLabel(this EstadoAnoLectivo estado) => estado switch
    {
        EstadoAnoLectivo.Aberto => "Aberto",
        EstadoAnoLectivo.Encerrado => "Encerrado",
        _ => estado.ToString()
    };
}

/// <summary>
/// Categoria de um <see cref="ServicoEscolarModel"/> - espelha
/// <c>ScoolManager.Core.Enums.CategoriaServico</c> (mesma convenção já usada
/// aqui para TurnoLetivo/NivelEnsino/EstadoAnoLectivo: enum próprio no
/// Desktop, convertido por cast no mapeamento Core -> Desktop em
/// EscolaViewModel). Só agrupa/organiza a UI - quem determina o preço é
/// sempre <see cref="ServicoEscolarModel.Preco"/>, nunca a categoria.
/// </summary>
public enum CategoriaServico
{
    Propina,
    Cartao,
    Prova,
    Uniforme,
    Outro
}

public static class CategoriaServicoExtensions
{
    public static string ParaLabel(this CategoriaServico categoria) => categoria switch
    {
        CategoriaServico.Propina => "Propina",
        CategoriaServico.Cartao => "Cartão",
        CategoriaServico.Prova => "Prova",
        CategoriaServico.Uniforme => "Uniforme",
        CategoriaServico.Outro => "Outro",
        _ => categoria.ToString()
    };
}

// =====================================================================
// Catálogo interno (Classe) - NÃO tem CRUD, é só usado como seleção
// dentro do modal "Nova Turma"/"Editar Turma".
// =====================================================================

/// <summary>
/// Uma Classe representa o ano/grau de escolaridade (1ª à 13ª). É um
/// catálogo interno fornecido pelo sistema (não é cadastrado pelo
/// utilizador): existe apenas para ser escolhido ao criar uma Turma.
/// </summary>
public class ClasseModel
{
    public int Id { get; set; }

    /// <summary>Número da classe (1, 2, ..., 13).</summary>
    public int Numero { get; set; }

    public NivelEnsino Nivel { get; set; }

    /// <summary>Nome de apresentação (ex.: "10ª Classe").</summary>
    public string Nome => $"{Numero}ª Classe";
}

// =====================================================================
// Entidades com CRUD próprio (Curso, Sala, Ano Lectivo, Serviço)
// =====================================================================

/// <summary>
/// Curso oferecido pela instituição (ex.: "Gestão de Redes e Sistemas
/// Informáticos" / sigla "GRSI"). Só se aplica a Turmas do Ensino Médio; nas
/// Turmas de Primário/Secundário o Curso fica por preencher (null).
/// </summary>
public class CursoModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
}

/// <summary>Sala/espaço físico onde uma Turma funciona.</summary>
public class SalaModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }

    /// <summary>Bloco/pavilhão onde a sala se encontra (opcional).</summary>
    public string? Bloco { get; set; }

    /// <summary>Observações livres sobre a sala (opcional).</summary>
    public string? Observacoes { get; set; }
}

/// <summary>Ano lectivo (ex.: "2025/2026"), com datas e estado Aberto/Encerrado.</summary>
public class AnoLectivoModel
{
    public int Id { get; set; }

    /// <summary>Designação do ano lectivo (ex.: "2025/2026").</summary>
    public string Nome { get; set; } = string.Empty;

    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
    public EstadoAnoLectivo Estado { get; set; }

    public string EstadoLabel => Estado.ParaLabel();

    /// <summary>Verdadeiro enquanto o ano lectivo estiver Aberto (editável e ainda por encerrar).</summary>
    public bool EstaAberto => Estado == EstadoAnoLectivo.Aberto;
}

/// <summary>
/// Serviço/Produto que a escola cobra ou vende aos alunos na secção de
/// Pagamentos (ex.: "Propina Mensal", "Cartão de Estudante - 1ª Via").
/// Cada linha tem o seu próprio preço; <see cref="Ativo"/> = falso significa
/// "desativado" (deixou de aparecer como opção nova, mas mantém-se no
/// histórico) - ver comentário completo em
/// <c>ScoolManager.Core.Entities.Escola.ServicoEscolar</c>.
/// </summary>
public class ServicoEscolarModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public CategoriaServico Categoria { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Turma a que esta propina se aplica (só preenchido quando
    /// Categoria=Propina - ver comentário completo em
    /// ScoolManager.Core.Entities.Escola.ServicoEscolar.TurmaId).
    /// </summary>
    public int? TurmaId { get; set; }

    /// <summary>Nome já composto da turma (ex.: "10ª GRSI A"), para exibir sem precisar de outra consulta.</summary>
    public string? TurmaNome { get; set; }

    public string CategoriaLabel => Categoria.ParaLabel();
    public string PrecoLabel => Preco.ToString("N2", CultureInfo.GetCultureInfo("pt-PT")) + " Kz";
    public string EstadoLabel => Ativo ? "Ativo" : "Desativado";
}

// =====================================================================
// Turma - entidade principal da aba "Turmas"
// =====================================================================

/// <summary>
/// Uma Turma é a combinação concreta de Ano Lectivo + Classe + Curso
/// (quando aplicável) + Letra (ex.: "10ª GRSI A"), sempre associada a uma
/// Sala e a um Turno. A Letra segue ordem alfabética dentro da mesma
/// combinação Classe+Curso: a turma "B" só nasce quando a "A" enche. Ver
/// <see cref="Services.TurmaNamingService"/> para a regra completa.
/// </summary>
public class TurmaModel
{
    public int Id { get; set; }
    public required AnoLectivoModel AnoLectivo { get; set; }
    public required ClasseModel Classe { get; set; }

    /// <summary>Curso da turma; null quando não aplicável (Primário/Secundário).</summary>
    public CursoModel? Curso { get; set; }

    /// <summary>Letra da turma dentro da combinação Classe+Curso (A, B, C, ...).</summary>
    public char Letra { get; set; }

    public required SalaModel Sala { get; set; }
    public TurnoLetivo Turno { get; set; }
    public int Capacidade { get; set; }
    public int Matriculados { get; set; }

    /// <summary>Nome gerado automaticamente (ex.: "10ª GRSI A" ou, sem curso, "7ª A").</summary>
    public string Nome => Curso is null || string.IsNullOrWhiteSpace(Curso.Sigla)
        ? $"{Classe.Numero}ª {Letra}"
        : $"{Classe.Numero}ª {Curso.Sigla} {Letra}";

    public double OcupacaoPercentual =>
        Capacidade <= 0 ? 0 : Math.Clamp(Matriculados / (double)Capacidade * 100, 0, 100);

    /// <summary>Verdadeiro quando a turma atingiu a capacidade máxima (gatilho para abrir a próxima letra).</summary>
    public bool EstaCheia => Capacidade > 0 && Matriculados >= Capacidade;

    public string TurnoLabel => Turno.ParaLabel();
    public string OcupacaoLabel => $"{OcupacaoPercentual:0}% Ocupado";
    public string MatriculadosCapacidadeLabel => $"{Matriculados}/{Capacidade}";
}
