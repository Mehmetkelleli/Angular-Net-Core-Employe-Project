using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Persistence.Migrations
{
    public partial class AddedDaysWages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonelTasks_EmployeId",
                table: "PersonelTasks");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelTasks_EmployeId",
                table: "PersonelTasks",
                column: "EmployeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonelTasks_EmployeId",
                table: "PersonelTasks");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelTasks_EmployeId",
                table: "PersonelTasks",
                column: "EmployeId",
                unique: true);
        }
    }
}
