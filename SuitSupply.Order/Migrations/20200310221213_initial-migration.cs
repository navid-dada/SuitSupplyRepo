using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuitSupply.Order.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CustomerEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alternation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlternationPart = table.Column<int>(nullable: false),
                    AlternationSide = table.Column<int>(nullable: false),
                    AlternationType = table.Column<int>(nullable: false),
                    AlternationLength = table.Column<float>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alternation_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternation_OrderId",
                table: "Alternation",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alternation");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
