using Microsoft.EntityFrameworkCore.Migrations;

namespace Avaliacoes.Infra.Migrations
{
    public partial class ajustetabelaaluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Usuarios_UsuarioId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenador_Usuarios_UsuarioId",
                table: "Coordenador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coordenador",
                table: "Coordenador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno");

            migrationBuilder.RenameTable(
                name: "Coordenador",
                newName: "Coordenadores");

            migrationBuilder.RenameTable(
                name: "Aluno",
                newName: "Alunos");

            migrationBuilder.RenameIndex(
                name: "IX_Coordenador_UsuarioId",
                table: "Coordenadores",
                newName: "IX_Coordenadores_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_UsuarioId",
                table: "Alunos",
                newName: "IX_Alunos_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "Alunos",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coordenadores",
                table: "Coordenadores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos",
                column: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Usuarios_UsuarioId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenadores_Usuarios_UsuarioId",
                table: "Coordenadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coordenadores",
                table: "Coordenadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos");

            migrationBuilder.RenameTable(
                name: "Coordenadores",
                newName: "Coordenador");

            migrationBuilder.RenameTable(
                name: "Alunos",
                newName: "Aluno");

            migrationBuilder.RenameIndex(
                name: "IX_Coordenadores_UsuarioId",
                table: "Coordenador",
                newName: "IX_Coordenador_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Alunos_UsuarioId",
                table: "Aluno",
                newName: "IX_Aluno_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "Aluno",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coordenador",
                table: "Coordenador",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Usuarios_UsuarioId",
                table: "Aluno",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenador_Usuarios_UsuarioId",
                table: "Coordenador",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
