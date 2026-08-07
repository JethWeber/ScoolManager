#!/bin/bash

echo "🚀 Criando estrutura ScoolManager.Core..."

# ==========================
# DTOs
# ==========================

mkdir -p Dtos/{Escola,Alunos,Financeiro,Relatorios,Notificacoes,Dashboard,Auth}

touch Dtos/Escola/{TurmaDto,NovaTurmaRequest,EditarTurmaRequest,CursoDto,SalaDto,AnoLectivoDto}.cs

touch Dtos/Alunos/{AlunoResumoDto,AlunoDetalheDto,NovoAlunoRequest,DocumentoAlunoDto,FiltroAlunoDto,ImportacaoAlunosResultadoDto}.cs

touch Dtos/Financeiro/{PagamentoDto,EfetuarPagamentoRequest,MovimentoCaixaDto,SessaoCaixaDto}.cs

touch Dtos/Relatorios/{MatriculaRelatorioDto,AlunoRelatorioDto,PropinaRelatorioDto,RelatorioMovimentoDto,FluxoCaixaRelatorioDto,FiltroRelatorioDto}.cs

touch Dtos/Notificacoes/NotificacaoDto.cs
touch Dtos/Dashboard/ResumoDashboardDto.cs

touch Dtos/Auth/{LoginRequest,LoginResponse}.cs


# ==========================
# Validation
# ==========================

mkdir -p Validation

touch Validation/{NovaTurmaRequestValidator,NovoAlunoRequestValidator,EfetuarPagamentoRequestValidator,LoginRequestValidator}.cs


# ==========================
# Mapping
# ==========================

mkdir -p Mapping

touch Mapping/{EscolaMappingExtensions,AlunoMappingExtensions,FinanceiroMappingExtensions,RelatorioMappingExtensions}.cs


# ==========================
# Extensions
# ==========================

mkdir -p Extensions

touch Extensions/ServiceCollectionExtensions.cs


# ==========================
# Test Project
# ==========================

mkdir -p ScoolManager.Core.Tests/Fakes
mkdir -p ScoolManager.Core.Tests/{Escola,Alunos,Financeiro,Auth}


touch ScoolManager.Core.Tests/Fakes/{InMemoryClasseRepository,InMemoryCursoRepository,InMemorySalaRepository,InMemoryAnoLectivoRepository,InMemoryTurmaRepository,InMemoryAlunoRepository,InMemoryPagamentoRepository,InMemoryMovimentoCaixaRepository,InMemorySessaoCaixaRepository,InMemoryUtilizadorRepository,FakeLicenseGate}.cs


touch ScoolManager.Core.Tests/Escola/TurmaNamingServiceTests.cs

touch ScoolManager.Core.Tests/Alunos/AlunoServiceTests.cs

touch ScoolManager.Core.Tests/Financeiro/{FinanceiroServiceTests,CaixaServiceTests}.cs

touch ScoolManager.Core.Tests/Auth/AuthServiceTests.cs


# ==========================
# Desktop Integration
# ==========================

mkdir -p ScoolManager.Desktop/Infrastructure

touch ScoolManager.Desktop/Infrastructure/WeberTechLicenseGate.cs


echo "✅ Estrutura criada com sucesso!"
echo ""
echo "📂 Nova árvore:"
tree -L 3