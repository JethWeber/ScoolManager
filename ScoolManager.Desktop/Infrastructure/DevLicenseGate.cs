using ScoolManager.Core.Abstractions;

namespace ScoolManager.Desktop.Infrastructure;

/// <summary>
/// ⚠️ TEMPORÁRIO — só existe porque o WeberTech.Licensing ainda não está
/// pronto para ser referenciado. Devolve sempre "licenciado", para não
/// bloquear o desenvolvimento das features que dependem de ILicenseGate
/// (Financeiro, Relatórios).
///
/// SUBSTITUIR por WeberTechLicenseGate (chamando Licensing.Initialize/
/// CurrentStatus/HasFeature reais) assim que o WeberTech.Licensing tiver
/// uma versão publicável — só isso muda em App.axaml.cs, nada nos Services
/// do Core precisa de tocar.
/// </summary>
public class DevLicenseGate : ILicenseGate
{
    public bool IsLicenseValid => true;
    public bool HasFeature(string feature) => true;
}
