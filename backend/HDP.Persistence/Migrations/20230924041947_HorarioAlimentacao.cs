using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HDP.Persistence.Migrations
{
    public partial class HorarioAlimentacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alimento",
                columns: table => new
                {
                    alimentoid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    quantidadeestoque = table.Column<int>(type: "integer", nullable: false),
                    precoreabastecimento = table.Column<float>(type: "real", nullable: false),
                    dataentrada = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimento", x => x.alimentoid);
                });

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    medicamentoid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    dataentrada = table.Column<DateOnly>(type: "date", nullable: true),
                    preco = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.medicamentoid);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    servicoid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    dataservico = table.Column<DateOnly>(type: "date", nullable: true),
                    preco = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.servicoid);
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    tutorid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    telefone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cpf = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    nomenormalizado = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.tutorid);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    enderecoid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    logradouro = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    cidade = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    estado = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tutorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.enderecoid);
                    table.ForeignKey(
                        name: "fk_tutor",
                        column: x => x.tutorid,
                        principalTable: "Tutor",
                        principalColumn: "tutorid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    petid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NomeNormalizado = table.Column<string>(type: "text", nullable: false),
                    idade = table.Column<int>(type: "integer", nullable: true),
                    raca = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    sexo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    especie = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    fotourl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    peso = table.Column<float>(type: "real", nullable: false),
                    tutorid = table.Column<int>(type: "integer", nullable: true)
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
                name: "Dieta",
                columns: table => new
                {
                    dietaid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    horarioAlimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    observacoes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    petid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Dieta_pkey", x => x.dietaid);
                    table.ForeignKey(
                        name: "fk_pet",
                        column: x => x.petid,
                        principalTable: "Pet",
                        principalColumn: "petid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hospedagem",
                columns: table => new
                {
                    hospedagemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    dataentrada = table.Column<DateOnly>(type: "date", nullable: true),
                    datasaida = table.Column<DateOnly>(type: "date", nullable: true),
                    observacoes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    checkin = table.Column<bool>(type: "boolean", nullable: true),
                    petid = table.Column<int>(type: "integer", nullable: false),
                    precohospedagem = table.Column<float>(type: "real", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Veterinario",
                columns: table => new
                {
                    veterinarioid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    telefone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    petid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinario", x => x.veterinarioid);
                    table.ForeignKey(
                        name: "fk_pet",
                        column: x => x.petid,
                        principalTable: "Pet",
                        principalColumn: "petid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietaAlimento",
                columns: table => new
                {
                    dietaid = table.Column<int>(type: "integer", nullable: false),
                    alimentoid = table.Column<int>(type: "integer", nullable: false),
                    dietaalimentoid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dietaalimento", x => new { x.dietaid, x.alimentoid });
                    table.ForeignKey(
                        name: "DietaAlimento_AlimentoId_fkey",
                        column: x => x.alimentoid,
                        principalTable: "Alimento",
                        principalColumn: "alimentoid");
                    table.ForeignKey(
                        name: "DietaAlimento_DietaId_fkey",
                        column: x => x.dietaid,
                        principalTable: "Dieta",
                        principalColumn: "dietaid");
                });

            migrationBuilder.CreateTable(
                name: "CuidadoEspecial",
                columns: table => new
                {
                    cuidadoespecialid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    alergias = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    condicoessaude = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    instrucoesespeciais = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    hospedagemid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuidadoEspecial", x => x.cuidadoespecialid);
                    table.ForeignKey(
                        name: "fk_hospedagem",
                        column: x => x.hospedagemid,
                        principalTable: "Hospedagem",
                        principalColumn: "hospedagemid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    reservaid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    datareserva = table.Column<DateOnly>(type: "date", nullable: true),
                    hospedagemid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.reservaid);
                    table.ForeignKey(
                        name: "Reserva_HospedagemId_fkey",
                        column: x => x.hospedagemid,
                        principalTable: "Hospedagem",
                        principalColumn: "hospedagemid");
                });

            migrationBuilder.CreateTable(
                name: "ServicoHospedagem",
                columns: table => new
                {
                    servicohospedagemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    hospedagemid = table.Column<int>(type: "integer", nullable: true),
                    servicoid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicoHospedagem", x => x.servicohospedagemid);
                    table.ForeignKey(
                        name: "ServicoHospedagem_HospedagemId_fkey",
                        column: x => x.hospedagemid,
                        principalTable: "Hospedagem",
                        principalColumn: "hospedagemid");
                    table.ForeignKey(
                        name: "ServicoHospedagem_ServicoId_fkey",
                        column: x => x.servicoid,
                        principalTable: "Servico",
                        principalColumn: "servicoid");
                });

            migrationBuilder.CreateTable(
                name: "CuidadoEspecialMedicamento",
                columns: table => new
                {
                    cemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    medicamentoid = table.Column<int>(type: "integer", nullable: true),
                    cuidadoespecialid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CuidadoEspecialMedicamento_pkey", x => x.cemid);
                    table.ForeignKey(
                        name: "CuidadoEspecialMedicamento_cuidadoespecialid_fkey",
                        column: x => x.cuidadoespecialid,
                        principalTable: "CuidadoEspecial",
                        principalColumn: "cuidadoespecialid");
                    table.ForeignKey(
                        name: "CuidadoEspecialMedicamento_MedicamentoId_fkey",
                        column: x => x.medicamentoid,
                        principalTable: "Medicamento",
                        principalColumn: "medicamentoid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuidadoEspecial_hospedagemid",
                table: "CuidadoEspecial",
                column: "hospedagemid");

            migrationBuilder.CreateIndex(
                name: "IX_CuidadoEspecialMedicamento_cuidadoespecialid",
                table: "CuidadoEspecialMedicamento",
                column: "cuidadoespecialid");

            migrationBuilder.CreateIndex(
                name: "IX_CuidadoEspecialMedicamento_medicamentoid",
                table: "CuidadoEspecialMedicamento",
                column: "medicamentoid");

            migrationBuilder.CreateIndex(
                name: "IX_Dieta_petid",
                table: "Dieta",
                column: "petid");

            migrationBuilder.CreateIndex(
                name: "IX_DietaAlimento_alimentoid",
                table: "DietaAlimento",
                column: "alimentoid");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_tutorid",
                table: "Endereco",
                column: "tutorid");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedagem_petid",
                table: "Hospedagem",
                column: "petid");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_tutorid",
                table: "Pet",
                column: "tutorid");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_hospedagemid",
                table: "Reserva",
                column: "hospedagemid");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoHospedagem_hospedagemid",
                table: "ServicoHospedagem",
                column: "hospedagemid");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoHospedagem_servicoid",
                table: "ServicoHospedagem",
                column: "servicoid");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinario_petid",
                table: "Veterinario",
                column: "petid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuidadoEspecialMedicamento");

            migrationBuilder.DropTable(
                name: "DietaAlimento");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "ServicoHospedagem");

            migrationBuilder.DropTable(
                name: "Veterinario");

            migrationBuilder.DropTable(
                name: "CuidadoEspecial");

            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "Alimento");

            migrationBuilder.DropTable(
                name: "Dieta");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Hospedagem");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Tutor");
        }
    }
}
