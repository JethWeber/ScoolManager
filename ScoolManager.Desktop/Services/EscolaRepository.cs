using System.Collections.ObjectModel;
using System.Linq;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.Services;

/// <summary>
/// "Base de dados" em memória do módulo Escola. É partilhada entre as
/// ViewModels de Classes e de Turmas para que os números fiquem consistentes
/// entre os dois ecrãs (ex.: o total de matriculados de "10ª Classe" em
/// Classes é sempre a soma das Turmas dessa classe), e para que as Turmas
/// criadas durante a sessão persistam ao navegar entre páginas (o
/// MainWindowViewModel recria a ViewModel de cada página a cada navegação).
///
/// TODO: substituir por um EscolaService real (ligado a ScoolManager.Core /
/// base de dados) quando existir, mantendo a mesma forma pública (Cursos,
/// Classes, Salas, AnosLectivos, Turmas) para minimizar alterações nas
/// ViewModels que já consomem este repositório.
/// </summary>
public static class EscolaRepository
{
    public static ObservableCollection<CursoModel> Cursos { get; }
    public static ObservableCollection<ClasseModel> Classes { get; }
    public static ObservableCollection<SalaModel> Salas { get; }
    public static ObservableCollection<AnoLectivoModel> AnosLectivos { get; }
    public static ObservableCollection<TurmaModel> Turmas { get; }

    static EscolaRepository()
    {
        Cursos = new ObservableCollection<CursoModel>
        {
            new() { Id = 1, Nome = "Formação Geral",                Nivel = NivelEnsino.Primario },
            new() { Id = 2, Nome = "Formação Geral",                Nivel = NivelEnsino.Secundario },
            new() { Id = 3, Nome = "Informática",                   Nivel = NivelEnsino.Medio },
            new() { Id = 4, Nome = "Ciências Físicas e Biológicas", Nivel = NivelEnsino.Medio },
            new() { Id = 5, Nome = "Economia e Contabilidade",      Nivel = NivelEnsino.Medio },
            new() { Id = 6, Nome = "Ciências Jurídicas",            Nivel = NivelEnsino.Medio },
        };

        Classes = new ObservableCollection<ClasseModel>
        {
            new() { Id = 1, Numero = 7,  Nivel = NivelEnsino.Secundario, Descricao = "I Ciclo do Ensino Secundário" },
            new() { Id = 2, Numero = 8,  Nivel = NivelEnsino.Secundario, Descricao = "I Ciclo do Ensino Secundário" },
            new() { Id = 3, Numero = 9,  Nivel = NivelEnsino.Secundario, Descricao = "I Ciclo do Ensino Secundário" },
            new() { Id = 4, Numero = 10, Nivel = NivelEnsino.Medio,      Descricao = "Ensino Médio Regular" },
            new() { Id = 5, Numero = 11, Nivel = NivelEnsino.Medio,      Descricao = "Ensino Médio Regular" },
            new() { Id = 6, Numero = 12, Nivel = NivelEnsino.Medio,      Descricao = "Finalistas / Exames" },
            new() { Id = 7, Numero = 13, Nivel = NivelEnsino.Medio,      Descricao = "Ensino Médio Profissionalizante" },
        };

        Salas = new ObservableCollection<SalaModel>
        {
            new() { Id = 1, Nome = "Sala 01",     Capacidade = 40 },
            new() { Id = 2, Nome = "Sala 04",     Capacidade = 40 },
            new() { Id = 3, Nome = "Sala 08",     Capacidade = 40 },
            new() { Id = 4, Nome = "Sala 12",     Capacidade = 40 },
            new() { Id = 5, Nome = "Lab Info 2",  Capacidade = 25 },
            new() { Id = 6, Nome = "Oficina B",   Capacidade = 30 },
        };

        AnosLectivos = new ObservableCollection<AnoLectivoModel>
        {
            new() { Id = 1, Nome = "2025/2026", Ativo = true },
        };

        var classe7  = Classes.First(c => c.Numero == 7);
        var classe10 = Classes.First(c => c.Numero == 10);
        var classe11 = Classes.First(c => c.Numero == 11);
        var classe12 = Classes.First(c => c.Numero == 12);

        var geralSecundario = Cursos.First(c => c.Nivel == NivelEnsino.Secundario);
        var informatica     = Cursos.First(c => c.Nome == "Informática");
        var ciencias        = Cursos.First(c => c.Nome == "Ciências Físicas e Biológicas");
        var economia        = Cursos.First(c => c.Nome == "Economia e Contabilidade");

        var sala01   = Salas.First(s => s.Nome == "Sala 01");
        var sala04   = Salas.First(s => s.Nome == "Sala 04");
        var sala08   = Salas.First(s => s.Nome == "Sala 08");
        var sala12   = Salas.First(s => s.Nome == "Sala 12");
        var labInfo2 = Salas.First(s => s.Nome == "Lab Info 2");

        Turmas = new ObservableCollection<TurmaModel>
        {
            // 7ª Classe (Secundário, curso genérico) - duas turmas, mesma sala em turnos diferentes.
            new() { Id = 1, Classe = classe7,  Curso = geralSecundario, Letra = 'A', Sala = sala01,   Periodo = PeriodoLetivo.Manha, CapacidadeMaxima = 40, AlunosMatriculados = 24 },
            new() { Id = 2, Classe = classe7,  Curso = geralSecundario, Letra = 'B', Sala = sala01,   Periodo = PeriodoLetivo.Tarde, CapacidadeMaxima = 40, AlunosMatriculados = 36 },

            // 10ª Classe de Informática: a "A" já está cheia -> foi por isso que a "B" foi aberta.
            new() { Id = 3, Classe = classe10, Curso = informatica,    Letra = 'A', Sala = labInfo2, Periodo = PeriodoLetivo.Noite, CapacidadeMaxima = 25, AlunosMatriculados = 25 },
            new() { Id = 4, Classe = classe10, Curso = informatica,    Letra = 'B', Sala = sala12,   Periodo = PeriodoLetivo.Tarde, CapacidadeMaxima = 40, AlunosMatriculados = 28 },

            // 10ª Classe de Ciências: só a "A", ainda com vagas.
            new() { Id = 5, Classe = classe10, Curso = ciencias,       Letra = 'A', Sala = sala04,   Periodo = PeriodoLetivo.Manha, CapacidadeMaxima = 40, AlunosMatriculados = 32 },

            // 11ª Classe de Ciências: ainda com vagas -> não é permitido abrir a "B" já.
            new() { Id = 6, Classe = classe11, Curso = ciencias,       Letra = 'A', Sala = sala12,   Periodo = PeriodoLetivo.Tarde, CapacidadeMaxima = 40, AlunosMatriculados = 28 },

            // 12ª Classe de Economia: cheia -> gera o alerta de lotação na UI.
            new() { Id = 7, Classe = classe12, Curso = economia,       Letra = 'A', Sala = sala08,   Periodo = PeriodoLetivo.Manha, CapacidadeMaxima = 40, AlunosMatriculados = 40 },
        };
    }

    public static int ProximoIdTurma() => (Turmas.Count == 0 ? 0 : Turmas.Max(t => t.Id)) + 1;
}
