using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Data.Migrations
{
    public partial class CreateSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Settings",
                newName: "WelcomeTitle");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Settings",
                newName: "WelcomeDesc");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PinterestUrl",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VimeoUrl",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WelcomeImage",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PinterestUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "VimeoUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "WelcomeImage",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "WelcomeTitle",
                table: "Settings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "WelcomeDesc",
                table: "Settings",
                newName: "Key");
        }
    }
}
