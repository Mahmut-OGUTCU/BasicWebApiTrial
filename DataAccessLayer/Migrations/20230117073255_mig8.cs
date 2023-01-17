using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonUpdateID",
                table: "Persons",
                newName: "UpdateID");

            migrationBuilder.RenameColumn(
                name: "PersonUpdateDate",
                table: "Persons",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "PersonIsActive",
                table: "Persons",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "PersonCreatedID",
                table: "Persons",
                newName: "CreatedID");

            migrationBuilder.RenameColumn(
                name: "PersonCreatedDate",
                table: "Persons",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateID",
                table: "Persons",
                newName: "PersonUpdateID");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Persons",
                newName: "PersonUpdateDate");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Persons",
                newName: "PersonIsActive");

            migrationBuilder.RenameColumn(
                name: "CreatedID",
                table: "Persons",
                newName: "PersonCreatedID");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Persons",
                newName: "PersonCreatedDate");
        }
    }
}
