namespace ScoolManager.Core.Entities.Escola;

/// <summary>
/// Sala/espaço físico onde uma Turma funciona.
///
/// Migrado de <c>ScoolManager.Desktop.Models.SalaModel</c>.
/// </summary>
public class Sala
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }

    /// <summary>Bloco/pavilhão onde a sala se encontra (opcional).</summary>
    public string? Bloco { get; set; }

    /// <summary>Observações livres sobre a sala (opcional).</summary>
    public string? Observacoes { get; set; }
}
