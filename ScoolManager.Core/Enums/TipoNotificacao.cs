namespace ScoolManager.Core.Enums;

/// <summary>
/// Tipo de uma <c>Notificacao</c> — controla a cor e o ícone exibidos no
/// painel "sino" do Dashboard (a cor/ícone em si continuam na UI).
///
/// Migrado de <c>NotificationType</c> (ScoolManager.Desktop.Models), só
/// renomeado para português para consistência com o resto do Core.
/// </summary>
public enum TipoNotificacao
{
    Info,
    Success,
    Warning,
    Error
}
