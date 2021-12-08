using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quacore.Migrations
{
    public partial class TokenRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Tokens",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "Expiration",
                table: "Tokens",
                newName: "RefreshTokenExpiration");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Tokens",
                newName: "AccessToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessTokenExpiration",
                table: "Tokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpiration",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiration",
                table: "Tokens",
                newName: "Expiration");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "Tokens",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "Tokens",
                newName: "Discriminator");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
