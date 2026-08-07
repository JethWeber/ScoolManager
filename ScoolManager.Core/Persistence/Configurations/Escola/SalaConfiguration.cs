using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Configurations.Escola;

public class SalaConfiguration : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.ToTable("Salas");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Nome).IsRequired().HasMaxLength(100);

        // Migração literal do seed de EscolaRepository.cs (Desktop).
        builder.HasData(
            new Sala { Id = 1, Nome = "Sala 01",    Capacidade = 40, Bloco = "Bloco A" },
            new Sala { Id = 2, Nome = "Sala 04",    Capacidade = 40, Bloco = "Bloco A" },
            new Sala { Id = 3, Nome = "Sala 08",    Capacidade = 40, Bloco = "Bloco B" },
            new Sala { Id = 4, Nome = "Sala 12",    Capacidade = 40, Bloco = "Bloco B" },
            new Sala { Id = 5, Nome = "Lab Info 2", Capacidade = 25, Bloco = "Bloco C", Observacoes = "Computadores - requer marcação prévia" },
            new Sala { Id = 6, Nome = "Oficina B",  Capacidade = 30, Bloco = "Bloco C" });
    }
}
