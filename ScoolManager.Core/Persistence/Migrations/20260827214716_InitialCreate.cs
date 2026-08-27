using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoolManager.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnosLectivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnosLectivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeArquivo = table.Column<string>(type: "TEXT", maxLength: 260, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TamanhoBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    Localizacao = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false),
                    EhNaNuvem = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Nivel = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesBackup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BackupDiarioAutomatico = table.Column<bool>(type: "INTEGER", nullable: false),
                    SincronizacaoNuvem = table.Column<bool>(type: "INTEGER", nullable: false),
                    NotificarFalhasEmail = table.Column<bool>(type: "INTEGER", nullable: false),
                    UltimaVerificacaoIntegridade = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesBackup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Sigla = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DadosInstituicao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeInstituicao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Nif = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAdministrativo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EnderecoCompleto = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false),
                    TelefonePrincipal = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    TelefoneSecundario = table.Column<string>(type: "TEXT", nullable: true),
                    LogotipoPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosInstituicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Lida = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfisPermissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Perfil = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Bloqueado = table.Column<bool>(type: "INTEGER", nullable: false),
                    VerAlunos = table.Column<bool>(type: "INTEGER", nullable: false),
                    EditarAlunos = table.Column<bool>(type: "INTEGER", nullable: false),
                    Financeiro = table.Column<bool>(type: "INTEGER", nullable: false),
                    Relatorios = table.Column<bool>(type: "INTEGER", nullable: false),
                    Configuracoes = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfisPermissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Capacidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Bloco = table.Column<string>(type: "TEXT", nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    UltimoAcesso = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    PerfilPermissaoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilizadores_PerfisPermissao_PerfilPermissaoId",
                        column: x => x.PerfilPermissaoId,
                        principalTable: "PerfisPermissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnoLectivoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClasseId = table.Column<int>(type: "INTEGER", nullable: false),
                    CursoId = table.Column<int>(type: "INTEGER", nullable: true),
                    Letra = table.Column<char>(type: "TEXT", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Turno = table.Column<string>(type: "TEXT", nullable: false),
                    Capacidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Matriculados = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_AnosLectivos_AnoLectivoId",
                        column: x => x.AnoLectivoId,
                        principalTable: "AnosLectivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turmas_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turmas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turmas_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessoesCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataAbertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    UtilizadorAberturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UtilizadorFechamentoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessoesCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessoesCaixa_Utilizadores_UtilizadorAberturaId",
                        column: x => x.UtilizadorAberturaId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessoesCaixa_Utilizadores_UtilizadorFechamentoId",
                        column: x => x.UtilizadorFechamentoId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Genero = table.Column<string>(type: "TEXT", nullable: true),
                    Nacionalidade = table.Column<string>(type: "TEXT", nullable: true),
                    Naturalidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Provincia = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Pais = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    NumeroBiCedula = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    FotografiaCaminho = table.Column<string>(type: "TEXT", nullable: true),
                    TemCondicaoMedica = table.Column<bool>(type: "INTEGER", nullable: false),
                    DescricaoCondicaoMedica = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    TurmaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AnoLectivoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_AnosLectivos_AnoLectivoId",
                        column: x => x.AnoLectivoId,
                        principalTable: "AnosLectivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alunos_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicosEscolares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    TurmaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosEscolares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicosEscolares_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimentosCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    SessaoCaixaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentosCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentosCaixa_SessoesCaixa_SessaoCaixaId",
                        column: x => x.SessaoCaixaId,
                        principalTable: "SessoesCaixa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosAluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    NomeArquivo = table.Column<string>(type: "TEXT", maxLength: 260, nullable: true),
                    DataUpload = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosAluno_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encarregados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Contacto = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Profissao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encarregados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encarregados_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MesReferencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroRecibo = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    Anulado = table.Column<bool>(type: "INTEGER", nullable: false),
                    MotivoAnulacao = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    MetodoPagamento = table.Column<string>(type: "TEXT", nullable: true),
                    SessaoCaixaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamentos_SessoesCaixa_SessaoCaixaId",
                        column: x => x.SessaoCaixaId,
                        principalTable: "SessoesCaixa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AnosLectivos",
                columns: new[] { "Id", "DataInicio", "DataTermino", "Estado", "Nome" },
                values: new object[] { 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aberto", "2025/2026" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Nivel", "Numero" },
                values: new object[,]
                {
                    { 1, "Primario", 1 },
                    { 2, "Primario", 2 },
                    { 3, "Primario", 3 },
                    { 4, "Primario", 4 },
                    { 5, "Primario", 5 },
                    { 6, "Primario", 6 },
                    { 7, "Secundario", 7 },
                    { 8, "Secundario", 8 },
                    { 9, "Secundario", 9 },
                    { 10, "Medio", 10 },
                    { 11, "Medio", 11 },
                    { 12, "Medio", 12 },
                    { 13, "Medio", 13 }
                });

            migrationBuilder.InsertData(
                table: "ConfiguracoesBackup",
                columns: new[] { "Id", "BackupDiarioAutomatico", "NotificarFalhasEmail", "SincronizacaoNuvem", "UltimaVerificacaoIntegridade" },
                values: new object[] { 1, false, false, false, null });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome", "Sigla" },
                values: new object[,]
                {
                    { 1, "Gestão de Redes e Sistemas Informáticos", "GRSI" },
                    { 2, "Gestão de Recursos Humanos", "GRH" },
                    { 3, "Gestão Empresarial", "GE" },
                    { 4, "Ciências Físicas e Biológicas", "CFB" },
                    { 5, "Ciências Jurídicas", "CJ" }
                });

            migrationBuilder.InsertData(
                table: "DadosInstituicao",
                columns: new[] { "Id", "EmailAdministrativo", "EnderecoCompleto", "LogotipoPath", "Nif", "NomeInstituicao", "TelefonePrincipal", "TelefoneSecundario", "Website" },
                values: new object[] { 1, "", "", null, "", "", "", null, null });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "Id", "Bloco", "Capacidade", "Nome", "Observacoes" },
                values: new object[,]
                {
                    { 1, "Bloco A", 40, "Sala 01", null },
                    { 2, "Bloco A", 40, "Sala 04", null },
                    { 3, "Bloco B", 40, "Sala 08", null },
                    { 4, "Bloco B", 40, "Sala 12", null },
                    { 5, "Bloco C", 25, "Lab Info 2", "Computadores - requer marcação prévia" },
                    { 6, "Bloco C", 30, "Oficina B", null }
                });

            migrationBuilder.InsertData(
                table: "Turmas",
                columns: new[] { "Id", "AnoLectivoId", "Capacidade", "ClasseId", "CursoId", "Letra", "Matriculados", "SalaId", "Turno" },
                values: new object[,]
                {
                    { 1, 1, 40, 7, null, 'A', 24, 1, "Manha" },
                    { 2, 1, 40, 7, null, 'B', 36, 1, "Tarde" },
                    { 3, 1, 25, 10, 1, 'A', 25, 5, "Noite" },
                    { 4, 1, 40, 10, 1, 'B', 28, 4, "Tarde" },
                    { 5, 1, 40, 10, 4, 'A', 32, 2, "Manha" },
                    { 6, 1, 40, 11, 4, 'A', 28, 4, "Tarde" },
                    { 7, 1, 40, 12, 3, 'A', 40, 3, "Manha" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_AnoLectivoId",
                table: "Alunos",
                column: "AnoLectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Codigo",
                table: "Alunos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaId",
                table: "Alunos",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosAluno_AlunoId",
                table: "DocumentosAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Encarregados_AlunoId",
                table: "Encarregados",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosCaixa_SessaoCaixaId",
                table: "MovimentosCaixa",
                column: "SessaoCaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_AlunoId",
                table: "Pagamentos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_SessaoCaixaId",
                table: "Pagamentos",
                column: "SessaoCaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicosEscolares_TurmaId",
                table: "ServicosEscolares",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_SessoesCaixa_UtilizadorAberturaId",
                table: "SessoesCaixa",
                column: "UtilizadorAberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_SessoesCaixa_UtilizadorFechamentoId",
                table: "SessoesCaixa",
                column: "UtilizadorFechamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_AnoLectivoId",
                table: "Turmas",
                column: "AnoLectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ClasseId",
                table: "Turmas",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_SalaId",
                table: "Turmas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_PerfilPermissaoId",
                table: "Utilizadores",
                column: "PerfilPermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_Telefone",
                table: "Utilizadores",
                column: "Telefone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Backups");

            migrationBuilder.DropTable(
                name: "ConfiguracoesBackup");

            migrationBuilder.DropTable(
                name: "DadosInstituicao");

            migrationBuilder.DropTable(
                name: "DocumentosAluno");

            migrationBuilder.DropTable(
                name: "Encarregados");

            migrationBuilder.DropTable(
                name: "MovimentosCaixa");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "ServicosEscolares");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "SessoesCaixa");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "AnosLectivos");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "PerfisPermissao");
        }
    }
}
