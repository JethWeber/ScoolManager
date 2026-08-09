using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Persistence;
using ScoolManager.Core.Persistence.Repositories;
using ScoolManager.Core.Services.Alunos;
using ScoolManager.Core.Services.Auth;
using ScoolManager.Core.Services.Configuracoes;
using ScoolManager.Core.Services.Dashboard;
using ScoolManager.Core.Services.Exportacao;
using ScoolManager.Core.Services.Financeiro;
using ScoolManager.Core.Services.Notificacoes;
using ScoolManager.Core.Services.Relatorios;

namespace ScoolManager.Core.Extensions;

/// <summary>
/// Ponto único de registo do ScoolManager.Core num <c>IServiceCollection</c>.
///
/// IMPORTANTE: <c>ILicenseGate</c> NÃO é registado aqui de propósito — é
/// responsabilidade do host (hoje o ScoolManager.Desktop, no futuro também
/// uma API) escolher e registar a implementação concreta
/// (<c>WeberTechLicenseGate</c>, que chama o WeberTech.Licensing real).
/// Ver roteiro, Secção 8. Se <c>ILicenseGate</c> não estiver registado pelo
/// host, a resolução de <c>IFinanceiroService</c>/<c>IRelatorioService</c>
/// falha em tempo de execução (erro claro de DI, não um bug escondido).
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Regista o DbContext (SQLite) usando o caminho por omissão já
    /// decidido: <c>%LocalAppData%\ScoolManager\scoolmanager.db</c>
    /// (equivalente, fora do Windows, a <c>Environment.SpecialFolder.LocalApplicationData</c>).
    /// </summary>
    public static IServiceCollection AddScoolManagerCore(this IServiceCollection services)
    {
        var pasta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ScoolManager");
        Directory.CreateDirectory(pasta);

        var connectionString = $"Data Source={Path.Combine(pasta, "scoolmanager.db")}";
        return AddScoolManagerCore(services, connectionString);
    }

    /// <summary>Sobrecarga para quem quer controlar o caminho/connection string explicitamente (ex.: testes, ambientes diferentes).</summary>
    public static IServiceCollection AddScoolManagerCore(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ScoolManagerDbContext>(options => options.UseSqlite(connectionString));

        RegistarRepositorios(services);
        RegistarServicos(services);

        return services;
    }

    private static void RegistarRepositorios(IServiceCollection services)
    {
        // Scoped (não Singleton): cada um depende do ScoolManagerDbContext,
        // que por sua vez é Scoped por definição do AddDbContext — um
        // repositório Singleton sobre um DbContext Scoped era exatamente o
        // tipo de bug sutil que queremos evitar aqui.
        services.AddScoped<IClasseRepository, EfClasseRepository>();
        services.AddScoped<ICursoRepository, EfCursoRepository>();
        services.AddScoped<ISalaRepository, EfSalaRepository>();
        services.AddScoped<IAnoLectivoRepository, EfAnoLectivoRepository>();
        services.AddScoped<ITurmaRepository, EfTurmaRepository>();
        services.AddScoped<IAlunoRepository, EfAlunoRepository>();
        services.AddScoped<IPagamentoRepository, EfPagamentoRepository>();
        services.AddScoped<IMovimentoCaixaRepository, EfMovimentoCaixaRepository>();
        services.AddScoped<ISessaoCaixaRepository, EfSessaoCaixaRepository>();
        services.AddScoped<IUtilizadorRepository, EfUtilizadorRepository>();
        services.AddScoped<IPerfilPermissaoRepository, EfPerfilPermissaoRepository>();
        services.AddScoped<INotificacaoRepository, EfNotificacaoRepository>();
        services.AddScoped<IBackupRepository, EfBackupRepository>();
        services.AddScoped<IConfiguracaoBackupRepository, EfConfiguracaoBackupRepository>();
        services.AddScoped<IDadosInstituicaoRepository, EfDadosInstituicaoRepository>();
    }

    private static void RegistarServicos(IServiceCollection services)
    {
        services.AddScoped<IEscolaService, Services.Escola.EscolaService>();
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<IFinanceiroService, FinanceiroService>();
        services.AddScoped<ICaixaService, CaixaService>();
        services.AddScoped<INotificacaoService, NotificacaoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();
        services.AddScoped<IUtilizadorService, UtilizadorService>();
        services.AddScoped<IPermissaoService, PermissaoService>();
        services.AddScoped<IConfiguracaoInstitucionalService, ConfiguracaoInstitucionalService>();
        services.AddScoped<IBackupService, BackupService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDashboardService, DashboardService>();

        // Gap 2: sessão do utilizador atual — Scoped por decisão (ver
        // ISessaoAtualService.cs). Não confundir com ILicenseGate: este
        // fica registado aqui porque é puramente do domínio do Core
        // (guarda um Utilizador do Core), não depende de nenhum SDK externo.
        services.AddScoped<ISessaoAtualService, SessaoAtualService>();

        // CORREÇÃO URGENTE: verificação de PerfilPermissao antes de agir —
        // depende de ISessaoAtualService (Scoped), por isso também Scoped.
        services.AddScoped<IAutorizacaoService, AutorizacaoService>();

        // Gap 4: exportação transversal — sem estado (além do License
        // estático do QuestPDF, definido uma vez no construtor estático),
        // por isso Singleton é seguro e evita recriar a cada resolução.
        services.AddSingleton<IExportService, ExportService>();
    }
}
