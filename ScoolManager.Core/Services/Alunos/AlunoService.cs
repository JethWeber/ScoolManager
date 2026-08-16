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
        if (aluno.Codigo == null) throw new Exception("O campo Codigo esta vindo nulo.");
        Console.WriteLine("O campo Codigo esta vindo nulo.");
        
        if (aluno.Nome == null) throw new Exception("O campo Nome esta vindo nulo.");
        
        if (aluno.DataNascimento == null) throw new Exception("O campo Data De Nascimento esta vindo nulo."); 

        if (aluno.Genero == null) throw new Exception("O campo Genero esta vindo nulo.");

        if (aluno.Nacionalidade == null) throw new Exception("O campo Nacionalidade esta vindo nulo.");

        if (aluno.Naturalidade == null) throw new Exception("O campo Naturalidade esta vindo nulo.");

        if (aluno.Provincia == null) throw new Exception("O campo Provincia esta vindo nulo.");

        if (aluno.Pais == null) throw new Exception("O campo Pais esta vindo nulo.");

        if (aluno.NumeroBiCedula == null) throw new Exception("O campo NumeroBiCedula esta vindo nulo.");

        if (aluno.Turma.Classe.Numero <= 0) throw new Exception("O campo Turma esta vindo nulo.");
        
    } 
}
/// 
/// Mapeando todos os campos da entidade aluno
/// 
/// Id, Codigo, Nome, DataNascimento, Genero, Nacionalidade, Naturalidade, Provincia, Pais, NumeroBiCedula, 