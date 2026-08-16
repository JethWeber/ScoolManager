using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Alunos;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Alunos;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunos;
    private readonly ITurmaRepository _turmas;
    private readonly IAutorizacaoService _autorizacao;

    public AlunoService(IAlunoRepository alunos, ITurmaRepository turmas, IAutorizacaoService autorizacao)
    {
        _alunos = alunos;
        _turmas = turmas;
        _autorizacao = autorizacao;
    }

    public Task<IReadOnlyList<Aluno>> ObterListaAsync(FiltroAlunoDto filtro, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.VerAlunos, "Ver Alunos");
        return _alunos.ObterPorFiltroAsync(filtro, ct);
    }

    public async Task<Aluno> ObterDetalhesAsync(int id, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.VerAlunos, "Ver Alunos");
        return await _alunos.ObterPorIdAsync(id, ct) ?? throw new EntidadeNaoEncontradaException(nameof(Aluno), id);
    }

    public async Task<Aluno> CriarAsync(Aluno aluno, IEnumerable<Encarregado> encarregados, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.EditarAlunos, "Editar Alunos");

        var turma = await _turmas.ObterPorIdAsync(aluno.TurmaId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Turma), aluno.TurmaId);

        if (turma.EstaCheia)
            throw new InvalidOperationException($"A turma {turma.Nome} está cheia — não é possível matricular mais alunos.");

        aluno.Encarregados = encarregados.ToList();
        ValidarCampos(aluno);
        var criado = await _alunos.AdicionarAsync(aluno, ct);

        turma.Matriculados++;
        await _turmas.AtualizarAsync(turma, ct);

        return criado;
    }

    public Task AtualizarAsync(Aluno aluno, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.EditarAlunos, "Editar Alunos");
        return _alunos.AtualizarAsync(aluno, ct);
    }

    public Task RemoverAsync(int id, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.EditarAlunos, "Editar Alunos");
        return _alunos.RemoverAsync(id, ct);
    }

    public async Task RenovarMatriculaAsync(int alunoId, int novoAnoLectivoId, int novaTurmaId, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.EditarAlunos, "Editar Alunos");

        var aluno = await _alunos.ObterPorIdAsync(alunoId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Aluno), alunoId);

        var novaTurma = await _turmas.ObterPorIdAsync(novaTurmaId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Turma), novaTurmaId);

        if (novaTurma.EstaCheia)
            throw new InvalidOperationException($"A turma {novaTurma.Nome} está cheia — não é possível renovar para esta turma.");

        aluno.AnoLectivoId = novoAnoLectivoId;
        aluno.TurmaId = novaTurmaId;
        aluno.DataMatricula = DateTime.Now;

        await _alunos.AtualizarAsync(aluno, ct);
    }

    public async Task AdicionarDocumentoAsync(int alunoId, DocumentoAluno documento, CancellationToken ct = default)
    {
        _autorizacao.GarantirPermissao(p => p.EditarAlunos, "Editar Alunos");

        var aluno = await _alunos.ObterPorIdAsync(alunoId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Aluno), alunoId);

        documento.AlunoId = alunoId;
        aluno.Documentos.Add(documento);
        await _alunos.AtualizarAsync(aluno, ct);
    }

    public Task<ImportacaoAlunosResultadoDto> ImportarAsync(Stream arquivo, CancellationToken ct = default)
    {
        // PENDÊNCIA (ver roteiro, Secção 11): o formato do ficheiro
        // (CSV/Excel) e o mapeamento de colunas não estão especificados —
        // o SM_Flow.md só confirma que o modal "Importar Alunos" existe,
        // sem detalhar o formato esperado. Devolve resultado vazio em vez
        // de lançar, para não bloquear a composição do serviço; implementar
        // a leitura real quando o formato for confirmado.
        return Task.FromResult(new ImportacaoAlunosResultadoDto());
    }

    public void ValidarCampos(Aluno aluno)
    {
        if (string.IsNullOrWhiteSpace(aluno.Codigo))
            throw new InvalidOperationException("O código do aluno é obrigatório.");

        if (string.IsNullOrWhiteSpace(aluno.Nome))
            throw new InvalidOperationException("O nome do aluno é obrigatório.");

        if (aluno.DataNascimento is null)
            throw new InvalidOperationException("A data de nascimento é obrigatória.");

        if (string.IsNullOrWhiteSpace(aluno.Genero))
            throw new InvalidOperationException("O género é obrigatório.");

        if (string.IsNullOrWhiteSpace(aluno.Naturalidade))
            throw new InvalidOperationException("A naturalidade é obrigatória.");

        if (string.IsNullOrWhiteSpace(aluno.Provincia))
            throw new InvalidOperationException("A província é obrigatória.");

        if (string.IsNullOrWhiteSpace(aluno.Pais))
            throw new InvalidOperationException("O país é obrigatório.");

        if (string.IsNullOrWhiteSpace(aluno.NumeroBiCedula))
            throw new InvalidOperationException("O número do BI/Cédula é obrigatório.");

        if (string.IsNullOrWhiteSpace(aluno.Endereco))
            throw new InvalidOperationException("A morada é obrigatória.");

        if (aluno.TurmaId <= 0)
            throw new InvalidOperationException("A turma é obrigatória.");

        // Nacionalidade: se a entidade exigir, usa País como fallback
        if (string.IsNullOrWhiteSpace(aluno.Nacionalidade))
            aluno.Nacionalidade = aluno.Pais;

        // Doença: só valida descrição se TemCondicaoMedica == true
        if (aluno.TemCondicaoMedica && string.IsNullOrWhiteSpace(aluno.DescricaoCondicaoMedica))
            throw new InvalidOperationException("Indique qual a doença/condição médica.");

        // Pelo menos um encarregado
        if (aluno.Encarregados is null || aluno.Encarregados.Count == 0)
            throw new InvalidOperationException("Indique pelo menos o nome do pai ou da mãe.");

        // BI/Cédula obrigatório nos documentos
        var temBi = aluno.Documentos?.Any(d => d.Tipo == ScoolManager.Core.Enums.TipoDocumentoAluno.BiCedula
            && !string.IsNullOrWhiteSpace(d.NomeArquivo)) == true;

        if (!temBi)
            throw new InvalidOperationException("O documento BI/Cédula é obrigatório.");
    } 
}
/// 
/// Mapeando todos os campos da entidade aluno
/// 
/// Id, Codigo, Nome, DataNascimento, Genero, Nacionalidade, Naturalidade, Provincia, Pais, NumeroBiCedula, 