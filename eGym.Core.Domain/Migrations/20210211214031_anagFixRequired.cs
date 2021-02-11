using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class anagFixRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master",
                type: "char(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ang_BirthDate",
                schema: "dbo",
                table: "Anag_Master",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Ang_BirthCountrySpec",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_BirthCountry",
                schema: "dbo",
                table: "Anag_Master",
                type: "char(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_BirthCity",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ang_Citizenship",
                schema: "dbo",
                table: "Anag_Master",
                type: "char(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ang_BirthDate",
                schema: "dbo",
                table: "Anag_Master",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_BirthCountrySpec",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_BirthCountry",
                schema: "dbo",
                table: "Anag_Master",
                type: "char(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ang_BirthCity",
                schema: "dbo",
                table: "Anag_Master",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
