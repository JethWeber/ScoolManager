using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfDadosInstituicaoRepository : IDadosInstituicaoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfDadosInstituicaoRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<DadosInstituicao> ObterAsync(CancellationToken ct = default)
    {
        var dados = await _db.DadosInstituicao.FirstOrDefaultAsync(ct);

        if (dados is not null)
            return dados;

        dados = new DadosInstituicao { Id = 1 };
        _db.DadosInstituicao.Add(dados);
        await _db.SaveChangesAsync(ct);
        return dados;
    }

    public async Task AtualizarAsync(DadosInstituicao dados, CancellationToken ct = default)
    {
        // A tela envia um DTO novo sem Id. Nunca fazemos Update(dados) diretamente:
        // com uma chave int não definida, o EF pode tratá-lo como entidade nova.
        // Como DadosInstituicao é singleton, atualizamos sempre a linha existente.
        var existente = await _db.DadosInstituicao.FirstOrDefaultAsync(ct);

        if (existente is null)
        {
            dados.Id = 1;
            _db.DadosInstituicao.Add(dados);
        }
        else
        {
            existente.NomeInstituicao = dados.NomeInstituicao;
            existente.Nif = dados.Nif;
            existente.Website = dados.Website;
            existente.EmailAdministrativo = dados.EmailAdministrativo;
            existente.EnderecoCompleto = dados.EnderecoCompleto;
            existente.TelefonePrincipal = dados.TelefonePrincipal;
            existente.TelefoneSecundario = dados.TelefoneSecundario;
            existente.LogotipoPath = dados.LogotipoPath;
        }

        await _db.SaveChangesAsync(ct);
    }
}
