using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Configurations.Configuracoes;

public class ConfiguracaoBackupConfiguration : IEntityTypeConfiguration<ConfiguracaoBackup>
{
    public void Configure(EntityTypeBuilder<ConfiguracaoBackup> builder)
    {
        builder.ToTable("ConfiguracoesBackup");
        builder.HasKey(c => c.Id);

        // Singleton de domínio: seed com a linha inicial (Id = 1) — os 3
        // toggles nascem desligados por segurança (nunca ativar
        // sincronização com a nuvem sem o utilizador confirmar explicitamente).
        builder.HasData(new ConfiguracaoBackup
        {
            Id = 1,
            BackupDiarioAutomatico = false,
            SincronizacaoNuvem = false,
            NotificarFalhasEmail = false
        });
    }
}
