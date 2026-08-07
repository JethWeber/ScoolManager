using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface ITurmaRepository
{
    /// <summary>Deve incluir (Include) AnoLectivo, Classe, Curso e Sala — necessários para Turma.Nome/EstaCheia.</summary>
    Task<IReadOnlyList<Turma>> ObterTodasAsync(CancellationToken ct = default);

    Task<Turma?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<Turma> AdicionarAsync(Turma turma, CancellationToken ct = default);
    Task AtualizarAsync(Turma turma, CancellationToken ct = default);
    Task RemoverAsync(int id, CancellationToken ct = default);
}
