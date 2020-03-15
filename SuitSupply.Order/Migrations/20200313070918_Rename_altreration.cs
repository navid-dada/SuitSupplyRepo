using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuitSupply.Order.Migrations
{
    public partial class Rename_altreration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alternation");

            migrationBuilder.CreateTable(
                name: "Alteration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AlternationPart = table.Column<int>(nullable: false),
                    AlternationSide = table.Column<int>(nullable: false),
                    AlternationType = table.Column<int>(nullable: false),
                    AlterationLength = table.Column<float>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alteration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alteration_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alteration_OrderId",
                table: "Alteration",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alteration");

            migrationBuilder.CreateTable(
                name: "Alternation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternationLength = table.Column<float>(type: "real", nullable: false),
                    AlternationPart = table.Column<int>(type: "int", nullable: false),
                    AlternationSide = table.Column<int>(type: "int", nullable: false),
                    AlternationType = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
    }
}
