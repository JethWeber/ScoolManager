namespace ScoolManager.Core.Entities.Configuracoes;

/// <summary>
/// Dados institucionais da escola (aba "Dados da Escola" da View 7 —
/// Configurações). É um "singleton" de domínio: há sempre uma única linha
/// nesta tabela — não uma lista de escolas.
///
/// Migrado dos campos de <c>ConfiguracoesViewModel</c>
/// (NomeInstituicao, Nif, Website, EmailAdministrativo, EnderecoCompleto,
/// TelefonePrincipal, TelefoneSecundario, LogotipoPath). Os campos do
/// cartão "Estado do Sistema" (LicencaDiasRestantes, EspacoUsadoLabel,
/// EspacoTotalLabel) não sobem — licenciamento é tratado via
/// <c>ILicenseGate</c> (fora do Core) e espaço em disco é leitura direta
/// do sistema de ficheiros, não algo persistido.
/// </summary>
public class DadosInstituicao
{
    public int Id { get; set; }
    public string NomeInstituicao { get; set; } = string.Empty;
    public string Nif { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string EmailAdministrativo { get; set; } = string.Empty;
    public string EnderecoCompleto { get; set; } = string.Empty;
    public string TelefonePrincipal { get; set; } = string.Empty;
    public string? TelefoneSecundario { get; set; }

    /// <summary>Caminho/URI do logotipo carregado. Nulo enquanto não houver logotipo.</summary>
    public string? LogotipoPath { get; set; }
}
