using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    cash = table.Column<decimal>(type: "numeric(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    comp_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Resources__comp___300424B4",
                        column: x => x.comp_id,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Resources__user___2F10007B",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buy_Offers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    resource_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    max_price = table.Column<decimal>(type: "numeric(10, 4)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_valid = table.Column<bool>(nullable: false),
                    start_amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buy_Offers", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Buy_Offer__resou__31EC6D26",
                        column: x => x.resource_id,
                        principalTable: "Resources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sell_Offers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    resource_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(type: "numeric(10, 4)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_valid = table.Column<bool>(nullable: false),
                    start_amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sell_Offers", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Sell_Offe__resou__30F848ED",
                        column: x => x.resource_id,
                        principalTable: "Resources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sell_offer_id = table.Column<int>(nullable: false),
                    buy_offer_id = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    amount = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(type: "numeric(10, 4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Transacti__buy_o__33D4B598",
                        column: x => x.buy_offer_id,
                        principalTable: "Buy_Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Transacti__sell___32E0915F",
                        column: x => x.sell_offer_id,
                        principalTable: "Sell_Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buy_Offers_resource_id",
                table: "Buy_Offers",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_comp_id",
                table: "Resources",
                column: "comp_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_user_id",
                table: "Resources",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_Offers_resource_id",
                table: "Sell_Offers",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_buy_offer_id",
                table: "Transactions",
                column: "buy_offer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_sell_offer_id",
                table: "Transactions",
                column: "sell_offer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Buy_Offers");

            migrationBuilder.DropTable(
                name: "Sell_Offers");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
