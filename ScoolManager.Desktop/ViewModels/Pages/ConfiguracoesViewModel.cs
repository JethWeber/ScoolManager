using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>Identifica cada aba do módulo Configurações.</summary>
public enum AbaConfiguracoes
{
    Institucional,
    Utilizadores,
    Permissoes,
    Backup,
    Licenca
}

/// <summary>Item da faixa de abas (ícone + título + o valor do enum correspondente).
/// Mesmo padrão usado em EscolaViewModel.AbaEscolaItem, para que a faixa de
/// abas de Configurações use exatamente o mesmo visual (ListBox "pill").</summary>
public class AbaConfiguracoesItem
{
    public required MaterialIconKind Icon { get; init; }
    public required string Titulo { get; init; }
    public required AbaConfiguracoes Valor { get; init; }
}

/// <summary>
/// ViewModel da View 7 - Configurações (ver SM_Flow.md).
/// Abas: Dados da Escola, Utilizadores, Permissões, Backup &amp; Segurança, Licença.
/// Cada aba é "poucas views, muitos modais": as ações (Editar Utilizador,
/// Criar Backup, etc.) abrem modais - por agora marcadas como TODO, seguindo
/// o mesmo padrão usado no resto do projeto (ex.: MainWindowViewModel).
/// </summary>
public partial class ConfiguracoesViewModel : ViewModelBase
{
    // ================================================================
    // NAVEGAÇÃO ENTRE ABAS (faixa "pill", igual à usada em EscolaView)
    // ================================================================

    public ObservableCollection<AbaConfiguracoesItem> Abas { get; } = new()
    {
        new() { Icon = MaterialIconKind.School,        Titulo = "Dados da Escola",    Valor = AbaConfiguracoes.Institucional },
        new() { Icon = MaterialIconKind.AccountCog,    Titulo = "Utilizadores",       Valor = AbaConfiguracoes.Utilizadores },
        new() { Icon = MaterialIconKind.ShieldAccount, Titulo = "Permissões",         Valor = AbaConfiguracoes.Permissoes },
        new() { Icon = MaterialIconKind.CloudSync,     Titulo = "Backup & Segurança", Valor = AbaConfiguracoes.Backup },
        new() { Icon = MaterialIconKind.Key,           Titulo = "Licença",            Valor = AbaConfiguracoes.Licenca },
    };

    [ObservableProperty]
    private AbaConfiguracoesItem? _abaItemSelecionada;

    public bool EhTabInstitucional => AbaItemSelecionada?.Valor == AbaConfiguracoes.Institucional;
    public bool EhTabUtilizadores => AbaItemSelecionada?.Valor == AbaConfiguracoes.Utilizadores;
    public bool EhTabPermissoes => AbaItemSelecionada?.Valor == AbaConfiguracoes.Permissoes;
    public bool EhTabBackup => AbaItemSelecionada?.Valor == AbaConfiguracoes.Backup;
    public bool EhTabLicenca => AbaItemSelecionada?.Valor == AbaConfiguracoes.Licenca;

    partial void OnAbaItemSelecionadaChanged(AbaConfiguracoesItem? value)
    {
        OnPropertyChanged(nameof(EhTabInstitucional));
        OnPropertyChanged(nameof(EhTabUtilizadores));
        OnPropertyChanged(nameof(EhTabPermissoes));
        OnPropertyChanged(nameof(EhTabBackup));
        OnPropertyChanged(nameof(EhTabLicenca));
    }

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
    // ABA 5 - LICENÇA
    // ================================================================
    // Campos alinhados com o payload da licença descrito em
    // WeberTech_Licensing_Documentacao_V01.pdf (LicenseId, ProductId,
    // MachineId, Plan, Type, Features, IssuedAt, ExpiresAt). Por agora os
    // valores são estáticos; quando WeberTech.Licensing estiver integrado,
    // devem vir de Licensing.GetLicenseInfo() / Licensing.CurrentStatus.

    [ObservableProperty]
    private string _licencaEstado = "Válida";

    [ObservableProperty]
    private string _licencaProduto = "School Manager Desktop";

    [ObservableProperty]
    private string _licencaCliente = "Complexo Escolar Politécnico de Luanda";

    [ObservableProperty]
    private string _licencaPlano = "Professional";

    [ObservableProperty]
    private string _licencaTipo = "Assinatura Anual";

    [ObservableProperty]
    private string _licencaDataEmissao = "30/07/2025";

    [ObservableProperty]
    private string _licencaDataExpiracao = "30/07/2026";

    /// <summary>Machine ID local (ver MachineIdService no documento de licenciamento).</summary>
    [ObservableProperty]
    private string _licencaMachineId = "9F2C7A1E4B6D0083";

    public ObservableCollection<string> LicencaModulos { get; } = new()
    {
        "Alunos", "Propinas", "Financeiro", "Relatórios"
    };

    [RelayCommand]
    private void CopiarMachineId()
    {
        // TODO: copiar LicencaMachineId para a área de transferência
        // (Avalonia IClipboard via TopLevel.GetTopLevel(view)).
    }

    [RelayCommand]
    private void GerarPedidoAtivacao()
    {
        // TODO: gerar o pedido de ativação (ActivationRequestService) e
        // mostrar o QR Code correspondente neste cartão, tal como descrito
        // na secção 6-7 de WeberTech_Licensing_Documentacao_V01.pdf.
    }

    [RelayCommand]
    private void ImportarLicenca()
    {
        // TODO: abrir seletor de ficheiros (IStorageProvider) filtrado por
        // *.wta e chamar Licensing.ImportLicenseFile(caminho).
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
        _abaItemSelecionada = Abas[0]; // Dados da Escola

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
