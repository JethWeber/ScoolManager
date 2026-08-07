namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada quando se tenta abrir uma nova Turma (ex.: a letra "B") antes de
/// a anterior ("A") estar cheia. A mensagem já vem pronta — equivalente ao
/// que <c>TurmaNamingService.MotivoBloqueio</c> calculava e devolvia como
/// <c>string</c> solta no Desktop — para não perder a informação ao subir
/// para exceção.
/// </summary>
public sealed class AberturaDeTurmaNaoPermitidaException : ScoolManagerDomainException
{
    public AberturaDeTurmaNaoPermitidaException(string motivo) : base(motivo) { }
}
