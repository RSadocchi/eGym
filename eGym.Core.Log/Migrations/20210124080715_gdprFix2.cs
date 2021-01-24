using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Log.Migrations
{
    public partial class gdprFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntiyTypeName",
                schema: "dbo",
                table: "Log_GDPR",
                newName: "RequestUrl");

            migrationBuilder.RenameColumn(
                name: "EntitySerialized",
                schema: "dbo",
                table: "Log_GDPR",
                newName: "ResponseBody");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ExecutionTime",
                schema: "dbo",
                table: "Log_GDPR",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestBody",
                schema: "dbo",
                table: "Log_GDPR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestCookies",
                schema: "dbo",
                table: "Log_GDPR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestHeaders",
                schema: "dbo",
                table: "Log_GDPR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponseStatusCode",
                schema: "dbo",
                table: "Log_GDPR",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionTime",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "RequestBody",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "RequestCookies",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "RequestHeaders",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "ResponseStatusCode",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.RenameColumn(
                name: "ResponseBody",
                schema: "dbo",
                table: "Log_GDPR",
                newName: "EntitySerialized");

            migrationBuilder.RenameColumn(
                name: "RequestUrl",
                schema: "dbo",
                table: "Log_GDPR",
                newName: "EntiyTypeName");
        }
    }
}
