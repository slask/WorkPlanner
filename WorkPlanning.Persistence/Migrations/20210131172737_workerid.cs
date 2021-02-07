using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkPlanning.Persistence.Migrations
{
    public partial class workerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Shifts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Shifts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workers_WorkerId",
                table: "Shifts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
