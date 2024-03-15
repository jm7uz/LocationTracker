using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserModifyDeletePhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Salt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<short>(
                name: "RoleId",
                table: "Users",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
