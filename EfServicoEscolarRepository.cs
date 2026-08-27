using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

/// <summary>
/// Repositório EF de Serviços/Produtos escolares. Inclui Turma (e,
/// dentro dela, Classe/Curso) porque o Nome de uma Turma é uma propriedade
/// calculada que depende dessas navegações estarem carregadas - necessário
/// para mostrar "10ª GRSI A" e não apenas o Id da turma numa propina.
/// </summary>
public class EfServicoEscolarRepository : IServicoEscolarRepository
{
    private readonly IDbContextFactory<ScoolManagerDbContext> _factory;

    public EfServicoEscolarRepository(IDbContextFactory<ScoolManagerDbContext> factory)
        => _factory = factory;

    private static IQueryable<ServicoEscolar> ComNavegacoes(ScoolManagerDbContext db) =>
        db.ServicosEscolares
            .AsNoTracking()
            .Include(s => s.Turma!).ThenInclude(t => t.Classe)
            .Include(s => s.Turma!).ThenInclude(t => t.Curso);

    public async Task<IReadOnlyList<ServicoEscolar>> ObterTodosAsync(CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await ComNavegacoes(db).ToListAsync(ct);
    }

    public async Task<ServicoEscolar?> ObterPorIdAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await ComNavegacoes(db).FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<ServicoEscolar> AdicionarAsync(ServicoEscolar servico, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        db.ServicosEscolares.Add(servico);
        await db.SaveChangesAsync(ct);
        return servico;
    }

    public async Task AtualizarAsync(ServicoEscolar servico, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);

        var existente = await db.ServicosEscolares.FindAsync([servico.Id], ct)
            ?? throw new InvalidOperationException($"Serviço escolar {servico.Id} não encontrado.");

        existente.Nome = servico.Nome;
        existente.Categoria = servico.Categoria;
        existente.Preco = servico.Preco;
        existente.Descricao = servico.Descricao;
        existente.Ativo = servico.Ativo;
        existente.TurmaId = servico.TurmaId;

        await db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        var servico = await db.ServicosEscolares.FindAsync([id], ct);
        if (servico is null) return;

        db.ServicosEscolares.Remove(servico);
        await db.SaveChangesAsync(ct);
    }
}
