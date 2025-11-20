using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBuySel.Migrations
{
    public partial class AddImagePathToListings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "CarListings",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "CarListings",
                type: "nvarchar(260)",
                maxLength: 260,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "CarListings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(260)",
                oldMaxLength: 260,
                oldNullable: true);

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "CarListings",
                newName: "ImageUrl");
        }
    }
}
