using ScoolManager.Core.Dtos.Alunos;
using ScoolManager.Core.Entities.Alunos;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IAlunoRepository
{
    Task<IReadOnlyList<Aluno>> ObterTodosAsync(CancellationToken ct = default);

    /// <summary>Aplica os campos preenchidos de <see cref="FiltroAlunoDto"/> (Classe/Turma/Situacao/TextoBusca/ApenasAtivos).</summary>
    Task<IReadOnlyList<Aluno>> ObterPorFiltroAsync(FiltroAlunoDto filtro, CancellationToken ct = default);

    Task<Aluno?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<Aluno?> ObterPorCodigoAsync(string codigo, CancellationToken ct = default);
    Task<Aluno> AdicionarAsync(Aluno aluno, CancellationToken ct = default);
    Task AtualizarAsync(Aluno aluno, CancellationToken ct = default);
    Task RemoverAsync(int id, CancellationToken ct = default);
}
