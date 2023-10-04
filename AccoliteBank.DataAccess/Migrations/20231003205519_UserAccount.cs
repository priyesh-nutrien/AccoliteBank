using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccoliteBank.DataAccess.Migrations
{
    public partial class UserAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Checking" },
                    { 2, "Saving" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7534), "User1" },
                    { 2, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7563), "User2" },
                    { 3, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7565), "User3" },
                    { 4, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(7566), "User4" }
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "AccountTypeId", "Balance", "CreatedDate", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 10000.67m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9279), "Personal 1", 1 },
                    { 2, 1, 122.44m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9294), "Personal 2", 1 },
                    { 3, 1, 500.99m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9296), "Personal 3", 1 },
                    { 4, 2, 600.28m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9298), "Saving 2", 1 },
                    { 5, 1, 15000.62m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9300), "Personal 1", 2 },
                    { 6, 1, 1500.42m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9306), "Personal 2", 2 },
                    { 7, 2, 5500.32m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9308), "Saving 1", 2 },
                    { 8, 2, 5600.23m, new DateTime(2023, 10, 3, 15, 55, 19, 530, DateTimeKind.Local).AddTicks(9310), "Saving 2", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_AccountTypeId",
                table: "UserAccounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserId",
                table: "UserAccounts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
