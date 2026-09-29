using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ScoolManager.Desktop.Services;

public partial class AtividadeSucessoService : ObservableObject, IAtividadeSucessoService
{
    private CancellationTokenSource? _cancelamento;

    [ObservableProperty] private bool _isVisivel;
    [ObservableProperty] private Guid? _atividadeAtual;
    [ObservableProperty] private string _mensagem = string.Empty;

    public void AtividadeSucedida(Guid atividade, string mensagem)
    {
        _cancelamento?.Cancel();
        _cancelamento?.Dispose();
        _cancelamento = new CancellationTokenSource();

        AtividadeAtual = atividade;
        Mensagem = mensagem;
        IsVisivel = true;

        _ = OcultarDepoisAsync(_cancelamento.Token);
    }

    private async Task OcultarDepoisAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(1.8), cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
                await Dispatcher.UIThread.InvokeAsync(() => IsVisivel = false);
        }
        catch (OperationCanceledException)
        {
            // Uma nova atividade substituiu o feedback anterior.
        }
    }
}
