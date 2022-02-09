using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trails.Web.Data.Migrations
{
    public partial class ParticipantChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Participants");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Participants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Participants");

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
