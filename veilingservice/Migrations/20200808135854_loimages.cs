using Microsoft.EntityFrameworkCore.Migrations;

namespace veilingservice.Migrations
{
    public partial class loimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LotImage_LotID",
                table: "LotImage",
                column: "LotID");

            migrationBuilder.AddForeignKey(
                name: "FK_LotImage_Lot_LotID",
                table: "LotImage",
                column: "LotID",
                principalTable: "Lot",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotImage_Lot_LotID",
                table: "LotImage");

            migrationBuilder.DropIndex(
                name: "IX_LotImage_LotID",
                table: "LotImage");
        }
    }
}
