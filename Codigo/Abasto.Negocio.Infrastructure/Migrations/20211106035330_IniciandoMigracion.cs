using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abasto.Negocio.Infrastructure.Migrations
{
    public partial class IniciandoMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngUsuario",
                columns: table => new
                {
                    UsuId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuNombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UsuEmail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UsuFechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    UsuTelefono = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    UsuActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngUsuario", x => x.UsuId);
                });

            migrationBuilder.CreateTable(
                name: "IngPublicacion",
                columns: table => new
                {
                    PubId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuId = table.Column<long>(type: "bigint", nullable: false),
                    PubFecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    PubDescripcion = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    PubImagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngPublicacion", x => x.PubId);
                    table.ForeignKey(
                        name: "FK_IngPublicacion_IngUsuario_UsuId",
                        column: x => x.UsuId,
                        principalTable: "IngUsuario",
                        principalColumn: "UsuId");
                });

            migrationBuilder.CreateTable(
                name: "IngComentario",
                columns: table => new
                {
                    ComId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PubId = table.Column<long>(type: "bigint", nullable: false),
                    UsuId = table.Column<long>(type: "bigint", nullable: false),
                    ComDescripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ComFecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    ComActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngComentario", x => x.ComId);
                    table.ForeignKey(
                        name: "FK_IngComentario_IngPublicacion_PubId",
                        column: x => x.PubId,
                        principalTable: "IngPublicacion",
                        principalColumn: "PubId");
                    table.ForeignKey(
                        name: "FK_IngComentario_IngUsuario_UsuId",
                        column: x => x.UsuId,
                        principalTable: "IngUsuario",
                        principalColumn: "UsuId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngComentario_PubId",
                table: "IngComentario",
                column: "PubId");

            migrationBuilder.CreateIndex(
                name: "IX_IngComentario_UsuId",
                table: "IngComentario",
                column: "UsuId");

            migrationBuilder.CreateIndex(
                name: "IX_IngPublicacion_UsuId",
                table: "IngPublicacion",
                column: "UsuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngComentario");

            migrationBuilder.DropTable(
                name: "IngPublicacion");

            migrationBuilder.DropTable(
                name: "IngUsuario");
        }
    }
}
