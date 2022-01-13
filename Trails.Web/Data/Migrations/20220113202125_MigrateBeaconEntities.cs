using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trails.Web.Data.Migrations
{
    public partial class MigrateBeaconEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsersCount",
                table: "Teams",
                newName: "UsersMaxCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsersMaxCount",
                table: "Teams",
                newName: "UsersCount");
        }
    }
}
