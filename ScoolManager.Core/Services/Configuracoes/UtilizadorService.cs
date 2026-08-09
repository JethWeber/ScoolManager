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

    public UtilizadorService(IUtilizadorRepository utilizadores, IPerfilPermissaoRepository perfis)
    {
        _utilizadores = utilizadores;
        _perfis = perfis;
    }

    public Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default) => _utilizadores.ObterTodosAsync(ct);

    public async Task<Utilizador> CriarAsync(string nome, string cargo, string telefone, string password, int? perfilPermissaoId, CancellationToken ct = default)
    {
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

    public Task AtualizarAsync(Utilizador utilizador, CancellationToken ct = default) => _utilizadores.AtualizarAsync(utilizador, ct);

    public async Task AtribuirPerfilAsync(int utilizadorId, int perfilPermissaoId, CancellationToken ct = default)
    {
        var utilizador = await _utilizadores.ObterPorIdAsync(utilizadorId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Utilizador), utilizadorId);

        if (await _perfis.ObterPorIdAsync(perfilPermissaoId, ct) is null)
            throw new EntidadeNaoEncontradaException(nameof(PerfilPermissao), perfilPermissaoId);

        utilizador.PerfilPermissaoId = perfilPermissaoId;
        await _utilizadores.AtualizarAsync(utilizador, ct);
    }

    public async Task DesativarAsync(int id, CancellationToken ct = default)
    {
        var utilizador = await _utilizadores.ObterPorIdAsync(id, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Utilizador), id);

        // Alterna o estado — mesmo comportamento de DesativarUtilizador em
        // ConfiguracoesViewModel hoje (não é uma remoção física).
        utilizador.Ativo = !utilizador.Ativo;
        await _utilizadores.AtualizarAsync(utilizador, ct);
    }
}
