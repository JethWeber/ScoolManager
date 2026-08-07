using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Notificacoes;

/// <summary>
/// Uma notificação do sistema (painel "sino" do Dashboard).
///
/// Migrado de <c>NotificationItem</c>. <c>Title</c>/<c>Message</c>/
/// <c>Type</c>/<c>IsRead</c> ficam <c>Titulo</c>/<c>Mensagem</c>/<c>Tipo</c>/
/// <c>Lida</c> (português, consistente com o resto do Core). <c>TimeAgo</c>
/// (cálculo relativo "há 5 min") não sobe — é cálculo de apresentação,
/// refeito na UI a partir do <see cref="Timestamp"/> cru.
/// </summary>
public class Notificacao
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public TipoNotificacao Tipo { get; set; } = TipoNotificacao.Info;
    public bool Lida { get; set; }
}
