namespace ScoolManager.Core.Services.Auth;

/// <summary>
/// Hashing de password via BCrypt (pacote BCrypt.Net-Next, já instalado no
/// .csproj). O algoritmo embute o salt e o custo (work factor) dentro da
/// própria string de hash devolvida por HashPassword — não é preciso
/// guardar salt/iterações à parte, como seria com PBKDF2 manual.
/// </summary>
internal static class PasswordHasher
{
    /// <summary>Work factor 12 (2^12 iterações) — equilíbrio razoável entre segurança e tempo de resposta em 2026.</summary>
    private const int WorkFactor = 12;

    public static string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);

    public static bool Verify(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}
