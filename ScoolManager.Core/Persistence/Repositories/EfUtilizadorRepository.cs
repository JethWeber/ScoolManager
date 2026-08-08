using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfUtilizadorRepository : IUtilizadorRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfUtilizadorRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default) =>
        await _db.Utilizadores.OrderBy(u => u.Nome).ToListAsync(ct);

    public async Task<Utilizador?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Utilizadores.FindAsync([id], ct);

    public async Task<Utilizador?> ObterPorTelefoneAsync(string telefone, CancellationToken ct = default) =>
        await _db.Utilizadores.FirstOrDefaultAsync(u => u.Telefone == telefone, ct);

    public async Task<Utilizador> AdicionarAsync(Utilizador utilizador, CancellationToken ct = default)
    {
        _db.Utilizadores.Add(utilizador);
        await _db.SaveChangesAsync(ct);
        return utilizador;
    }

    public async Task AtualizarAsync(Utilizador utilizador, CancellationToken ct = default)
    {
        _db.Utilizadores.Update(utilizador);
        await _db.SaveChangesAsync(ct);
    }
}
