using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions;

/// <summary>
/// Verifica se o utilizador autenticado (via <see cref="ISessaoAtualService"/>)
/// tem uma determinada permissão no seu <see cref="PerfilPermissao"/>.
///
/// CORREÇÃO URGENTE (gap identificado): <c>Utilizador.PerfilPermissaoId</c>
/// já existia como FK, mas nenhum Service verificava as flags
/// (<c>VerAlunos</c>/<c>EditarAlunos</c>/<c>Financeiro</c>/<c>Relatorios</c>/
/// <c>Configuracoes</c>) antes de agir — a ligação existia, a regra não era
/// aplicada. Esta interface fecha isso.
///
/// Sem sessão iniciada (<c>UtilizadorAtual == null</c>) ou sem perfil
/// atribuído (<c>PerfilPermissaoId == null</c>), <see cref="TemPermissao"/>
/// devolve sempre <c>false</c> — "sem perfil" é tratado como "sem nenhum
/// acesso", nunca como acesso total (mesmo princípio já usado na entidade
/// <c>Utilizador</c>).
/// </summary>
public interface IAutorizacaoService
{
    bool TemPermissao(Func<PerfilPermissao, bool> seletor);

    /// <exception cref="Exceptions.PermissaoNegadaException">O utilizador atual não tem a permissão exigida.</exception>
    void GarantirPermissao(Func<PerfilPermissao, bool> seletor, string nomePermissao);
}
