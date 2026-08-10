using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Persistence;
using Xunit;

namespace ScoolManager.Core.Tests.Persistence;

public class DatabaseSeederTests
{
    [Fact]
    public async Task SeedAsync_CreatesClassesAdminProfileAndUser()
    {
        var options = new DbContextOptionsBuilder<ScoolManagerDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        await using var db = new ScoolManagerDbContext(options);
        await db.Database.OpenConnectionAsync();
        await db.Database.EnsureCreatedAsync();

        await DatabaseSeeder.SeedAsync(db);

        var classes = await db.Classes.OrderBy(c => c.Numero).ToListAsync();
        Assert.Equal(13, classes.Count);
        Assert.Equal(1, classes.First().Numero);
        Assert.Equal(13, classes.Last().Numero);

        var perfil = await db.PerfisPermissao.SingleAsync(p => p.Perfil == "Administrador");
        Assert.True(perfil.Bloqueado);
        Assert.True(perfil.VerAlunos);
        Assert.True(perfil.EditarAlunos);
        Assert.True(perfil.Financeiro);
        Assert.True(perfil.Relatorios);
        Assert.True(perfil.Configuracoes);

        var administrador = await db.Utilizadores.SingleAsync(u => u.Telefone == DatabaseSeeder.TelefoneAdminPadrao);
        Assert.Equal("Administrador", administrador.Nome);
        Assert.Equal("Administrador", administrador.Cargo);
        Assert.NotEmpty(administrador.PasswordHash);
        Assert.NotNull(administrador.PerfilPermissao);
        Assert.Equal(perfil.Id, administrador.PerfilPermissao!.Id);
    }

    [Fact]
    public async Task SeedAsync_CreatesSampleStudentsAndTurmas()
    {
        var options = new DbContextOptionsBuilder<ScoolManagerDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        await using var db = new ScoolManagerDbContext(options);
        await db.Database.OpenConnectionAsync();
        await db.Database.EnsureCreatedAsync();

        await DatabaseSeeder.SeedAsync(db);

        var turmas = await db.Turmas.ToListAsync();
        Assert.NotEmpty(turmas);

        var alunos = await db.Alunos.ToListAsync();
        Assert.NotEmpty(alunos);
        Assert.Contains(alunos, a => a.Nome.Contains("João", StringComparison.OrdinalIgnoreCase));
    }
}
