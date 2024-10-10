using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HDP.Persistence.Migrations
{
    public partial class SenhaAtivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Tutor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Servico",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Bloqueado",
                table: "Pet",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Item",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Dieta",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "Bloqueado",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Dieta");
        }
    }
}
