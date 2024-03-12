using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class twomigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "AttachedArea",
                table: "Users",
                newName: "AttachedAreaId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Districts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AttachedAreaId",
                table: "Users",
                column: "AttachedAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AttachedAreas_AttachedAreaId",
                table: "Users",
                column: "AttachedAreaId",
                principalTable: "AttachedAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AttachedAreas_AttachedAreaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AttachedAreaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Districts_RegionId",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Districts");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "AttachedAreaId",
                table: "Users",
                newName: "AttachedArea");
        }
    }
}
