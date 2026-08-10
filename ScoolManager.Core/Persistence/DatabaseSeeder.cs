using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Entities.Alunos;
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
        await SeedEscolaAsync(db, ct);
        await SeedAlunosAsync(db, ct);
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

    private static async Task SeedEscolaAsync(ScoolManagerDbContext db, CancellationToken ct)
    {
        if (!await db.AnosLectivos.AnyAsync(ct))
        {
            db.AnosLectivos.Add(new AnoLectivo
            {
                Nome = "2025/2026",
                DataInicio = new DateTime(2025, 9, 1),
                DataTermino = new DateTime(2026, 7, 31),
                Estado = EstadoAnoLectivo.Aberto
            });
        }

        if (!await db.Cursos.AnyAsync(ct))
        {
            db.Cursos.AddRange(
                new Curso { Nome = "Ciências e Tecnologia", Sigla = "CT" },
                new Curso { Nome = "Letras e Ciências Sociais", Sigla = "LCS" },
                new Curso { Nome = "Gestão e Administração", Sigla = "GA" });
        }

        if (!await db.Salas.AnyAsync(ct))
        {
            db.Salas.AddRange(
                new Sala { Nome = "Sala 01", Capacidade = 35, Bloco = "A" },
                new Sala { Nome = "Sala 02", Capacidade = 35, Bloco = "A" },
                new Sala { Nome = "Sala 12", Capacidade = 40, Bloco = "B" });
        }

        if (!await db.Turmas.AnyAsync(ct))
        {
            var ano = await db.AnosLectivos.OrderBy(a => a.Id).FirstAsync(ct);
            var classe10 = await db.Classes.FirstAsync(c => c.Numero == 10, ct);
            var cursoCiencias = await db.Cursos.FirstOrDefaultAsync(c => c.Sigla == "CT", ct);
            var sala = await db.Salas.OrderBy(s => s.Id).FirstAsync(ct);

            db.Turmas.Add(new Turma
            {
                AnoLectivoId = ano.Id,
                ClasseId = classe10.Id,
                CursoId = cursoCiencias?.Id,
                Letra = 'A',
                SalaId = sala.Id,
                Turno = TurnoLetivo.Manha,
                Capacidade = 35,
                Matriculados = 1
            });
        }
    }

    private static async Task SeedAlunosAsync(ScoolManagerDbContext db, CancellationToken ct)
    {
        if (await db.Alunos.AnyAsync(ct))
            return;

        var turma = await db.Turmas.Include(t => t.Classe).Include(t => t.Curso).Include(t => t.Sala).OrderBy(t => t.Id).FirstAsync(ct);
        var ano = await db.AnosLectivos.OrderBy(a => a.Id).FirstAsync(ct);

        var alunos = new[]
        {
            new Aluno
            {
                Codigo = "2026/1001",
                Nome = "João Pedro da Silva",
                DataNascimento = new DateTime(2012, 5, 18),
                Genero = "Masculino",
                Nacionalidade = "Angolana",
                Naturalidade = "Luanda",
                Provincia = "Luanda",
                Pais = "Angola",
                NumeroBiCedula = "004123456LA045",
                Endereco = "Talatona, Rua 12",
                Telefone = "+244 923 000 111",
                Email = "joao@escola.test",
                Ativo = true,
                TurmaId = turma.Id,
                AnoLectivoId = ano.Id,
                DataMatricula = new DateTime(2025, 9, 10),
                Encarregados = new List<Encarregado>
                {
                    new() { Tipo = TipoEncarregado.Pai, Nome = "Ricardo da Silva", Contacto = "+244 923 000 111", Profissao = "Motorista" }
                },
                Documentos = new List<DocumentoAluno>
                {
                    new() { Tipo = TipoDocumentoAluno.BiCedula, NomeArquivo = "bi_joao.pdf", DataUpload = DateTime.Now.AddMonths(-1) }
                }
            },
            new Aluno
            {
                Codigo = "2026/1002",
                Nome = "Maria Luísa Alberto",
                DataNascimento = new DateTime(2011, 9, 8),
                Genero = "Feminino",
                Nacionalidade = "Angolana",
                Naturalidade = "Benguela",
                Provincia = "Benguela",
                Pais = "Angola",
                NumeroBiCedula = "004654321LA089",
                Endereco = "Bairro Operário",
                Telefone = "+244 931 444 222",
                Email = "maria@escola.test",
                Ativo = true,
                TurmaId = turma.Id,
                AnoLectivoId = ano.Id,
                DataMatricula = new DateTime(2025, 9, 10),
                Encarregados = new List<Encarregado>
                {
                    new() { Tipo = TipoEncarregado.Mae, Nome = "Isabel Alberto", Contacto = "+244 931 444 222", Profissao = "Professora" }
                },
                Documentos = new List<DocumentoAluno>
                {
                    new() { Tipo = TipoDocumentoAluno.BiCedula, NomeArquivo = "bi_maria.pdf", DataUpload = DateTime.Now.AddMonths(-1) }
                }
            }
        };

        db.Alunos.AddRange(alunos);
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
