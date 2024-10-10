using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HDP.Persistence.Migrations
{
    public partial class ModificacaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NomeUsuario",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "NomeUsuario",
                table: "Usuario");
        }
    }
}
