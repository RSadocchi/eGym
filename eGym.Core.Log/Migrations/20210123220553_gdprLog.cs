using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Log.Migrations
{
    public partial class gdprLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log_GDPR",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    TableNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldNames = table.Column<string>(type: "ntext", nullable: false),
                    DbQuery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyTimestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_GDPR", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_GDPR",
                schema: "dbo");
        }
    }
}
