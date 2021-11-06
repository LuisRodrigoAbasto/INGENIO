using Microsoft.EntityFrameworkCore.Migrations;

namespace Abasto.Negocio.Infrastructure.Migrations
{
    public partial class Cambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngComentario_IngPublicacion_PubId",
                table: "IngComentario");

            migrationBuilder.DropForeignKey(
                name: "FK_IngComentario_IngUsuario_UsuId",
                table: "IngComentario");

            migrationBuilder.DropForeignKey(
                name: "FK_IngPublicacion_IngUsuario_UsuId",
                table: "IngPublicacion");

            migrationBuilder.AlterColumn<string>(
                name: "PubImagen",
                table: "IngPublicacion",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IngComentario_IngPublicacion_PubId",
                table: "IngComentario",
                column: "PubId",
                principalTable: "IngPublicacion",
                principalColumn: "PubId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngComentario_IngUsuario_UsuId",
                table: "IngComentario",
                column: "UsuId",
                principalTable: "IngUsuario",
                principalColumn: "UsuId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngPublicacion_IngUsuario_UsuId",
                table: "IngPublicacion",
                column: "UsuId",
                principalTable: "IngUsuario",
                principalColumn: "UsuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngComentario_IngPublicacion_PubId",
                table: "IngComentario");

            migrationBuilder.DropForeignKey(
                name: "FK_IngComentario_IngUsuario_UsuId",
                table: "IngComentario");

            migrationBuilder.DropForeignKey(
                name: "FK_IngPublicacion_IngUsuario_UsuId",
                table: "IngPublicacion");

            migrationBuilder.AlterColumn<string>(
                name: "PubImagen",
                table: "IngPublicacion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IngComentario_IngPublicacion_PubId",
                table: "IngComentario",
                column: "PubId",
                principalTable: "IngPublicacion",
                principalColumn: "PubId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngComentario_IngUsuario_UsuId",
                table: "IngComentario",
                column: "UsuId",
                principalTable: "IngUsuario",
                principalColumn: "UsuId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngPublicacion_IngUsuario_UsuId",
                table: "IngPublicacion",
                column: "UsuId",
                principalTable: "IngUsuario",
                principalColumn: "UsuId");
        }
    }
}
