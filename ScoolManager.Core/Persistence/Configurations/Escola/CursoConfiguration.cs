using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Configurations.Escola;

public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Cursos");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Sigla).IsRequired().HasMaxLength(20);

        // Migração literal do seed de EscolaRepository.cs (Desktop).
        builder.HasData(
            new Curso { Id = 1, Nome = "Gestão de Redes e Sistemas Informáticos", Sigla = "GRSI" },
            new Curso { Id = 2, Nome = "Gestão de Recursos Humanos",              Sigla = "GRH" },
            new Curso { Id = 3, Nome = "Gestão Empresarial",                     Sigla = "GE" },
            new Curso { Id = 4, Nome = "Ciências Físicas e Biológicas",          Sigla = "CFB" },
            new Curso { Id = 5, Nome = "Ciências Jurídicas",                     Sigla = "CJ" });
    }
}
