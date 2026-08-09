using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions;

/// <summary>
/// Guarda o <see cref="Utilizador"/> autenticado, disponível a qualquer
/// ViewModel/Service que precise de saber "quem está a usar a app agora"
/// (ex.: <c>MainWindowViewModel.UserName/UserRole</c>, ou para verificar
/// permissões via <c>UtilizadorAtual.PerfilPermissao</c>).
///
/// Decisão (ver conversa): vive no Core, registado como <c>Scoped</c> — o
/// mesmo padrão do <c>ScoolManagerDbContext</c>. Numa app Desktop de
/// utilizador único, resolvido a partir do container raiz, isto comporta-se
/// como um singleton para a duração da execução (não há múltiplos "scopes"
/// concorrentes) — mesma vida útil que o próprio login.
///
/// <c>IAuthService.AutenticarAsync</c> NÃO chama isto sozinho — é
/// responsabilidade do chamador (ex.: <c>LoginViewModel.LoginAsync</c>)
/// invocar <see cref="IniciarSessao"/> com o utilizador devolvido, para
/// deixar explícito onde a sessão começa.
/// </summary>
public interface ISessaoAtualService
{
    Utilizador? UtilizadorAtual { get; }

    void IniciarSessao(Utilizador utilizador);
    void EncerrarSessao();
}
