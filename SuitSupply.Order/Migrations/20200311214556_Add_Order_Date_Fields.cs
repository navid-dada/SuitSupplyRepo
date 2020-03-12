using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuitSupply.Order.Migrations
{
    public partial class Add_Order_Date_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinisedDateTime",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDatetime",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FinisedDateTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaidDatetime",
                table: "Orders");
        }
    }
}
