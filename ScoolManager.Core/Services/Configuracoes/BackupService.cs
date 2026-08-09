using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Configuracoes;
using ScoolManager.Core.Exceptions;
using ScoolManager.Core.Persistence;

namespace ScoolManager.Core.Services.Configuracoes;

/// <summary>
/// "Backup" aqui significa copiar o ficheiro .db do SQLite inteiro — mais
/// simples e mais fiável do que exportar/reimportar dados linha a linha, e
/// já cobre o caso de uso real: "Restaurar" volta a copiar o ficheiro
/// escolhido para o lugar do atual.
/// </summary>
public class BackupService : IBackupService
{
    private readonly IBackupRepository _backups;
    private readonly IConfiguracaoBackupRepository _configuracao;
    private readonly ScoolManagerDbContext _db;
    private readonly IAutorizacaoService _autorizacao;

    public BackupService(IBackupRepository backups, IConfiguracaoBackupRepository configuracao, ScoolManagerDbContext db, IAutorizacaoService autorizacao)
    {
        _backups = backups;
        _configuracao = configuracao;
        _db = db;
        _autorizacao = autorizacao;
    }

    private void GarantirAcesso() => _autorizacao.GarantirPermissao(p => p.Configuracoes, "Configuracoes");

    public Task<ConfiguracaoBackup> ObterConfiguracaoAsync(CancellationToken ct = default)
    {
        GarantirAcesso();
        return _configuracao.ObterAsync(ct);
    }

    public Task AtualizarConfiguracaoAsync(ConfiguracaoBackup configuracao, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _configuracao.AtualizarAsync(configuracao, ct);
    }

    public Task<IReadOnlyList<BackupRegistro>> ObterTodosAsync(CancellationToken ct = default)
    {
        GarantirAcesso();
        return _backups.ObterTodosAsync(ct);
    }

    public async Task<BackupRegistro> CriarBackupAsync(CancellationToken ct = default)
    {
        GarantirAcesso();

        var caminhoOrigem = ObterCaminhoBaseDeDados();
        var pastaBackups = Path.Combine(Path.GetDirectoryName(caminhoOrigem)!, "Backups");
        Directory.CreateDirectory(pastaBackups);

        var nomeArquivo = $"scoolmanager_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
        var caminhoDestino = Path.Combine(pastaBackups, nomeArquivo);

        File.Copy(caminhoOrigem, caminhoDestino, overwrite: false);

        var registo = new BackupRegistro
        {
            NomeArquivo = nomeArquivo,
            DataCriacao = DateTime.Now,
            TamanhoBytes = new FileInfo(caminhoDestino).Length,
            Localizacao = caminhoDestino,
            EhNaNuvem = false
        };

        return await _backups.AdicionarAsync(registo, ct);
    }

    public async Task RestaurarAsync(int backupId, CancellationToken ct = default)
    {
        GarantirAcesso();

        var registo = await _backups.ObterPorIdAsync(backupId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(BackupRegistro), backupId);

        if (!File.Exists(registo.Localizacao))
            throw new FileNotFoundException("O ficheiro de backup já não existe no caminho registado.", registo.Localizacao);

        var caminhoAtual = ObterCaminhoBaseDeDados();

        // Fecha a conexão atual antes de sobrescrever o ficheiro físico —
        // no SQLite não se pode substituir um ficheiro com uma conexão aberta.
        await _db.Database.CloseConnectionAsync();
        File.Copy(registo.Localizacao, caminhoAtual, overwrite: true);
    }

    private string ObterCaminhoBaseDeDados()
    {
        var connectionString = _db.Database.GetConnectionString()
            ?? throw new InvalidOperationException("Connection string do DbContext não disponível.");

        // Formato esperado: "Data Source=/caminho/scoolmanager.db"
        var parteDataSource = connectionString.Split(';')
            .FirstOrDefault(p => p.TrimStart().StartsWith("Data Source", StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException("Connection string não contém 'Data Source'.");

        return parteDataSource.Split('=', 2)[1].Trim();
    }
}
