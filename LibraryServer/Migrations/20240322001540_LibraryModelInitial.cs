using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryServer.Migrations
{
    /// <inheritdoc />
    public partial class LibraryModelInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookIsbn13 = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BookIsbn10 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BookTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BookAuthor = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BookPublisher = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BookPublishYear = table.Column<short>(type: "smallint", nullable: false),
                    BookGenre = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "Patron",
                columns: table => new
                {
                    PatronID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatronFName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PatronLName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PatronAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PatronCheckedBookID = table.Column<int>(type: "int", nullable: true),
                    PatronCheckedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PatronCheckedDueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PatronCheckedOverdueAmt = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PatronUsername = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PatronPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patron", x => x.PatronID);
                    table.ForeignKey(
                        name: "FK_Patron_Book",
                        column: x => x.PatronCheckedBookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patron_PatronCheckedBookID",
                table: "Patron",
                column: "PatronCheckedBookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patron");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
