using System.Threading;

namespace ScoolManager.Desktop.Services;

public interface IImpressoraTermicaService
{
    Task<bool> ImprimirAsync(string texto, CancellationToken ct = default);
}
