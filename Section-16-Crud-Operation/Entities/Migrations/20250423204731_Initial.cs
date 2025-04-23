using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonGender = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { new Guid("12e15727-d369-49a9-8b13-bc22e9362179"), "China" },
                    { new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"), "Philippines" },
                    { new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"), "China" },
                    { new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), "Thailand" },
                    { new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"), "Palestinian Territory" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Address", "CountryId", "DateOfBirth", "Email", "PersonGender", "PersonName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("1926fe42-d0d7-4423-88f7-ddec22896e8d"), "50467 Holy Cross Crossing", null, new DateTime(1995, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "ttregona4@stumbleupon.com", null, "Tani", false },
                    { new Guid("24ffd828-493c-49b3-b776-bc36f4c70774"), "73 Heath Avenue", null, new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "fbowsher2@howstuffworks.com", null, "Franchot", true },
                    { new Guid("3d403fb6-776b-420f-852d-89ddc3987de4"), "57449 Brown Way", null, new DateTime(1983, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "mjarrell6@wisc.edu", null, "Maddy", true },
                    { new Guid("6224e464-8a64-4a6e-b4e3-6350fd59248f"), "83187 Merry Drive", null, new DateTime(1987, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "asarvar3@dropbox.com", null, "Angie", true },
                    { new Guid("62fd20fc-65a7-41ad-b4a2-da521082fe95"), "2 Warrior Avenue", null, new DateTime(1990, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "mconachya@va.gov", null, "Minta", true },
                    { new Guid("6e569b63-a594-407c-9a32-647b793626db"), "484 Clarendon Court", null, new DateTime(1997, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lwoodwing9@wix.com", null, "Lombard", false },
                    { new Guid("714d8831-ec8b-4305-9639-5a4a89ad3d00"), "97570 Raven Circle", null, new DateTime(1988, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "mlingfoot5@netvibes.com", null, "Mitchael", false },
                    { new Guid("754bf0c7-1862-41ba-b248-688032d1bf57"), "413 Sachtjen Way", null, new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "hmosco8@tripod.com", null, "Hansiain", true },
                    { new Guid("801aa514-22f8-4a96-abfa-b00e5e3fb47f"), "4 Parkside Point", null, new DateTime(1989, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "mwebsdale0@people.com.cn", null, "Marguerite", false },
                    { new Guid("8b82fd3c-a925-4a4c-9ad4-ac8f2ce363e8"), "9334 Fremont Street", null, new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "vklussb@nationalgeographic.com", null, "Verene", true },
                    { new Guid("b3ef2380-7bd3-4975-a8e8-b771c351e132"), "6 Morningstar Circle", null, new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ushears1@globo.com", null, "Ursa", false },
                    { new Guid("e893c874-66c5-410d-9101-de179264a7cd"), "4 Stuart Drive", null, new DateTime(1998, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "pretchford7@virginia.edu", null, "Pegeen", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
