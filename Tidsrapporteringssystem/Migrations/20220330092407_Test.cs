using Microsoft.EntityFrameworkCore.Migrations;

namespace Tidsrapporteringssystem.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmployeesEmployeeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeesEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeesEmployeeId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeesEmployeeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeesEmployeeId",
                table: "Employees",
                column: "EmployeesEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmployeesEmployeeId",
                table: "Employees",
                column: "EmployeesEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
