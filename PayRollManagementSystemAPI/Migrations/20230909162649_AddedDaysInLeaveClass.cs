using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagementSystemAPI.Migrations
{
    public partial class AddedDaysInLeaveClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalNoOfDays",
                table: "Leave",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalNoOfDays",
                table: "Leave");
        }
    }
}
