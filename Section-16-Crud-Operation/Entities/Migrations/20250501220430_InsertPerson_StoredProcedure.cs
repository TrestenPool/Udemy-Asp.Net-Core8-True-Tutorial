using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          string sp_InsertPerson = @"
            Create Procedure [dbo].[InsertPerson]
            (
              @PersonId uniqueidentifier,
              @PersonName nvarchar(40),
              @Email nvarchar(max),
              @Address nvarchar(200),
              @DateOfBirth datetime2(7),
              @PersonGender nvarchar(10),
              @CountryId uniqueidentifier,
              @ReceiveNewsLetters bit
            )
            As Begin

            Insert Into [dbo].[Persons](
              PersonId,
              PersonName,
              Email,
              Address,
              DateOfBirth,
              PersonGender,
              CountryId,
              ReceiveNewsLetters
            )
            Values (
              @PersonId,
              @PersonName,
              @Email,
              @Address,
              @DateOfBirth,
              @PersonGender,
              @CountryId,
              @ReceiveNewsLetters
            )

            End
          ";
          migrationBuilder.Sql(sp_InsertPerson);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          string sp_InsertPerson = @"
            Drop Procedure [dbo].[InsertPerson];
          ";
          migrationBuilder.Sql(sp_InsertPerson);

        }
    }
}
