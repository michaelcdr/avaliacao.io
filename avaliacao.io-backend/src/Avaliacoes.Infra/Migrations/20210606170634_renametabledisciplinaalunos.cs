using Microsoft.EntityFrameworkCore.Migrations;

namespace Avaliacoes.Infra.Migrations
{
    public partial class renametabledisciplinaalunos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoDisciplina_Alunos_AlunosId",
                table: "AlunoDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_AlunoDisciplina_Disciplinas_DisciplinasId",
                table: "AlunoDisciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlunoDisciplina",
                table: "AlunoDisciplina");

            migrationBuilder.RenameTable(
                name: "AlunoDisciplina",
                newName: "DisciplinasAlunos");

            migrationBuilder.RenameIndex(
                name: "IX_AlunoDisciplina_DisciplinasId",
                table: "DisciplinasAlunos",
                newName: "IX_DisciplinasAlunos_DisciplinasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisciplinasAlunos",
                table: "DisciplinasAlunos",
                columns: new[] { "AlunosId", "DisciplinasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasAlunos_Alunos_AlunosId",
                table: "DisciplinasAlunos",
                column: "AlunosId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasAlunos_Disciplinas_DisciplinasId",
                table: "DisciplinasAlunos",
                column: "DisciplinasId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasAlunos_Alunos_AlunosId",
                table: "DisciplinasAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasAlunos_Disciplinas_DisciplinasId",
                table: "DisciplinasAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisciplinasAlunos",
                table: "DisciplinasAlunos");

            migrationBuilder.RenameTable(
                name: "DisciplinasAlunos",
                newName: "AlunoDisciplina");

            migrationBuilder.RenameIndex(
                name: "IX_DisciplinasAlunos_DisciplinasId",
                table: "AlunoDisciplina",
                newName: "IX_AlunoDisciplina_DisciplinasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlunoDisciplina",
                table: "AlunoDisciplina",
                columns: new[] { "AlunosId", "DisciplinasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoDisciplina_Alunos_AlunosId",
                table: "AlunoDisciplina",
                column: "AlunosId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoDisciplina_Disciplinas_DisciplinasId",
                table: "AlunoDisciplina",
                column: "DisciplinasId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
