using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoolManager.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlunoSaudeNaturalidadeETipoCobranca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anulado",
                table: "Pagamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MotivoAnulacao",
                table: "Pagamentos",
                type: "TEXT",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Pagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profissao",
                table: "Encarregados",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoCondicaoMedica",
                table: "Alunos",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Naturalidade",
                table: "Alunos",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Alunos",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Alunos",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TemCondicaoMedica",
                table: "Alunos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anulado",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "MotivoAnulacao",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "Profissao",
                table: "Encarregados");

            migrationBuilder.DropColumn(
                name: "DescricaoCondicaoMedica",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Naturalidade",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "TemCondicaoMedica",
                table: "Alunos");
        }
    }
}
