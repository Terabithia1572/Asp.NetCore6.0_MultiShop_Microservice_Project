using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Order.Persistence.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressDetail",
                table: "Addresses",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "AddressDescription",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressDetail1",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressDetail2",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressZipCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressDescription",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressDetail1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressDetail2",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressZipCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Addresses",
                newName: "AddressDetail");
        }
    }
}
