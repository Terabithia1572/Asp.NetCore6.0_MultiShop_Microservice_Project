using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Comment.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    UserCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCommentNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCommentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentRating = table.Column<int>(type: "int", nullable: false),
                    UserCommentCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCommentStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.UserCommentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserComments");
        }
    }
}
