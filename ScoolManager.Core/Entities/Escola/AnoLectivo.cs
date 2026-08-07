using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Escola;

/// <summary>
/// Ano lectivo (ex.: "2025/2026"), com datas e estado Aberto/Encerrado.
///
/// Migrado de <c>ScoolManager.Desktop.Models.AnoLectivoModel</c>.
/// <c>EstadoLabel</c> (texto de apresentação) não sobe — fica na UI.
/// </summary>
public class AnoLectivo
{
    public int Id { get; set; }

    /// <summary>Designação do ano lectivo (ex.: "2025/2026").</summary>
    public string Nome { get; set; } = string.Empty;

    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
    public EstadoAnoLectivo Estado { get; set; }

    /// <summary>Verdadeiro enquanto o ano lectivo estiver Aberto (editável e ainda por encerrar).</summary>
    public bool EstaAberto => Estado == EstadoAnoLectivo.Aberto;
}
