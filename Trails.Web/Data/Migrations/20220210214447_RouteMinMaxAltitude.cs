using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trails.Web.Data.Migrations
{
    public partial class ParticipantMinMaxAltitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaximumAltitude",
                table: "Routes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinimumAltitude",
                table: "Routes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumAltitude",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "MinimumAltitude",
                table: "Routes");
        }
    }
}
