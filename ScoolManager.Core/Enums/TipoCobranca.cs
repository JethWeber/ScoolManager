namespace ScoolManager.Core.Enums;

/// <summary>
/// Categoria de uma cobrança/pagamento (aba "Recebimentos" da View 4 —
/// Financeiro). Confirmado pelo <c>FinanceiroViewModel</c> real do Desktop
/// (<c>OpcoesTipoCobranca</c>), que trata pagamentos de forma mais ampla do
/// que só propinas mensais.
///
/// Decisão: enum fechado (não string livre) — os 8 valores já são um
/// conjunto conhecido e fechado da instituição; um enum impede erros de
/// digitação ("Uniforme" vs "uniforme" vs "Fardamento") e permite filtrar
/// com segurança de tipos.
/// </summary>
public enum TipoCobranca
{
    Matricula,
    Propina,
    Confirmacao,
    Uniforme,
    CartaoEscolar,
    Declaracao,
    Certificado,
    Outros
}
