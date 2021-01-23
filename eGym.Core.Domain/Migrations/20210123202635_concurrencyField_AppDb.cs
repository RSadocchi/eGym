using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class concurrencyField_AppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Schedule",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Master",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_LevelXSport",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_LevelLocalized",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Level",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResultTypeLocalized",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResultType",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResultLocalized",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResult",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_DivisionXSport",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_DivisionLocalized",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Division",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Country",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_WeightXAthlete",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_Master",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_LevelXAthlete",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_DivisionXAthlete",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_MasterRole",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Master",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Document",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_CorporateRole",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Contact",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_AddressRole",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Address",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Schedule");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Master");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_LevelXSport");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_LevelLocalized");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Level");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResultTypeLocalized");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResultType");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResultLocalized");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_EventResult");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_DivisionXSport");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_DivisionLocalized");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Sport_Division");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "conf",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_WeightXAthlete");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_Master");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_LevelXAthlete");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "ath",
                table: "Athlete_DivisionXAthlete");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_MasterRole");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Master");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Document");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_CorporateRole");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Contact");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_AddressRole");

            migrationBuilder.DropColumn(
                name: "ConcurrencyTimestamp",
                schema: "dbo",
                table: "Anag_Address");
        }
    }
}
