using Microsoft.EntityFrameworkCore.Migrations;

namespace Ocuco.Hydra.WebMVC21.V2.Migrations
{
    public partial class version21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtOrderItem_Orders_OrderId",
                table: "ArtOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "ArtOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtOrders",
                table: "ArtOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtOrderItem_ArtOrders_OrderId",
                table: "ArtOrderItem",
                column: "OrderId",
                principalTable: "ArtOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtOrderItem_ArtOrders_OrderId",
                table: "ArtOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtOrders",
                table: "ArtOrders");

            migrationBuilder.RenameTable(
                name: "ArtOrders",
                newName: "Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtOrderItem_Orders_OrderId",
                table: "ArtOrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
