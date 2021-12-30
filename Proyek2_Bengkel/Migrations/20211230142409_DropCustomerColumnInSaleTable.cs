using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyek2_Bengkel.Migrations
{
    public partial class DropCustomerColumnInSaleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Customer_CustomerId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_CustomerId",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Sale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_SparePart_SparePartId",
                table: "SaleDetail");

            migrationBuilder.AlterColumn<int>(
                name: "SparePartId",
                table: "SaleDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "SaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Sale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Customer_CustomerId",
                table: "Sale",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_SparePart_SparePartId",
                table: "SaleDetail",
                column: "SparePartId",
                principalTable: "SparePart",
                principalColumn: "Id");
        }
    }
}
