using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Configurations.Escola;

public class AnoLectivoConfiguration : IEntityTypeConfiguration<AnoLectivo>
{
    public void Configure(EntityTypeBuilder<AnoLectivo> builder)
    {
        builder.ToTable("AnosLectivos");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Nome).IsRequired().HasMaxLength(20);
        builder.Property(a => a.Estado).HasConversion<string>();

        // Propriedade calculada — não é coluna.
        builder.Ignore(a => a.EstaAberto);

        // Migração literal do seed de EscolaRepository.cs (Desktop).
        builder.HasData(new AnoLectivo
        {
            Id = 1,
            Nome = "2025/2026",
            DataInicio = new DateTime(2025, 10, 1),
            DataTermino = new DateTime(2026, 8, 15),
            Estado = EstadoAnoLectivo.Aberto
        });
    }
}
