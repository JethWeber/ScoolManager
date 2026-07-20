using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.ViewModels.Pages;

namespace ScoolManager.Desktop.Views.Pages;

public partial class AlunosView : UserControl
{
    // Filtros comuns para os documentos do formulário de matrícula.
    private static readonly FilePickerFileType DocumentosEFotos = new("Documentos e imagens")
    {
        Patterns = new List<string> { "*.pdf", "*.jpg", "*.jpeg", "*.png" }
    };

    private static readonly FilePickerFileType ApenasFotos = new("Imagens")
    {
        Patterns = new List<string> { "*.jpg", "*.jpeg", "*.png" }
    };

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

    // ================================================================
    // Seleção de documentos (Passo 4 do wizard "Novo Aluno").
    // A leitura/upload real do ficheiro fica para quando existir um
    // serviço de documentos; por agora guardamos apenas o nome exibido.
    // ================================================================

    private async void OnSelecionarCertificadoClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => await SelecionarFicheiroAsync("Certificado / Declaração", DocumentosEFotos,
            nome => { if (DataContext is AlunosViewModel vm) vm.CertificadoDeclaracaoNomeArquivo = nome; });

    private async void OnSelecionarFotoClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => await SelecionarFicheiroAsync("Foto Tipo Passe", ApenasFotos,
            nome => { if (DataContext is AlunosViewModel vm) vm.FotoTipoPasseNomeArquivo = nome; });

    private async void OnSelecionarBiCedulaClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => await SelecionarFicheiroAsync("BI / Cédula", DocumentosEFotos,
            nome => { if (DataContext is AlunosViewModel vm) vm.BiCedulaNomeArquivo = nome; });

    private async void OnSelecionarAtestadoClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => await SelecionarFicheiroAsync("Atestado Médico", DocumentosEFotos,
            nome => { if (DataContext is AlunosViewModel vm) vm.AtestadoMedicoNomeArquivo = nome; });

    private async Task SelecionarFicheiroAsync(string titulo, FilePickerFileType tipo, System.Action<string> aoSelecionar)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider is null) return;

        var resultado = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = titulo,
            AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType> { tipo, FilePickerFileTypes.All }
        });

        var ficheiro = resultado.Count > 0 ? resultado[0] : null;
        if (ficheiro is not null)
            aoSelecionar(ficheiro.Name);
    }
}
