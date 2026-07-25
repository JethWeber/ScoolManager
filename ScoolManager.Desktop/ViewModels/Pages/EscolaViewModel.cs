using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.Services;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>Identifica cada aba do módulo Escola.</summary>
public enum AbaEscola
{
    Classes,
    Turmas,
    Salas,
    Cursos,
    AnosLectivos
}

/// <summary>Item da faixa de abas (ícone + título + o valor do enum correspondente).</summary>
public class AbaEscolaItem
{
    public required MaterialIconKind Icon { get; init; }
    public required string Titulo { get; init; }
    public required AbaEscola Valor { get; init; }
}

/// <summary>Um grupo de cartões de Classe dentro de um mesmo Curso (2º nível de agrupamento da aba Classes).</summary>
public class ClasseCursoGrupoViewModel
{
    public required string CursoLabel { get; init; }
    public required ObservableCollection<ClasseCardModel> Cards { get; init; }
}

/// <summary>Um grupo de Cursos dentro de um mesmo Nível de ensino (1º nível de agrupamento da aba Classes).</summary>
public class NivelGrupoViewModel
{
    public required string NivelLabel { get; init; }
    public required ObservableCollection<ClasseCursoGrupoViewModel> GruposPorCurso { get; init; }
}

/// <summary>
/// ViewModel única do módulo Escola. Sustenta TODAS as abas (Classes, Turmas,
/// Salas, Cursos, Anos Lectivos) - não existe uma ViewModel por aba. Cada aba
/// tem a sua secção claramente demarcada abaixo; para dar vida a uma aba
/// ainda vazia, acrescente as propriedades/comandos dela na secção
/// correspondente (já com um comentário "TODO") e ligue-os na
/// EscolaView.axaml, no bloco dessa aba.
///
/// Por agora só a aba "Classes" está pronta; as restantes mostram apenas um
/// estado "em desenvolvimento" (ver <see cref="TituloAbaEmDesenvolvimento"/>).
/// </summary>
public partial class EscolaViewModel : ViewModelBase
{
    // =================================================================
    // Faixa de abas (comum a todas)
    // =================================================================

    public ObservableCollection<AbaEscolaItem> Abas { get; } = new()
    {
        new() { Icon = MaterialIconKind.ViewGrid,        Titulo = "Classes",       Valor = AbaEscola.Classes },
        new() { Icon = MaterialIconKind.AccountMultiple, Titulo = "Turmas",        Valor = AbaEscola.Turmas },
        new() { Icon = MaterialIconKind.Door,            Titulo = "Salas",         Valor = AbaEscola.Salas },
        new() { Icon = MaterialIconKind.BookOpenVariant, Titulo = "Cursos",        Valor = AbaEscola.Cursos },
        new() { Icon = MaterialIconKind.CalendarMonth,   Titulo = "Anos Lectivos", Valor = AbaEscola.AnosLectivos },
    };

    [ObservableProperty]
    private AbaEscolaItem? _abaItemSelecionada;

    // Flags de visibilidade por aba (usadas na View para trocar o conteúdo
    // visível sem sair desta ViewModel nem desta View).
    public bool AbaClassesAtiva => AbaItemSelecionada?.Valor == AbaEscola.Classes;
    public bool AbaTurmasAtiva => AbaItemSelecionada?.Valor == AbaEscola.Turmas;
    public bool AbaSalasAtiva => AbaItemSelecionada?.Valor == AbaEscola.Salas;
    public bool AbaCursosAtiva => AbaItemSelecionada?.Valor == AbaEscola.Cursos;
    public bool AbaAnosLectivosAtiva => AbaItemSelecionada?.Valor == AbaEscola.AnosLectivos;

    /// <summary>Título mostrado no placeholder de qualquer aba ainda não implementada.</summary>
    public string TituloAbaEmDesenvolvimento => AbaItemSelecionada?.Valor switch
    {
        AbaEscola.Turmas => "Turmas",
        AbaEscola.Salas => "Salas",
        AbaEscola.Cursos => "Cursos",
        AbaEscola.AnosLectivos => "Anos Lectivos",
        _ => string.Empty
    };

    partial void OnAbaItemSelecionadaChanged(AbaEscolaItem? value)
    {
        OnPropertyChanged(nameof(AbaClassesAtiva));
        OnPropertyChanged(nameof(AbaTurmasAtiva));
        OnPropertyChanged(nameof(AbaSalasAtiva));
        OnPropertyChanged(nameof(AbaCursosAtiva));
        OnPropertyChanged(nameof(AbaAnosLectivosAtiva));
        OnPropertyChanged(nameof(TituloAbaEmDesenvolvimento));
    }

    public EscolaViewModel()
    {
        _abaItemSelecionada = Abas[0]; // Classes
        AtualizarAgrupamentosClasses();
    }

    // =================================================================
    // Aba "Classes" (única já funcional)
    //
    // Agrega as Turmas existentes (EscolaRepository) por combinação
    // Classe+Curso e organiza os cartões resultantes em Nível de Ensino >
    // Curso, conforme pedido: "as classes devem estar agrupadas por Nível
    // (Primário, Secundário, Médio) e por curso".
    // =================================================================

    [ObservableProperty] private string _termoPesquisaClasses = string.Empty;
    [ObservableProperty] private bool _semResultadosClasses;

    public ObservableCollection<NivelGrupoViewModel> NiveisAgrupados { get; } = new();

    [ObservableProperty] private string _totalAlunosClassesTexto = "0";
    [ObservableProperty] private string _capacidadeTotalClassesTexto = "0";
    [ObservableProperty] private string _vagasDisponiveisClassesTexto = "0";
    [ObservableProperty] private string _vagasEmClassesLabel = string.Empty;

    partial void OnTermoPesquisaClassesChanged(string value) => AtualizarAgrupamentosClasses();

    private void AtualizarAgrupamentosClasses()
    {
        NiveisAgrupados.Clear();

        var cardsPorClasseCurso = EscolaRepository.Turmas
            .GroupBy(t => (t.Classe, t.Curso))
            .Select(g => new ClasseCardModel
            {
                Classe = g.Key.Classe,
                Curso = g.Key.Curso,
                Matriculados = g.Sum(t => t.AlunosMatriculados),
                Capacidade = g.Sum(t => t.CapacidadeMaxima),
                NumeroDeTurmas = g.Count()
            })
            .ToList();

        if (!string.IsNullOrWhiteSpace(TermoPesquisaClasses))
        {
            var termo = TermoPesquisaClasses.Trim();
            cardsPorClasseCurso = cardsPorClasseCurso
                .Where(c => c.Classe.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                            c.Curso.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        foreach (var nivel in new[] { NivelEnsino.Primario, NivelEnsino.Secundario, NivelEnsino.Medio })
        {
            var cardsDoNivel = cardsPorClasseCurso.Where(c => c.Classe.Nivel == nivel).ToList();
            if (cardsDoNivel.Count == 0) continue;

            var gruposPorCurso = cardsDoNivel
                .GroupBy(c => c.Curso.Nome)
                .OrderBy(g => g.Key)
                .Select(g => new ClasseCursoGrupoViewModel
                {
                    CursoLabel = g.Key,
                    Cards = new ObservableCollection<ClasseCardModel>(g.OrderBy(c => c.Classe.Numero))
                });

            NiveisAgrupados.Add(new NivelGrupoViewModel
            {
                NivelLabel = nivel.ParaLabel(),
                GruposPorCurso = new ObservableCollection<ClasseCursoGrupoViewModel>(gruposPorCurso)
            });
        }

        SemResultadosClasses = NiveisAgrupados.Count == 0;

        var totalAlunos = EscolaRepository.Turmas.Sum(t => t.AlunosMatriculados);
        var capacidadeTotal = EscolaRepository.Turmas.Sum(t => t.CapacidadeMaxima);

        TotalAlunosClassesTexto = totalAlunos.ToString();
        CapacidadeTotalClassesTexto = capacidadeTotal.ToString();
        VagasDisponiveisClassesTexto = (capacidadeTotal - totalAlunos).ToString();

        var classesComVaga = EscolaRepository.Turmas.Where(t => !t.EstaCheia).Select(t => t.Classe.Id).Distinct().Count();
        VagasEmClassesLabel = $"Em {classesComVaga} classe(s)";
    }

    [RelayCommand]
    private void NovaClasse()
    {
        // TODO: abrir o modal "Nova Classe" (Número, Nível, Descrição) quando
        // o fluxo de modais estiver centralizado.
    }

    // =================================================================
    // Aba "Turmas" (vazia por agora)
    //
    // TODO: quando for a vez desta aba, acrescentar aqui as propriedades de
    // listagem/filtros e o formulário Nova/Editar Turma, seguindo a mesma
    // regra de nomeação por letras já implementada em TurmaNamingService
    // (Services/TurmaNamingService.cs) - o serviço já está pronto e à espera.
    // =================================================================

    // =================================================================
    // Aba "Salas" (vazia por agora)
    // TODO: CRUD simples sobre EscolaRepository.Salas.
    // =================================================================

    // =================================================================
    // Aba "Cursos" (vazia por agora)
    // TODO: CRUD simples sobre EscolaRepository.Cursos.
    // =================================================================

    // =================================================================
    // Aba "Anos Lectivos" (vazia por agora)
    // TODO: CRUD + encerramento de ano sobre EscolaRepository.AnosLectivos.
    // =================================================================
}
