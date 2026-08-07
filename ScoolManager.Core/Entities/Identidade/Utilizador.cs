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
}
