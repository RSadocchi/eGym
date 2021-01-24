using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Log.Migrations
{
    public partial class gdprFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldNames",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "TableNames",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.RenameColumn(
                name: "DbQuery",
                schema: "dbo",
                table: "Log_GDPR",
                newName: "UserAgent");

            migrationBuilder.AddColumn<string>(
                name: "EntitySerialized",
                schema: "dbo",
                table: "Log_GDPR",
                type: "ntext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntiyTypeName",
                schema: "dbo",
                table: "Log_GDPR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                schema: "dbo",
                table: "Log_GDPR",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntitySerialized",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "EntiyTypeName",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                schema: "dbo",
                table: "Log_GDPR");

            migrationBuilder.RenameColumn(
                name: "UserAgent",
                schema: "dbo",
                table: "Log_GDPR",
                newName: "DbQuery");

            migrationBuilder.AddColumn<string>(
                name: "FieldNames",
                schema: "dbo",
                table: "Log_GDPR",
                type: "ntext",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TableNames",
                schema: "dbo",
                table: "Log_GDPR",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
