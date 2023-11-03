using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTCTestnetCoins.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCaptureResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Eligible",
                table: "Users",
                newName: "IsEligible");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastAccesed",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Captures",
                columns: table => new
                {
                    CaptchaResponseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ip = table.Column<string>(type: "TEXT", nullable: true),
                    Success = table.Column<bool>(type: "INTEGER", nullable: true),
                    ChallengeTimeStamp = table.Column<string>(type: "TEXT", nullable: true),
                    CaptchaScore = table.Column<double>(type: "REAL", nullable: true),
                    ErrorCodes = table.Column<string>(type: "TEXT", nullable: false),
                    ActionToResponse = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captures", x => x.CaptchaResponseId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captures");

            migrationBuilder.RenameColumn(
                name: "IsEligible",
                table: "Users",
                newName: "Eligible");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastAccesed",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
