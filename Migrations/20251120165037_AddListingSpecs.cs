using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBuySel.Migrations
{
    public partial class AddListingSpecs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condition1",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition2",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition3",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition4",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History1Date",
                table: "CarListings",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History1Subtitle",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History1Title",
                table: "CarListings",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History2Date",
                table: "CarListings",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History2Subtitle",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History2Title",
                table: "CarListings",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History3Date",
                table: "CarListings",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History3Subtitle",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History3Title",
                table: "CarListings",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecAssist",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecConnectivity",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecEconomy",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecEngine",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecExterior",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecInterior",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecPower",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecTransmission",
                table: "CarListings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition1",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "Condition2",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "Condition3",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "Condition4",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History1Date",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History1Subtitle",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History1Title",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History2Date",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History2Subtitle",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History2Title",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History3Date",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History3Subtitle",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "History3Title",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecAssist",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecConnectivity",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecEconomy",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecEngine",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecExterior",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecInterior",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecPower",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SpecTransmission",
                table: "CarListings");
        }
    }
}
