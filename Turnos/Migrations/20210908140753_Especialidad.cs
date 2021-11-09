using Microsoft.EntityFrameworkCore.Migrations;

namespace Turnos.Migrations
{
    public partial class Especialidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Especilidad",
                table: "Especilidad");

            migrationBuilder.RenameTable(
                name: "Especilidad",
                newName: "Especialidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidad",
                table: "Especialidad",
                column: "IdEspecialidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidad",
                table: "Especialidad");

            migrationBuilder.RenameTable(
                name: "Especialidad",
                newName: "Especilidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especilidad",
                table: "Especilidad",
                column: "IdEspecialidad");
        }
    }
}
