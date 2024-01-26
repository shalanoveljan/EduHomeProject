using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Data.Migrations
{
    public partial class SecondUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_CategoriesOfBlog_CategoryOfBlogId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_CategoriesOfEvent_CategoryOfEventId",
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
                name: "FK_TagsBlog_TagsOfBlog_TagOfBlogId",
                table: "TagsBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsEvent_Events_EventId",
                table: "TagsEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsEvent_TagsOfEvent_TagOfEventId",
                table: "TagsEvent");

            migrationBuilder.DropIndex(
                name: "IX_TagsEvent_TagOfEventId",
                table: "TagsEvent");

            migrationBuilder.DropIndex(
                name: "IX_TagsBlog_TagOfBlogId",
                table: "TagsBlog");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryOfEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CategoryOfBlogId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TagOfEventId",
                table: "TagsEvent");

            migrationBuilder.DropColumn(
                name: "TagOfBlogId",
                table: "TagsBlog");

            migrationBuilder.DropColumn(
                name: "CategoryOfEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CategoryOfBlogId",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_TagsEvent_TagIdOfEvent",
                table: "TagsEvent",
                column: "TagIdOfEvent");

            migrationBuilder.CreateIndex(
                name: "IX_TagsBlog_TagIdOfBlog",
                table: "TagsBlog",
                column: "TagIdOfBlog");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryIdOfEvent",
                table: "Events",
                column: "CategoryIdOfEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryIdOfBlog",
                table: "Blogs",
                column: "CategoryIdOfBlog");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

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

            migrationBuilder.DropIndex(
                name: "IX_TagsEvent_TagIdOfEvent",
                table: "TagsEvent");

            migrationBuilder.DropIndex(
                name: "IX_TagsBlog_TagIdOfBlog",
                table: "TagsBlog");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryIdOfEvent",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CategoryIdOfBlog",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "TagOfEventId",
                table: "TagsEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagOfBlogId",
                table: "TagsBlog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryOfEventId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryOfBlogId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TagsEvent_TagOfEventId",
                table: "TagsEvent",
                column: "TagOfEventId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsBlog_TagOfBlogId",
                table: "TagsBlog",
                column: "TagOfBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryOfEventId",
                table: "Events",
                column: "CategoryOfEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryOfBlogId",
                table: "Blogs",
                column: "CategoryOfBlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_CategoriesOfBlog_CategoryOfBlogId",
                table: "Blogs",
                column: "CategoryOfBlogId",
                principalTable: "CategoriesOfBlog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CategoriesOfEvent_CategoryOfEventId",
                table: "Events",
                column: "CategoryOfEventId",
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
                onDelete: ReferentialAction.NoAction);

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
                name: "FK_TagsBlog_TagsOfBlog_TagOfBlogId",
                table: "TagsBlog",
                column: "TagOfBlogId",
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
                name: "FK_TagsEvent_TagsOfEvent_TagOfEventId",
                table: "TagsEvent",
                column: "TagOfEventId",
                principalTable: "TagsOfEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
