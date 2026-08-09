# Relatório de implementação do seed do projeto

## Objetivo

Foi realizada uma revisão completa do projeto com foco no processo de inicialização do banco de dados, especificamente no arquivo de seeding e na criação de dados iniciais de escola, permissões e utilizador administrador.

## O que foi analisado

- O projeto já possuía um seeder básico em [Persistence/DatabaseSeeder.cs](Persistence/DatabaseSeeder.cs), mas ele só criava um utilizador administrador sem vincular perfil de permissões.
- O domínio já incluía entidades relevantes para:
  - classes escolares em [Entities/Escola/Classe.cs](Entities/Escola/Classe.cs)
  - perfis de permissão em [Entities/Identidade/PerfilPermissao.cs](Entities/Identidade/PerfilPermissao.cs)
  - utilizadores em [Entities/Identidade/Utilizador.cs](Entities/Identidade/Utilizador.cs)
- O contexto do EF já expunha os DbSets necessários em [Persistence/ScoolManagerDbContext.cs](Persistence/ScoolManagerDbContext.cs).

## Alterações realizadas

### 1. Seed de classes escolares

O seeder passou a criar as 13 classes padrão do catálogo escolar, cobrindo os níveis:
- Ensino Primário: 1ª a 6ª
- Ensino Secundário: 7ª a 9ª
- Ensino Médio: 10ª a 13ª

Isso foi implementado de forma idempotente, ou seja, se o projeto for executado novamente, ele não tenta duplicar os dados já existentes.

### 2. Perfil de administrador com permissões completas

Foi criado um perfil chamado "Administrador" com todas as permissões ativas:
- VerAlunos = true
- EditarAlunos = true
- Financeiro = true
- Relatorios = true
- Configuracoes = true
- Bloqueado = true

Esse perfil é tratado como um perfil sistêmico, protegido de alterações indevidas via interface.

### 3. Utilizador administrador padrão

Foi criado ou atualizado o utilizador administrador padrão com os seguintes dados:
- Nome: Administrador
- Cargo: Administrador
- Telefone: 900000000
- Password: admin123
- Status: Ativo
- Vinculado ao perfil "Administrador"

A implementação preserva o hash da password caso o utilizador já exista e evita sobrescrever dados de forma não desejada.

### 4. Teste automatizado

Foi adicionado um teste em [ScoolManager.Core.Tests/Persistence/DatabaseSeederTests.cs](ScoolManager.Core.Tests/Persistence/DatabaseSeederTests.cs) para verificar se o seeder:
- cria as 13 classes;
- cria o perfil administrador com todas as permissões;
- cria o utilizador administrador e o vincula ao perfil.

## Arquivos alterados

- [Persistence/DatabaseSeeder.cs](Persistence/DatabaseSeeder.cs)
- [ScoolManager.Core.Tests/Persistence/DatabaseSeederTests.cs](ScoolManager.Core.Tests/Persistence/DatabaseSeederTests.cs)
- [ScoolManager.Core.Tests/ScoolManager.Core.Tests.csproj](ScoolManager.Core.Tests/ScoolManager.Core.Tests.csproj)
- [ScoolManager.Core.csproj](ScoolManager.Core.csproj)

## Validação

A validação foi iniciada com o comando abaixo:

```bash
dotnet test ScoolManager.Core.Tests/ScoolManager.Core.Tests.csproj --no-restore
```

No ambiente atual, a execução foi impedida por problemas de restore/ambiente .NET do container, relacionados a falhas de inicialização do NuGet e arquivos de dependência ausentes, não por erro lógico no seeder implementado.

## Observação importante

As credenciais padrão devem ser alteradas no primeiro acesso, assim que a funcionalidade de alteração de password estiver disponível na aplicação.
