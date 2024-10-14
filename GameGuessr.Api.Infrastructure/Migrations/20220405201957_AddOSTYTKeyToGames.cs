using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGuessr.Api.Infrastructure.Migrations
{
    public partial class AddOSTYTKeyToGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OstYouTubeKey",
                table: "Games",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OstYouTubeKey",
                table: "Games");
        }
    }
}
