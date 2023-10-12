using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chirp.Razor.Migrations
{
    /// <inheritdoc />
    public partial class PathUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeps_Authors_AuthorId",
                table: "Cheeps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cheeps",
                table: "Cheeps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Cheeps",
                newName: "message");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "user");

            migrationBuilder.RenameIndex(
                name: "IX_Cheeps_AuthorId",
                table: "message",
                newName: "IX_message_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_message",
                table: "message",
                column: "CheepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_message_user_AuthorId",
                table: "message",
                column: "AuthorId",
                principalTable: "user",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_user_AuthorId",
                table: "message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_message",
                table: "message");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Authors");

            migrationBuilder.RenameTable(
                name: "message",
                newName: "Cheeps");

            migrationBuilder.RenameIndex(
                name: "IX_message_AuthorId",
                table: "Cheeps",
                newName: "IX_Cheeps_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cheeps",
                table: "Cheeps",
                column: "CheepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeps_Authors_AuthorId",
                table: "Cheeps",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
