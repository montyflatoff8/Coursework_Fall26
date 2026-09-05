using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderEntrySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class LocationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationDescription",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Products",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "LocationDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
