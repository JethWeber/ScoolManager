using Avalonia.Media;

namespace ScoolManager.Desktop.Models;

/// <summary>
/// Representa uma linha da tabela de Alunos.
/// NOTA: "Codigo" é o identificador/código do aluno (antigo "NumeroEstudante").
/// "Encarregado" e "Telefone" continuam a existir no modelo (úteis para a
/// pesquisa e para a futura view de Detalhes do Aluno) mesmo não sendo mais
/// exibidos como colunas na tabela principal.
/// </summary>
public record AlunoListItemModel(
    string Codigo,
    string Nome,
    string Classe,
    string Curso,
    string Sala,
    string Encarregado,
    string Telefone,
    bool Ativo)
{
    public string Iniciais
    {
        get
        {
            var partes = Nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 0) return "?";
            if (partes.Length == 1) return partes[0][..1].ToUpperInvariant();
            return (partes[0][..1] + partes[^1][..1]).ToUpperInvariant();
        }
    }

    public string EstadoTexto => Ativo ? "Ativo" : "Inativo";

    public IBrush EstadoFundoBrush => Ativo
        ? new SolidColorBrush(Color.Parse("#1A34D399"))
        : new SolidColorBrush(Color.Parse("#1AFFB4AB"));

    public IBrush EstadoTextoBrush => Ativo
        ? new SolidColorBrush(Color.Parse("#34D399"))
        : new SolidColorBrush(Color.Parse("#FFB4AB"));
}
