# Propina passa a ser por Turma — o que mudou e o que falta correr

## Ideia
A propina não tem um valor único por Classe: a mesma Classe pode ter vários
Cursos com propinas diferentes (ex.: 10ª GRSI A = 18.000 Kz, 10ª CFB A =
20.000 Kz). Por isso o preço passa a fixar-se por **Turma** (a combinação já
concreta Classe+Curso+Letra), não por Classe isolada.

## O que mudou

### Core (`core/`)
- `ServicoEscolar.cs` — novos campos `TurmaId` (int?) e `Turma` (navegação).
  Obrigatório quando `Categoria = Propina`; sempre `null` nas outras
  categorias.
- `ServicoEscolarConfiguration.cs` — FK para `Turma`, `DeleteBehavior.Restrict`
  (não deixa apagar uma Turma enquanto houver uma propina a apontar para
  ela — a secretaria tem de desativar/eliminar a propina primeiro).
- `EfServicoEscolarRepository.cs` — agora inclui `Turma.Classe`/`Turma.Curso`
  nas leituras, para o `Turma.Nome` (propriedade calculada) vir sempre
  pronto a mostrar.
- `EscolaService.cs` — `CriarServicoAsync`/`AtualizarServicoAsync` chamam
  `ValidarTurmaDoServicoAsync`: se `Categoria=Propina` exige uma Turma
  válida (existente na BD); nas outras categorias, limpa qualquer
  `TurmaId` que tenha vindo por engano.
- `DatabaseSeeder.cs` — a propina semeada deixa de ser genérica
  ("Propina Mensal") e passa a apontar para a turma semeada
  (`Propina - <Nome da Turma>`), mesmo preço de antes (15.000 Kz).

### Desktop (`desktop/`)
- `EscolaModel.cs` — `ServicoEscolarModel` ganha `TurmaId`/`TurmaNome`.
- `EscolaViewModel.cs`:
  - `TurmasOpcoes` — todas as turmas da BD (sem filtro de pesquisa), para
    o ComboBox do modal.
  - `PropinaAplicavel` — verdadeiro quando a categoria escolhida é Propina;
    controla a visibilidade do campo Turma.
  - `FormTurmaSelecionadaServico` + validação em `SalvarServico` (exige
    turma quando `PropinaAplicavel`).
  - `Mapear(ServicoEscolar)` traz `TurmaId`/`TurmaNome`.
- `EscolaView.axaml`:
  - Cartão de serviço mostra o nome da turma (quando aplicável), a par da
    categoria.
  - Modal Novo/Editar Serviço ganha o ComboBox "Turma", só visível quando
    `PropinaAplicavel` — e passou a ter `ScrollViewer` (como o modal de
    Turma), porque cresceu.

## Migração necessária
Como a entidade `ServicoEscolar` ganhou uma coluna nova (`TurmaId`) e uma FK,
é preciso gerar mais uma migração (a partir da pasta `ScoolManager.Core/`):

```bash
dotnet ef migrations add PropinaPorTurma \
  -p ScoolManager.Core \
  -s ScoolManager.Core \
  -o Persistence/Migrations

dotnet ef database update \
  -p ScoolManager.Core \
  -s ScoolManager.Core
```

Se já tiverem dados de teste na tabela `ServicosEscolares` de antes desta
mudança (ex.: a "Propina Mensal" genérica do seed antigo), essa linha vai
ficar com `TurmaId = NULL` depois da migração — o que quebra a regra
(Propina exige Turma). Mais simples: apagar o `scoolmanager.db` local e
deixar o seed recriar tudo do zero, como fizeram da última vez.

## Por fazer a seguir
Ainda falta ligar o `AlunoPagamentosViewModel` ao catálogo — é o próximo
passo natural, agora com a informação extra de que a Propina, ao ser
escolhida no pagamento, deve filtrar os serviços pela Turma do aluno em
causa (não mostrar propinas de outras turmas).
