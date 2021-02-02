using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class todoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todo_Master",
                schema: "dbo",
                columns: table => new
                {
                    TD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TD_CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TD_Priority = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TD_Important = table.Column<bool>(type: "bit", nullable: false),
                    TD_Deadline = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    TD_DeadlineDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TD_DeadlineTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    TD_Title = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    TD_Content = table.Column<string>(type: "ntext", nullable: true),
                    TD_Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    TD_StatusID = table.Column<short>(type: "smallint", nullable: false),
                    TD_StatusDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConcurrencyTimestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo_Master", x => x.TD_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo_Master",
                schema: "dbo");
        }
    }
}
