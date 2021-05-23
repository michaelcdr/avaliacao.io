using Microsoft.EntityFrameworkCore.Migrations;

namespace Avaliacoes.Infra.Migrations
{
    public partial class deletecascada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Usuarios_UsuarioId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenadores_Usuarios_UsuarioId",
                table: "Coordenadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Usuarios_UsuarioId",
                table: "Professores");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Usuarios_UsuarioId",
                table: "Alunos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenadores_Usuarios_UsuarioId",
                table: "Coordenadores",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Usuarios_UsuarioId",
                table: "Professores",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Usuarios_UsuarioId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenadores_Usuarios_UsuarioId",
                table: "Coordenadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Usuarios_UsuarioId",
                table: "Professores");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Usuarios_UsuarioId",
                table: "Alunos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenadores_Usuarios_UsuarioId",
                table: "Coordenadores",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Usuarios_UsuarioId",
                table: "Professores",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
