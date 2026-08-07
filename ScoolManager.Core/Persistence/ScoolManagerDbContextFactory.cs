using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ScoolManager.Core.Persistence;

/// <summary>
/// Usada SÓ pela ferramenta <c>dotnet ef</c> (migrations) em tempo de
/// design — a Class Library não tem um <c>Program.cs</c>/host próprio para
/// o EF Core descobrir a connection string automaticamente.
///
/// Em produção, a connection string real é fornecida pelo composition root
/// (Desktop/API) via <c>AddScoolManagerCore(connectionString)</c> — ver
/// roteiro, Fase 9. Aqui usa-se o mesmo caminho já decidido
/// (<c>%LocalAppData%\ScoolManager\scoolmanager.db</c>) só para as
/// migrations serem geradas/aplicadas localmente durante o desenvolvimento.
///
/// Comandos (correr dentro da pasta do ScoolManager.Core):
///   dotnet ef migrations add InitialCreate
///   dotnet ef database update
/// </summary>
public class ScoolManagerDbContextFactory : IDesignTimeDbContextFactory<ScoolManagerDbContext>
{
    public ScoolManagerDbContext CreateDbContext(string[] args)
    {
        var pasta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ScoolManager");
        Directory.CreateDirectory(pasta);

        var caminhoDb = Path.Combine(pasta, "scoolmanager.db");

        var optionsBuilder = new DbContextOptionsBuilder<ScoolManagerDbContext>();
        optionsBuilder.UseSqlite($"Data Source={caminhoDb}");

        return new ScoolManagerDbContext(optionsBuilder.Options);
    }
}
