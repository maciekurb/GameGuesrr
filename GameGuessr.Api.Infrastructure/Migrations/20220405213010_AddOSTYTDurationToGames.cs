using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGuessr.Api.Infrastructure.Migrations
{
    public partial class AddOSTYTDurationToGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OstYouTubeDuration",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OstYouTubeDuration",
                table: "Games");
        }
    }
}
