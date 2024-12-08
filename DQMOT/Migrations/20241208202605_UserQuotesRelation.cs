using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQMOT.Migrations
{
    /// <inheritdoc />
    public partial class UserQuotesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CreatorId",
                table: "Quotes",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Users_CreatorId",
                table: "Quotes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_CreatorId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_CreatorId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Quotes");
        }
    }
}
