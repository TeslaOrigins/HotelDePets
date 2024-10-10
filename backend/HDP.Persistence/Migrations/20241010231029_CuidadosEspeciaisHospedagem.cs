using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HDP.Persistence.Migrations
{
    public partial class CuidadosEspeciaisHospedagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Hospedagemid",
                table: "CuidadosEspeciais",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CuidadosEspeciais_Hospedagemid",
                table: "CuidadosEspeciais",
                column: "Hospedagemid");

            migrationBuilder.AddForeignKey(
                name: "FK_CuidadosEspeciais_Hospedagem_Hospedagemid",
                table: "CuidadosEspeciais",
                column: "Hospedagemid",
                principalTable: "Hospedagem",
                principalColumn: "hospedagemid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuidadosEspeciais_Hospedagem_Hospedagemid",
                table: "CuidadosEspeciais");

            migrationBuilder.DropIndex(
                name: "IX_CuidadosEspeciais_Hospedagemid",
                table: "CuidadosEspeciais");

            migrationBuilder.DropColumn(
                name: "Hospedagemid",
                table: "CuidadosEspeciais");
        }
    }
}
