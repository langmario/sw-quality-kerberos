using Microsoft.EntityFrameworkCore.Migrations;

namespace Kerberos.Server.Migrations
{
    public partial class LanguageDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salutations_Languages_LanguageId",
                table: "Salutations");

            migrationBuilder.AddForeignKey(
                name: "FK_Salutations_Languages_LanguageId",
                table: "Salutations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salutations_Languages_LanguageId",
                table: "Salutations");

            migrationBuilder.AddForeignKey(
                name: "FK_Salutations_Languages_LanguageId",
                table: "Salutations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
