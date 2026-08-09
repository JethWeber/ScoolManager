using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Services.Configuracoes;

public class PermissaoService : IPermissaoService
{
    private readonly IPerfilPermissaoRepository _perfis;
    private readonly IAutorizacaoService _autorizacao;

    public PermissaoService(IPerfilPermissaoRepository perfis, IAutorizacaoService autorizacao)
    {
        _perfis = perfis;
        _autorizacao = autorizacao;
    }

    private void GarantirAcesso() => _autorizacao.GarantirPermissao(p => p.Configuracoes, "Configuracoes");

    public Task<IReadOnlyList<PerfilPermissao>> ObterTodosAsync(CancellationToken ct = default)
    {
        GarantirAcesso();
        return _perfis.ObterTodosAsync(ct);
    }

    public Task<PerfilPermissao> CriarAsync(PerfilPermissao perfil, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _perfis.AdicionarAsync(perfil, ct);
    }

    public async Task AtualizarAsync(PerfilPermissao perfil, CancellationToken ct = default)
    {
        GarantirAcesso();

        var existente = await _perfis.ObterPorIdAsync(perfil.Id, ct);
        if (existente?.Bloqueado == true)
            throw new InvalidOperationException($"O perfil '{existente.Perfil}' é um perfil de sistema e não pode ser editado.");

        await _perfis.AtualizarAsync(perfil, ct);
    }
}
