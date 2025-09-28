using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MultiShop.Message.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    UserMessageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageSenderID = table.Column<string>(type: "text", nullable: false),
                    MessageReceiverID = table.Column<string>(type: "text", nullable: false),
                    MessageSubject = table.Column<string>(type: "text", nullable: false),
                    MessageDetail = table.Column<string>(type: "text", nullable: false),
                    MessageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MessageStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.UserMessageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMessages");
        }
    }
}
