using System.Globalization;
using System.Text;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Services.Infra;

public class ArmazenamentoArquivosService : IArmazenamentoArquivosService
{
    // Environment.SpecialFolder.MyDocuments resolve corretamente em
    // Windows (C:\Users\<user>\Documents) e em Linux/.NET Core (~/Documents),
    // então a mesma raiz funciona em dev (Fedora) e produção (Windows).
    private static readonly string RaizApp = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "ScoolManager");

    private static readonly string PastaDados       = Path.Combine(RaizApp, "Dados");
    private static readonly string PastaBackups     = Path.Combine(RaizApp, "Backups");
    private static readonly string PastaExportacoes = Path.Combine(RaizApp, "Exportacoes");
    private static readonly string PastaLogs        = Path.Combine(RaizApp, "Logs");

    public async Task<string> SalvarDocumentoAlunoAsync(
        string anoLectivo,
        string codigoAluno,
        string nomeCompletoAluno,
        TipoDocumentoAluno tipo,
        string extensaoOriginal,
        Stream conteudo,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(anoLectivo))
            throw new ArgumentException("Ano lectivo é obrigatório.", nameof(anoLectivo));
        if (string.IsNullOrWhiteSpace(codigoAluno))
            throw new ArgumentException("Código do aluno é obrigatório.", nameof(codigoAluno));
        if (string.IsNullOrWhiteSpace(nomeCompletoAluno))
            throw new ArgumentException("Nome do aluno é obrigatório.", nameof(nomeCompletoAluno));

        var pastaAluno = ObterPastaAluno(anoLectivo, codigoAluno);
        Directory.CreateDirectory(pastaAluno);

        var (primeiro, sobrenome) = SepararNome(nomeCompletoAluno);
        var codigoPasta = SanitizarComponente(codigoAluno.Replace('/', '-'));
        var ext = extensaoOriginal.StartsWith('.') ? extensaoOriginal : $".{extensaoOriginal}";

        var nomeBase = sobrenome.Length > 0
            ? $"{tipo}_{codigoPasta}_{SanitizarComponente(primeiro)}_{SanitizarComponente(sobrenome)}{ext}"
            : $"{tipo}_{codigoPasta}_{SanitizarComponente(primeiro)}{ext}";

        var caminhoFinal = GarantirNomeUnico(Path.Combine(pastaAluno, nomeBase));

        await using (var destino = new FileStream(caminhoFinal, FileMode.CreateNew, FileAccess.Write))
        {
            await conteudo.CopyToAsync(destino, ct);
        }

        return Path.GetFileName(caminhoFinal);
    }

    public string ObterCaminhoDocumentoAluno(string anoLectivo, string codigoAluno, string nomeArquivo)
        => Path.Combine(ObterPastaAluno(anoLectivo, codigoAluno), nomeArquivo);

    public void RemoverDocumentoAluno(string anoLectivo, string codigoAluno, string nomeArquivo)
    {
        var caminho = ObterCaminhoDocumentoAluno(anoLectivo, codigoAluno, nomeArquivo);
        if (File.Exists(caminho))
            File.Delete(caminho);
    }

    public string ObterPastaBackups()     { Directory.CreateDirectory(PastaBackups);     return PastaBackups; }
    public string ObterPastaExportacoes() { Directory.CreateDirectory(PastaExportacoes); return PastaExportacoes; }
    public string ObterPastaLogs()        { Directory.CreateDirectory(PastaLogs);        return PastaLogs; }

    private static string ObterPastaAluno(string anoLectivo, string codigoAluno)
    {
        var anoPasta = anoLectivo.Replace('/', '-'); // "2025/2026" -> "2025-2026"
        var codigoPasta = SanitizarComponente(codigoAluno.Replace('/', '-')); // "2026/0003" -> "2026-0003"
        return Path.Combine(PastaDados, anoPasta, "Alunos", codigoPasta);
    }

    private static (string primeiro, string sobrenome) SepararNome(string nomeCompleto)
    {
        var partes = nomeCompleto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var primeiro = partes.Length > 0 ? partes[0] : "Aluno";
        var sobrenome = partes.Length > 1 ? partes[^1] : string.Empty;
        return (primeiro, sobrenome);
    }

    /// <summary>Remove acentos e caracteres inválidos de pasta/arquivo (Windows + Linux).</summary>
    private static string SanitizarComponente(string texto)
    {
        var semAcento = RemoverAcentos(texto);
        var invalidos = Path.GetInvalidFileNameChars();
        return new string(semAcento.Where(c => !invalidos.Contains(c)).ToArray()).Trim();
    }

    private static string RemoverAcentos(string texto)
    {
        var normalizado = texto.Normalize(NormalizationForm.FormD);
        var semAcento = new string(normalizado
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray());
        return semAcento.Normalize(NormalizationForm.FormC);
    }

    private static string GarantirNomeUnico(string caminho)
    {
        if (!File.Exists(caminho)) return caminho;

        var pasta = Path.GetDirectoryName(caminho)!;
        var nomeSemExt = Path.GetFileNameWithoutExtension(caminho);
        var ext = Path.GetExtension(caminho);

        var i = 1;
        string novo;
        do { novo = Path.Combine(pasta, $"{nomeSemExt} ({i}){ext}"); i++; }
        while (File.Exists(novo));

        return novo;
    }
}
