using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Abstractions.Repositories;

/// <summary>
/// Classe é um catálogo interno (1ª à 13ª) fornecido pelo sistema — não
/// tem CRUD (por isso a interface só tem leitura), espelhando o
/// comportamento do <c>EscolaRepository</c> atual no Desktop.
/// </summary>
public interface IClasseRepository
{
    Task<IReadOnlyList<Classe>> ObterTodasAsync(CancellationToken ct = default);
    Task<Classe?> ObterPorIdAsync(int id, CancellationToken ct = default);
}
