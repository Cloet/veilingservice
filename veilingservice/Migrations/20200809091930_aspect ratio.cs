using Microsoft.EntityFrameworkCore.Migrations;

namespace veilingservice.Migrations
{
    public partial class aspectratio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AspectRatio",
                table: "LotImage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AspectRatio",
                table: "AuctionImage",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspectRatio",
                table: "LotImage");

            migrationBuilder.DropColumn(
                name: "AspectRatio",
                table: "AuctionImage");
        }
    }
}
