using Microsoft.EntityFrameworkCore.Migrations;

namespace SuitSupply.Order.Migrations
{
    public partial class rename_alteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlternationPart",
                table: "Alteration");

            migrationBuilder.DropColumn(
                name: "AlternationSide",
                table: "Alteration");

            migrationBuilder.DropColumn(
                name: "AlternationType",
                table: "Alteration");

            migrationBuilder.AddColumn<int>(
                name: "AlterationPart",
                table: "Alteration",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlterationSide",
                table: "Alteration",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlterationType",
                table: "Alteration",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlterationPart",
                table: "Alteration");

            migrationBuilder.DropColumn(
                name: "AlterationSide",
                table: "Alteration");

            migrationBuilder.DropColumn(
                name: "AlterationType",
                table: "Alteration");

            migrationBuilder.AddColumn<int>(
                name: "AlternationPart",
                table: "Alteration",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlternationSide",
                table: "Alteration",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlternationType",
                table: "Alteration",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
