using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fearlessforever.Databases.Migrations.AppMsSql
{
    /// <inheritdoc />
    public partial class UpdateBookAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BookId",
                schema: "CityLibrary",
                table: "BookCategories",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Books_BookId",
                schema: "CityLibrary",
                table: "BookCategories",
                column: "BookId",
                principalSchema: "CityLibrary",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Books_BookId",
                schema: "CityLibrary",
                table: "BookCategories");

            migrationBuilder.DropIndex(
                name: "IX_BookCategories_BookId",
                schema: "CityLibrary",
                table: "BookCategories");
        }
    }
}
