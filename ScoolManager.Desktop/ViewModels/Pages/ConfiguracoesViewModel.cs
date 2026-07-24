using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>
/// ViewModel da View 7 - Configurações (ver SM_Flow.md).
/// Abas: Dados da Escola, Utilizadores, Permissões, Backup &amp; Segurança.
/// Cada aba é "poucas views, muitos modais": as ações (Editar Utilizador,
/// Criar Backup, etc.) abrem modais - por agora marcadas como TODO, seguindo
/// o mesmo padrão usado no resto do projeto (ex.: MainWindowViewModel).
/// </summary>
public partial class ConfiguracoesViewModel : ViewModelBase
{
    // ================================================================
    // NAVEGAÇÃO ENTRE ABAS
    // ================================================================

    [ObservableProperty]
    private string _tabAtual = "institucional";

    public bool EhTabInstitucional => TabAtual == "institucional";
    public bool EhTabUtilizadores => TabAtual == "utilizadores";
    public bool EhTabPermissoes => TabAtual == "permissoes";
    public bool EhTabBackup => TabAtual == "backup";

    partial void OnTabAtualChanged(string value)
    {
        OnPropertyChanged(nameof(EhTabInstitucional));
        OnPropertyChanged(nameof(EhTabUtilizadores));
        OnPropertyChanged(nameof(EhTabPermissoes));
        OnPropertyChanged(nameof(EhTabBackup));
    }

    [RelayCommand]
    private void SelecionarTab(string tab) => TabAtual = tab;

    // ================================================================
    // ABA 1 - DADOS DA ESCOLA
    // ================================================================

    [ObservableProperty]
    private string _nomeInstituicao = "Complexo Escolar Politécnico de Luanda";

    [ObservableProperty]
    private string _nif = "5412009876";

    [ObservableProperty]
    private string _website = "www.cepl-edu.ao";

    [ObservableProperty]
    private string _emailAdministrativo = "geral@cepl-edu.ao";

    [ObservableProperty]
    private string _enderecoCompleto = "Rua Direita de Luanda, Bairro Talatona, Sector C, Luanda, Angola";

    [ObservableProperty]
    private string _telefonePrincipal = "+244 923 000 000";

    [ObservableProperty]
    private string _telefoneSecundario = "+244 222 000 000";

    /// <summary>Caminho/URI do logotipo carregado. Nulo enquanto não houver logotipo.</summary>
    [ObservableProperty]
    private string? _logotipoPath;

    // Estado do Sistema (cartão lateral)
    public int LicencaDiasRestantes { get; set; } = 240;
    public string EspacoUsadoLabel { get; set; } = "45.2 GB";
    public string EspacoTotalLabel { get; set; } = "100 GB";

    [RelayCommand]
    private void AlterarLogotipo()
    {
        // TODO: abrir seletor de ficheiros (IStorageProvider) e aplicar a
        // LogotipoPath, tal como feito em AlunosView para os documentos.
    }

    // ================================================================
    // ABA 2 - UTILIZADORES
    // ================================================================

    public ObservableCollection<UtilizadorItemModel> Utilizadores { get; }

    [RelayCommand]
    private void NovoUtilizador()
    {
        // TODO: modal "Novo Utilizador" (ver SM_Flow.md).
    }

    [RelayCommand]
    private void EditarUtilizador(UtilizadorItemModel utilizador)
    {
        // TODO: modal "Editar Utilizador".
    }

    [RelayCommand]
    private void DesativarUtilizador(UtilizadorItemModel utilizador)
    {
        // TODO: confirmar antes de desativar. Por agora alterna o estado.
        utilizador.Ativo = !utilizador.Ativo;
    }

    // ================================================================
    // ABA 3 - PERMISSÕES
    // ================================================================

    public ObservableCollection<PermissaoPerfilModel> PerfisPermissao { get; }

    // ================================================================
    // ABA 4 - BACKUP & SEGURANÇA
    // ================================================================

    public ObservableCollection<BackupItemModel> Backups { get; }

    [ObservableProperty]
    private bool _backupDiarioAutomatico = true;

    [ObservableProperty]
    private bool _sincronizacaoNuvem = true;

    [ObservableProperty]
    private bool _notificarFalhasEmail;

    public string UltimaVerificacaoLabel { get; set; } =
        "Última verificação de integridade realizada há 2 horas. Nenhum erro encontrado.";

    [RelayCommand]
    private void CriarBackup()
    {
        // TODO: modal "Criar Backup" + chamada ao serviço de backup real.
    }

    [RelayCommand]
    private void RestaurarBackup(BackupItemModel backup)
    {
        // TODO: confirmar e restaurar a partir de `backup`.
    }

    [RelayCommand]
    private void DescarregarBackup(BackupItemModel backup)
    {
        // TODO: descarregar o ficheiro de `backup` para o disco.
    }

    // ================================================================
    // AÇÃO GLOBAL
    // ================================================================

    [RelayCommand]
    private void GuardarAlteracoes()
    {
        // TODO: persistir os dados institucionais e as configurações de backup.
    }

    public ConfiguracoesViewModel()
    {
        Utilizadores = new ObservableCollection<UtilizadorItemModel>
        {
            new()
            {
                Nome = "Ricardo Silva",
                Iniciais = "RS",
                Cargo = "Diretor Geral",
                UltimoAcessoLabel = "Hoje, 10:45",
                Ativo = true,
            },
            new()
            {
                Nome = "Maria Antónia",
                Iniciais = "MA",
                Cargo = "Tesoureira",
                UltimoAcessoLabel = "Ontem, 16:30",
                Ativo = true,
            },
        };

        PerfisPermissao = new ObservableCollection<PermissaoPerfilModel>
        {
            new()
            {
                Perfil = "Administrador",
                Bloqueado = true,
                VerAlunos = true,
                EditarAlunos = true,
                Financeiro = true,
                Relatorios = true,
                Configuracoes = true,
            },
            new()
            {
                Perfil = "Secretária",
                VerAlunos = true,
                EditarAlunos = true,
                Financeiro = true,
                Relatorios = true,
                Configuracoes = false,
            },
            new()
            {
                Perfil = "Tesoureiro(a)",
                VerAlunos = true,
                EditarAlunos = false,
                Financeiro = true,
                Relatorios = true,
                Configuracoes = false,
            },
        };

        Backups = new ObservableCollection<BackupItemModel>
        {
            new()
            {
                NomeArquivo = "backup_escolar_full_20231024.sql",
                DetalheLabel = "24 Out 2023 | 124.5 MB | Servidor Local",
                Icon = MaterialIconKind.FileDocumentOutline,
                EhNaNuvem = false,
            },
            new()
            {
                NomeArquivo = "daily_automatic_cloud_sync.bak",
                DetalheLabel = "Hoje, 04:00 | 128.2 MB | Google Drive",
                Icon = MaterialIconKind.CloudCheck,
                EhNaNuvem = true,
            },
        };
    }
}
