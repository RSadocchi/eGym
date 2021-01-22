using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Security.Migrations
{
    public partial class securityTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVouchers_User_UserId",
                table: "UserVouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVouchers",
                table: "UserVouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTokens",
                table: "ServiceTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Captchas",
                table: "Captchas");

            migrationBuilder.RenameTable(
                name: "UserVouchers",
                newName: "UserVoucher",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "ServiceTokens",
                newName: "ServiceToken",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "Captchas",
                newName: "Captcha",
                newSchema: "Security");

            migrationBuilder.RenameIndex(
                name: "IX_UserVouchers_UserId",
                schema: "Security",
                table: "UserVoucher",
                newName: "IX_UserVoucher_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVoucher",
                schema: "Security",
                table: "UserVoucher",
                column: "UV_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceToken",
                schema: "Security",
                table: "ServiceToken",
                column: "ST_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Captcha",
                schema: "Security",
                table: "Captcha",
                column: "TokenID");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4e3de014-daa8-4816-ab06-1f2fa329d36a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "49ed6c03-f37f-4529-81a3-45e6f18b7338");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "29ecb2ba-8ec0-453a-81d2-1d4de020b111");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "bda1374e-ecc7-44ac-a169-53cc95cc76ba");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordExpirationDateTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c92ba590-dc27-45a2-9bb3-3a9dbb4b98f5", new DateTime(2121, 1, 22, 22, 58, 8, 248, DateTimeKind.Local).AddTicks(3667), "AQAAAAEAACcQAAAAEJo018FWHhXai+wRLzhyB3QMiWS7PKja6O9B77XNxSrOEIMKu1ySsygAGsikKnLmKg==", "26a7ec2b-d1d5-4447-b490-4360bf4e04b6" });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordExpirationDateTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b35cfa6e-602d-478b-8f7d-0128553f51a4", new DateTime(2121, 1, 22, 22, 58, 8, 267, DateTimeKind.Local).AddTicks(3834), "AQAAAAEAACcQAAAAEOo5SlT332byVQat31t84vMCadsxehuHqeRrHj9PZIKsCpx9mTg2rf0dl5Km7+hzXQ==", "ef1a07fb-6e9f-47e9-bcd8-d60d59bd8c1d" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserVoucher_User_UserId",
                schema: "Security",
                table: "UserVoucher",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVoucher_User_UserId",
                schema: "Security",
                table: "UserVoucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVoucher",
                schema: "Security",
                table: "UserVoucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceToken",
                schema: "Security",
                table: "ServiceToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Captcha",
                schema: "Security",
                table: "Captcha");

            migrationBuilder.RenameTable(
                name: "UserVoucher",
                schema: "Security",
                newName: "UserVouchers");

            migrationBuilder.RenameTable(
                name: "ServiceToken",
                schema: "Security",
                newName: "ServiceTokens");

            migrationBuilder.RenameTable(
                name: "Captcha",
                schema: "Security",
                newName: "Captchas");

            migrationBuilder.RenameIndex(
                name: "IX_UserVoucher_UserId",
                table: "UserVouchers",
                newName: "IX_UserVouchers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVouchers",
                table: "UserVouchers",
                column: "UV_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTokens",
                table: "ServiceTokens",
                column: "ST_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Captchas",
                table: "Captchas",
                column: "TokenID");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "45362ee3-275f-4de5-bd7b-64ce27cf6e8a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bb2e0f3f-ee7c-4e49-9d3f-7d60b12b2e6b");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "117f982c-ad56-47dd-93b4-5db6d7b3c256");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "RoleID",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "7537e7e8-3f96-4730-b8b1-996c9e26c75d");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordExpirationDateTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef2fbd87-b2c6-4343-81f3-0d7815a15e45", new DateTime(2121, 1, 21, 17, 23, 0, 31, DateTimeKind.Local).AddTicks(9891), "AQAAAAEAACcQAAAAEOhanXONgJqNvhABr4rZGsxkDj2IfnityhCuMCQm2nGMIKqxoLoJKFy2vMWGDVh/cw==", "7617cf59-47d2-4c9c-a8ba-e7dbd3b714ba" });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordExpirationDateTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57a8fff0-8717-45ec-bb65-972aef26f6b7", new DateTime(2121, 1, 21, 17, 23, 0, 47, DateTimeKind.Local).AddTicks(426), "AQAAAAEAACcQAAAAEBKAMOgEcwoXZlp0LKA/s+ZsbK9QprBB5hRlhdvyLBONjtIVJzaPDovi1NxJ/hujSQ==", "0561b499-c557-4d79-9e3d-957edefb1d6b" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserVouchers_User_UserId",
                table: "UserVouchers",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
