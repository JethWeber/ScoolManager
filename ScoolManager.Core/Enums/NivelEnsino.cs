namespace ScoolManager.Core.Enums;

/// <summary>
/// Nível de ensino de uma Classe, conforme o sistema educativo angolano:
/// Ensino Primário (1ª-6ª classe), Ensino Secundário / I Ciclo (7ª-9ª classe)
/// e Ensino Médio / II Ciclo (10ª-13ª classe, onde surge o Curso).
///
/// NOTA: o método de apresentação (label em português, "Ensino Primário"
/// etc.) NÃO sobe para o Core por design — isso é responsabilidade da UI
/// (ver ScoolManager_Core_Implementacao_Final.md, Secção 0, princípio 1).
/// </summary>
public enum NivelEnsino
{
    Primario,
    Secundario,
    Medio
}
