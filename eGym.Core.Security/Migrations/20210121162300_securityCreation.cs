using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Security.Migrations
{
    public partial class securityCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Captchas",
                columns: table => new
                {
                    TokenID = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Validity = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captchas", x => x.TokenID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTokens",
                columns: table => new
                {
                    ST_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ST_Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ST_RequestorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ST_Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ST_IssueDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTokens", x => x.ST_ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    PINHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    EmailConfirmedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumberConfirmedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordExpirationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrivacyAcceptanceDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PolicyAcceptanceDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TwoFactorTokenProviders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "ntext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "Security",
                columns: table => new
                {
                    RoleClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.RoleClaimID);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordHistory_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Security",
                columns: table => new
                {
                    UserClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.UserClaimID);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVouchers",
                columns: table => new
                {
                    UV_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UV_StatusID = table.Column<int>(type: "int", nullable: false),
                    UV_Voucher = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    UV_VoucherCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UV_VoucherExpirationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UV_SentToAnagID = table.Column<int>(type: "int", nullable: true),
                    UV_SentToEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_SentToFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_SentToLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_SentToTaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_SentToMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_SentToRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_SentToCulture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    UV_AnagID = table.Column<int>(type: "int", nullable: true),
                    UV_RegistrationStartedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UV_RegistrationCompletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UV_RegistrationClosedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UV_TemporaryToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_TemporaryTokenExpirationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UV_UserID = table.Column<int>(type: "int", nullable: true),
                    UV_UserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_UserLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_UserMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UV_PrivacyAcceptanceDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UV_PolicyAcceptanceDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UV_LastModifyByUserID = table.Column<int>(type: "int", nullable: false),
                    UV_LastModifyDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVouchers", x => x.UV_ID);
                    table.ForeignKey(
                        name: "FK_UserVouchers_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "RoleID", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "45362ee3-275f-4de5-bd7b-64ce27cf6e8a", "Auto", "AUTO" },
                    { 2, "bb2e0f3f-ee7c-4e49-9d3f-7d60b12b2e6b", "SysAdmin", "SYSADMIN" },
                    { 3, "117f982c-ad56-47dd-93b4-5db6d7b3c256", "Administarator", "ADMINISTARATOR" },
                    { 4, "7537e7e8-3f96-4730-b8b1-996c9e26c75d", "User", "USER" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "User",
                columns: new[] { "UserID", "AccessFailedCount", "ConcurrencyStamp", "Culture", "Disabled", "Email", "EmailConfirmed", "EmailConfirmedDateTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PINHash", "PasswordExpirationDateTime", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhoneNumberConfirmedDateTime", "PolicyAcceptanceDateTime", "PrivacyAcceptanceDateTime", "SecurityStamp", "TwoFactorEnabled", "TwoFactorTokenProviders", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "ef2fbd87-b2c6-4343-81f3-0d7815a15e45", "it-IT", true, "dev@digitalbubbles.cloud", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "DEV@DIGITALBUBBLES.CLOUD", "AUTO", null, new DateTime(2121, 1, 21, 17, 23, 0, 31, DateTimeKind.Local).AddTicks(9891), "AQAAAAEAACcQAAAAEOhanXONgJqNvhABr4rZGsxkDj2IfnityhCuMCQm2nGMIKqxoLoJKFy2vMWGDVh/cw==", null, false, null, null, null, "7617cf59-47d2-4c9c-a8ba-e7dbd3b714ba", false, null, "auto" },
                    { 2, 0, "57a8fff0-8717-45ec-bb65-972aef26f6b7", "it-IT", false, "info@digitalbubbles.cloud", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "INFO@DIGITALBUBBLES.CLOUD", "ADMIN", null, new DateTime(2121, 1, 21, 17, 23, 0, 47, DateTimeKind.Local).AddTicks(426), "AQAAAAEAACcQAAAAEBKAMOgEcwoXZlp0LKA/s+ZsbK9QprBB5hRlhdvyLBONjtIVJzaPDovi1NxJ/hujSQ==", null, false, null, null, null, "0561b499-c557-4d79-9e3d-957edefb1d6b", false, null, "admin" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "RoleClaim",
                columns: new[] { "RoleClaimID", "ClaimType", "ClaimValue", "RoleID" },
                values: new object[,]
                {
                    { 1, "AUTO", "1", 1 },
                    { 2, "GOD", "1", 2 },
                    { 3, "ADMINISTRATOR", "1", 2 },
                    { 4, "USER", "1", 2 },
                    { 5, "ADMINISTRATOR", "1", 3 },
                    { 6, "USER", "1", 3 },
                    { 7, "USER", "1", 4 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRole",
                columns: new[] { "Id", "RoleID", "UserID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistory_UserID",
                schema: "Security",
                table: "PasswordHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleID",
                schema: "Security",
                table: "RoleClaim",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserID",
                schema: "Security",
                table: "UserClaim",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserID",
                schema: "Security",
                table: "UserLogin",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                schema: "Security",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                schema: "Security",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserID",
                schema: "Security",
                table: "UserToken",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_UserId",
                table: "UserVouchers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captchas");

            migrationBuilder.DropTable(
                name: "PasswordHistory",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ServiceTokens");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserVouchers");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");
        }
    }
}
