using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagementSystemAPI.Migrations
{
    public partial class changesInAllowance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowanceAndDeduction_AspNetUsers_UserId",
                table: "AllowanceAndDeduction");

            migrationBuilder.DropIndex(
                name: "IX_AllowanceAndDeduction_UserId",
                table: "AllowanceAndDeduction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AllowanceAndDeduction");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "AllowanceAndDeduction",
                newName: "WashingAllowance");

            migrationBuilder.RenameColumn(
                name: "AllowanceOrDeductionType",
                table: "AllowanceAndDeduction",
                newName: "ClassName");

            migrationBuilder.AddColumn<int>(
                name: "allowanceAndDeductionId",
                table: "Salary",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DAAllowance",
                table: "AllowanceAndDeduction",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HRAllowance",
                table: "AllowanceAndDeduction",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LeaveDeduction",
                table: "AllowanceAndDeduction",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MedicalAllowance",
                table: "AllowanceAndDeduction",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TravelAllowance",
                table: "AllowanceAndDeduction",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Salary_allowanceAndDeductionId",
                table: "Salary",
                column: "allowanceAndDeductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_AllowanceAndDeduction_allowanceAndDeductionId",
                table: "Salary",
                column: "allowanceAndDeductionId",
                principalTable: "AllowanceAndDeduction",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salary_AllowanceAndDeduction_allowanceAndDeductionId",
                table: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Salary_allowanceAndDeductionId",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "allowanceAndDeductionId",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "DAAllowance",
                table: "AllowanceAndDeduction");

            migrationBuilder.DropColumn(
                name: "HRAllowance",
                table: "AllowanceAndDeduction");

            migrationBuilder.DropColumn(
                name: "LeaveDeduction",
                table: "AllowanceAndDeduction");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                table: "AllowanceAndDeduction");

            migrationBuilder.DropColumn(
                name: "TravelAllowance",
                table: "AllowanceAndDeduction");

            migrationBuilder.RenameColumn(
                name: "WashingAllowance",
                table: "AllowanceAndDeduction",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "ClassName",
                table: "AllowanceAndDeduction",
                newName: "AllowanceOrDeductionType");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AllowanceAndDeduction",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceAndDeduction_UserId",
                table: "AllowanceAndDeduction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowanceAndDeduction_AspNetUsers_UserId",
                table: "AllowanceAndDeduction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
