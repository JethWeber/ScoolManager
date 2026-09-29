using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoolManager.Core.Persistence.Migrations;

public partial class OneOpenCashSession : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_SessoesCaixa_Estado",
            table: "SessoesCaixa",
            column: "Estado",
            unique: true,
            filter: "[Estado] = 'Aberta'");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_SessoesCaixa_Estado",
            table: "SessoesCaixa");
    }
}