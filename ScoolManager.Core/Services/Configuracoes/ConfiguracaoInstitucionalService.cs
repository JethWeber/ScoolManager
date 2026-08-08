using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Services.Configuracoes;

public class ConfiguracaoInstitucionalService : IConfiguracaoInstitucionalService
{
    private readonly IDadosInstituicaoRepository _dados;
    public ConfiguracaoInstitucionalService(IDadosInstituicaoRepository dados) => _dados = dados;

    public Task<DadosInstituicao> ObterAsync(CancellationToken ct = default) => _dados.ObterAsync(ct);
    public Task AtualizarAsync(DadosInstituicao dados, CancellationToken ct = default) => _dados.AtualizarAsync(dados, ct);
}
