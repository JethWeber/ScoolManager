using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IServicoEscolarRepository
{
    Task<IReadOnlyList<ServicoEscolar>> ObterTodosAsync(CancellationToken ct = default);
    Task<ServicoEscolar?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<ServicoEscolar> AdicionarAsync(ServicoEscolar servico, CancellationToken ct = default);
    Task AtualizarAsync(ServicoEscolar servico, CancellationToken ct = default);

    /// <summary>
    /// Hard delete. Chamador (EscolaService) é responsável por só invocar
    /// isto quando o serviço nunca foi usado em nenhum pagamento - caso
    /// contrário deve usar AtualizarAsync com Ativo=false.
    /// </summary>
    Task RemoverAsync(int id, CancellationToken ct = default);
}
