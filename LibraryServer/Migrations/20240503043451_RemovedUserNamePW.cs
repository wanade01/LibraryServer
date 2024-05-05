using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryServer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserNamePW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatronPassword",
                table: "Patron");

            migrationBuilder.DropColumn(
                name: "PatronUsername",
                table: "Patron");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatronPassword",
                table: "Patron",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatronUsername",
                table: "Patron",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
