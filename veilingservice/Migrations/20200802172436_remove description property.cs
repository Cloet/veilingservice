using Microsoft.EntityFrameworkCore.Migrations;

namespace veilingservice.Migrations
{
    public partial class removedescriptionproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lot");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Auction");

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Lot",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Auction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Lot");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Auction");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lot",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Auction",
                type: "text",
                nullable: true);
        }
    }
}
