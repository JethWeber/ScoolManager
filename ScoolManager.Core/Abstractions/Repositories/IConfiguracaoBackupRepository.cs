using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Abstractions.Repositories;

/// <summary>Singleton (uma só linha, seed com Id = 1) — só leitura e atualização, mesmo padrão de <see cref="IDadosInstituicaoRepository"/>.</summary>
public interface IConfiguracaoBackupRepository
{
    Task<ConfiguracaoBackup> ObterAsync(CancellationToken ct = default);
    Task AtualizarAsync(ConfiguracaoBackup configuracao, CancellationToken ct = default);
}
