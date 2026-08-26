namespace ScoolManager.Core.Enums;

/// <summary>
/// Categoria de um <c>ServicoEscolar</c> - as mesmas 5 categorias que hoje
/// já existem no fluxo "Efetuar Pagamento" (View 3, Detalhes do Aluno):
/// Propina, Cartão, Prova, Uniforme e Outro. Serve só para agrupar/organizar
/// a UI (ex.: qual ícone mostrar, em que secção listar); quem determina o
/// preço é sempre <c>ServicoEscolar.Preco</c>, nunca a categoria.
/// </summary>
public enum CategoriaServico
{
    Propina,
    Cartao,
    Prova,
    Uniforme,
    Outro
}
