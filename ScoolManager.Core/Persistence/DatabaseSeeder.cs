using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Enums;
using ScoolManager.Core.Services.Auth;

namespace ScoolManager.Core.Persistence;

/// <summary>
/// Seed inicial de dados indispensáveis para a app arrancar utilizável.
///
/// Este seeder cria:
/// - as 13 classes do catálogo escolar;
/// - um perfil de permissões de Administrador com acesso total;
/// - o utilizador administrador padrão, vinculado ao perfil acima.
///
/// O processo é idempotente: ao correr novamente, só atualiza/complete os
/// registos que ainda não existam ou que precisem de ser corrigidos.
/// </summary>
public static class DatabaseSeeder
{
    public const string TelefoneAdminPadrao = "900000000";
    private const string PasswordAdminPadrao = "admin123";

    public static async Task SeedAsync(ScoolManagerDbContext db, CancellationToken ct = default)
    {
        await SeedClassesAsync(db, ct);
        var perfilAdministrador = await EnsureAdministradorPerfilAsync(db, ct);
        await EnsureAdministradorUsuarioAsync(db, perfilAdministrador, ct);

        await db.SaveChangesAsync(ct);
    }

    private static async Task SeedClassesAsync(ScoolManagerDbContext db, CancellationToken ct)
    {
        for (var numero = 1; numero <= 13; numero++)
        {
            var existe = await db.Classes.AnyAsync(c => c.Numero == numero, ct);
            if (existe)
                continue;

            db.Classes.Add(new Classe
            {
                Numero = numero,
                Nivel = numero <= 6 ? NivelEnsino.Primario
                    : numero <= 9 ? NivelEnsino.Secundario
                    : NivelEnsino.Medio
            });
        }
    }

    private static async Task<PerfilPermissao> EnsureAdministradorPerfilAsync(ScoolManagerDbContext db, CancellationToken ct)
    {
        var perfil = await db.PerfisPermissao.FirstOrDefaultAsync(p => p.Perfil == "Administrador", ct);

        if (perfil is null)
        {
            perfil = new PerfilPermissao
            {
                Perfil = "Administrador",
                Bloqueado = true,
                VerAlunos = true,
                EditarAlunos = true,
                Financeiro = true,
                Relatorios = true,
                Configuracoes = true
            };

            db.PerfisPermissao.Add(perfil);
        }
        else
        {
            perfil.Bloqueado = true;
            perfil.VerAlunos = true;
            perfil.EditarAlunos = true;
            perfil.Financeiro = true;
            perfil.Relatorios = true;
            perfil.Configuracoes = true;
        }

        return perfil;
    }

    private static async Task EnsureAdministradorUsuarioAsync(ScoolManagerDbContext db, PerfilPermissao perfil, CancellationToken ct)
    {
        var administrador = await db.Utilizadores.FirstOrDefaultAsync(
            u => u.Telefone == TelefoneAdminPadrao || u.Nome == "Administrador",
            ct);

        if (administrador is null)
        {
            administrador = new Utilizador
            {
                Nome = "Administrador",
                Cargo = "Administrador",
                Telefone = TelefoneAdminPadrao,
                PasswordHash = PasswordHasher.Hash(PasswordAdminPadrao),
                Ativo = true
            };

            db.Utilizadores.Add(administrador);
        }

        administrador.Nome = "Administrador";
        administrador.Cargo = "Administrador";
        administrador.Telefone = TelefoneAdminPadrao;
        administrador.Ativo = true;
        administrador.PasswordHash = string.IsNullOrWhiteSpace(administrador.PasswordHash)
            ? PasswordHasher.Hash(PasswordAdminPadrao)
            : administrador.PasswordHash;
        administrador.PerfilPermissao = perfil;
    }
}
