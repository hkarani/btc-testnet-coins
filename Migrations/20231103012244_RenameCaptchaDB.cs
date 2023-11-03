using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTCTestnetCoins.Migrations
{
    /// <inheritdoc />
    public partial class RenameCaptchaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Captures",
                table: "Captures");

            migrationBuilder.RenameTable(
                name: "Captures",
                newName: "CaptchaResponses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaptchaResponses",
                table: "CaptchaResponses",
                column: "CaptchaResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CaptchaResponses",
                table: "CaptchaResponses");

            migrationBuilder.RenameTable(
                name: "CaptchaResponses",
                newName: "Captures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Captures",
                table: "Captures",
                column: "CaptchaResponseId");
        }
    }
}
