using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Exceptions;
using ScoolManager.Core.Services.Auth;

namespace ScoolManager.Core.Services.Configuracoes;

public class UtilizadorService : IUtilizadorService
{
    private readonly IUtilizadorRepository _utilizadores;
    public UtilizadorService(IUtilizadorRepository utilizadores) => _utilizadores = utilizadores;

    public Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default) => _utilizadores.ObterTodosAsync(ct);

    public async Task<Utilizador> CriarAsync(string nome, string cargo, string telefone, string password, CancellationToken ct = default)
    {
        if (await _utilizadores.ObterPorTelefoneAsync(telefone, ct) is not null)
            throw new InvalidOperationException($"Já existe um utilizador com o telefone '{telefone}'.");

        var utilizador = new Utilizador
        {
            Nome = nome,
            Cargo = cargo,
            Telefone = telefone,
            PasswordHash = PasswordHasher.Hash(password),
            Ativo = true
        };

        return await _utilizadores.AdicionarAsync(utilizador, ct);
    }

    public Task AtualizarAsync(Utilizador utilizador, CancellationToken ct = default) => _utilizadores.AtualizarAsync(utilizador, ct);

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
