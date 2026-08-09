using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Services.Auth;

public class SessaoAtualService : ISessaoAtualService
{
    public Utilizador? UtilizadorAtual { get; private set; }

    public void IniciarSessao(Utilizador utilizador) => UtilizadorAtual = utilizador;
    public void EncerrarSessao() => UtilizadorAtual = null;
}
