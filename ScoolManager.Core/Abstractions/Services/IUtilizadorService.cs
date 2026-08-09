using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>Serviço da aba "Utilizadores" (View 7 — Configurações, ver SM_Flow.md).</summary>
public interface IUtilizadorService
{
    Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default);

    /// <summary>Recebe a password em claro só aqui — o serviço é responsável por gerar o PasswordHash.</summary>
    Task<Utilizador> CriarAsync(string nome, string cargo, string telefone, string password, int? perfilPermissaoId, CancellationToken ct = default);

    Task AtualizarAsync(Utilizador utilizador, CancellationToken ct = default);

    /// <summary>Atribui/troca o perfil de permissões de um utilizador já existente.</summary>
    /// <exception cref="Exceptions.EntidadeNaoEncontradaException">Utilizador ou PerfilPermissao não existem.</exception>
    Task AtribuirPerfilAsync(int utilizadorId, int perfilPermissaoId, CancellationToken ct = default);

    /// <summary>Alterna Ativo — não é uma remoção física (ver DesativarUtilizador em ConfiguracoesViewModel).</summary>
    Task DesativarAsync(int id, CancellationToken ct = default);
}
