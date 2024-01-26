using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Data.Migrations
{
    public partial class CreateHobbyTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_Teachers_TeacherId",
                table: "Hobbies");

            migrationBuilder.DropTable(
                name: "Subcribes");

            migrationBuilder.DropIndex(
                name: "IX_Hobbies_TeacherId",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Hobbies");

            migrationBuilder.CreateTable(
                name: "HobbyTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbyID = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HobbyTeachers_Hobbies_HobbyID",
                        column: x => x.HobbyID,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyTeachers_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HobbyTeachers_HobbyID",
                table: "HobbyTeachers",
                column: "HobbyID");

            migrationBuilder.CreateIndex(
                name: "IX_HobbyTeachers_TeacherID",
                table: "HobbyTeachers",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbyTeachers");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Hobbies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subcribes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcribes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_TeacherId",
                table: "Hobbies",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_Teachers_TeacherId",
                table: "Hobbies",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
