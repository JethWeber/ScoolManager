using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Configurations.Escola;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("Turmas");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Turno).HasConversion<string>();

        // Propriedades calculadas — não são colunas.
        builder.Ignore(t => t.Nome);
        builder.Ignore(t => t.OcupacaoPercentual);
        builder.Ignore(t => t.EstaCheia);

        builder.HasOne(t => t.AnoLectivo).WithMany().HasForeignKey(t => t.AnoLectivoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.Classe).WithMany().HasForeignKey(t => t.ClasseId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.Curso).WithMany().HasForeignKey(t => t.CursoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.Sala).WithMany().HasForeignKey(t => t.SalaId).OnDelete(DeleteBehavior.Restrict);

        // Migração literal das 7 turmas de EscolaRepository.cs (Desktop),
        // mesmos Ids e valores de matrícula (inclui os cenários que geram
        // os alertas visuais atuais: 10ª GRSI A cheia, 12ª GE A cheia).
        builder.HasData(
            new { Id = 1, AnoLectivoId = 1, ClasseId = 7,  CursoId = (int?)null, Letra = 'A', SalaId = 1, Turno = TurnoLetivo.Manha, Capacidade = 40, Matriculados = 24 },
            new { Id = 2, AnoLectivoId = 1, ClasseId = 7,  CursoId = (int?)null, Letra = 'B', SalaId = 1, Turno = TurnoLetivo.Tarde, Capacidade = 40, Matriculados = 36 },
            new { Id = 3, AnoLectivoId = 1, ClasseId = 10, CursoId = (int?)1,    Letra = 'A', SalaId = 5, Turno = TurnoLetivo.Noite, Capacidade = 25, Matriculados = 25 },
            new { Id = 4, AnoLectivoId = 1, ClasseId = 10, CursoId = (int?)1,    Letra = 'B', SalaId = 4, Turno = TurnoLetivo.Tarde, Capacidade = 40, Matriculados = 28 },
            new { Id = 5, AnoLectivoId = 1, ClasseId = 10, CursoId = (int?)4,    Letra = 'A', SalaId = 2, Turno = TurnoLetivo.Manha, Capacidade = 40, Matriculados = 32 },
            new { Id = 6, AnoLectivoId = 1, ClasseId = 11, CursoId = (int?)4,    Letra = 'A', SalaId = 4, Turno = TurnoLetivo.Tarde, Capacidade = 40, Matriculados = 28 },
            new { Id = 7, AnoLectivoId = 1, ClasseId = 12, CursoId = (int?)3,    Letra = 'A', SalaId = 3, Turno = TurnoLetivo.Manha, Capacidade = 40, Matriculados = 40 }
        );
    }
}
