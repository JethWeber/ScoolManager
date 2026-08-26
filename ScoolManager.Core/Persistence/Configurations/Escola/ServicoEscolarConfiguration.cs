using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Configurations.Escola;

public class ServicoEscolarConfiguration : IEntityTypeConfiguration<ServicoEscolar>
{
    public void Configure(EntityTypeBuilder<ServicoEscolar> builder)
    {
        builder.ToTable("ServicosEscolares");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Nome)
            .IsRequired()
            .HasMaxLength(150);

        // String, tal como Nivel/Estado nas outras entidades da Escola -
        // mais fácil de ler diretamente na base de dados que um int cru.
        builder.Property(s => s.Categoria)
            .HasConversion<string>();

        builder.Property(s => s.Descricao)
            .HasMaxLength(500);
    }
}
