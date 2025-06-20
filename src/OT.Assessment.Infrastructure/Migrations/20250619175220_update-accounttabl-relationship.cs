using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OT.Assessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateaccounttablrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Players_PlayerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTransactions_Wagers_CasinoWagerWagerId",
                table: "GameTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wagers_Accounts_AccountId",
                table: "Wagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wagers",
                table: "Wagers");

            migrationBuilder.DropIndex(
                name: "IX_Wagers_CreatedDateTime",
                table: "Wagers");

            migrationBuilder.DropIndex(
                name: "IX_Wagers_TransactionId",
                table: "Wagers");

            migrationBuilder.DropIndex(
                name: "IX_GameTransactions_CasinoWagerWagerId",
                table: "GameTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PlayerId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Username",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CasinoWagerWagerId",
                table: "GameTransactions");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Theme",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Provider",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "GameName",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "WagerId",
                table: "Wagers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wagers",
                table: "Wagers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_AccountId",
                table: "Players",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_EmailAddress",
                table: "Players",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_CasinoWagerId",
                table: "GameTransactions",
                column: "CasinoWagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransactions_Wagers_CasinoWagerId",
                table: "GameTransactions",
                column: "CasinoWagerId",
                principalTable: "Wagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Accounts_AccountId",
                table: "Players",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wagers_Accounts_AccountId",
                table: "Wagers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTransactions_Wagers_CasinoWagerId",
                table: "GameTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Accounts_AccountId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Wagers_Accounts_AccountId",
                table: "Wagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wagers",
                table: "Wagers");

            migrationBuilder.DropIndex(
                name: "IX_Players_AccountId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EmailAddress",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_GameTransactions_CasinoWagerId",
                table: "GameTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Email",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Username",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "WagerId",
                table: "Wagers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Wagers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Wagers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Theme",
                table: "Wagers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Provider",
                table: "Wagers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GameName",
                table: "Wagers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Wagers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CasinoWagerWagerId",
                table: "GameTransactions",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "BrandId",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Accounts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wagers",
                table: "Wagers",
                column: "WagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wagers_CreatedDateTime",
                table: "Wagers",
                column: "CreatedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Wagers_TransactionId",
                table: "Wagers",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactions_CasinoWagerWagerId",
                table: "GameTransactions",
                column: "CasinoWagerWagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PlayerId",
                table: "Accounts",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Players_PlayerId",
                table: "Accounts",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransactions_Wagers_CasinoWagerWagerId",
                table: "GameTransactions",
                column: "CasinoWagerWagerId",
                principalTable: "Wagers",
                principalColumn: "WagerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wagers_Accounts_AccountId",
                table: "Wagers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
