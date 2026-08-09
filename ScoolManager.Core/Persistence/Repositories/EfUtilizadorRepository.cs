using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfUtilizadorRepository : IUtilizadorRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfUtilizadorRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default) =>
        await _db.Utilizadores.Include(u => u.PerfilPermissao).OrderBy(u => u.Nome).ToListAsync(ct);

    public async Task<Utilizador?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Utilizadores.Include(u => u.PerfilPermissao).FirstOrDefaultAsync(u => u.Id == id, ct);

    /// <summary>
    /// CORREÇÃO URGENTE: faltava o Include — usado por AuthService.AutenticarAsync,
    /// cujo resultado alimenta ISessaoAtualService.IniciarSessao. Sem o
    /// PerfilPermissao carregado aqui, IAutorizacaoService negava sempre
    /// tudo (perfil sempre null), mesmo para utilizadores com perfil atribuído.
    /// </summary>
    public async Task<Utilizador?> ObterPorTelefoneAsync(string telefone, CancellationToken ct = default) =>
        await _db.Utilizadores.Include(u => u.PerfilPermissao).FirstOrDefaultAsync(u => u.Telefone == telefone, ct);

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
