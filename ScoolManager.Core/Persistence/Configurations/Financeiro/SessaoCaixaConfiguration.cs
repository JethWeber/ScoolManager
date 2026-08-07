using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Persistence.Configurations.Financeiro;

public class SessaoCaixaConfiguration : IEntityTypeConfiguration<SessaoCaixa>
{
    public void Configure(EntityTypeBuilder<SessaoCaixa> builder)
    {
        builder.ToTable("SessoesCaixa");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Estado).HasConversion<string>();
        builder.Property(s => s.SaldoInicial).HasColumnType("decimal(18,2)");
        builder.Property(s => s.SaldoFinal).HasColumnType("decimal(18,2)");

        builder.HasOne(s => s.UtilizadorAbertura).WithMany().HasForeignKey(s => s.UtilizadorAberturaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(s => s.UtilizadorFechamento).WithMany().HasForeignKey(s => s.UtilizadorFechamentoId).OnDelete(DeleteBehavior.Restrict);
    }
}
