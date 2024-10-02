using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HDP.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "CuidadosEspeciais",
                columns: table => new
                {
                    cuidadosespeciaisid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    porcoespordia = table.Column<int>(type: "integer", nullable: false),
                    valorporcao = table.Column<int>(type: "integer", nullable: false),
                    descricaousomedicamento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CuidadosEspeciais_pkey", x => x.cuidadosespeciaisid);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    itemid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    preco = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    tipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.itemid);
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    tutorid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    datanascimento = table.Column<DateOnly>(type: "date", nullable: true),
                    telefone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cpf = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    rua = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cep = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    bairro = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    numero = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.tutorid);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    usuarioid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    datanascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.usuarioid);
                });

            migrationBuilder.CreateTable(
                name: "CuidadosEspeciaisItem",
                columns: table => new
                {
                    cuidadosespeciaisid = table.Column<Guid>(type: "uuid", nullable: false),
                    itemid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cuidadosespeciaisitem", x => new { x.cuidadosespeciaisid, x.itemid });
                    table.ForeignKey(
                        name: "CuidadosEspeciaisItem_cuidadosespeciaisid_fkey",
                        column: x => x.cuidadosespeciaisid,
                        principalTable: "CuidadosEspeciais",
                        principalColumn: "cuidadosespeciaisid");
                    table.ForeignKey(
                        name: "CuidadosEspeciaisItem_itemid_fkey",
                        column: x => x.itemid,
                        principalTable: "Item",
                        principalColumn: "itemid");
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    petid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    datanascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    sexo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    motivobloqueio = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    peso = table.Column<float>(type: "real", nullable: false),
                    tutorid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.petid);
                    table.ForeignKey(
                        name: "fk_tutor",
                        column: x => x.tutorid,
                        principalTable: "Tutor",
                        principalColumn: "tutorid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hospedagem",
                columns: table => new
                {
                    hospedagemid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    datainicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datafim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    datacheckin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datacheckout = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    petid = table.Column<Guid>(type: "uuid", nullable: false),
                    usuarioid = table.Column<Guid>(type: "uuid", nullable: false),
                    paga = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospedagem", x => x.hospedagemid);
                    table.ForeignKey(
                        name: "fk_pet",
                        column: x => x.petid,
                        principalTable: "Pet",
                        principalColumn: "petid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "usuarioid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dieta",
                columns: table => new
                {
                    dietaid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    preco = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    hospedagemid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Dieta_pkey", x => x.dietaid);
                    table.ForeignKey(
                        name: "fk_hospedagem",
                        column: x => x.hospedagemid,
                        principalTable: "Hospedagem",
                        principalColumn: "hospedagemid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    servicoid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    preco = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    hospedagemid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.servicoid);
                    table.ForeignKey(
                        name: "fk_hospedagem",
                        column: x => x.hospedagemid,
                        principalTable: "Hospedagem",
                        principalColumn: "hospedagemid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietaItem",
                columns: table => new
                {
                    dietaid = table.Column<Guid>(type: "uuid", nullable: false),
                    itemid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dietaitem", x => new { x.dietaid, x.itemid });
                    table.ForeignKey(
                        name: "DietaItem_dietaid_fkey",
                        column: x => x.dietaid,
                        principalTable: "Dieta",
                        principalColumn: "dietaid");
                    table.ForeignKey(
                        name: "DietaItem_itemid_fkey",
                        column: x => x.itemid,
                        principalTable: "Item",
                        principalColumn: "itemid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuidadosEspeciaisItem_itemid",
                table: "CuidadosEspeciaisItem",
                column: "itemid");

            migrationBuilder.CreateIndex(
                name: "IX_Dieta_hospedagemid",
                table: "Dieta",
                column: "hospedagemid");

            migrationBuilder.CreateIndex(
                name: "IX_DietaItem_itemid",
                table: "DietaItem",
                column: "itemid");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedagem_petid",
                table: "Hospedagem",
                column: "petid");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedagem_usuarioid",
                table: "Hospedagem",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_tutorid",
                table: "Pet",
                column: "tutorid");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_hospedagemid",
                table: "Servico",
                column: "hospedagemid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuidadosEspeciaisItem");

            migrationBuilder.DropTable(
                name: "DietaItem");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "CuidadosEspeciais");

            migrationBuilder.DropTable(
                name: "Dieta");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Hospedagem");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Tutor");
        }
    }
}
