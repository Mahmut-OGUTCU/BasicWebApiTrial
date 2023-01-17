using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonIsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    PersonLastActivited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonCreatedID = table.Column<int>(type: "int", nullable: false),
                    PersonIsActive = table.Column<bool>(type: "bit", nullable: false),
                    PersonUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonDeletedID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
