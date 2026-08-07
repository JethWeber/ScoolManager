using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Configurations.Escola;

public class ClasseConfiguration : IEntityTypeConfiguration<Classe>
{
    public void Configure(EntityTypeBuilder<Classe> builder)
    {
        builder.ToTable("Classes");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nivel).HasConversion<string>();

        // Catálogo interno fixo (1ª à 13ª) — migração literal do seed de
        // EscolaRepository.cs (Desktop): Id == Numero; Primário até 6ª,
        // Secundário até 9ª, Médio a partir da 10ª.
        builder.HasData(
            Enumerable.Range(1, 13).Select(numero => new Classe
            {
                Id = numero,
                Numero = numero,
                Nivel = numero <= 6 ? NivelEnsino.Primario
                      : numero <= 9 ? NivelEnsino.Secundario
                      : NivelEnsino.Medio
            }));
    }
}
