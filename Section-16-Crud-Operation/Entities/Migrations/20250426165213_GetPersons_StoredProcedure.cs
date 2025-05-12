using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          string sp_GetAllPersons = @"
            Create Procedure [dbo].[GetAllPersons]
            As 
            Begin

              Select 
                *
              From 
                [dbo].[Persons]

            End
          ";
          migrationBuilder.Sql(sp_GetAllPersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

          string sp_GetAllPersons = @"
            Drop Procedure [dbo].[GetAllPersons]
          ";
          migrationBuilder.Sql(sp_GetAllPersons);
        }
    }
}
