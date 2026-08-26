using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Escola;

/// <summary>
/// Um Serviço/Produto escolar é qualquer item que a escola cobra ou vende
/// aos alunos na secção de Pagamentos (ex.: "Propina Mensal", "Cartão de
/// Estudante - 1ª Via", "Exame de Recuperação", "Uniforme Completo - M").
/// Cada linha tem o seu próprio nome e preço; a <see cref="Categoria"/> só
/// agrupa/organiza a UI (mesmas 5 categorias que já existiam, "hard-coded",
/// no fluxo "Efetuar Pagamento": Propina, Cartão, Prova, Uniforme, Outro).
///
/// Este catálogo substitui os preços que estavam fixos no código
/// (<c>AlunoPagamentosViewModel</c>, ex.: <c>ValorMensalidade</c> e os
/// vários <c>switch</c> de preço por Motivo/Tipo) - a partir daqui, quem
/// gere os preços é a secretaria, através deste CRUD, e não um recompilar
/// da aplicação.
/// </summary>
public class ServicoEscolar
{
    public int Id { get; set; }

    /// <summary>Nome exibido no formulário de pagamento (ex.: "Cartão de Estudante - 1ª Via").</summary>
    public string Nome { get; set; } = string.Empty;

    public CategoriaServico Categoria { get; set; }

    public decimal Preco { get; set; }

    /// <summary>Notas internas opcionais sobre o serviço (não aparece necessariamente ao aluno).</summary>
    public string? Descricao { get; set; }

    /// <summary>
    /// Falso = "desativado": deixa de aparecer como opção nova no formulário
    /// de pagamento, mas o registo mantém-se na base de dados - os
    /// pagamentos já feitos com este serviço não podem perder a referência
    /// ao preço/nome que tinham na altura. Por isso NÃO existe eliminação
    /// automática ao "descontinuar" um serviço: usa-se este campo.
    /// Eliminar (hard delete) só é permitido para um serviço que nunca foi
    /// usado em nenhum pagamento.
    /// </summary>
    public bool Ativo { get; set; } = true;
}
