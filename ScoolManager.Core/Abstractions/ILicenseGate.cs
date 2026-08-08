namespace ScoolManager.Core.Abstractions;

/// <summary>
/// Fronteira entre o ScoolManager.Core e o sistema de licenciamento
/// (WeberTech.Licensing). O Core não conhece RSA, QR Code, .wta ou hardware
/// — só faz perguntas simples através desta interface.
///
/// A implementação real (que chama <c>WeberTech.Licensing.Licensing.*</c>)
/// vive FORA do Core — hoje em <c>ScoolManager.Desktop/Infrastructure/
/// WeberTechLicenseGate.cs</c>, registada via DI no composition root. Nos
/// testes do Core usa-se um <c>FakeLicenseGate</c> que devolve sempre
/// válido/todas as features.
///
/// Serviços gated (<c>RelatorioService</c>, <c>FinanceiroService</c>)
/// recebem esta interface no construtor e chamam <see cref="HasFeature"/>
/// antes de executar operações do módulo correspondente, lançando
/// <c>FuncionalidadeNaoLicenciadaException</c> se não estiver licenciado —
/// isto garante que a regra é respeitada mesmo que quem chame o Core não
/// seja o Desktop (ex.: a futura API).
/// </summary>
public interface ILicenseGate
{
    /// <summary>Verdadeiro se a licença ativa é válida (não expirada, máquina correta, assinatura íntegra).</summary>
    bool IsLicenseValid { get; }

    /// <summary>
    /// Verdadeiro se o módulo indicado consta da licença ativa. Para o
    /// School Manager, os valores esperados são "Alunos", "Propinas",
    /// "Financeiro" e "Relatorios" (ver PDF de licenciamento, secção 9).
    /// </summary>
    bool HasFeature(string feature);
}
