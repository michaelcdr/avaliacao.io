using Microsoft.EntityFrameworkCore.Migrations;

namespace Avaliacoes.Infra.Migrations
{
    public partial class renametabledisciplinasprofessores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaProfessor_Disciplinas_DisciplinasId",
                table: "DisciplinaProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaProfessor_Professores_ProfessoresId",
                table: "DisciplinaProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisciplinaProfessor",
                table: "DisciplinaProfessor");

            migrationBuilder.RenameTable(
                name: "DisciplinaProfessor",
                newName: "DisciplinasProfessores");

            migrationBuilder.RenameIndex(
                name: "IX_DisciplinaProfessor_ProfessoresId",
                table: "DisciplinasProfessores",
                newName: "IX_DisciplinasProfessores_ProfessoresId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisciplinasProfessores",
                table: "DisciplinasProfessores",
                columns: new[] { "DisciplinasId", "ProfessoresId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasProfessores_Disciplinas_DisciplinasId",
                table: "DisciplinasProfessores",
                column: "DisciplinasId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasProfessores_Professores_ProfessoresId",
                table: "DisciplinasProfessores",
                column: "ProfessoresId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasProfessores_Disciplinas_DisciplinasId",
                table: "DisciplinasProfessores");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasProfessores_Professores_ProfessoresId",
                table: "DisciplinasProfessores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisciplinasProfessores",
                table: "DisciplinasProfessores");

            migrationBuilder.RenameTable(
                name: "DisciplinasProfessores",
                newName: "DisciplinaProfessor");

            migrationBuilder.RenameIndex(
                name: "IX_DisciplinasProfessores_ProfessoresId",
                table: "DisciplinaProfessor",
                newName: "IX_DisciplinaProfessor_ProfessoresId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisciplinaProfessor",
                table: "DisciplinaProfessor",
                columns: new[] { "DisciplinasId", "ProfessoresId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaProfessor_Disciplinas_DisciplinasId",
                table: "DisciplinaProfessor",
                column: "DisciplinasId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaProfessor_Professores_ProfessoresId",
                table: "DisciplinaProfessor",
                column: "ProfessoresId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
