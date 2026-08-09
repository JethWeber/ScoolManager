using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Entities.Configuracoes;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Entities.Notificacoes;

namespace ScoolManager.Core.Persistence;

/// <summary>
/// DbContext único do ScoolManager.Core. Cada módulo tem os seus DbSets;
/// o mapeamento (chaves, índices, relações, conversão de enums para
/// string, seed) fica em <c>Persistence/Configurations</c>, aplicado via
/// <see cref="ModelBuilder.ApplyConfigurationsFromAssembly"/> — assim,
/// adicionar uma entidade nova é só criar a Configuration correspondente,
/// sem tocar aqui.
/// </summary>
public class ScoolManagerDbContext : DbContext
{
    public ScoolManagerDbContext(DbContextOptions<ScoolManagerDbContext> options) : base(options)
    {
    }

    // Escola
    public DbSet<Classe> Classes => Set<Classe>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<AnoLectivo> AnosLectivos => Set<AnoLectivo>();
    public DbSet<Turma> Turmas => Set<Turma>();

    // Alunos
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Encarregado> Encarregados => Set<Encarregado>();
    public DbSet<DocumentoAluno> DocumentosAluno => Set<DocumentoAluno>();

    // Financeiro
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
    public DbSet<MovimentoCaixa> MovimentosCaixa => Set<MovimentoCaixa>();
    public DbSet<SessaoCaixa> SessoesCaixa => Set<SessaoCaixa>();

    // Identidade
    public DbSet<Utilizador> Utilizadores => Set<Utilizador>();
    public DbSet<PerfilPermissao> PerfisPermissao => Set<PerfilPermissao>();

    // Notificações
    public DbSet<Notificacao> Notificacoes => Set<Notificacao>();

    // Configurações
    public DbSet<DadosInstituicao> DadosInstituicao => Set<DadosInstituicao>();
    public DbSet<BackupRegistro> Backups => Set<BackupRegistro>();
    public DbSet<ConfiguracaoBackup> ConfiguracoesBackup => Set<ConfiguracaoBackup>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScoolManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
