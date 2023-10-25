using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chirp.Razor.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCheeps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "message",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "message",
                newName: "Text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "message",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "message",
                newName: "Message");
        }
    }
}
