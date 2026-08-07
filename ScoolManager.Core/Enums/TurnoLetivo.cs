namespace ScoolManager.Core.Enums;

/// <summary>
/// Turno em que uma Turma funciona.
///
/// NOTA: a conversão para label ("Manhã"/"Tarde"/"Noite") e a conversão
/// inversa (label -> enum, usada hoje pelos ComboBox dos formulários)
/// ficam do lado da UI — não sobem para o Core.
/// </summary>
public enum TurnoLetivo
{
    Manha,
    Tarde,
    Noite
}
