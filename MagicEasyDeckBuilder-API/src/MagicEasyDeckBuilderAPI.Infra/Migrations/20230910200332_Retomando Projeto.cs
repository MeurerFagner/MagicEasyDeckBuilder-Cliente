using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEasyDeckBuilderAPI.Infra.Migrations
{
    public partial class RetomandoProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carta_Carta_IdOutroLado",
                table: "Carta");

            migrationBuilder.DropForeignKey(
                name: "FK_CartaDeck_Carta_IdCarta",
                table: "CartaDeck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carta",
                table: "Carta");

            migrationBuilder.DropIndex(
                name: "IX_Carta_IdOutroLado",
                table: "Carta");

            migrationBuilder.DropColumn(
                name: "IdOutroLado",
                table: "Carta");

            migrationBuilder.RenameTable(
                name: "Carta",
                newName: "Cartas");

            migrationBuilder.AddColumn<string>(
                name: "Capa",
                table: "Decks",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lealdade",
                table: "Cartas",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Layout",
                table: "Cartas",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cartas",
                table: "Cartas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CartaFace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    NomeOriginal = table.Column<string>(type: "text", nullable: true),
                    Texto = table.Column<string>(type: "text", nullable: true),
                    TextoOriginal = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Cores = table.Column<string>(type: "text", nullable: true),
                    UrlImage = table.Column<string>(type: "text", nullable: true),
                    UrlCropImage = table.Column<string>(type: "text", nullable: true),
                    Poder = table.Column<string>(type: "text", nullable: true),
                    Resistencia = table.Column<string>(type: "text", nullable: true),
                    Lealdade = table.Column<string>(type: "text", nullable: true),
                    IdCarta = table.Column<Guid>(type: "uuid", nullable: false),
                    CustoMana = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaFace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartaFace_Cartas_IdCarta",
                        column: x => x.IdCarta,
                        principalTable: "Cartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartaFace_IdCarta",
                table: "CartaFace",
                column: "IdCarta");

            migrationBuilder.AddForeignKey(
                name: "FK_CartaDeck_Cartas_IdCarta",
                table: "CartaDeck",
                column: "IdCarta",
                principalTable: "Cartas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartaDeck_Cartas_IdCarta",
                table: "CartaDeck");

            migrationBuilder.DropTable(
                name: "CartaFace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cartas",
                table: "Cartas");

            migrationBuilder.DropColumn(
                name: "Capa",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Layout",
                table: "Cartas");

            migrationBuilder.RenameTable(
                name: "Cartas",
                newName: "Carta");

            migrationBuilder.AlterColumn<int>(
                name: "Lealdade",
                table: "Carta",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdOutroLado",
                table: "Carta",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carta",
                table: "Carta",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carta_IdOutroLado",
                table: "Carta",
                column: "IdOutroLado",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carta_Carta_IdOutroLado",
                table: "Carta",
                column: "IdOutroLado",
                principalTable: "Carta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartaDeck_Carta_IdCarta",
                table: "CartaDeck",
                column: "IdCarta",
                principalTable: "Carta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
