using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Alunos;

namespace ScoolManager.Core.Persistence.Configurations.Alunos;

public class EncarregadoConfiguration : IEntityTypeConfiguration<Encarregado>
{
    public void Configure(EntityTypeBuilder<Encarregado> builder)
    {
        builder.ToTable("Encarregados");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Tipo).HasConversion<string>();
        builder.Property(e => e.Nome).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Contacto).IsRequired().HasMaxLength(30);
        builder.Property(e => e.Profissao).HasMaxLength(100);
    }
}
