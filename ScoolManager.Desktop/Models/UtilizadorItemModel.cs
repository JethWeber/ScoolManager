using CommunityToolkit.Mvvm.ComponentModel;

namespace ScoolManager.Desktop.Models;

/// <summary>
/// Representa uma linha da tabela "Utilizadores" em Configurações.
/// </summary>
public partial class UtilizadorItemModel : ObservableObject
{
    public string Nome { get; set; } = string.Empty;

    public string Iniciais { get; set; } = string.Empty;

    public string Cargo { get; set; } = string.Empty;

    public string UltimoAcessoLabel { get; set; } = string.Empty;

    [ObservableProperty]
    private bool _ativo = true;

    public string EstadoLabel => Ativo ? "Ativo" : "Inativo";

    partial void OnAtivoChanged(bool value) => OnPropertyChanged(nameof(EstadoLabel));
}
