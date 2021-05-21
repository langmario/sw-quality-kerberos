using Microsoft.EntityFrameworkCore.Migrations;

namespace Kerberos.Server.Migrations
{
    public partial class UniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Salutations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Salutations_Value_LanguageId",
                table: "Salutations",
                columns: new[] { "Value", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Key",
                table: "Languages",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salutations_Value_LanguageId",
                table: "Salutations");

            migrationBuilder.DropIndex(
                name: "IX_Languages_Key",
                table: "Languages");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Salutations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
