using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Configurations.Configuracoes;

public class BackupRegistroConfiguration : IEntityTypeConfiguration<BackupRegistro>
{
    public void Configure(EntityTypeBuilder<BackupRegistro> builder)
    {
        builder.ToTable("Backups");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.NomeArquivo).IsRequired().HasMaxLength(260);
        builder.Property(b => b.Localizacao).IsRequired().HasMaxLength(400);
    }
}
