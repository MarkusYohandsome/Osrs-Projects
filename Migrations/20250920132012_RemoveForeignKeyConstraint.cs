using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Old_School_Runescape_Project.Migrations
{
    /// <inheritdoc />
    public partial class RemoveForeignKeyConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove foreign key constraint if it exists
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'tasklists_user_id_fkey') THEN
                        ALTER TABLE tasklists DROP CONSTRAINT tasklists_user_id_fkey;
                    END IF;
                END $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // This would re-add the constraint, but we'll leave it empty
            // since we don't know the exact original constraint definition
        }
    }
}
