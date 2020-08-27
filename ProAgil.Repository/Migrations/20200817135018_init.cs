using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Local = table.Column<string>(nullable: true),
                    DataEvento = table.Column<DateTime>(nullable: false),
                    Tema = table.Column<string>(nullable: true),
                    QtdPessoas = table.Column<int>(nullable: false),
                    ImagemURL = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Palestrantes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    MiniCurriculo = table.Column<string>(nullable: true),
                    ImagemURL = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestrantes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Preco = table.Column<decimal>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: true),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    EventoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lotes_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PalestrantesEventos",
                columns: table => new
                {
                    PalestranteID = table.Column<int>(nullable: false),
                    EventoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalestrantesEventos", x => new { x.EventoID, x.PalestranteID });
                    table.ForeignKey(
                        name: "FK_PalestrantesEventos_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PalestrantesEventos_Palestrantes_PalestranteID",
                        column: x => x.PalestranteID,
                        principalTable: "Palestrantes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedesSociais",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    EventoID = table.Column<int>(nullable: true),
                    PalestranteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociais", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RedesSociais_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RedesSociais_Palestrantes_PalestranteID",
                        column: x => x.PalestranteID,
                        principalTable: "Palestrantes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_EventoID",
                table: "Lotes",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_PalestrantesEventos_PalestranteID",
                table: "PalestrantesEventos",
                column: "PalestranteID");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_EventoID",
                table: "RedesSociais",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_PalestranteID",
                table: "RedesSociais",
                column: "PalestranteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "PalestrantesEventos");

            migrationBuilder.DropTable(
                name: "RedesSociais");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Palestrantes");
        }
    }
}
