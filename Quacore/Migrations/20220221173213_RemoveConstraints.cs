using Microsoft.EntityFrameworkCore.Migrations;

namespace Quacore.Migrations
{
    public partial class RemoveConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Profiles",
                type: "nvarchar(420)",
                maxLength: 420,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(420)",
                oldMaxLength: 420);

            migrationBuilder.AlterColumn<string>(
                name: "BannerImageLink",
                table: "Profiles",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(280)",
                oldMaxLength: 280);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarImageLink",
                table: "Profiles",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(280)",
                oldMaxLength: 280);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Profiles",
                type: "nvarchar(420)",
                maxLength: 420,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(420)",
                oldMaxLength: 420,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerImageLink",
                table: "Profiles",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(280)",
                oldMaxLength: 280,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarImageLink",
                table: "Profiles",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(280)",
                oldMaxLength: 280,
                oldNullable: true);
        }
    }
}
