using CommunityToolkit.Mvvm.ComponentModel;

namespace ScoolManager.Desktop.Models;

/// <summary>
/// Representa uma linha da matriz de permissões (aba Permissões): um perfil
/// de utilizador (Diretor Geral, Secretária, Tesoureiro, ...) e a que áreas
/// do sistema tem acesso.
/// </summary>
public partial class PermissaoPerfilModel : ObservableObject
{
    public string Perfil { get; set; } = string.Empty;

    /// <summary>Perfis de sistema (ex.: Administrador) não podem ser editados.</summary>
    public bool Bloqueado { get; set; }

    [ObservableProperty]
    private bool _verAlunos;

    [ObservableProperty]
    private bool _editarAlunos;

    [ObservableProperty]
    private bool _financeiro;

    [ObservableProperty]
    private bool _relatorios;

    [ObservableProperty]
    private bool _configuracoes;
}
