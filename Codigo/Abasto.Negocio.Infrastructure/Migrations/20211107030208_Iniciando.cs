using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abasto.Negocio.Infrastructure.Migrations
{
    public partial class Iniciando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingUsuario",
                columns: table => new
                {
                    usrId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usrNombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    usrEmail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    usrFechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    usrTelefono = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    usrActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingUsuario", x => x.usrId);
                });

            migrationBuilder.CreateTable(
                name: "ingPublicacion",
                columns: table => new
                {
                    pubId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usrId = table.Column<long>(type: "bigint", nullable: false),
                    pubFecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    pubDescripcion = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    pubImagen = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingPublicacion", x => x.pubId);
                    table.ForeignKey(
                        name: "FK_ingPublicacion_ingUsuario_usrId",
                        column: x => x.usrId,
                        principalTable: "ingUsuario",
                        principalColumn: "usrId");
                });

            migrationBuilder.CreateTable(
                name: "ingComentario",
                columns: table => new
                {
                    comId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pubId = table.Column<long>(type: "bigint", nullable: false),
                    usrId = table.Column<long>(type: "bigint", nullable: false),
                    comDescripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    comFecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    comActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingComentario", x => x.comId);
                    table.ForeignKey(
                        name: "FK_ingComentario_ingPublicacion_pubId",
                        column: x => x.pubId,
                        principalTable: "ingPublicacion",
                        principalColumn: "pubId");
                    table.ForeignKey(
                        name: "FK_ingComentario_ingUsuario_usrId",
                        column: x => x.usrId,
                        principalTable: "ingUsuario",
                        principalColumn: "usrId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingComentario_pubId",
                table: "ingComentario",
                column: "pubId");

            migrationBuilder.CreateIndex(
                name: "IX_ingComentario_usrId",
                table: "ingComentario",
                column: "usrId");

            migrationBuilder.CreateIndex(
                name: "IX_ingPublicacion_usrId",
                table: "ingPublicacion",
                column: "usrId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingComentario");

            migrationBuilder.DropTable(
                name: "ingPublicacion");

            migrationBuilder.DropTable(
                name: "ingUsuario");
        }
    }
}
