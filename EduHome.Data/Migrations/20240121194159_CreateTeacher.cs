using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Data.Migrations
{
    public partial class CreateTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_CategoriesOfBlog_CategoryIdOfBlog",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_CategoriesOfEvent_CategoryIdOfEvent",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_PositionsOfSpeaker_PositionId",
                table: "Speakers");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeakersEvent_Events_EventId",
                table: "SpeakersEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeakersEvent_Speakers_SpeakerId",
                table: "SpeakersEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsBlog_Blogs_BlogId",
                table: "TagsBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsBlog_TagsOfBlog_TagIdOfBlog",
                table: "TagsBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsEvent_Events_EventId",
                table: "TagsEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsEvent_TagsOfEvent_TagIdOfEvent",
                table: "TagsEvent");

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DutyId = table.Column<int>(type: "int", nullable: false),
                    DegreeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Duties_DutyId",
                        column: x => x.DutyId,
                        principalTable: "Duties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyTeachers_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hobbies_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialNetworks_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTeachers_FacultyId",
                table: "FacultyTeachers",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTeachers_TeacherId",
                table: "FacultyTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_TeacherId",
                table: "Hobbies",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_TeacherId",
                table: "Skills",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialNetworks_TeacherId",
                table: "SocialNetworks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DegreeId",
                table: "Teachers",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DutyId",
                table: "Teachers",
                column: "DutyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_CategoriesOfBlog_CategoryIdOfBlog",
                table: "Blogs",
                column: "CategoryIdOfBlog",
                principalTable: "CategoriesOfBlog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CategoriesOfEvent_CategoryIdOfEvent",
                table: "Events",
                column: "CategoryIdOfEvent",
                principalTable: "CategoriesOfEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_PositionsOfSpeaker_PositionId",
                table: "Speakers",
                column: "PositionId",
                principalTable: "PositionsOfSpeaker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakersEvent_Events_EventId",
                table: "SpeakersEvent",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakersEvent_Speakers_SpeakerId",
                table: "SpeakersEvent",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsBlog_Blogs_BlogId",
                table: "TagsBlog",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsBlog_TagsOfBlog_TagIdOfBlog",
                table: "TagsBlog",
                column: "TagIdOfBlog",
                principalTable: "TagsOfBlog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsEvent_Events_EventId",
                table: "TagsEvent",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsEvent_TagsOfEvent_TagIdOfEvent",
                table: "TagsEvent",
                column: "TagIdOfEvent",
                principalTable: "TagsOfEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_CategoriesOfBlog_CategoryIdOfBlog",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_CategoriesOfEvent_CategoryIdOfEvent",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_PositionsOfSpeaker_PositionId",
                table: "Speakers");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeakersEvent_Events_EventId",
                table: "SpeakersEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeakersEvent_Speakers_SpeakerId",
                table: "SpeakersEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsBlog_Blogs_BlogId",
                table: "TagsBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsBlog_TagsOfBlog_TagIdOfBlog",
                table: "TagsBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsEvent_Events_EventId",
                table: "TagsEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsEvent_TagsOfEvent_TagIdOfEvent",
                table: "TagsEvent");

            migrationBuilder.DropTable(
                name: "FacultyTeachers");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SocialNetworks");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_CategoriesOfBlog_CategoryIdOfBlog",
                table: "Blogs",
                column: "CategoryIdOfBlog",
                principalTable: "CategoriesOfBlog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CategoriesOfEvent_CategoryIdOfEvent",
                table: "Events",
                column: "CategoryIdOfEvent",
                principalTable: "CategoriesOfEvent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_PositionsOfSpeaker_PositionId",
                table: "Speakers",
                column: "PositionId",
                principalTable: "PositionsOfSpeaker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakersEvent_Events_EventId",
                table: "SpeakersEvent",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakersEvent_Speakers_SpeakerId",
                table: "SpeakersEvent",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagsBlog_Blogs_BlogId",
                table: "TagsBlog",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagsBlog_TagsOfBlog_TagIdOfBlog",
                table: "TagsBlog",
                column: "TagIdOfBlog",
                principalTable: "TagsOfBlog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagsEvent_Events_EventId",
                table: "TagsEvent",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagsEvent_TagsOfEvent_TagIdOfEvent",
                table: "TagsEvent",
                column: "TagIdOfEvent",
                principalTable: "TagsOfEvent",
                principalColumn: "Id");
        }
    }
}
