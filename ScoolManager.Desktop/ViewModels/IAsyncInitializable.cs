using System.Threading.Tasks;

namespace ScoolManager.Desktop.ViewModels;

/// <summary>
/// ViewModels que precisam de carregar dados do Core de forma assíncrona
/// implementam isto. O code-behind da View correspondente chama
/// InitializeAsync() assim que a View é anexada à árvore visual
/// (ver DashboardView.axaml.cs, OnDataContextChanged).
/// </summary>
public interface IAsyncInitializable
{
    Task InitializeAsync();
}
