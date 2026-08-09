using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoolManager.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AjustesGapsDesktop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerfilPermissaoId",
                table: "Utilizadores",
                type: "INTEGER",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "ConfiguracoesBackup",
                columns: new[] { "Id", "BackupDiarioAutomatico", "NotificarFalhasEmail", "SincronizacaoNuvem", "UltimaVerificacaoIntegridade" },
                values: new object[] { 1, false, false, false, null });

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_PerfilPermissaoId",
                table: "Utilizadores",
                column: "PerfilPermissaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizadores_PerfisPermissao_PerfilPermissaoId",
                table: "Utilizadores",
                column: "PerfilPermissaoId",
                principalTable: "PerfisPermissao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizadores_PerfisPermissao_PerfilPermissaoId",
                table: "Utilizadores");

            migrationBuilder.DropTable(
                name: "ConfiguracoesBackup");

            migrationBuilder.DropIndex(
                name: "IX_Utilizadores_PerfilPermissaoId",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "PerfilPermissaoId",
                table: "Utilizadores");
        }
    }
}
