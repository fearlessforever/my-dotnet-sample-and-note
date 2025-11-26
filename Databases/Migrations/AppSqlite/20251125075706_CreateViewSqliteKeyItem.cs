using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fearlessforever.Databases.Migrations.AppSqlite
{
    /// <inheritdoc />
    public partial class CreateViewSqliteKeyItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // NOTE: byte / blob type must be cast-ed to HEX STRING. then the HEX STRING can be convert each char to byte. the byte array then can be convert to base64 string
            migrationBuilder.Sql(@"
                CREATE VIEW ViewSqliteKeyItem AS
                    SELECT 'SqliteKeyByteItems' as Source,hex(Id) as Id,Name,DateCreated,DateUpdated,CreatedBy,UpdatedBy,IsDeleted,DeletedBy,DateDeleted  from SqliteKeyByteItems
                    UNION ALL
                    SELECT 'SqliteKeyGuidItems' as Source,Id,Name,DateCreated,DateUpdated,CreatedBy,UpdatedBy,IsDeleted,DeletedBy,DateDeleted  from SqliteKeyGuidItems
                    UNION ALL
                    SELECT 'SqliteKeyStringItems' as Source,Id,Name,DateCreated,DateUpdated,CreatedBy,UpdatedBy,IsDeleted,DeletedBy,DateDeleted  from SqliteKeyStringItems
                ;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW ViewSqliteKeyItem;
            ");
        }
    }
}
