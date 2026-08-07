namespace ScoolManager.Core.Entities.Identidade;

/// <summary>
/// Um perfil de utilizador (Diretor Geral, Secretária, Tesoureiro, ...) e a
/// que áreas do sistema tem acesso (aba "Permissões" da View 7 —
/// Configurações).
///
/// Migrado de <c>PermissaoPerfilModel</c>, sem <c>ObservableObject</c>
/// (não é preciso notificação de propriedade fora da UI).
/// </summary>
public class PerfilPermissao
{
    public int Id { get; set; }
    public string Perfil { get; set; } = string.Empty;

    /// <summary>Perfis de sistema (ex.: Administrador) não podem ser editados.</summary>
    public bool Bloqueado { get; set; }

    public bool VerAlunos { get; set; }
    public bool EditarAlunos { get; set; }
    public bool Financeiro { get; set; }
    public bool Relatorios { get; set; }
    public bool Configuracoes { get; set; }
}
