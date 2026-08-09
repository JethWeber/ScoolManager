using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Persistence.Configurations.Financeiro;

public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("Pagamentos");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Estado).HasConversion<string>();
        builder.Property(p => p.Tipo).HasConversion<string>();
        builder.Property(p => p.Valor).HasColumnType("decimal(18,2)");
        builder.Property(p => p.NumeroRecibo).IsRequired().HasMaxLength(30);
        builder.Property(p => p.MotivoAnulacao).HasMaxLength(300);

        // DateOnly não tem mapeamento nativo no provider SQLite do EF Core
        // — conversão explícita para/de DateTime (hora sempre 00:00).
        builder.Property(p => p.MesReferencia)
            .HasConversion(d => d.ToDateTime(TimeOnly.MinValue), d => DateOnly.FromDateTime(d));

        builder.HasOne(p => p.Aluno).WithMany().HasForeignKey(p => p.AlunoId).OnDelete(DeleteBehavior.Restrict);

        // Opcional (SessaoCaixaId é int? na entidade): um pagamento pode,
        // em teoria, ser importado/migrado sem estar ligado a uma sessão.
        builder.HasOne(p => p.SessaoCaixa).WithMany(s => s.Pagamentos).HasForeignKey(p => p.SessaoCaixaId).OnDelete(DeleteBehavior.Restrict);
    }
}
