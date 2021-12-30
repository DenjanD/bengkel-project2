using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyek2_Bengkel.Migrations
{
    public partial class DropPartIdColumnInServiceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartId",
                table: "ServiceDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetail_SparePart_SparePartId",
                table: "ServiceDetail");

            migrationBuilder.AlterColumn<int>(
                name: "SparePartId",
                table: "ServiceDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "ServiceDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetail_SparePart_SparePartId",
                table: "ServiceDetail",
                column: "SparePartId",
                principalTable: "SparePart",
                principalColumn: "Id");
        }
    }
}
