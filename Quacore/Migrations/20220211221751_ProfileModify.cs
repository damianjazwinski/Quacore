using Microsoft.EntityFrameworkCore.Migrations;

namespace Quacore.Migrations
{
    public partial class ProfileModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Profiles",
                newName: "BannerImageLink");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageLink",
                table: "Profiles",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageLink",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "BannerImageLink",
                table: "Profiles",
                newName: "ImageLink");
        }
    }
}
