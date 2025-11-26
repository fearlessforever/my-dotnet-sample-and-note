using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fearlessforever.Databases.Migrations.AppSqlite
{
    /// <inheritdoc />
    public partial class CreateTestingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SqliteKeyByteItems",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqliteKeyByteItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqliteKeyGuidItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqliteKeyGuidItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqliteKeyStringItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqliteKeyStringItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqliteKeyByteItems_IsDeleted",
                table: "SqliteKeyByteItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SqliteKeyGuidItems_IsDeleted",
                table: "SqliteKeyGuidItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SqliteKeyStringItems_IsDeleted",
                table: "SqliteKeyStringItems",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqliteKeyByteItems");

            migrationBuilder.DropTable(
                name: "SqliteKeyGuidItems");

            migrationBuilder.DropTable(
                name: "SqliteKeyStringItems");
        }
    }
}
