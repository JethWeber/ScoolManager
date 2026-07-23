using Avalonia.Controls;
using ScoolManager.Desktop.ViewModels.Pages;

namespace ScoolManager.Desktop.Views.Pages;

public partial class DetalhesAlunoView : UserControl
{
    public DetalhesAlunoView()
    {
        InitializeComponent();

        DataContextChanged += (_, _) =>
        {
            if (DataContext is DetalhesAlunoViewModel vm)
            {
                vm.VoltarParaAlunosSolicitado -= OnVoltarParaAlunosSolicitado;
                vm.VoltarParaAlunosSolicitado += OnVoltarParaAlunosSolicitado;

                vm.ExclusaoConfirmada -= OnExclusaoConfirmada;
                vm.ExclusaoConfirmada += OnExclusaoConfirmada;
            }
        };
    }

    private void OnVoltarParaAlunosSolicitado(object? sender, System.EventArgs e)
    {
        // TODO: quando a navegação estiver centralizada (ex.: no MainWindowViewModel),
        // trocar o CurrentPage de volta para AlunosViewModel a partir daqui.
    }

    private void OnExclusaoConfirmada(object? sender, System.EventArgs e)
    {
        // TODO: remover o aluno da lista em AlunosViewModel e navegar de volta,
        // quando existir um serviço partilhado entre as duas ViewModels.
    }
}
