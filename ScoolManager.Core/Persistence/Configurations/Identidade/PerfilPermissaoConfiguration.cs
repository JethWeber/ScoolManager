using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Persistence.Configurations.Identidade;

public class PerfilPermissaoConfiguration : IEntityTypeConfiguration<PerfilPermissao>
{
    public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
    {
        builder.ToTable("PerfisPermissao");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Perfil).IsRequired().HasMaxLength(100);
    }
}
