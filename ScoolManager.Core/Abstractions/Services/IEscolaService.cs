using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Serviço do módulo Escola (View 5, ver SM_Flow.md): Turmas, Salas, Cursos,
/// Anos Lectivos, Serviços/Produtos. <see cref="CriarTurmaAsync"/> chama
/// internamente <c>TurmaNamingService</c> para calcular a letra e validar a
/// regra de lotação — quem consome este serviço não precisa de saber essa
/// lógica.
/// </summary>
public interface IEscolaService
{
    Task<IReadOnlyList<Classe>> ObterClassesAsync(CancellationToken ct = default);

    Task<IReadOnlyList<Curso>> ObterCursosAsync(CancellationToken ct = default);
    Task<Curso> CriarCursoAsync(Curso curso, CancellationToken ct = default);
    Task AtualizarCursoAsync(Curso curso, CancellationToken ct = default);
    Task RemoverCursoAsync(int id, CancellationToken ct = default);

    Task<IReadOnlyList<Sala>> ObterSalasAsync(CancellationToken ct = default);
    Task<Sala> CriarSalaAsync(Sala sala, CancellationToken ct = default);
    Task AtualizarSalaAsync(Sala sala, CancellationToken ct = default);
    Task RemoverSalaAsync(int id, CancellationToken ct = default);

    Task<IReadOnlyList<AnoLectivo>> ObterAnosLectivosAsync(CancellationToken ct = default);
    Task<AnoLectivo> CriarAnoLectivoAsync(AnoLectivo anoLectivo, CancellationToken ct = default);

    /// <summary>CORREÇÃO (gap): faltava — SM_Flow.md lista 3 modais na aba Anos Lectivos (Novo/Editar/Encerrar), só os outros dois estavam cobertos.</summary>
    Task AtualizarAnoLectivoAsync(AnoLectivo anoLectivo, CancellationToken ct = default);

    Task EncerrarAnoLectivoAsync(int id, CancellationToken ct = default);

    Task<IReadOnlyList<Turma>> ObterTurmasAsync(CancellationToken ct = default);

    /// <summary>
    /// Cria a próxima Turma para a combinação Ano Lectivo+Classe+Curso,
    /// calculando a Letra automaticamente e validando a regra de abertura.
    /// </summary>
    /// <exception cref="Exceptions.AberturaDeTurmaNaoPermitidaException">A turma anterior ainda não está cheia.</exception>
    /// <exception cref="Exceptions.LimiteDeLetrasAtingidoException">A-Z já esgotado para esta combinação.</exception>
    Task<Turma> CriarTurmaAsync(int anoLectivoId, int classeId, int? cursoId, int salaId, TurnoLetivo turno, int capacidade, CancellationToken ct = default);

    Task AtualizarTurmaAsync(Turma turma, CancellationToken ct = default);
    Task RemoverTurmaAsync(int id, CancellationToken ct = default);

    Task<char> ProximaLetraDisponivelAsync(int anoLectivoId, int classeId, int? cursoId, CancellationToken ct = default);
    Task<bool> PodeAbrirNovaTurmaAsync(int anoLectivoId, int classeId, int? cursoId, CancellationToken ct = default);

    // =================================================================
    // Serviços/Produtos (aba "Serviços" do módulo Escola) - catálogo de
    // tudo o que a escola cobra ao aluno (propinas, cartões, provas,
    // uniformes, outros), consumido pelo fluxo "Efetuar Pagamento".
    // =================================================================

    Task<IReadOnlyList<ServicoEscolar>> ObterServicosAsync(CancellationToken ct = default);
    Task<ServicoEscolar> CriarServicoAsync(ServicoEscolar servico, CancellationToken ct = default);
    Task AtualizarServicoAsync(ServicoEscolar servico, CancellationToken ct = default);

    /// <summary>
    /// Desativa (Ativo=false) ou reativa um serviço. Preferível a
    /// <see cref="RemoverServicoAsync"/> sempre que o serviço já foi usado
    /// nalgum pagamento, para não perder o histórico.
    /// </summary>
    Task DefinirAtivoServicoAsync(int id, bool ativo, CancellationToken ct = default);

    /// <summary>
    /// Elimina definitivamente um serviço. Lança
    /// <see cref="Exceptions.ScoolManagerDomainException"/> se o serviço já
    /// tiver sido usado nalgum pagamento — nesse caso use
    /// <see cref="DefinirAtivoServicoAsync"/> em vez disto.
    /// </summary>
    Task RemoverServicoAsync(int id, CancellationToken ct = default);
}
