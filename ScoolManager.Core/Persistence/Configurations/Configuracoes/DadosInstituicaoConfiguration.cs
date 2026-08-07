using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Configurations.Configuracoes;

public class DadosInstituicaoConfiguration : IEntityTypeConfiguration<DadosInstituicao>
{
    public void Configure(EntityTypeBuilder<DadosInstituicao> builder)
    {
        builder.ToTable("DadosInstituicao");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.NomeInstituicao).IsRequired().HasMaxLength(200);
        builder.Property(d => d.Nif).IsRequired().HasMaxLength(30);
        builder.Property(d => d.EmailAdministrativo).IsRequired().HasMaxLength(200);
        builder.Property(d => d.EnderecoCompleto).IsRequired().HasMaxLength(400);
        builder.Property(d => d.TelefonePrincipal).IsRequired().HasMaxLength(30);

        // Singleton de domínio: seed com a linha inicial (Id = 1), a
        // ConfiguracaoInstitucionalService nunca cria uma segunda.
        builder.HasData(new DadosInstituicao
        {
            Id = 1,
            NomeInstituicao = string.Empty,
            Nif = string.Empty,
            EmailAdministrativo = string.Empty,
            EnderecoCompleto = string.Empty,
            TelefonePrincipal = string.Empty
        });
    }
}
