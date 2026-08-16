using ScoolManager.Core.Dtos.Alunos;
using ScoolManager.Core.Entities.Alunos;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>Serviço do módulo Alunos (View 2 + View 3, ver SM_Flow.md).</summary>
public interface IAlunoService
{
    Task<IReadOnlyList<Aluno>> ObterListaAsync(FiltroAlunoDto filtro, CancellationToken ct = default);
    Task<Aluno> ObterDetalhesAsync(int id, CancellationToken ct = default);

    /// <exception cref="Exceptions.EntidadeNaoEncontradaException">TurmaId não existe.</exception>
    Task<Aluno> CriarAsync(Aluno aluno, IEnumerable<Encarregado> encarregados, CancellationToken ct = default);

    Task AtualizarAsync(Aluno aluno, CancellationToken ct = default);
    Task RemoverAsync(int id, CancellationToken ct = default);
    void ValidarCampos(Aluno aluno);
    Task RenovarMatriculaAsync(int alunoId, int novoAnoLectivoId, int novaTurmaId, CancellationToken ct = default);
    Task AdicionarDocumentoAsync(int alunoId, DocumentoAluno documento, CancellationToken ct = default);

    /// <summary>Modal "Importar Alunos" (View 2). O formato do <paramref name="arquivo"/> (CSV/Excel) é decisão de implementação.</summary>
    Task<ImportacaoAlunosResultadoDto> ImportarAsync(Stream arquivo, CancellationToken ct = default);
}
