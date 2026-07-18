

namespace ScoolManager.Desktop.Models
{

/// <summary>
/// Linha da tabela de Alunos. Leve e imutável — apenas para exibição na lista.
/// TODO: mover para o ficheiro Models/AlunoListItemModel.cs próprio quando o
/// projeto crescer (mantido aqui agora porque só estes 3 ficheiros foram pedidos).
/// </summary>
public sealed class AlunoListItemModel
{
    public string NumeroEstudante { get; }
    public string Nome { get; }
    public string Classe { get; }
    public string Encarregado { get; }
    public string Telefone { get; }
    public bool Ativo { get; }

    public string Iniciais { get; }
    public string EstadoTexto => Ativo ? "Ativo" : "Inativo";

    // Cores calculadas a partir da mesma paleta usada no resto da app
    // (EmeraldBrush #34D399 / ErrorBrush #FFB4AB, ver MainWindow.axaml).
    public IBrush EstadoTextoBrush { get; }
    public IBrush EstadoFundoBrush { get; }

    public AlunoListItemModel(
        string numeroEstudante, string nome, string classe,
        string encarregado, string telefone, bool ativo)
    {
        NumeroEstudante = numeroEstudante;
        Nome = nome;
        Classe = classe;
        Encarregado = encarregado;
        Telefone = telefone;
        Ativo = ativo;

        Iniciais = string.Concat(
            nome.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(2)
                .Select(p => char.ToUpperInvariant(p[0])));

        EstadoTextoBrush = new SolidColorBrush(Color.Parse(ativo ? "#34D399" : "#FFB4AB"));
        EstadoFundoBrush = new SolidColorBrush(Color.Parse(ativo ? "#1A34D399" : "#1AFFB4AB"));
    }
}

} // fim namespace ScoolManager.Desktop.Models