using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Persistence.Configurations.Identidade;

public class UtilizadorConfiguration : IEntityTypeConfiguration<Utilizador>
{
    public void Configure(EntityTypeBuilder<Utilizador> builder)
    {
        builder.ToTable("Utilizadores");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome).IsRequired().HasMaxLength(200);

        builder.Property(u => u.Telefone).IsRequired().HasMaxLength(30);
        builder.HasIndex(u => u.Telefone).IsUnique(); // identificador de login

        builder.Property(u => u.PasswordHash).IsRequired();
    }
}
