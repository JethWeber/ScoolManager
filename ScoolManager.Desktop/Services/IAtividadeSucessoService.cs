using System;

namespace ScoolManager.Desktop.Services;

public interface IAtividadeSucessoService
{
    bool IsVisivel { get; }
    Guid? AtividadeAtual { get; }
    string Mensagem { get; }

    void AtividadeSucedida(Guid atividade, string mensagem);
}
