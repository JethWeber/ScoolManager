SCHOOL MANAGER - ARQUITETURA DE NAVEGAÇÃO (MVP SECRETARIA ESCOLAR)

OBJETIVO Sistema focado exclusivamente na Secretaria Escolar. Não inclui
módulo pedagógico (professores, disciplinas, notas, pautas, avaliações
ou horários).

================================================== RESUMO GERAL
==================================================

VIEWS PRINCIPAIS: 6 
VIEW SECUNDÁRIA: 1

Total de Views: 7

1.  Dashboard
2.  Alunos
3.  Detalhes do Aluno
4.  Financeiro
5.  Relatórios
6.  Escola
7.  Configurações

================================================== SIDEBAR
==================================================

Dashboard
Alunos
Financeiro
Relatórios
Escola
Configurações

================================================== VIEW 1 - DASHBOARD
==================================================

Objetivo: Apresentar visão geral da escola.

Indicadores: - Total de alunos - Matrículas do ano - Propinas pagas -
Propinas em atraso - Entradas - Saídas - Saldo de caixa - Últimos
pagamentos

Modais: - Detalhes de indicador - Notificações

================================================== VIEW 2 - ALUNOS
==================================================

Objetivo: Gerir todos os alunos da escola.

Elementos: - Pesquisa - Filtros - Lista de alunos - Exportação

Fluxo:

Alunos -> Clicar numa linha -> Detalhes do Aluno

Modais: 1. Nova Matrícula 2. Importar Alunos 3. Exportar PDF 4. Exportar
Excel 5. Filtros Avançados

IMPORTANTE: Cada linha da tabela é clicável. Não existem botões “Ver
Detalhes”.

================================================== VIEW 3 - DETALHES DO
ALUNO ==================================================

Objetivo: Centralizar todas as informações do aluno.

Cabeçalho: - Fotografia - Nome - Número de matrícula - Classe - Turma -
Situação

Botão discreto: <- Voltar para Alunos

Abas:

1.  Dados Pessoais
2.  Encarregado
3.  Documentação
4.  Histórico Financeiro

Ações:

-   Editar Perfil
-   Efetuar Pagamento
-   Imprimir Cartão
-   Mensagem ao Encarregado
-   Renovar Matrícula

Modais:

1.  Editar Aluno
2.  Alterar Fotografia
3.  Efetuar Pagamento
4.  Renovar Matrícula
5.  Transferir Turma
6.  Enviar Mensagem
7.  Confirmar Exclusão

IMPORTANTE:

Detalhes do Aluno é VIEW. Não é modal.

Motivo: Editar já é modal. Pagamento já é modal.

Evita Modal sobre Modal.

================================================== VIEW 4 - FINANCEIRO
==================================================

Objetivo: Controlar receitas e despesas.

Abas Internas:

1.  Pagamentos
2.  Entradas
3.  Saídas
4.  Caixa

  ----------------
  ABA PAGAMENTOS
  ----------------

Lista: - Aluno - Referência - Valor - Data

Ao clicar: -> Modal Detalhes do Pagamento

Modais:

1.  Novo Pagamento
2.  Detalhes do Pagamento
3.  Ver Recibo
4.  Imprimir Recibo

  --------------
  ABA ENTRADAS
  --------------

Modais:

1.  Nova Entrada
2.  Editar Entrada
3.  Detalhes Entrada

  ------------
  ABA SAÍDAS
  ------------

Modais:

1.  Nova Saída
2.  Editar Saída
3.  Detalhes Saída

  -----------
  ABA CAIXA
  -----------

Modais:

1.  Abrir Caixa
2.  Fechar Caixa
3.  Reabrir Caixa

IMPORTANTE:

Recibo NÃO é View.

Fluxo:

Pagamento -> Detalhes do Pagamento (Modal) -> Ver Recibo (Modal)

================================================== VIEW 5 - ESCOLA
==================================================

Objetivo: Configuração estrutural da instituição.

Abas:

1.  Classes
2.  Turmas
3.  Salas
4.  Cursos
5.  Anos Lectivos

Modais:

Classes: - Nova Classe - Editar Classe - Eliminar Classe

Turmas: - Nova Turma - Editar Turma - Eliminar Turma

Salas: - Nova Sala - Editar Sala - Eliminar Sala

Cursos: - Novo Curso - Editar Curso - Eliminar Curso

Anos Lectivos: - Novo Ano Lectivo - Editar Ano Lectivo - Encerrar Ano
Lectivo

================================================== VIEW 6 - RELATÓRIOS
==================================================

Objetivo: Gerar relatórios administrativos.

Relatórios:

-   Matrículas
-   Lista de Alunos
-   Propinas Pagas
-   Propinas em Atraso
-   Entradas
-   Saídas
-   Fluxo de Caixa

Modais:

1.  Configurar Relatório
2.  Pré-Visualizar
3.  Exportar PDF
4.  Exportar Excel
5.  Imprimir

================================================== VIEW 7 -
CONFIGURAÇÕES ==================================================

Objetivo: Configuração do sistema.

Abas:

1.  Dados da Escola
2.  Utilizadores
3.  Permissões
4.  Backup
5.  Impressão

Modais:

Dados da Escola: - Editar Dados

Utilizadores: - Novo Utilizador - Editar Utilizador - Desativar
Utilizador

Backup: - Criar Backup - Restaurar Backup

Impressão: - Configurar Impressora - Teste de Impressão

================================================== FLUXO PRINCIPAL DA
SECRETÁRIA ==================================================

Dashboard -> Alunos -> Nova Matrícula (Modal)

Dashboard -> Alunos -> Detalhes do Aluno -> Editar (Modal)

Dashboard -> Alunos -> Detalhes do Aluno -> Efetuar Pagamento (Modal)

Dashboard -> Financeiro -> Pagamentos -> Detalhes Pagamento (Modal) ->
Recibo (Modal)

Dashboard -> Escola -> Classes/Turmas/Salas

Dashboard -> Relatórios -> Exportar PDF/Excel

================================================== FILOSOFIA DO SISTEMA
==================================================

Poucas Views. Muitos Modais.

Views: - Representam áreas principais do negócio.

Modais: - Representam ações rápidas.

Benefícios: - Menos navegação. - Menos cliques. - Menos treinamento. -
Mais fácil para escolas que atualmente usam papel ou Excel.
