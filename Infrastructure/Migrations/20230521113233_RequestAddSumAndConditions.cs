using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RequestAddSumAndConditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "AvailabilityRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sum",
                table: "AvailabilityRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "AvailabilityRequest");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "AvailabilityRequest");
        }
    }
}
