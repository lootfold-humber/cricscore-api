using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CricScore.Migrations
{
    public partial class AddModelForToss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tosses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    WinningTeamId = table.Column<int>(type: "int", nullable: false),
                    TossDecisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tosses_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tosses_Teams_WinningTeamId",
                        column: x => x.WinningTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tosses_TossDecisions_TossDecisionId",
                        column: x => x.TossDecisionId,
                        principalTable: "TossDecisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tosses_MatchId",
                table: "Tosses",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tosses_TossDecisionId",
                table: "Tosses",
                column: "TossDecisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tosses_WinningTeamId",
                table: "Tosses",
                column: "WinningTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tosses");
        }
    }
}
