using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.ViewModels.Pages;

namespace ScoolManager.Desktop.Views.Pages;

public partial class AlunosView : UserControl
{
    // Filtros de ficheiro para os documentos do formulário de matrícula.
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

        // Liga o suporte a arrastar-e-soltar em cada uma das 4 zonas de
        // upload do Passo 4 do wizard "Novo Aluno". As zonas também aceitam
        // clique (ver OnZonaUploadClick) como alternativa ao arrastar.
        foreach (var zona in ZonasDeUpload())
        {
            zona.AddHandler(DragDrop.DropEvent, OnZonaDrop);
            zona.AddHandler(DragDrop.DragOverEvent, OnZonaDragOver);
            zona.AddHandler(DragDrop.DragEnterEvent, OnZonaDragEnter);
            zona.AddHandler(DragDrop.DragLeaveEvent, OnZonaDragLeave);
        }
    }

    private IEnumerable<Button> ZonasDeUpload()
    {
        yield return ZonaCertificado;
        yield return ZonaFoto;
        yield return ZonaBiCedula;
        yield return ZonaAtestado;
    }

    private void OnDetalhesAlunoSolicitado(object? sender, AlunoListItemModel aluno)
    {
        // TODO: quando a view "Detalhes do Aluno" existir, navegar até lá
        // (ex.: trocando o CurrentPage do MainWindowViewModel).
    }

    // ================================================================
    // Upload de documentos (Passo 4 do wizard "Novo Aluno").
    // Cada zona é ao mesmo tempo um alvo de arrastar-e-soltar e um botão
    // clicável (fallback para quem preferir escolher pelo seletor de
    // ficheiros). O DataContext de cada zona já é o DocumentoRequeridoItem
    // correspondente (ver AlunosView.axaml), por isso não é preciso mapear
    // o botão para a propriedade certa.
    // ================================================================

    private void OnZonaDragEnter(object? sender, DragEventArgs e)
    {
        if (sender is Button zona)
            zona.Classes.Add("arrastando");
    }

    private void OnZonaDragLeave(object? sender, RoutedEventArgs e)
    {
        if (sender is Button zona)
            zona.Classes.Remove("arrastando");
    }

    private void OnZonaDragOver(object? sender, DragEventArgs e)
    {
        var ficheiros = e.DataTransfer?.TryGetFiles();
        e.DragEffects = ficheiros is not null && ficheiros.Any()
            ? DragDropEffects.Copy
            : DragDropEffects.None;
    }

    private void OnZonaDrop(object? sender, DragEventArgs e)
    {
        if (sender is not Button zona) return;
        zona.Classes.Remove("arrastando");

        if (zona.DataContext is not DocumentoRequeridoItem documento) return;

        var ficheiro = e.DataTransfer?.TryGetFiles()?.FirstOrDefault();
        if (ficheiro is not null)
            documento.NomeArquivo = ficheiro.Name;
    }

    private async void OnZonaUploadClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button zona) return;
        if (zona.DataContext is not DocumentoRequeridoItem documento) return;

        // A Foto Tipo Passe só aceita imagens; os demais aceitam PDF ou imagem.
        var tipoFicheiro = documento.Tipo.Contains("Foto") ? ApenasFotos : DocumentosEFotos;

        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel?.StorageProvider is null) return;

        var resultado = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = $"Selecionar: {documento.Tipo}",
            AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType> { tipoFicheiro, FilePickerFileTypes.All }
        });

        var ficheiro = resultado.Count > 0 ? resultado[0] : null;
        if (ficheiro is not null)
            documento.NomeArquivo = ficheiro.Name;
    }
}
