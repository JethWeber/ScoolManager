using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Notificacoes;

namespace ScoolManager.Core.Persistence.Configurations.Notificacoes;

public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.ToTable("Notificacoes");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Tipo).HasConversion<string>();
        builder.Property(n => n.Titulo).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Mensagem).IsRequired().HasMaxLength(1000);
    }
}
