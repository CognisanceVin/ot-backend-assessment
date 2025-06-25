using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OT.Assessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatewagertrasacntiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTransactions");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "ExternalReferenceId",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "GameName",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "NumberOfBets",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "SessionData",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "WagerId",
                table: "Wagers");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Wagers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Wagers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionRecords",
                columns: table => new
                {
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecords", x => x.EntityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wagers_GameId",
                table: "Wagers",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wagers_Games_GameId",
                table: "Wagers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wagers_Games_GameId",
                table: "Wagers");

            migrationBuilder.DropTable(
                name: "TransactionRecords");

            migrationBuilder.DropIndex(
                name: "IX_Wagers_GameId",
                table: "Wagers");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Wagers");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Wagers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "BrandId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Wagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Wagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExternalReferenceId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBets",
                table: "Wagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionData",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionTypeId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WagerId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GameTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CasinoWagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrandId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ExternalReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfBets = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SessionData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionTypeId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WagerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTransactions_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameTransactions_Wagers_CasinoWagerId",
                        column: x => x.CasinoWagerId,
                        principalTable: "Wagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_AccountId",
                table: "GameTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_AccountId1",
                table: "GameTransactions",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_CasinoWagerId",
                table: "GameTransactions",
                column: "CasinoWagerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_CreatedDateTime",
                table: "GameTransactions",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_TransactionId",
                table: "GameTransactions",
                column: "TransactionId",
                unique: true);
        }
    }
}
