using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Services.Configuracoes;

public class ConfiguracaoInstitucionalService : IConfiguracaoInstitucionalService
{
    private readonly IDadosInstituicaoRepository _dados;
    private readonly IAutorizacaoService _autorizacao;

    public ConfiguracaoInstitucionalService(IDadosInstituicaoRepository dados, IAutorizacaoService autorizacao)
    {
        _dados = dados;
        _autorizacao = autorizacao;
    }

    private void GarantirAcesso() => _autorizacao.GarantirPermissao(p => p.Configuracoes, "Configuracoes");

    public Task<DadosInstituicao> ObterAsync(CancellationToken ct = default)
    {
        GarantirAcesso();
        return _dados.ObterAsync(ct);
    }

    public Task AtualizarAsync(DadosInstituicao dados, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _dados.AtualizarAsync(dados, ct);
    }
}
