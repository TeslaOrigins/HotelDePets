using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HDP.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        // protected override void Up(MigrationBuilder migrationBuilder)
        // {
        //     migrationBuilder.CreateTable(
        //         name: "AdicionaisVendas",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             Name = table.Column<string>(type: "text", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_AdicionaisVendas", x => x.Id);
        //         });
        //
        //     migrationBuilder.CreateTable(
        //         name: "Clientes",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             Name = table.Column<string>(type: "text", nullable: false),
        //             NomeNormalizado = table.Column<string>(type: "text", nullable: false),
        //             Cpf = table.Column<string>(type: "text", nullable: true)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_Clientes", x => x.Id);
        //         });
        //
        //     migrationBuilder.CreateTable(
        //         name: "TiposCaminhoes",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             Name = table.Column<string>(type: "text", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_TiposCaminhoes", x => x.Id);
        //         });
        //
        //     migrationBuilder.CreateTable(
        //         name: "TiposPagamentos",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             Name = table.Column<string>(type: "text", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_TiposPagamentos", x => x.Id);
        //         });
        //
        //     migrationBuilder.CreateTable(
        //         name: "Caminhoes",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
        //             PlacaCaminhao = table.Column<string>(type: "text", nullable: false),
        //             TipoCaminhaoId = table.Column<Guid>(type: "uuid", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_Caminhoes", x => x.Id);
        //             table.ForeignKey(
        //                 name: "FK_Caminhoes_Clientes_ClienteId",
        //                 column: x => x.ClienteId,
        //                 principalTable: "Clientes",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //             table.ForeignKey(
        //                 name: "FK_Caminhoes_TiposCaminhoes_TipoCaminhaoId",
        //                 column: x => x.TipoCaminhaoId,
        //                 principalTable: "TiposCaminhoes",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //         });
        //
        //     migrationBuilder.CreateTable(
        //         name: "Materiais",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             TipoCaminhaoId = table.Column<Guid>(type: "uuid", nullable: false),
        //             Name = table.Column<string>(type: "text", nullable: false),
        //             Preco = table.Column<float>(type: "real", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_Materiais", x => x.Id);
        //             table.ForeignKey(
        //                 name: "FK_Materiais_TiposCaminhoes_TipoCaminhaoId",
        //                 column: x => x.TipoCaminhaoId,
        //                 principalTable: "TiposCaminhoes",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //         });
        //
        //     migrationBuilder.CreateTable(
        //         name: "Vendas",
        //         columns: table => new
        //         {
        //             Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
        //             ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
        //             MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
        //             CaminhaoId = table.Column<Guid>(type: "uuid", nullable: false),
        //             TipoPagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
        //             AdicionaisVendaId = table.Column<Guid>(type: "uuid", nullable: false),
        //             Valor = table.Column<float>(type: "real", nullable: false),
        //             DataVenda = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_Vendas", x => x.Id);
        //             table.ForeignKey(
        //                 name: "FK_Vendas_AdicionaisVendas_AdicionaisVendaId",
        //                 column: x => x.AdicionaisVendaId,
        //                 principalTable: "AdicionaisVendas",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //             table.ForeignKey(
        //                 name: "FK_Vendas_Caminhoes_CaminhaoId",
        //                 column: x => x.CaminhaoId,
        //                 principalTable: "Caminhoes",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //             table.ForeignKey(
        //                 name: "FK_Vendas_Clientes_ClienteId",
        //                 column: x => x.ClienteId,
        //                 principalTable: "Clientes",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //             table.ForeignKey(
        //                 name: "FK_Vendas_Materiais_MaterialId",
        //                 column: x => x.MaterialId,
        //                 principalTable: "Materiais",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //             table.ForeignKey(
        //                 name: "FK_Vendas_TiposPagamentos_TipoPagamentoId",
        //                 column: x => x.TipoPagamentoId,
        //                 principalTable: "TiposPagamentos",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //         });
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Caminhoes_ClienteId",
        //         table: "Caminhoes",
        //         column: "ClienteId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Caminhoes_TipoCaminhaoId",
        //         table: "Caminhoes",
        //         column: "TipoCaminhaoId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Materiais_TipoCaminhaoId",
        //         table: "Materiais",
        //         column: "TipoCaminhaoId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Vendas_AdicionaisVendaId",
        //         table: "Vendas",
        //         column: "AdicionaisVendaId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Vendas_CaminhaoId",
        //         table: "Vendas",
        //         column: "CaminhaoId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Vendas_ClienteId",
        //         table: "Vendas",
        //         column: "ClienteId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Vendas_MaterialId",
        //         table: "Vendas",
        //         column: "MaterialId");
        //
        //     migrationBuilder.CreateIndex(
        //         name: "IX_Vendas_TipoPagamentoId",
        //         table: "Vendas",
        //         column: "TipoPagamentoId");
        // }
        //
        // protected override void Down(MigrationBuilder migrationBuilder)
        // {
        //     migrationBuilder.DropTable(
        //         name: "Vendas");
        //
        //     migrationBuilder.DropTable(
        //         name: "AdicionaisVendas");
        //
        //     migrationBuilder.DropTable(
        //         name: "Caminhoes");
        //
        //     migrationBuilder.DropTable(
        //         name: "Materiais");
        //
        //     migrationBuilder.DropTable(
        //         name: "TiposPagamentos");
        //
        //     migrationBuilder.DropTable(
        //         name: "Clientes");
        //
        //     migrationBuilder.DropTable(
        //         name: "TiposCaminhoes");
        // }
    }
}
