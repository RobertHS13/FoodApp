using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Migrations
{
    public partial class InitialCreateStripe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Carts_CartId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuPackages_MenuPackageId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPackages_Carts_CartId",
                table: "MenuPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPackages_Menus_MenuId",
                table: "MenuPackages");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuPackageId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuPackageId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "MenuPackages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "MenuPackages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "MenuItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "MenuItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuPackagedId",
                table: "MenuItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Client_Secret = table.Column<string>(nullable: true),
                    Created = table.Column<bool>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Flow = table.Column<string>(nullable: true),
                    SourceId = table.Column<string>(nullable: true),
                    Livemode = table.Column<bool>(nullable: false),
                    Object = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Usage = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sources_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    SubscriptionId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Quantity = table.Column<long>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Cvc_Check = table.Column<string>(nullable: true),
                    Exp_Month = table.Column<int>(nullable: false),
                    Exp_Year = table.Column<int>(nullable: false),
                    Funding = table.Column<string>(nullable: true),
                    Last4 = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Three_D_Secure = table.Column<string>(nullable: true),
                    Tokenization_Method = table.Column<string>(nullable: true),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuPackagedId",
                table: "MenuItems",
                column: "MenuPackagedId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_SourceId",
                table: "Cards",
                column: "SourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sources_UserId",
                table: "Sources",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Carts_CartId",
                table: "MenuItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuPackages_MenuPackagedId",
                table: "MenuItems",
                column: "MenuPackagedId",
                principalTable: "MenuPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPackages_Carts_CartId",
                table: "MenuPackages",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPackages_Menus_MenuId",
                table: "MenuPackages",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Carts_CartId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuPackages_MenuPackagedId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPackages_Carts_CartId",
                table: "MenuPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPackages_Menus_MenuId",
                table: "MenuPackages");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuPackagedId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuPackagedId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "MenuPackages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "MenuPackages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "MenuItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "MenuItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MenuPackageId",
                table: "MenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuPackageId",
                table: "MenuItems",
                column: "MenuPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Carts_CartId",
                table: "MenuItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuPackages_MenuPackageId",
                table: "MenuItems",
                column: "MenuPackageId",
                principalTable: "MenuPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPackages_Carts_CartId",
                table: "MenuPackages",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPackages_Menus_MenuId",
                table: "MenuPackages",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
