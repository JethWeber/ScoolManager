using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Alunos;

namespace ScoolManager.Core.Persistence.Configurations.Alunos;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Alunos");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Codigo).IsRequired().HasMaxLength(30);
        builder.HasIndex(a => a.Codigo).IsUnique();

        builder.Property(a => a.Nome).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Telefone).IsRequired().HasMaxLength(30);
        builder.Property(a => a.Naturalidade).HasMaxLength(100);
        builder.Property(a => a.Provincia).HasMaxLength(100);
        builder.Property(a => a.Pais).HasMaxLength(100);
        builder.Property(a => a.DescricaoCondicaoMedica).HasMaxLength(500);

        builder.HasOne(a => a.Turma).WithMany().HasForeignKey(a => a.TurmaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(a => a.AnoLectivo).WithMany().HasForeignKey(a => a.AnoLectivoId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Encarregados).WithOne(e => e.Aluno).HasForeignKey(e => e.AlunoId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(a => a.Documentos).WithOne(d => d.Aluno).HasForeignKey(d => d.AlunoId).OnDelete(DeleteBehavior.Cascade);
    }
}
