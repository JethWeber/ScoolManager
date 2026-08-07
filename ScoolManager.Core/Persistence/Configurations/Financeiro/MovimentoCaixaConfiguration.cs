using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Persistence.Configurations.Financeiro;

public class MovimentoCaixaConfiguration : IEntityTypeConfiguration<MovimentoCaixa>
{
    public void Configure(EntityTypeBuilder<MovimentoCaixa> builder)
    {
        builder.ToTable("MovimentosCaixa");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Tipo).HasConversion<string>();
        builder.Property(m => m.Valor).HasColumnType("decimal(18,2)");
        builder.Property(m => m.Descricao).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Categoria).IsRequired().HasMaxLength(100);

        // SessaoCaixaId é obrigatório na entidade (int, não int?): todo
        // movimento nasce dentro de uma sessão aberta (ver CaixaFechadoException).
        builder.HasOne(m => m.SessaoCaixa).WithMany(s => s.Movimentos).HasForeignKey(m => m.SessaoCaixaId).OnDelete(DeleteBehavior.Restrict);
    }
}
