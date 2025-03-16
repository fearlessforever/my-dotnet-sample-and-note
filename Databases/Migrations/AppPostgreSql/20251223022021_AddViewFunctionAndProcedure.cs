using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fearlessforever.Databases.Migrations.AppPostgreSql
{
    /// <inheritdoc />
    public partial class AddViewFunctionAndProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW ""ViewKeyItems"" AS
                    SELECT 'KeyByteItems' as ""Source"", encode(""Id"", 'base64') as ""Id"", ""Name"", ""DateCreated"", ""DateUpdated"", ""CreatedBy"", ""UpdatedBy"", ""IsDeleted"", ""DeletedBy"", ""DateDeleted"" 
                    FROM public.""PostgreKeyByteItems""
                    UNION ALL
                    SELECT 'KeyGuidItems' as ""Source"", ""Id""::TEXT, ""Name"", ""DateCreated"", ""DateUpdated"", ""CreatedBy"", ""UpdatedBy"", ""IsDeleted"", ""DeletedBy"", ""DateDeleted""
                    FROM public.""PostgreKeyGuidItems"";
            ");

            // migrationBuilder.Sql(@"
            //     CREATE OR REPLACE FUNCTION get_data_guid_key(table_id uuid)
            //     RETURNS TABLE (
            //         ""Id"" uuid,
            //         ""Name"" text,
            //         ""DateCreated"" timestamptz,
            //         ""DateUpdated"" timestamptz,
            //         ""CreatedBy"" text,
            //         ""UpdatedBy"" text,
            //         ""IsDeleted"" bool,
            //         ""DeletedBy"" text,
            //         ""DateDeleted"" timestamptz
            //     )
            //     LANGUAGE sql
            //     AS $$
            //     SELECT * FROM public.""PostgreKeyGuidItems"" WHERE ""Id"" = table_id
            //     $$;
            // ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION get_data_guid_key(table_id uuid)
                    RETURNS jsonb
                    LANGUAGE sql
                AS $$
                SELECT to_jsonb(row) 
                FROM public.""PostgreKeyGuidItems"" row 
                WHERE row.""Id"" = table_id;
                $$;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE insert_into_PostgreKeyItems(
                    p_guid_id uuid,
                    p_bytea_id bytea,
                    p_name text,
                    p_created_by varchar(255) DEFAULT NULL,
                    p_updated_by varchar(255) DEFAULT NULL,
                    p_is_deleted bool DEFAULT NULL,
                    p_deleted_by varchar(255) DEFAULT NULL,
                    p_date_deleted timestamptz DEFAULT NULL
                )
                LANGUAGE plpgsql
                AS $$
                BEGIN
                    -- Start a transaction
                    BEGIN
                        -- Insert into PostgreKeyGuidItems table
                        INSERT INTO public.""PostgreKeyGuidItems"" (
                            ""Id"", ""Name"", ""CreatedBy"", ""UpdatedBy"", ""IsDeleted"", ""DeletedBy"", ""DateDeleted""
                        )
                        VALUES (
                            p_guid_id, p_name, p_created_by, p_updated_by, p_is_deleted, p_deleted_by, p_date_deleted
                        );

                        -- Insert into PostgreKeyByteItems table
                        INSERT INTO public.""PostgreKeyByteItems"" (
                            ""Id"", ""Name"", ""CreatedBy"", ""UpdatedBy"", ""IsDeleted"", ""DeletedBy"", ""DateDeleted""
                        )
                        VALUES (
                            p_bytea_id,  -- Convert UUID to bytea
                            p_name, p_created_by, p_updated_by, p_is_deleted, p_deleted_by, p_date_deleted
                        );

                    EXCEPTION
                        -- Handle error and rollback transaction if an error occurs
                        WHEN OTHERS THEN
                            RAISE NOTICE 'Error during insert: %', SQLERRM;
                            ROLLBACK;
                            RETURN;
                    END;

                    COMMIT;
                END;
                $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS get_data_guid_key(uuid);");
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS ""ViewKeyItems"";");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS insert_into_PostgreKeyItems;");
        }
    }
}


/*
CREATE OR REPLACE FUNCTION get_data_guid_key(table_id uuid)
RETURNS TABLE (
    Id uuid,
    Name text,
    DateCreated timestamptz,
    DateUpdated timestamptz,
    CreatedBy text,
    UpdatedBy text,
    IsDeleted bool,
    DeletedBy text,
    DateDeleted timestamptz
)
LANGUAGE sql
AS $$
SELECT * FROM public."PostgreKeyGuidItems" WHERE "Id" = table_id
$$;



CREATE VIEW ViewKeyItems AS
    SELECT 'KeyByteItems' as Source,encode("Id" , 'base64') as "Id","Name","DateCreated","DateUpdated","CreatedBy","UpdatedBy","IsDeleted","DeletedBy","DateDeleted"  from public."PostgreKeyByteItems"
    UNION ALL
    SELECT 'KeyGuidItems' as Source,"Id"::TEXT,"Name","DateCreated","DateUpdated","CreatedBy","UpdatedBy","IsDeleted","DeletedBy","DateDeleted"  from public."PostgreKeyGuidItems"
;


CREATE OR REPLACE PROCEDURE insert_into_PostgreKeyItems(
    -- For PostgreKeyGuidItems
    p_guid_id uuid,
    p_name text,
    p_created_by varchar(255) DEFAULT NULL,
    p_updated_by varchar(255) DEFAULT NULL,
    p_is_deleted bool DEFAULT NULL,
    p_deleted_by varchar(255) DEFAULT NULL,
    p_date_deleted timestamptz DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Start a transaction
    BEGIN
        -- Insert into PostgreKeyGuidItems table
        INSERT INTO public."PostgreKeyGuidItems" (
            "Id", "Name", "CreatedBy", "UpdatedBy", "IsDeleted", "DeletedBy", "DateDeleted"
        )
        VALUES (
            p_guid_id, p_name, p_created_by, p_updated_by, p_is_deleted, p_deleted_by, p_date_deleted
        );

        -- Insert into PostgreKeyByteItems table
        INSERT INTO public."PostgreKeyByteItems" (
            "Id", "Name", "CreatedBy", "UpdatedBy", "IsDeleted", "DeletedBy", "DateDeleted"
        )
        VALUES (
            encode(p_guid_id::text, 'hex')::bytea,  -- Convert UUID to bytea
            p_name, p_created_by, p_updated_by, p_is_deleted, p_deleted_by, p_date_deleted
        );

    EXCEPTION
        -- Handle error and rollback transaction if an error occurs
        WHEN OTHERS THEN
            RAISE NOTICE 'Error during insert: %', SQLERRM;
            ROLLBACK;
            RETURN;
    END;

    COMMIT;
END;
$$;


CALL insert_into_PostgreKeyItems(
    'b6f03e87-1c71-4c5d-8d7b-45b71f1cb9a3'::uuid,  -- p_guid_id
    'Example Name',                               -- p_name
    'John Doe',                                   -- p_created_by
    'Jane Smith',                                 -- p_updated_by
    FALSE,                                        -- p_is_deleted
    'Admin',                                      -- p_deleted_by
    NULL                                          -- p_date_deleted
);




*/ 