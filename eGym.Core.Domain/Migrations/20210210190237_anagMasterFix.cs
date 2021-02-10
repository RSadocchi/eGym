using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class anagMasterFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ang_LastName",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_FirstName",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master",
                type: "char(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<string>(
                name: "Ang_BusinessName",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anag_Master_Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master",
                column: "Ang_Citizenship");

            migrationBuilder.AddForeignKey(
                name: "FK_Anag_Master_Country_Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master",
                column: "Ang_Citizenship",
                principalSchema: "conf",
                principalTable: "Country",
                principalColumn: "Country_IsoCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anag_Master_Country_Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master");

            migrationBuilder.DropIndex(
                name: "IX_Anag_Master_Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master");

            migrationBuilder.DropColumn(
                name: "Ang_BusinessName",
                schema: "dbo",
                table: "Anag_Master");

            migrationBuilder.AlterColumn<string>(
                name: "Ang_LastName",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_FirstName",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldMaxLength: 3);
        }
    }
}
