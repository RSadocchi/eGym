using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Localization.Migrations
{
    public partial class concurrencyField_LocDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                table: "CMS_Master",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                table: "CMS_History",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                table: "CMS_Master");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                table: "CMS_History");
        }
    }
}
