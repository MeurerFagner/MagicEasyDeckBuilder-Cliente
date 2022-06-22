using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEasyDeckBuilderAPI.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdScryfall = table.Column<string>(type: "text", nullable: true),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    NomeOriginal = table.Column<string>(type: "text", nullable: true),
                    Texto = table.Column<string>(type: "text", nullable: true),
                    TextoOriginal = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Raridade = table.Column<string>(type: "text", nullable: true),
                    Cores = table.Column<string>(type: "text", nullable: true),
                    IdentidadeDeCor = table.Column<string>(type: "text", nullable: true),
                    Keywords = table.Column<string>(type: "text", nullable: true),
                    CustoMana = table.Column<string>(type: "text", nullable: true),
                    UrlImage = table.Column<string>(type: "text", nullable: true),
                    UrlCropImage = table.Column<string>(type: "text", nullable: true),
                    UrlApi = table.Column<string>(type: "text", nullable: true),
                    CardDuplo = table.Column<bool>(type: "boolean", nullable: false),
                    IdOutroLado = table.Column<Guid>(type: "uuid", nullable: true),
                    Poder = table.Column<string>(type: "text", nullable: true),
                    Resistencia = table.Column<string>(type: "text", nullable: true),
                    Lealdade = table.Column<int>(type: "integer", nullable: false),
                    LegalidadePorFormato = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carta_Carta_IdOutroLado",
                        column: x => x.IdOutroLado,
                        principalTable: "Carta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    TipoFormato = table.Column<string>(type: "text", nullable: true),
                    Erros = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decks_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartaDeck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDeck = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCarta = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoDeck = table.Column<string>(type: "text", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Comandante = table.Column<bool>(type: "boolean", nullable: false),
                    Erros = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartaDeck_Carta_IdCarta",
                        column: x => x.IdCarta,
                        principalTable: "Carta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartaDeck_Decks_IdDeck",
                        column: x => x.IdDeck,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carta_IdOutroLado",
                table: "Carta",
                column: "IdOutroLado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartaDeck_IdCarta",
                table: "CartaDeck",
                column: "IdCarta");

            migrationBuilder.CreateIndex(
                name: "IX_CartaDeck_IdDeck",
                table: "CartaDeck",
                column: "IdDeck");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_IdUsuario",
                table: "Decks",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartaDeck");

            migrationBuilder.DropTable(
                name: "Carta");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
