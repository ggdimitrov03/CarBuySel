using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBuySel.Migrations
{
    public partial class AddCarImagesGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "CarListings");

            migrationBuilder.CreateTable(
                name: "CarImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CarListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImages_CarListings_CarListingId",
                        column: x => x.CarListingId,
                        principalTable: "CarListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_CarListingId",
                table: "CarImages",
                column: "CarListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarImages");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "CarListings",
                type: "nvarchar(260)",
                maxLength: 260,
                nullable: true);
        }
    }
}
