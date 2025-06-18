using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OT.Assessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTransaction_Accounts_AccountId1",
                table: "GameTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTransaction_Wagers_CasinoWagerWagerId",
                table: "GameTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTransaction",
                table: "GameTransaction");

            migrationBuilder.RenameTable(
                name: "GameTransaction",
                newName: "GameTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransaction_TransactionId",
                table: "GameTransactions",
                newName: "IX_GameTransactions_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransaction_CreatedDateTime",
                table: "GameTransactions",
                newName: "IX_GameTransactions_CreatedDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransaction_CasinoWagerWagerId",
                table: "GameTransactions",
                newName: "IX_GameTransactions_CasinoWagerWagerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransaction_AccountId1",
                table: "GameTransactions",
                newName: "IX_GameTransactions_AccountId1");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransaction_AccountId",
                table: "GameTransactions",
                newName: "IX_GameTransactions_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTransactions",
                table: "GameTransactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransactions_Accounts_AccountId1",
                table: "GameTransactions",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransactions_Wagers_CasinoWagerWagerId",
                table: "GameTransactions",
                column: "CasinoWagerWagerId",
                principalTable: "Wagers",
                principalColumn: "WagerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTransactions_Accounts_AccountId1",
                table: "GameTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTransactions_Wagers_CasinoWagerWagerId",
                table: "GameTransactions");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTransactions",
                table: "GameTransactions");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "GameTransactions",
                newName: "GameTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactions_TransactionId",
                table: "GameTransaction",
                newName: "IX_GameTransaction_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactions_CreatedDateTime",
                table: "GameTransaction",
                newName: "IX_GameTransaction_CreatedDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactions_CasinoWagerWagerId",
                table: "GameTransaction",
                newName: "IX_GameTransaction_CasinoWagerWagerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactions_AccountId1",
                table: "GameTransaction",
                newName: "IX_GameTransaction_AccountId1");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactions_AccountId",
                table: "GameTransaction",
                newName: "IX_GameTransaction_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTransaction",
                table: "GameTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransaction_Accounts_AccountId1",
                table: "GameTransaction",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransaction_Wagers_CasinoWagerWagerId",
                table: "GameTransaction",
                column: "CasinoWagerWagerId",
                principalTable: "Wagers",
                principalColumn: "WagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
