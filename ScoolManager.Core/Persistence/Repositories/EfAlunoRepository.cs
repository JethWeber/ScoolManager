using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Dtos.Alunos;
using ScoolManager.Core.Entities.Alunos;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfAlunoRepository : IAlunoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfAlunoRepository(ScoolManagerDbContext db) => _db = db;

    private IQueryable<Aluno> ComNavegacoes() => _db.Alunos
        .Include(a => a.Turma).ThenInclude(t => t!.Classe)
        .Include(a => a.Turma).ThenInclude(t => t!.Curso)
        .Include(a => a.Encarregados)
        .Include(a => a.Documentos);

    public async Task<IReadOnlyList<Aluno>> ObterTodosAsync(CancellationToken ct = default) =>
        await ComNavegacoes().ToListAsync(ct);

    public async Task<IReadOnlyList<Aluno>> ObterPorFiltroAsync(FiltroAlunoDto filtro, CancellationToken ct = default)
    {
        var query = ComNavegacoes();

        if (!string.IsNullOrWhiteSpace(filtro.TextoBusca))
            query = query.Where(a => a.Nome.Contains(filtro.TextoBusca) || a.Codigo.Contains(filtro.TextoBusca));

        if (filtro.ApenasAtivos == true)
            query = query.Where(a => a.Ativo);

        if (!string.IsNullOrWhiteSpace(filtro.Situacao))
        {
            var ativo = filtro.Situacao.Equals("Ativo", StringComparison.OrdinalIgnoreCase);
            query = query.Where(a => a.Ativo == ativo);
        }

        // Classe é filtrada pelo Numero (int), não pela label "10ª" — evita
        // comparação de string derivada dentro da tradução para SQL.
        if (int.TryParse(filtro.Classe, out var numeroClasse))
            query = query.Where(a => a.Turma != null && a.Turma.Classe != null && a.Turma.Classe.Numero == numeroClasse);

        // Idem para Turma: compara a Letra (char), não Turma.Nome (string calculada).
        if (!string.IsNullOrWhiteSpace(filtro.Turma) && filtro.Turma.Length == 1)
        {
            var letra = filtro.Turma[0];
            query = query.Where(a => a.Turma != null && a.Turma.Letra == letra);
        }

        return await query.ToListAsync(ct);
    }

    public async Task<Aluno?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await ComNavegacoes().FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<Aluno?> ObterPorCodigoAsync(string codigo, CancellationToken ct = default) =>
        await ComNavegacoes().FirstOrDefaultAsync(a => a.Codigo == codigo, ct);

    public async Task<Aluno> AdicionarAsync(Aluno aluno, CancellationToken ct = default)
    {
        _db.Alunos.Add(aluno);
        await _db.SaveChangesAsync(ct);
        return aluno;
    }

    public async Task AtualizarAsync(Aluno aluno, CancellationToken ct = default)
    {
        _db.Alunos.Update(aluno);
        await _db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        var aluno = await _db.Alunos.FindAsync([id], ct);
        if (aluno is null) return;
        _db.Alunos.Remove(aluno);
        await _db.SaveChangesAsync(ct);
    }
}
