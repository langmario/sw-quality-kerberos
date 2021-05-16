using Microsoft.EntityFrameworkCore.Migrations;

namespace Kerberos.Server.Migrations
{
    public partial class FormalSalutationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormalSalutation",
                table: "Salutations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormalSalutation",
                table: "Salutations");
        }
    }
}
