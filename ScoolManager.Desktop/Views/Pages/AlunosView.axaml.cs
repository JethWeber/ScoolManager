using Avalonia.Controls;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.ViewModels.Pages;

namespace ScoolManager.Desktop.Views.Pages;

public partial class AlunosView : UserControl
{
    public AlunosView()
    {
        InitializeComponent();

        // O DataContext é atribuído automaticamente pelo ViewLocator quando esta
        // view é aberta a partir do MainWindowViewModel (NavigationItems), por
        // isso a subscrição ao evento é feita reagindo à mudança de DataContext.
        DataContextChanged += (_, _) =>
        {
            if (DataContext is AlunosViewModel vm)
            {
                vm.DetalhesAlunoSolicitado -= OnDetalhesAlunoSolicitado;
                vm.DetalhesAlunoSolicitado += OnDetalhesAlunoSolicitado;
            }
        };
    }

    private void OnDetalhesAlunoSolicitado(object? sender, AlunoListItemModel aluno)
    {
        // TODO: quando a view "Detalhes do Aluno" existir, navegar até lá
        // (ex.: trocando o CurrentPage do MainWindowViewModel).
    }
}
