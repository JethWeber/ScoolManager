using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Alunos;

namespace ScoolManager.Core.Persistence.Configurations.Alunos;

public class DocumentoAlunoConfiguration : IEntityTypeConfiguration<DocumentoAluno>
{
    public void Configure(EntityTypeBuilder<DocumentoAluno> builder)
    {
        builder.ToTable("DocumentosAluno");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Tipo).HasConversion<string>();
        builder.Property(d => d.NomeArquivo).HasMaxLength(260);
    }
}
