using System.Threading;
using System.Diagnostics;
using System.Text;

namespace ScoolManager.Desktop.Services;

public sealed class ImpressoraTermicaService : IImpressoraTermicaService
{
    public async Task<bool> ImprimirAsync(string texto, CancellationToken ct = default)
    {
        if (!OperatingSystem.IsLinux())
            return false;

        var conteudo = MontarEscPos(texto);
        var impressora = await ObterImpressoraAsync(ct);

        if (string.IsNullOrWhiteSpace(impressora))
        {
            foreach (var dispositivo in new[] { "/dev/usb/lp0", "/dev/usb/lp1" })
            {
                try
                {
                    if (!File.Exists(dispositivo))
                        continue;

                    await File.WriteAllBytesAsync(dispositivo, conteudo, ct);
                    return true;
                }
                catch
                {
                    // Tenta o próximo dispositivo ou CUPS.
                }
            }

            return false;
        }

        var ficheiro = Path.Combine(Path.GetTempPath(), $"scoolmanager-recibo-{Guid.NewGuid():N}.txt");

        try
        {
            await File.WriteAllBytesAsync(ficheiro, conteudo, ct);

            var resultado = await ExecutarAsync(
                "lp",
                $"-d \"{EscaparArgumento(impressora)}\" -o raw \"{EscaparArgumento(ficheiro)}\"",
                ct);

            return resultado.ExitCode == 0;
        }
        finally
        {
            try { if (File.Exists(ficheiro)) File.Delete(ficheiro); } catch { }
        }
    }

    private static async Task<string?> ObterImpressoraAsync(CancellationToken ct)
    {
        var padrao = await ExecutarAsync("lpstat", "-d", ct);
        if (padrao.ExitCode == 0)
        {
            var linha = padrao.StdOut
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(x => x.Contains(':'));

            if (linha is not null)
            {
                var nome = linha[(linha.IndexOf(':') + 1)..].Trim();
                if (!string.IsNullOrWhiteSpace(nome) && !nome.Contains("no system default"))
                    return nome;
            }
        }

        var lista = await ExecutarAsync("lpstat", "-p", ct);
        if (lista.ExitCode == 0)
        {
            var linha = lista.StdOut
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(x => x.StartsWith("printer ", StringComparison.OrdinalIgnoreCase));

            if (linha is not null)
            {
                var partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length >= 2)
                    return partes[1];
            }
        }

        return null;
    }

    private static byte[] MontarEscPos(string texto)
    {
        var ascii = RemoverAcentos(texto);
        using var ms = new MemoryStream();
        ms.Write([0x1B, 0x40]); // inicializar
        ms.Write(Encoding.ASCII.GetBytes(ascii));
        ms.Write([0x0A, 0x0A, 0x0A]);
        ms.Write([0x1D, 0x56, 0x00]); // corte total, se suportado
        return ms.ToArray();
    }

    private static string RemoverAcentos(string texto)
    {
        var normalizado = texto.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalizado.Length);
        foreach (var c in normalizado)
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) !=
                System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    private static string EscaparArgumento(string valor) =>
        valor.Replace("\\", "\\\\").Replace("\"", "\\\"");

    private static async Task<(int ExitCode, string StdOut, string StdErr)> ExecutarAsync(
        string comando, string argumentos, CancellationToken ct)
    {
        try
        {
            using var processo = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = comando,
                    Arguments = argumentos,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            processo.Start();
            var stdout = await processo.StandardOutput.ReadToEndAsync(ct);
            var stderr = await processo.StandardError.ReadToEndAsync(ct);
            await processo.WaitForExitAsync(ct);
            return (processo.ExitCode, stdout, stderr);
        }
        catch
        {
            return (-1, string.Empty, string.Empty);
        }
    }
}
