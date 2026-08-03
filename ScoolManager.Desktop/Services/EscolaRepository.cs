using System;
using System.Collections.ObjectModel;
using System.Linq;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.Services;

/// <summary>
/// "Base de dados" em memória do módulo Escola. Partilhada entre todas as
/// ViewModels das abas (Turmas, Salas, Cursos, Anos Lectivos) para que os
/// dados fiquem consistentes ao navegar entre páginas (o MainWindowViewModel
/// recria a ViewModel de cada página a cada navegação).
///
/// <see cref="Classes"/> é um catálogo interno (1ª à 13ª) fornecido pelo
/// sistema - não tem CRUD, é só usado como campo de seleção no modal
/// "Nova Turma"/"Editar Turma".
///
/// TODO: substituir por um EscolaService real (ligado a ScoolManager.Core /
/// base de dados) quando existir, mantendo a mesma forma pública.
/// </summary>
public static class EscolaRepository
{
    /// <summary>Catálogo interno de Classes (1ª à 13ª) - sem CRUD.</summary>
    public static ObservableCollection<ClasseModel> Classes { get; }

    public static ObservableCollection<CursoModel> Cursos { get; }
    public static ObservableCollection<SalaModel> Salas { get; }
    public static ObservableCollection<AnoLectivoModel> AnosLectivos { get; }
    public static ObservableCollection<TurmaModel> Turmas { get; }

    static EscolaRepository()
    {
        Classes = new ObservableCollection<ClasseModel>(
            Enumerable.Range(1, 13).Select(numero => new ClasseModel
            {
                Id = numero,
                Numero = numero,
                Nivel = numero <= 6 ? NivelEnsino.Primario
                      : numero <= 9 ? NivelEnsino.Secundario
                      : NivelEnsino.Medio
            }));

        Cursos = new ObservableCollection<CursoModel>
        {
            new() { Id = 1, Nome = "Gestão de Redes e Sistemas Informáticos", Sigla = "GRSI" },
            new() { Id = 2, Nome = "Gestão de Recursos Humanos",              Sigla = "GRH" },
            new() { Id = 3, Nome = "Gestão Empresarial",                     Sigla = "GE" },
            new() { Id = 4, Nome = "Ciências Físicas e Biológicas",          Sigla = "CFB" },
            new() { Id = 5, Nome = "Ciências Jurídicas",                     Sigla = "CJ" },
        };

        Salas = new ObservableCollection<SalaModel>
        {
            new() { Id = 1, Nome = "Sala 01",    Capacidade = 40, Bloco = "Bloco A" },
            new() { Id = 2, Nome = "Sala 04",    Capacidade = 40, Bloco = "Bloco A" },
            new() { Id = 3, Nome = "Sala 08",    Capacidade = 40, Bloco = "Bloco B" },
            new() { Id = 4, Nome = "Sala 12",    Capacidade = 40, Bloco = "Bloco B" },
            new() { Id = 5, Nome = "Lab Info 2", Capacidade = 25, Bloco = "Bloco C", Observacoes = "Computadores - requer marcação prévia" },
            new() { Id = 6, Nome = "Oficina B",  Capacidade = 30, Bloco = "Bloco C" },
        };

        AnosLectivos = new ObservableCollection<AnoLectivoModel>
        {
            new()
            {
                Id = 1,
                Nome = "2025/2026",
                DataInicio = new DateTime(2025, 10, 1),
                DataTermino = new DateTime(2026, 8, 15),
                Estado = EstadoAnoLectivo.Aberto
            },
        };

        var classe7  = Classes.First(c => c.Numero == 7);
        var classe10 = Classes.First(c => c.Numero == 10);
        var classe11 = Classes.First(c => c.Numero == 11);
        var classe12 = Classes.First(c => c.Numero == 12);

        var grsi = Cursos.First(c => c.Sigla == "GRSI");
        var cfb  = Cursos.First(c => c.Sigla == "CFB");
        var ge   = Cursos.First(c => c.Sigla == "GE");

        var sala01   = Salas.First(s => s.Nome == "Sala 01");
        var sala04   = Salas.First(s => s.Nome == "Sala 04");
        var sala08   = Salas.First(s => s.Nome == "Sala 08");
        var sala12   = Salas.First(s => s.Nome == "Sala 12");
        var labInfo2 = Salas.First(s => s.Nome == "Lab Info 2");

        var anoAtivo = AnosLectivos.First();

        Turmas = new ObservableCollection<TurmaModel>
        {
            // 7ª Classe (Secundário, sem curso) - duas turmas, mesma sala em turnos diferentes.
            new() { Id = 1, AnoLectivo = anoAtivo, Classe = classe7,  Curso = null, Letra = 'A', Sala = sala01,   Turno = TurnoLetivo.Manha, Capacidade = 40, Matriculados = 24 },
            new() { Id = 2, AnoLectivo = anoAtivo, Classe = classe7,  Curso = null, Letra = 'B', Sala = sala01,   Turno = TurnoLetivo.Tarde, Capacidade = 40, Matriculados = 36 },

            // 10ª GRSI: a "A" já está cheia -> foi por isso que a "B" foi aberta.
            new() { Id = 3, AnoLectivo = anoAtivo, Classe = classe10, Curso = grsi, Letra = 'A', Sala = labInfo2, Turno = TurnoLetivo.Noite, Capacidade = 25, Matriculados = 25 },
            new() { Id = 4, AnoLectivo = anoAtivo, Classe = classe10, Curso = grsi, Letra = 'B', Sala = sala12,   Turno = TurnoLetivo.Tarde, Capacidade = 40, Matriculados = 28 },

            // 10ª CFB: só a "A", ainda com vagas.
            new() { Id = 5, AnoLectivo = anoAtivo, Classe = classe10, Curso = cfb,  Letra = 'A', Sala = sala04,   Turno = TurnoLetivo.Manha, Capacidade = 40, Matriculados = 32 },

            // 11ª CFB: ainda com vagas -> não é permitido abrir a "B" já.
            new() { Id = 6, AnoLectivo = anoAtivo, Classe = classe11, Curso = cfb,  Letra = 'A', Sala = sala12,   Turno = TurnoLetivo.Tarde, Capacidade = 40, Matriculados = 28 },

            // 12ª GE: cheia -> gera o alerta de lotação na UI.
            new() { Id = 7, AnoLectivo = anoAtivo, Classe = classe12, Curso = ge,   Letra = 'A', Sala = sala08,   Turno = TurnoLetivo.Manha, Capacidade = 40, Matriculados = 40 },
        };
    }

    public static int ProximoIdTurma() => (Turmas.Count == 0 ? 0 : Turmas.Max(t => t.Id)) + 1;
    public static int ProximoIdCurso() => (Cursos.Count == 0 ? 0 : Cursos.Max(c => c.Id)) + 1;
    public static int ProximoIdSala() => (Salas.Count == 0 ? 0 : Salas.Max(s => s.Id)) + 1;
    public static int ProximoIdAnoLectivo() => (AnosLectivos.Count == 0 ? 0 : AnosLectivos.Max(a => a.Id)) + 1;
}
