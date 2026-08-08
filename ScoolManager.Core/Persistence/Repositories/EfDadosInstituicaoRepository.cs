using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfDadosInstituicaoRepository : IDadosInstituicaoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfDadosInstituicaoRepository(ScoolManagerDbContext db) => _db = db;

    // Seed garante a linha Id = 1 desde a primeira migration — First()
    // nunca deve falhar em produção.
    public async Task<DadosInstituicao> ObterAsync(CancellationToken ct = default) =>
        await _db.DadosInstituicao.FirstAsync(ct);

    public async Task AtualizarAsync(DadosInstituicao dados, CancellationToken ct = default)
    {
        _db.DadosInstituicao.Update(dados);
        await _db.SaveChangesAsync(ct);
    }
}
