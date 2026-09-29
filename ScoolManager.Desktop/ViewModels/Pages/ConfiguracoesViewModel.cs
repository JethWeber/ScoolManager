using System.IO;
using System.Collections.ObjectModel;using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Material.Icons;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Configuracoes;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.Services;

namespace ScoolManager.Desktop.ViewModels.Pages;

public enum AbaConfiguracoes { Institucional, Utilizadores, Permissoes, Backup, Licenca }

public class AbaConfiguracoesItem
{
    public required MaterialIconKind Icon { get; init; }
    public required string Titulo { get; init; }
    public required AbaConfiguracoes Valor { get; init; }
}

public partial class ConfiguracoesViewModel : ViewModelBase
{
    private readonly IConfiguracaoInstitucionalService _institucional;
    private readonly IUtilizadorService _utilizadorService;
    private readonly IPermissaoService _permissaoService;
    private readonly IBackupService _backupService;
    private readonly IFilePickerService _filePicker;
    private readonly Dictionary<int, Utilizador> _utilizadores = new();

    public ObservableCollection<AbaConfiguracoesItem> Abas { get; } = new()
    {
        new() { Icon=MaterialIconKind.School, Titulo="Dados da Escola", Valor=AbaConfiguracoes.Institucional },
        new() { Icon=MaterialIconKind.AccountCog, Titulo="Utilizadores", Valor=AbaConfiguracoes.Utilizadores },
        new() { Icon=MaterialIconKind.ShieldAccount, Titulo="Permissões", Valor=AbaConfiguracoes.Permissoes },
        new() { Icon=MaterialIconKind.CloudSync, Titulo="Backup & Segurança", Valor=AbaConfiguracoes.Backup },
        new() { Icon=MaterialIconKind.Key, Titulo="Licença", Valor=AbaConfiguracoes.Licenca },
    };

    [ObservableProperty] private AbaConfiguracoesItem? _abaItemSelecionada;
    public bool EhTabInstitucional => AbaItemSelecionada?.Valor == AbaConfiguracoes.Institucional;
    public bool EhTabUtilizadores => AbaItemSelecionada?.Valor == AbaConfiguracoes.Utilizadores;
    public bool EhTabPermissoes => AbaItemSelecionada?.Valor == AbaConfiguracoes.Permissoes;
    public bool EhTabBackup => AbaItemSelecionada?.Valor == AbaConfiguracoes.Backup;
    public bool EhTabLicenca => AbaItemSelecionada?.Valor == AbaConfiguracoes.Licenca;
    partial void OnAbaItemSelecionadaChanged(AbaConfiguracoesItem? value)
    {
        OnPropertyChanged(nameof(EhTabInstitucional)); OnPropertyChanged(nameof(EhTabUtilizadores));
        OnPropertyChanged(nameof(EhTabPermissoes)); OnPropertyChanged(nameof(EhTabBackup)); OnPropertyChanged(nameof(EhTabLicenca));
    }

    [ObservableProperty] private string _nomeInstituicao = string.Empty;
    [ObservableProperty] private string _nif = string.Empty;
    [ObservableProperty] private string _website = string.Empty;
    [ObservableProperty] private string _emailAdministrativo = string.Empty;
    [ObservableProperty] private string _enderecoCompleto = string.Empty;
    [ObservableProperty] private string _telefonePrincipal = string.Empty;
    [ObservableProperty] private string _telefoneSecundario = string.Empty;
    [ObservableProperty] private string? _logotipoPath;

    public int LicencaDiasRestantes { get; private set; } = 240;
    public string EspacoUsadoLabel { get; private set; } = string.Empty;
    public string EspacoTotalLabel { get; private set; } = string.Empty;

    [ObservableProperty] private string _erroConfiguracoes = string.Empty;
    [ObservableProperty] private string _sucessoConfiguracoes = string.Empty;

    public ObservableCollection<UtilizadorItemModel> Utilizadores { get; } = new();
    public ObservableCollection<PermissaoPerfilModel> PerfisPermissao { get; } = new();
    public ObservableCollection<BackupItemModel> Backups { get; } = new();

    [ObservableProperty] private bool _backupDiarioAutomatico;
    [ObservableProperty] private bool _sincronizacaoNuvem;
    [ObservableProperty] private bool _notificarFalhasEmail;
    [ObservableProperty] private string _ultimaVerificacaoLabel = "Ainda não verificada.";

    // Modal de utilizador
    [ObservableProperty] private bool _modalUtilizadorVisivel;
    [ObservableProperty] private int _utilizadorEditandoId;
    [ObservableProperty] private string _nomeUtilizador = string.Empty;
    [ObservableProperty] private string _cargoUtilizador = string.Empty;
    [ObservableProperty] private string _telefoneUtilizador = string.Empty;
    [ObservableProperty] private string _passwordUtilizador = string.Empty;
    [ObservableProperty] private string _perfilUtilizadorIdTexto = string.Empty;
    public bool EditandoUtilizador => UtilizadorEditandoId > 0;
    partial void OnUtilizadorEditandoIdChanged(int value) => OnPropertyChanged(nameof(EditandoUtilizador));

    // Licença — a integração WeberTech ainda não está no projeto; estes campos
    // continuam a ser somente leitura até existir o provider de licença real.
    [ObservableProperty] private string _licencaEstado = "Válida";
    [ObservableProperty] private string _licencaProduto = "School Manager Desktop";
    [ObservableProperty] private string _licencaCliente = "—";
    [ObservableProperty] private string _licencaPlano = "—";
    [ObservableProperty] private string _licencaTipo = "—";
    [ObservableProperty] private string _licencaDataEmissao = "—";
    [ObservableProperty] private string _licencaDataExpiracao = "—";
    [ObservableProperty] private string _licencaMachineId = Environment.MachineName;
    public ObservableCollection<string> LicencaModulos { get; } = new() { "Alunos", "Propinas", "Financeiro", "Relatórios" };

    public ConfiguracoesViewModel(
        IConfiguracaoInstitucionalService institucional,
        IUtilizadorService utilizadorService,
        IPermissaoService permissaoService,
        IBackupService backupService,
        IFilePickerService filePicker)
    {
        _institucional = institucional; _utilizadorService = utilizadorService; _permissaoService = permissaoService;
        _backupService = backupService; _filePicker = filePicker;
        _abaItemSelecionada = Abas[0];
        _ = InicializarAsync();
    }

    private async Task InicializarAsync()
    {
        try
        {
            var dados = await _institucional.ObterAsync();
            NomeInstituicao=dados.NomeInstituicao; Nif=dados.Nif; Website=dados.Website ?? "";
            EmailAdministrativo=dados.EmailAdministrativo; EnderecoCompleto=dados.EnderecoCompleto;
            TelefonePrincipal=dados.TelefonePrincipal; TelefoneSecundario=dados.TelefoneSecundario ?? "";
            LogotipoPath=dados.LogotipoPath;

            Utilizadores.Clear(); _utilizadores.Clear();
            foreach (var u in await _utilizadorService.ObterTodosAsync())
            {
                _utilizadores[u.Id]=u;
                Utilizadores.Add(new UtilizadorItemModel
                {
                    Id=u.Id, Nome=u.Nome, Iniciais=Iniciais(u.Nome), Cargo=u.Cargo,
                    UltimoAcessoLabel=u.UltimoAcesso?.ToString("dd/MM/yyyy HH:mm") ?? "Nunca",
                    Ativo=u.Ativo
                });
            }

            PerfisPermissao.Clear();
            foreach (var p in await _permissaoService.ObterTodosAsync())
                PerfisPermissao.Add(new PermissaoPerfilModel
                {
                    Id=p.Id, Perfil=p.Perfil, Bloqueado=p.Bloqueado, VerAlunos=p.VerAlunos,
                    EditarAlunos=p.EditarAlunos, Financeiro=p.Financeiro, Relatorios=p.Relatorios, Configuracoes=p.Configuracoes
                });

            var cfg=await _backupService.ObterConfiguracaoAsync();
            BackupDiarioAutomatico=cfg.BackupDiarioAutomatico; SincronizacaoNuvem=cfg.SincronizacaoNuvem;
            NotificarFalhasEmail=cfg.NotificarFalhasEmail;
            UltimaVerificacaoLabel=cfg.UltimaVerificacaoIntegridade.HasValue
                ? $"Última verificação: {cfg.UltimaVerificacaoIntegridade:dd/MM/yyyy HH:mm}. Nenhum erro registado."
                : "Ainda não foi realizada.";

            await RecarregarBackupsAsync();
            CalcularEspaco();
        }
        catch(Exception ex) { ErroConfiguracoes=ex.Message; }
    }

    private async Task RecarregarBackupsAsync()
    {
        Backups.Clear();
        foreach(var b in await _backupService.ObterTodosAsync())
            Backups.Add(new BackupItemModel
            {
                Id=b.Id, NomeArquivo=b.NomeArquivo, Localizacao=b.Localizacao, EhNaNuvem=b.EhNaNuvem,
                DetalheLabel=$"{b.DataCriacao:dd MMM yyyy HH:mm} | {FormatBytes(b.TamanhoBytes)} | {(b.EhNaNuvem ? "Nuvem" : "Servidor Local")}",
                Icon=b.EhNaNuvem ? MaterialIconKind.CloudCheck : MaterialIconKind.FileDocumentOutline
            });
    }

    private void CalcularEspaco()
    {
        var pasta=_filePicker is not null ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : "";
        try
        {
            var drive=new DriveInfo(Path.GetPathRoot(Path.GetFullPath(pasta)) ?? Path.DirectorySeparatorChar.ToString());
            EspacoTotalLabel=FormatBytes(drive.TotalSize); EspacoUsadoLabel=FormatBytes(drive.TotalSize-drive.AvailableFreeSpace);
            OnPropertyChanged(nameof(EspacoTotalLabel)); OnPropertyChanged(nameof(EspacoUsadoLabel));
        } catch { EspacoTotalLabel="—"; EspacoUsadoLabel="—"; }
    }

    [RelayCommand]
    private async Task GuardarAlteracoes()
    {
        try
        {
            ErroConfiguracoes=string.Empty;
            await _institucional.AtualizarAsync(new DadosInstituicao
            {
                NomeInstituicao=NomeInstituicao.Trim(), Nif=Nif.Trim(), Website=Website.Trim(),
                EmailAdministrativo=EmailAdministrativo.Trim(), EnderecoCompleto=EnderecoCompleto.Trim(),
                TelefonePrincipal=TelefonePrincipal.Trim(), TelefoneSecundario=TelefoneSecundario.Trim(), LogotipoPath=LogotipoPath
            });

            var cfg=await _backupService.ObterConfiguracaoAsync();
            cfg.BackupDiarioAutomatico=BackupDiarioAutomatico; cfg.SincronizacaoNuvem=SincronizacaoNuvem;
            cfg.NotificarFalhasEmail=NotificarFalhasEmail;
            await _backupService.AtualizarConfiguracaoAsync(cfg);

            foreach(var p in PerfisPermissao.Where(x=>x.Id>0 && !x.Bloqueado))
                await _permissaoService.AtualizarAsync(new PerfilPermissao
                {
                    Id=p.Id, Perfil=p.Perfil, Bloqueado=p.Bloqueado, VerAlunos=p.VerAlunos,
                    EditarAlunos=p.EditarAlunos, Financeiro=p.Financeiro, Relatorios=p.Relatorios, Configuracoes=p.Configuracoes
                });

            MostrarSucesso("Configurações guardadas com sucesso.");
        }
        catch(Exception ex) { ErroConfiguracoes=ex.Message; }
    }

    [RelayCommand]
    private async Task AlterarLogotipo()
    {
        var caminho=await _filePicker.SelecionarArquivoAsync("Selecionar logotipo", "png","jpg","jpeg","webp");
        if(!string.IsNullOrWhiteSpace(caminho)) { LogotipoPath=caminho; }
    }

    [RelayCommand]
    private void NovoUtilizador()
    {
        UtilizadorEditandoId=0; NomeUtilizador=""; CargoUtilizador=""; TelefoneUtilizador=""; PasswordUtilizador=""; PerfilUtilizadorIdTexto="";
        ModalUtilizadorVisivel=true;
    }

    [RelayCommand]
    private void EditarUtilizador(UtilizadorItemModel utilizador)
    {
        if(!_utilizadores.TryGetValue(utilizador.Id,out var u)) return;
        UtilizadorEditandoId=u.Id; NomeUtilizador=u.Nome; CargoUtilizador=u.Cargo; TelefoneUtilizador=u.Telefone;
        PasswordUtilizador=""; PerfilUtilizadorIdTexto=u.PerfilPermissaoId?.ToString() ?? ""; ModalUtilizadorVisivel=true;
    }

    [RelayCommand]
    private async Task GuardarUtilizador()
    {
        try
        {
            if(string.IsNullOrWhiteSpace(NomeUtilizador)||string.IsNullOrWhiteSpace(TelefoneUtilizador))
                throw new InvalidOperationException("Nome e telefone são obrigatórios.");

            if(UtilizadorEditandoId==0)
            {
                if(string.IsNullOrWhiteSpace(PasswordUtilizador)) throw new InvalidOperationException("A password é obrigatória para um novo utilizador.");
                var u=await _utilizadorService.CriarAsync(NomeUtilizador.Trim(),CargoUtilizador.Trim(),TelefoneUtilizador.Trim(),PasswordUtilizador,ParsePerfilId());
                _utilizadores[u.Id]=u;
            }
            else
            {
                var u=_utilizadores[UtilizadorEditandoId];
                u.Nome=NomeUtilizador.Trim(); u.Cargo=CargoUtilizador.Trim(); u.Telefone=TelefoneUtilizador.Trim(); u.PerfilPermissaoId=ParsePerfilId();
                await _utilizadorService.AtualizarAsync(u);
            }
            ModalUtilizadorVisivel=false; await InicializarAsync(); MostrarSucesso("Utilizador guardado com sucesso.");
        }
        catch(Exception ex){ErroConfiguracoes=ex.Message;}
    }

    [RelayCommand]
    private async Task DesativarUtilizador(UtilizadorItemModel utilizador)
    {
        try { await _utilizadorService.DesativarAsync(utilizador.Id); await InicializarAsync(); MostrarSucesso("Estado do utilizador atualizado."); }
        catch(Exception ex){ErroConfiguracoes=ex.Message;}
    }

    [RelayCommand]
    private async Task CriarBackup()
    {
        try { await _backupService.CriarBackupAsync(); await RecarregarBackupsAsync(); MostrarSucesso("Backup criado com sucesso."); }
        catch(Exception ex){ErroConfiguracoes=ex.Message;}
    }

    [RelayCommand]
    private async Task RestaurarBackup(BackupItemModel backup)
    {
        try { await _backupService.RestaurarAsync(backup.Id); MostrarSucesso("Backup restaurado. Reinicie a aplicação para recarregar a base de dados."); }
        catch(Exception ex){ErroConfiguracoes=ex.Message;}
    }

    [RelayCommand]
    private async Task DescarregarBackup(BackupItemModel backup)
    {
        try
        {
            if(!File.Exists(backup.Localizacao)) throw new FileNotFoundException("O ficheiro de backup não existe.",backup.Localizacao);
            var destino=await _filePicker.SelecionarDestinoAsync("Guardar cópia do backup",backup.NomeArquivo,"db");
            if(string.IsNullOrWhiteSpace(destino)) return;
            File.Copy(backup.Localizacao,destino,true); MostrarSucesso("Backup exportado com sucesso.");
        }
        catch(Exception ex){ErroConfiguracoes=ex.Message;}
    }

    [RelayCommand]
    private async Task GuardarPermissao(PermissaoPerfilModel perfil)
    {
        try
        {
            if(perfil.Bloqueado) return;
            await _permissaoService.AtualizarAsync(new PerfilPermissao
            {
                Id=perfil.Id, Perfil=perfil.Perfil, Bloqueado=perfil.Bloqueado, VerAlunos=perfil.VerAlunos,
                EditarAlunos=perfil.EditarAlunos, Financeiro=perfil.Financeiro, Relatorios=perfil.Relatorios, Configuracoes=perfil.Configuracoes
            });
            MostrarSucesso($"Permissões de {perfil.Perfil} guardadas.");
        }
        catch(Exception ex){ErroConfiguracoes=ex.Message;}
    }

    [RelayCommand] private void FecharUtilizador() => ModalUtilizadorVisivel=false;

    [RelayCommand]
    private async Task CopiarMachineId()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var clipboard = desktop.MainWindow?.Clipboard;
            if (clipboard is not null)
                await clipboard.SetTextAsync(LicencaMachineId);
        }
        MostrarSucesso("Machine ID copiado.");
    }
    [RelayCommand] private void GerarPedidoAtivacao() => MostrarSucesso("O pedido de ativação será disponibilizado com o módulo WeberTech Licensing.");
    [RelayCommand] private async Task ImportarLicenca()
    {
        var caminho=await _filePicker.SelecionarArquivoAsync("Importar licença", "wta");
        if(!string.IsNullOrWhiteSpace(caminho)) MostrarSucesso("Ficheiro de licença selecionado. A integração do provider WeberTech ainda não está ligada.");
    }

    private int? ParsePerfilId() => int.TryParse(PerfilUtilizadorIdTexto, out var id) && id > 0 ? id : null;

    private void MostrarSucesso(string msg){ SucessoConfiguracoes=msg; ErroConfiguracoes=string.Empty; }
    private static string Iniciais(string nome)
    {
        var p=nome.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        return p.Length==0 ? "?" : string.Concat(p.Take(2).Select(x=>char.ToUpperInvariant(x[0])));
    }
    private static string FormatBytes(long bytes)
    {
        string[] units={"B","KB","MB","GB","TB"}; double n=bytes; int i=0;
        while(n>=1024&&i<units.Length-1){n/=1024;i++;}
        return $"{n:N1} {units[i]}";
    }
}
