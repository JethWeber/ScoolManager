using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Services.Auth;

namespace ScoolManager.Core.Persistence;

/// <summary>
/// Seed inicial de dados indispensáveis para a app arrancar utilizável —
/// hoje só o utilizador administrador padrão, para não ficar impossível
/// fazer login numa base de dados nova. Chamado pelo composition root
/// (Desktop) logo a seguir a <c>Database.Migrate()</c>.
///
/// Vive no Core (não no Desktop) porque precisa de <c>PasswordHasher</c>,
/// que é <c>internal</c> ao Core — só código na mesma assembly o consegue
/// chamar.
///
/// ⚠️ Credenciais padrão — TROCAR a password no primeiro acesso, assim que
/// existir a funcionalidade de alterar password (aba "Utilizadores").
/// </summary>
public static class DatabaseSeeder
{
    public const string TelefoneAdminPadrao = "900000000";
    private const string PasswordAdminPadrao = "admin123";

    public static async Task SeedAsync(ScoolManagerDbContext db, CancellationToken ct = default)
    {
        if (await db.Utilizadores.AnyAsync(ct))
            return; // já existe pelo menos um utilizador — não semear de novo

        db.Utilizadores.Add(new Utilizador
        {
            Nome = "Administrador",
            Cargo = "Administrador",
            Telefone = TelefoneAdminPadrao,
            PasswordHash = PasswordHasher.Hash(PasswordAdminPadrao),
            Ativo = true
        });

        await db.SaveChangesAsync(ct);
    }
}
