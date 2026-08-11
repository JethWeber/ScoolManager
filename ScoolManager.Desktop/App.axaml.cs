using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Extensions;
using ScoolManager.Core.Persistence;
using ScoolManager.Desktop.Infrastructure;
using ScoolManager.Desktop.ViewModels;
using ScoolManager.Desktop.ViewModels.Pages;
using ScoolManager.Desktop.Views;
using ScoolManager.Desktop.Views.Pages;

namespace ScoolManager.Desktop;

public partial class App : Application
{
    /// <summary>Acessível globalmente porque MainWindowViewModel (PageFactory)
    /// e as Windows precisam de resolver ViewModels a partir daqui.</summary>
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Services = ConfigurarServicos();

        // Aplica qualquer migration pendente e semeia o utilizador
        // administrador padrão se a BD ainda estiver vazia — sem isto,
        // uma base de dados nova fica sem nenhum utilizador para login.
        using (var scope = Services.CreateScope())
        {
            var factory = scope.ServiceProvider
                .GetRequiredService<IDbContextFactory<ScoolManagerDbContext>>();

            // CreateDbContext() síncrono — evita await dentro de método void
            using var db = factory.CreateDbContext();

            db.Database.Migrate();
            DatabaseSeeder.SeedAsync(db).GetAwaiter().GetResult();
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var loginWindow = new LoginWindow
            {
                DataContext = Services.GetRequiredService<LoginViewModel>()
            };
            desktop.MainWindow = loginWindow;
            loginWindow.Show();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceProvider ConfigurarServicos()
    {
        var services = new ServiceCollection();

        services.AddScoolManagerCore(); // caminho SQLite por omissão (%LocalAppData%\ScoolManager\scoolmanager.db)

        // TEMPORÁRIO — trocar por WeberTechLicenseGate assim que o
        // WeberTech.Licensing estiver referenciável (ver Infrastructure/DevLicenseGate.cs).
        services.AddSingleton<ILicenseGate, DevLicenseGate>();

        RegistarViewModels(services);

        return services.BuildServiceProvider();
    }

    private static void RegistarViewModels(IServiceCollection services)
    {
        // Transient: cada navegação cria uma instância nova — mesmo
        // comportamento que já existe hoje (MainWindowViewModel recria a
        // página a cada troca de aba).
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<EscolaViewModel>();
        services.AddTransient<AlunosViewModel>();
        services.AddTransient<FinanceiroViewModel>();
        services.AddTransient<RelatoriosViewModel>();
        services.AddTransient<ConfiguracoesViewModel>();
        services.AddTransient<NotificationsPanelViewModel>();
        // DetalhesAlunoViewModel fica de fora deste registo — recebe o
        // AlunoListItemModel/Aluno no construtor, por isso continua a ser
        // criada com `new` dentro de MainWindowViewModel.
    }
}
