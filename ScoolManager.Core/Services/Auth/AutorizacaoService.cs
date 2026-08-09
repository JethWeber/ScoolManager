using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Auth;

public class AutorizacaoService : IAutorizacaoService
{
    private readonly ISessaoAtualService _sessaoAtual;
    public AutorizacaoService(ISessaoAtualService sessaoAtual) => _sessaoAtual = sessaoAtual;

    public bool TemPermissao(Func<PerfilPermissao, bool> seletor)
    {
        var perfil = _sessaoAtual.UtilizadorAtual?.PerfilPermissao;
        return perfil is not null && seletor(perfil);
    }

    public void GarantirPermissao(Func<PerfilPermissao, bool> seletor, string nomePermissao)
    {
        if (!TemPermissao(seletor))
            throw new PermissaoNegadaException(nomePermissao);
    }
}
