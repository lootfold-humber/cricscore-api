using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CricScore.Migrations
{
    public partial class AddModelForTossDecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TossDecisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TossDecisions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TossDecisions",
                columns: new[] { "Id", "Value" },
                values: new object[] { 1, "Bat" });

            migrationBuilder.InsertData(
                table: "TossDecisions",
                columns: new[] { "Id", "Value" },
                values: new object[] { 2, "Bowl" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TossDecisions");
        }
    }
}
