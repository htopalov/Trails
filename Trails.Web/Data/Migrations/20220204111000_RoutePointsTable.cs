using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trails.Web.Data.Migrations
{
    public partial class RoutePointsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePoint_Routes_RouteId",
                table: "RoutePoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutePoint",
                table: "RoutePoint");

            migrationBuilder.RenameTable(
                name: "RoutePoint",
                newName: "RoutePoints");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePoint_RouteId",
                table: "RoutePoints",
                newName: "IX_RoutePoints_RouteId");

            migrationBuilder.AlterColumn<string>(
                name: "StartLocationName",
                table: "Routes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FinishLocationName",
                table: "Routes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutePoints",
                table: "RoutePoints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePoints_Routes_RouteId",
                table: "RoutePoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePoints_Routes_RouteId",
                table: "RoutePoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutePoints",
                table: "RoutePoints");

            migrationBuilder.RenameTable(
                name: "RoutePoints",
                newName: "RoutePoint");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePoints_RouteId",
                table: "RoutePoint",
                newName: "IX_RoutePoint_RouteId");

            migrationBuilder.AlterColumn<string>(
                name: "StartLocationName",
                table: "Routes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FinishLocationName",
                table: "Routes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutePoint",
                table: "RoutePoint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePoint_Routes_RouteId",
                table: "RoutePoint",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
