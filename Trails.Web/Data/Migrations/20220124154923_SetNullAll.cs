using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trails.Web.Data.Migrations
{
    public partial class SetNullAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BeaconId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Beacons");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BeaconId",
                table: "AspNetUsers",
                column: "BeaconId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BeaconId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Beacons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BeaconId",
                table: "AspNetUsers",
                column: "BeaconId",
                unique: true,
                filter: "[BeaconId] IS NOT NULL");
        }
    }
}
