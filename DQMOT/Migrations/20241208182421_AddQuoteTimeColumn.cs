using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQMOT.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteTimeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "QuoteDate",
                table: "Quotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuoteDate",
                table: "Quotes");
        }
    }
}
