using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Exceptions;
using ScoolManager.Core.Services.Auth;

namespace ScoolManager.Core.Services.Configuracoes;

public class UtilizadorService : IUtilizadorService
{
    private readonly IUtilizadorRepository _utilizadores;
    private readonly IPerfilPermissaoRepository _perfis;
    private readonly IAutorizacaoService _autorizacao;

    public UtilizadorService(IUtilizadorRepository utilizadores, IPerfilPermissaoRepository perfis, IAutorizacaoService autorizacao)
    {
        _utilizadores = utilizadores;
        _perfis = perfis;
        _autorizacao = autorizacao;
    }

    /// <summary>A gestão de Utilizadores vive na aba "Configurações" — exige a permissão Configuracoes.</summary>
    private void GarantirAcesso() => _autorizacao.GarantirPermissao(p => p.Configuracoes, "Configuracoes");

    public Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default)
    {
        GarantirAcesso();
        return _utilizadores.ObterTodosAsync(ct);
    }

    public async Task<Utilizador> CriarAsync(string nome, string cargo, string telefone, string password, int? perfilPermissaoId, CancellationToken ct = default)
    {
        GarantirAcesso();

        if (await _utilizadores.ObterPorTelefoneAsync(telefone, ct) is not null)
            throw new InvalidOperationException($"Já existe um utilizador com o telefone '{telefone}'.");

        if (perfilPermissaoId is not null && await _perfis.ObterPorIdAsync(perfilPermissaoId.Value, ct) is null)
            throw new EntidadeNaoEncontradaException(nameof(PerfilPermissao), perfilPermissaoId.Value);

        var utilizador = new Utilizador
        {
            Nome = nome,
            Cargo = cargo,
            Telefone = telefone,
            PasswordHash = PasswordHasher.Hash(password),
            PerfilPermissaoId = perfilPermissaoId,
            Ativo = true
        };

        return await _utilizadores.AdicionarAsync(utilizador, ct);
    }

    public Task AtualizarAsync(Utilizador utilizador, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _utilizadores.AtualizarAsync(utilizador, ct);
    }

    public async Task AtribuirPerfilAsync(int utilizadorId, int perfilPermissaoId, CancellationToken ct = default)
    {
        GarantirAcesso();

        var utilizador = await _utilizadores.ObterPorIdAsync(utilizadorId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Utilizador), utilizadorId);

        if (await _perfis.ObterPorIdAsync(perfilPermissaoId, ct) is null)
            throw new EntidadeNaoEncontradaException(nameof(PerfilPermissao), perfilPermissaoId);

        utilizador.PerfilPermissaoId = perfilPermissaoId;
        await _utilizadores.AtualizarAsync(utilizador, ct);
    }

    public async Task DesativarAsync(int id, CancellationToken ct = default)
    {
        GarantirAcesso();

        var utilizador = await _utilizadores.ObterPorIdAsync(id, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Utilizador), id);

        // Alterna o estado — mesmo comportamento de DesativarUtilizador em
        // ConfiguracoesViewModel hoje (não é uma remoção física).
        utilizador.Ativo = !utilizador.Ativo;
        await _utilizadores.AtualizarAsync(utilizador, ct);
    }
}
