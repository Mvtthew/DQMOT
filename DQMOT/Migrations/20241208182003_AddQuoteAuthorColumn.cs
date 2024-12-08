using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQMOT.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteAuthorColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuoteAuthor",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuoteAuthor",
                table: "Quotes");
        }
    }
}
