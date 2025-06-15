using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catgories",
                columns: table => new
                {
                    CatgoryID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatgoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catgories", x => x.CatgoryID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    PASSWORD = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    FIRSTNAME = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    LASTNAME = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatgoryID = table.Column<short>(type: "smallint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductDesdription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<short>(type: "smallint", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_PRoductCatgory",
                        column: x => x.CatgoryID,
                        principalTable: "Catgories",
                        principalColumn: "CatgoryID");
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ORDER_ID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDER_DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    ORDER_SUM = table.Column<int>(type: "int", nullable: true),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orderss", x => x.ORDER_ID);
                    table.ForeignKey(
                        name: "FK_OrdersUser",
                        column: x => x.USER_ID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ORDERITEM",
                columns: table => new
                {
                    ORDER_ITEM_ID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<short>(type: "smallint", nullable: false),
                    ORDER_ID = table.Column<short>(type: "smallint", nullable: false),
                    QUNTITY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.ORDER_ITEM_ID);
                    table.ForeignKey(
                        name: "FK_OrdersitemOrder",
                        column: x => x.PRODUCT_ID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_OrdersitemOrderNew",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ORDER_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORDERITEM_ORDER_ID",
                table: "ORDERITEM",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERITEM_PRODUCT_ID",
                table: "ORDERITEM",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_USER_ID",
                table: "ORDERS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatgoryID",
                table: "Products",
                column: "CatgoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDERITEM");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "Catgories");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
