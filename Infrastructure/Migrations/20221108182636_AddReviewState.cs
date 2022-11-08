using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddReviewState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewStateId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReviewState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewStateId",
                table: "Review",
                column: "ReviewStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_ReviewState_ReviewStateId",
                table: "Review",
                column: "ReviewStateId",
                principalTable: "ReviewState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_ReviewState_ReviewStateId",
                table: "Review");

            migrationBuilder.DropTable(
                name: "ReviewState");

            migrationBuilder.DropIndex(
                name: "IX_Review_ReviewStateId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewStateId",
                table: "Review");
        }
    }
}
