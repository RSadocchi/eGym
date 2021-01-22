using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class applicationTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Country",
                schema: "dbo",
                newName: "Country",
                newSchema: "conf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Country",
                schema: "conf",
                newName: "Country",
                newSchema: "dbo");
        }
    }
}
