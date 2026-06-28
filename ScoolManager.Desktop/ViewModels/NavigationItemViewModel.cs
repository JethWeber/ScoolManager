using System;
using Material.Icons;

namespace ScoolManager.Desktop.ViewModels;

/// <summary>
/// Representa um item de navegação da sidebar.
/// O PageFactory cria a ViewModel da página correspondente; o ViewLocator
/// (já registado em App.axaml) resolve automaticamente a View associada.
/// </summary>
public sealed class NavigationItemViewModel
{
    public MaterialIconKind Icon { get; }
    public string Title { get; }
    public Func<ViewModelBase> PageFactory { get; }

    public NavigationItemViewModel(MaterialIconKind icon, string title, Func<ViewModelBase> pageFactory)
    {
        Icon = icon;
        Title = title;
        PageFactory = pageFactory;
    }
}
