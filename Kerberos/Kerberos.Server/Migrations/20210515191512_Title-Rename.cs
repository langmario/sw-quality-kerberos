using Microsoft.EntityFrameworkCore.Migrations;

namespace Kerberos.Server.Migrations
{
    public partial class TitleRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Titles",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Titles",
                newName: "Name");
        }
    }
}
