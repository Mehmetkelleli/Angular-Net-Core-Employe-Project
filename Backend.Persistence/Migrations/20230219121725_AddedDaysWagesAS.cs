using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Persistence.Migrations
{
    public partial class AddedDaysWagesAS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonelTasks_EmployeId",
                table: "PersonelTasks");

            migrationBuilder.RenameColumn(
                name: "WageDays",
                table: "PersonelTasks",
                newName: "WageHours");

            migrationBuilder.RenameColumn(
                name: "ShiftHours",
                table: "PersonelTasks",
                newName: "WageHourState");

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

            migrationBuilder.RenameColumn(
                name: "WageHours",
                table: "PersonelTasks",
                newName: "WageDays");

            migrationBuilder.RenameColumn(
                name: "WageHourState",
                table: "PersonelTasks",
                newName: "ShiftHours");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelTasks_EmployeId",
                table: "PersonelTasks",
                column: "EmployeId",
                unique: true);
        }
    }
}
