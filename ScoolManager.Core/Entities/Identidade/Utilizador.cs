namespace ScoolManager.Core.Entities.Identidade;

/// <summary>
/// Utilizador do sistema (aba "Utilizadores" da View 7 — Configurações;
/// também quem autentica via <c>LoginViewModel</c>).
///
/// Migrado de <c>UtilizadorItemModel</c>, com dois campos acrescentados que
/// não existiam ali mas são exigidos pelo login: <see cref="Telefone"/>
/// (é o que o <c>LoginViewModel</c> valida hoje como "Phone") e
/// <see cref="PasswordHash"/> — nunca a password em claro.
/// <c>Iniciais</c>/<c>EstadoLabel</c> (apresentação) não sobem.
///
/// CORREÇÃO (gap identificado ao cruzar com ConfiguracoesViewModel/
/// PermissaoPerfilModel): faltava a ligação entre um Utilizador e o seu
/// perfil de permissões — sem isto, "que módulos este utilizador vê" não
/// era uma pergunta que o Core conseguisse responder de forma fiável.
/// <see cref="Cargo"/> continua a existir como o texto livre já usado hoje
/// (ex.: "Diretor Geral", "Tesoureira") para exibição — é
/// <see cref="PerfilPermissaoId"/> que carrega o vínculo real de acesso.
/// </summary>
public class Utilizador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;

    /// <summary>Usado como identificador de login (ver LoginViewModel.Phone).</summary>
    public string Telefone { get; set; } = string.Empty;

    /// <summary>Hash da password (nunca gravar em claro).</summary>
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? UltimoAcesso { get; set; }
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// FK para o perfil de permissões deste utilizador. Nullable porque um
    /// utilizador pode, em teoria, ainda não ter perfil atribuído (ex.:
    /// criado antes de decidir o cargo) — nesse caso, deve ser tratado como
    /// "sem nenhum acesso", nunca como "acesso total".
    /// </summary>
    public int? PerfilPermissaoId { get; set; }
    public PerfilPermissao? PerfilPermissao { get; set; }
}
