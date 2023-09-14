using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagementSystemAPI.Migrations
{
    public partial class changedSalaryClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Salary_SalaryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SalaryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Salary",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salary_UserId",
                table: "Salary",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_AspNetUsers_UserId",
                table: "Salary",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salary_AspNetUsers_UserId",
                table: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Salary_UserId",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Salary");

            migrationBuilder.AddColumn<int>(
                name: "SalaryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SalaryId",
                table: "AspNetUsers",
                column: "SalaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Salary_SalaryId",
                table: "AspNetUsers",
                column: "SalaryId",
                principalTable: "Salary",
                principalColumn: "Id");
        }
    }
}
