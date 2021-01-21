using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class applicationCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "ath");

            migrationBuilder.EnsureSchema(
                name: "cms");

            migrationBuilder.EnsureSchema(
                name: "conf");

            migrationBuilder.CreateTable(
                name: "CMS_Master",
                schema: "cms",
                columns: table => new
                {
                    CMS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMS_GroupKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CMS_ContextKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CMS_Value = table.Column<string>(type: "ntext", nullable: false),
                    CMS_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CMS_EditorTypeID = table.Column<short>(type: "smallint", nullable: false),
                    CMS_Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_Master", x => x.CMS_ID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "dbo",
                columns: table => new
                {
                    Country_IsoCode = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    Country_CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country_VIESCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Country_Language = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Country_CFiscCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Country_IsoCode);
                });

            migrationBuilder.CreateTable(
                name: "Sport_Division",
                schema: "conf",
                columns: table => new
                {
                    SD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SD_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SD_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SD_Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_Division", x => x.SD_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sport_EventResult",
                schema: "conf",
                columns: table => new
                {
                    SER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SER_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SER_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SER_ValueForRanking = table.Column<int>(type: "int", nullable: false),
                    SER_Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_EventResult", x => x.SER_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sport_EventResultType",
                schema: "conf",
                columns: table => new
                {
                    SERT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SERT_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SERT_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SERT_Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_EventResultType", x => x.SERT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sport_Level",
                schema: "conf",
                columns: table => new
                {
                    SL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SL_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SL_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SL_Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_Level", x => x.SL_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sport_Master",
                schema: "conf",
                columns: table => new
                {
                    Spr_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Spr_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Spr_FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Spr_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spr_FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    Spr_ToDate = table.Column<DateTime>(type: "date", nullable: true),
                    Spr_Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_Master", x => x.Spr_ID);
                });

            migrationBuilder.CreateTable(
                name: "CMS_History",
                schema: "cms",
                columns: table => new
                {
                    CMSH_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMSH_Value = table.Column<string>(type: "ntext", nullable: false),
                    CMSH_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CMSH_Active = table.Column<bool>(type: "bit", nullable: false),
                    CMSH_MasterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_History", x => x.CMSH_ID);
                    table.ForeignKey(
                        name: "FK_CMS_History_CMS_Master_CMSH_MasterID",
                        column: x => x.CMSH_MasterID,
                        principalSchema: "cms",
                        principalTable: "CMS_Master",
                        principalColumn: "CMS_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anag_Master",
                schema: "dbo",
                columns: table => new
                {
                    Ang_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ang_FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ang_LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ang_TaxCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Ang_VATNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Ang_BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Ang_BirthCity = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ang_BirthCountrySpec = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Ang_BirthCountry = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    Ang_Citizenship = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Ang_Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ang_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ang_GenderID = table.Column<short>(type: "smallint", nullable: false),
                    Ang_UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_Master", x => x.Ang_ID);
                    table.ForeignKey(
                        name: "FK_Anag_Master_Country_Ang_BirthCountry",
                        column: x => x.Ang_BirthCountry,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Country_IsoCode");
                });

            migrationBuilder.CreateTable(
                name: "Sport_DivisionLocalized",
                schema: "conf",
                columns: table => new
                {
                    SDL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SDL_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SDL_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDL_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDL_MinAge = table.Column<int>(type: "int", nullable: true),
                    SDL_MaxAge = table.Column<int>(type: "int", nullable: true),
                    SDL_MinWeight = table.Column<double>(type: "float", nullable: true),
                    SDL_MaxWeight = table.Column<double>(type: "float", nullable: true),
                    SDL_UnitOfMeasureID = table.Column<short>(type: "smallint", nullable: true),
                    SDL_DivisionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_DivisionLocalized", x => x.SDL_ID);
                    table.ForeignKey(
                        name: "FK_Sport_DivisionLocalized_Sport_Division_SDL_DivisionID",
                        column: x => x.SDL_DivisionID,
                        principalSchema: "conf",
                        principalTable: "Sport_Division",
                        principalColumn: "SD_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport_EventResultLocalized",
                schema: "conf",
                columns: table => new
                {
                    SerL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerL_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SerL_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SerL_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerL_EventResultID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_EventResultLocalized", x => x.SerL_ID);
                    table.ForeignKey(
                        name: "FK_Sport_EventResultLocalized_Sport_EventResult_SerL_EventResultID",
                        column: x => x.SerL_EventResultID,
                        principalSchema: "conf",
                        principalTable: "Sport_EventResult",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport_EventResultTypeLocalized",
                schema: "conf",
                columns: table => new
                {
                    SertL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SertL_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SertL_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SertL_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SertL_EventResultTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_EventResultTypeLocalized", x => x.SertL_ID);
                    table.ForeignKey(
                        name: "FK_Sport_EventResultTypeLocalized_Sport_EventResultType_SertL_EventResultTypeID",
                        column: x => x.SertL_EventResultTypeID,
                        principalSchema: "conf",
                        principalTable: "Sport_EventResultType",
                        principalColumn: "SERT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport_LevelLocalized",
                schema: "conf",
                columns: table => new
                {
                    SLL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SLL_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SLL_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SLL_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLL_MinAge = table.Column<int>(type: "int", nullable: true),
                    SLL_MaxAge = table.Column<int>(type: "int", nullable: true),
                    SLL_LevelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_LevelLocalized", x => x.SLL_ID);
                    table.ForeignKey(
                        name: "FK_Sport_LevelLocalized_Sport_Level_SLL_LevelID",
                        column: x => x.SLL_LevelID,
                        principalSchema: "conf",
                        principalTable: "Sport_Level",
                        principalColumn: "SL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport_DivisionXSport",
                schema: "conf",
                columns: table => new
                {
                    DXS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DXS_SportID = table.Column<int>(type: "int", nullable: false),
                    DXS_DivisionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_DivisionXSport", x => x.DXS_ID);
                    table.ForeignKey(
                        name: "FK_Sport_DivisionXSport_Sport_Division_DXS_DivisionID",
                        column: x => x.DXS_DivisionID,
                        principalSchema: "conf",
                        principalTable: "Sport_Division",
                        principalColumn: "SD_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sport_DivisionXSport_Sport_Master_DXS_SportID",
                        column: x => x.DXS_SportID,
                        principalSchema: "conf",
                        principalTable: "Sport_Master",
                        principalColumn: "Spr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport_LevelXSport",
                schema: "conf",
                columns: table => new
                {
                    LXS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LXS_SportID = table.Column<int>(type: "int", nullable: false),
                    LXS_LevelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_LevelXSport", x => x.LXS_ID);
                    table.ForeignKey(
                        name: "FK_Sport_LevelXSport_Sport_Level_LXS_LevelID",
                        column: x => x.LXS_LevelID,
                        principalSchema: "conf",
                        principalTable: "Sport_Level",
                        principalColumn: "SL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sport_LevelXSport_Sport_Master_LXS_SportID",
                        column: x => x.LXS_SportID,
                        principalSchema: "conf",
                        principalTable: "Sport_Master",
                        principalColumn: "Spr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport_Schedule",
                schema: "conf",
                columns: table => new
                {
                    SS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SS_DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    SS_Everyday = table.Column<bool>(type: "bit", nullable: false),
                    SS_FromTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    SS_ToTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    SS_AllowSelfRegistration = table.Column<bool>(type: "bit", nullable: false),
                    SS_RequireRegistration = table.Column<bool>(type: "bit", nullable: false),
                    SS_RegistrationCloseBeforeMinutes = table.Column<int>(type: "int", nullable: true),
                    SS_SportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport_Schedule", x => x.SS_ID);
                    table.ForeignKey(
                        name: "FK_Sport_Schedule_Sport_Master_SS_SportID",
                        column: x => x.SS_SportID,
                        principalSchema: "conf",
                        principalTable: "Sport_Master",
                        principalColumn: "Spr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anag_Address",
                schema: "dbo",
                columns: table => new
                {
                    Adr_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adr_Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Adr_Address1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Adr_Address2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Adr_HouseNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Adr_Staircase = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Adr_Interior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Adr_Floor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Adr_City = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Adr_PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Adr_CountrySpec = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Adr_District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adr_Country = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    Adr_AnagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_Address", x => x.Adr_ID);
                    table.ForeignKey(
                        name: "FK_Anag_Address_Anag_Master_Adr_AnagID",
                        column: x => x.Adr_AnagID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Master",
                        principalColumn: "Ang_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anag_Address_Country_Adr_Country",
                        column: x => x.Adr_Country,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Country_IsoCode");
                });

            migrationBuilder.CreateTable(
                name: "Anag_Contact",
                schema: "dbo",
                columns: table => new
                {
                    Cnt_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnt_Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cnt_DefaultInType = table.Column<bool>(type: "bit", nullable: false),
                    Cnt_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnt_AnagID = table.Column<int>(type: "int", nullable: false),
                    Cnt_TypeID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_Contact", x => x.Cnt_ID);
                    table.ForeignKey(
                        name: "FK_Anag_Contact_Anag_Master_Cnt_AnagID",
                        column: x => x.Cnt_AnagID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Master",
                        principalColumn: "Ang_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anag_CorporateRole",
                schema: "dbo",
                columns: table => new
                {
                    CR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CR_StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    CR_EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    CR_AnagID = table.Column<int>(type: "int", nullable: false),
                    CR_RoleID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_CorporateRole", x => x.CR_ID);
                    table.ForeignKey(
                        name: "FK_Anag_CorporateRole_Anag_Master_CR_AnagID",
                        column: x => x.CR_AnagID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Master",
                        principalColumn: "Ang_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anag_Document",
                schema: "dbo",
                columns: table => new
                {
                    Doc_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doc_CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Doc_Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Doc_IssueDate = table.Column<DateTime>(type: "date", nullable: true),
                    Doc_ExpiringDate = table.Column<DateTime>(type: "date", nullable: true),
                    Doc_Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Doc_EmissionCountry = table.Column<string>(type: "char(3)", nullable: true),
                    Doc_EmissionCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Doc_EmissionNote = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Doc_EmitterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Doc_Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doc_TypeID = table.Column<short>(type: "smallint", nullable: false),
                    Doc_EmitterID = table.Column<short>(type: "smallint", nullable: true),
                    Doc_AnagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_Document", x => x.Doc_ID);
                    table.ForeignKey(
                        name: "FK_Anag_Document_Anag_Master_Doc_AnagID",
                        column: x => x.Doc_AnagID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Master",
                        principalColumn: "Ang_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anag_MasterRole",
                schema: "dbo",
                columns: table => new
                {
                    AngR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AngR_StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    AngR_EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    AngR_AnagID = table.Column<int>(type: "int", nullable: false),
                    AngR_RoleID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_MasterRole", x => x.AngR_ID);
                    table.ForeignKey(
                        name: "FK_Anag_MasterRole_Anag_Master_AngR_AnagID",
                        column: x => x.AngR_AnagID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Master",
                        principalColumn: "Ang_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Athlete_Master",
                schema: "ath",
                columns: table => new
                {
                    Ath_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ath_Weight = table.Column<double>(type: "float", nullable: true),
                    Ath_Ranking = table.Column<double>(type: "float", nullable: true),
                    Ath_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ath_DivisionID = table.Column<int>(type: "int", nullable: true),
                    Ath_LevelID = table.Column<int>(type: "int", nullable: false),
                    Ath_SportID = table.Column<int>(type: "int", nullable: false),
                    Ath_AnagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athlete_Master", x => x.Ath_ID);
                    table.ForeignKey(
                        name: "FK_Athlete_Master_Anag_Master_Ath_AnagID",
                        column: x => x.Ath_AnagID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Master",
                        principalColumn: "Ang_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Athlete_Master_Sport_Master_Ath_SportID",
                        column: x => x.Ath_SportID,
                        principalSchema: "conf",
                        principalTable: "Sport_Master",
                        principalColumn: "Spr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anag_AddressRole",
                schema: "dbo",
                columns: table => new
                {
                    AdrR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdrR_StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    AdrR_EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    AdrR_AddressID = table.Column<int>(type: "int", nullable: false),
                    AdrR_RoleID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anag_AddressRole", x => x.AdrR_ID);
                    table.ForeignKey(
                        name: "FK_Anag_AddressRole_Anag_Address_AdrR_AddressID",
                        column: x => x.AdrR_AddressID,
                        principalSchema: "dbo",
                        principalTable: "Anag_Address",
                        principalColumn: "Adr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Athlete_DivisionXAthlete",
                schema: "ath",
                columns: table => new
                {
                    DXA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DXA_FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    DXA_ToDate = table.Column<DateTime>(type: "date", nullable: true),
                    DXA_DivisionID = table.Column<int>(type: "int", nullable: false),
                    DXA_AthleteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athlete_DivisionXAthlete", x => x.DXA_ID);
                    table.ForeignKey(
                        name: "FK_Athlete_DivisionXAthlete_Athlete_Master_DXA_AthleteID",
                        column: x => x.DXA_AthleteID,
                        principalSchema: "ath",
                        principalTable: "Athlete_Master",
                        principalColumn: "Ath_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Athlete_DivisionXAthlete_Sport_Division_DXA_DivisionID",
                        column: x => x.DXA_DivisionID,
                        principalSchema: "conf",
                        principalTable: "Sport_Division",
                        principalColumn: "SD_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Athlete_LevelXAthlete",
                schema: "ath",
                columns: table => new
                {
                    LXA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LXA_FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    LXA_ToDate = table.Column<DateTime>(type: "date", nullable: true),
                    LXA_LevelID = table.Column<int>(type: "int", nullable: false),
                    LXA_AthleteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athlete_LevelXAthlete", x => x.LXA_ID);
                    table.ForeignKey(
                        name: "FK_Athlete_LevelXAthlete_Athlete_Master_LXA_AthleteID",
                        column: x => x.LXA_AthleteID,
                        principalSchema: "ath",
                        principalTable: "Athlete_Master",
                        principalColumn: "Ath_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Athlete_LevelXAthlete_Sport_Level_LXA_LevelID",
                        column: x => x.LXA_LevelID,
                        principalSchema: "conf",
                        principalTable: "Sport_Level",
                        principalColumn: "SL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Athlete_WeightXAthlete",
                schema: "ath",
                columns: table => new
                {
                    WXA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WXA_Weight = table.Column<double>(type: "float", nullable: false),
                    WXA_FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    WXA_ToDate = table.Column<DateTime>(type: "date", nullable: true),
                    WXA_AthleteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athlete_WeightXAthlete", x => x.WXA_ID);
                    table.ForeignKey(
                        name: "FK_Athlete_WeightXAthlete_Athlete_Master_WXA_AthleteID",
                        column: x => x.WXA_AthleteID,
                        principalSchema: "ath",
                        principalTable: "Athlete_Master",
                        principalColumn: "Ath_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_Division",
                columns: new[] { "SD_ID", "SD_Description", "SD_Name", "SD_Note" },
                values: new object[,]
                {
                    { 4, null, "FeatherWeight", null },
                    { 1, null, "StrawWeight", null },
                    { 2, null, "FlyWeight", null },
                    { 3, null, "BantamWeight", null },
                    { 6, null, "WelterWeight", null },
                    { 7, null, "MiddleWeight", null },
                    { 8, null, "LightHeavyWeight", null },
                    { 9, null, "HeavyWeight", null },
                    { 5, null, "LightWeight", null }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_EventResult",
                columns: new[] { "SER_ID", "SER_Description", "SER_Name", "SER_Note", "SER_ValueForRanking" },
                values: new object[,]
                {
                    { 1, null, "Defeat", null, 0 },
                    { 3, null, "Victory", null, 1 },
                    { 2, null, "Draw", null, 0 }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_EventResultType",
                columns: new[] { "SERT_ID", "SERT_Description", "SERT_Name", "SERT_Note" },
                values: new object[,]
                {
                    { 1, null, "Unanimus decision", null },
                    { 3, null, "TKO", null },
                    { 4, null, "KO", null },
                    { 5, null, "Submission", null },
                    { 6, null, "Medical decision", null },
                    { 2, null, "Split decision", null }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_Level",
                columns: new[] { "SL_ID", "SL_Description", "SL_Name", "SL_Note" },
                values: new object[,]
                {
                    { 5, null, "Veteran", null },
                    { 4, null, "Pro", null },
                    { 2, null, "Amateur", null },
                    { 1, null, "Rookie", null },
                    { 3, null, "SemiPro", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Country_IsoCode", "Country_CFiscCode", "Country_CountryName", "Country_Language", "Country_VIESCode" },
                values: new object[,]
                {
                    { "540", null, "Nuova Caledonia", "ENG", null },
                    { "554", null, "Nuova Zelanda", "ENG", null },
                    { "528", null, "Olanda (Paesi Bassi)", "ENG", "NL" },
                    { "512", null, "Oman", "ENG", null },
                    { "586", null, "Pakistan", "ENG", null },
                    { "585", null, "Palau", "ENG", null },
                    { "591", null, "Panama", "ENG", null },
                    { "598", null, "Papua Nuova Guinea", "ENG", null },
                    { "600", null, "Paraguay", "ENG", null },
                    { "604", null, "Perù", "ENG", null },
                    { "578", null, "Norvegia", "ENG", null },
                    { "258", null, "Polinesia Francese", "ENG", null },
                    { "620", null, "Portogallo", "ENG", "PT" },
                    { "630", null, "Portorico", "ENG", null },
                    { "634", null, "Qatar", "ENG", null },
                    { "826", null, "Regno Unito", "ENG", "GB" },
                    { "638", null, "Reunione", "ENG", null },
                    { "642", null, "Romania", "ENG", "RO" },
                    { "646", null, "Ruanda", "ENG", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Country_IsoCode", "Country_CFiscCode", "Country_CountryName", "Country_Language", "Country_VIESCode" },
                values: new object[,]
                {
                    { "643", null, "Russia", "ENG", null },
                    { "659", null, "Saint Kittis e Nevis", "ENG", null },
                    { "662", null, "Saint Lucia", "ENG", null },
                    { "616", null, "Polonia", "ENG", "PL" },
                    { "470", null, "Malta", "ENG", "MT" },
                    { "574", null, "Norfolk", "ENG", null },
                    { "570", null, "Niue", "ENG", null },
                    { "580", null, "Marianne", "ENG", null },
                    { "504", null, "Marocco", "ENG", null },
                    { "584", null, "Marshall", "ENG", null },
                    { "474", null, "Martinica", "ENG", null },
                    { "478", null, "Mauritania", "ENG", null },
                    { "480", null, "Mauritius", "ENG", null },
                    { "175", null, "Mayotte", "ENG", null },
                    { "484", null, "Messico", "ENG", null },
                    { "583", null, "Micronesia", "ENG", null },
                    { "498", null, "Moldavia", "ENG", null },
                    { "492", null, "Monaco Principato", "ENG", null },
                    { "496", null, "Mongolia", "ENG", null },
                    { "500", null, "Monserrat", "ENG", null },
                    { "508", null, "Mozambico", "ENG", null },
                    { "104", null, "Myanmar (Birmania)", "ENG", null },
                    { "516", null, "Namibia", "ENG", null },
                    { "520", null, "Nauru", "ENG", null },
                    { "524", null, "Nepal", "ENG", null },
                    { "558", null, "Nicaragua", "ENG", null },
                    { "562", null, "Niger", "ENG", null },
                    { "566", null, "Nigeria", "ENG", null },
                    { "666", null, "Saint Pierre e Miquelon", "ENG", null },
                    { "999", null, "Non Classificabile", "ENG", null },
                    { "090", null, "Salomone", "ENG", null },
                    { "882", null, "Samoa", "ENG", null },
                    { "780", null, "Trinidad e Tobago", "ENG", null },
                    { "788", null, "Tunisia", "ENG", null },
                    { "792", null, "Turchia", "ENG", null },
                    { "795", null, "Turkmenistan", "ENG", null },
                    { "796", null, "Turks e Caicos", "ENG", null },
                    { "798", null, "Tuvalu", "ENG", null },
                    { "804", null, "Ucraina", "ENG", null },
                    { "800", null, "Uganda", "ENG", null },
                    { "348", null, "Ungheria", "ENG", "HU" },
                    { "858", null, "Uruguay", "ENG", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Country_IsoCode", "Country_CFiscCode", "Country_CountryName", "Country_Language", "Country_VIESCode" },
                values: new object[,]
                {
                    { "776", null, "Tonga", "ENG", null },
                    { "860", null, "Uzbekistan", "ENG", null },
                    { "336", null, "Vaticano", "ENG", null },
                    { "862", null, "Venezuela", "ENG", null },
                    { "850", null, "Vergini Is. Americane", "ENG", null },
                    { "092", null, "Vergini Is. Britanniche", "ENG", null },
                    { "704", null, "Vietnam", "ENG", null },
                    { "581", null, "Wake", "ENG", null },
                    { "876", null, "Wallis e Futuna", "ENG", null },
                    { "887", null, "Yemen", "ENG", null },
                    { "894", null, "Zambia", "ENG", null },
                    { "716", null, "Zimbabwe", "ENG", null },
                    { "548", null, "Vanuatu", "ENG", null },
                    { "670", null, "Saint Vincent e Grenadine", "ENG", null },
                    { "768", null, "Togo", "ENG", null },
                    { "834", null, "Tanzania", "ENG", null },
                    { "016", null, "Samoa Americane", "ENG", null },
                    { "674", null, "San Marino", "ENG", null },
                    { "678", null, "Sao Tomè e Principe", "ENG", null },
                    { "686", null, "Senegal", "ENG", null },
                    { "690", null, "Seychelles", "ENG", null },
                    { "694", null, "Sierra Leone", "ENG", null },
                    { "702", null, "Singapore", "ENG", null },
                    { "760", null, "Siria", "ENG", null },
                    { "466", null, "Mali", "ENG", null },
                    { "705", null, "Slovenia", "ENG", "SI" },
                    { "764", null, "Thailandia", "ENG", null },
                    { "706", null, "Somalia", "ENG", null },
                    { "144", null, "Sri Lanka", "ENG", null },
                    { "840", null, "Stati Uniti d'America", "ENG", null },
                    { "710", null, "Sudafricana Repubblica", "ENG", null },
                    { "736", null, "Sudan", "ENG", null },
                    { "740", null, "Suriname", "ENG", null },
                    { "752", null, "Svezia", "ENG", "SE" },
                    { "756", null, "Svizzera", "ENG", null },
                    { "748", null, "Swaziland", "ENG", null },
                    { "762", null, "Tagikistan", "ENG", null },
                    { "158", null, "Taiwan", "ENG", null },
                    { "724", null, "Spagna", "ENG", "ES" },
                    { "703", null, "Slovacca Repubblica", "ENG", "SK" },
                    { "462", null, "Maldive", "ENG", null },
                    { "454", null, "Malawi", "ENG", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Country_IsoCode", "Country_CFiscCode", "Country_CountryName", "Country_Language", "Country_VIESCode" },
                values: new object[,]
                {
                    { "854", null, "Burkina Faso (Alto Volta)", "ENG", null },
                    { "108", null, "Burundi", "ENG", null },
                    { "120", null, "Camerun", "ENG", null },
                    { "124", null, "Canada", "ENG", null },
                    { "132", null, "Capo Verde", "ENG", null },
                    { "136", null, "Cayman", "ENG", null },
                    { "203", null, "Ceca Repubblica", "ENG", "CZ" },
                    { "140", null, "Centrafricana Repubblica", "ENG", null },
                    { "148", null, "Ciad", "ENG", null },
                    { "152", null, "Cile", "ENG", null },
                    { "156", null, "Cina", "ENG", null },
                    { "196", null, "Cipro", "ENG", "CY" },
                    { "170", null, "Colombia", "ENG", null },
                    { "174", null, "Comore", "ENG", null },
                    { "180", null, "Congo Rep.Dem. (Zaire)", "ENG", null },
                    { "178", null, "Congo Repubblica", "ENG", null },
                    { "184", null, "Cook", "ENG", null },
                    { "408", null, "Corea del Nord", "ENG", null },
                    { "410", null, "Corea del Sud", "ENG", null },
                    { "384", null, "Costa d'Avorio", "ENG", null },
                    { "188", null, "Costa Rica", "ENG", null },
                    { "191", null, "Croazia", "ENG", "HR" },
                    { "192", null, "Cuba", "ENG", null },
                    { "208", null, "Danimarca", "ENG", "DK" },
                    { "212", null, "Dominica", "ENG", null },
                    { "214", null, "Dominicana Repubblica", "ENG", null },
                    { "218", null, "Ecuador", "ENG", null },
                    { "100", null, "Bulgaria", "ENG", "BG" },
                    { "818", null, "Egitto", "ENG", null },
                    { "096", null, "Brunei", "ENG", null },
                    { "072", null, "Botswana", "ENG", null },
                    { "008", null, "Albania", "ENG", null },
                    { "012", null, "Algeria", "ENG", null },
                    { "020", null, "Andorra", "ENG", null },
                    { "024", null, "Angola", "ENG", null },
                    { "660", null, "Anguilla", "ENG", null },
                    { "028", null, "Antigua e Barbuda", "ENG", null },
                    { "530", null, "Antille Olandesi", "ENG", null },
                    { "682", null, "Arabia Saudita", "ENG", null },
                    { "032", null, "Argentina", "ENG", null },
                    { "051", null, "Armenia", "ENG", null },
                    { "533", null, "Aruba", "ENG", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Country_IsoCode", "Country_CFiscCode", "Country_CountryName", "Country_Language", "Country_VIESCode" },
                values: new object[,]
                {
                    { "654", null, "Ascension", "ENG", null },
                    { "036", null, "Australia", "ENG", null },
                    { "040", null, "Austria", "ENG", "AT" },
                    { "031", null, "Azerbaigian", "ENG", null },
                    { "044", null, "Bahamas", "ENG", null },
                    { "048", null, "Bahrein", "ENG", null },
                    { "050", null, "Bangladesh", "ENG", null },
                    { "052", null, "Barbados", "ENG", null },
                    { "056", null, "Belgio", "ENG", "BE" },
                    { "084", null, "Belize", "ENG", null },
                    { "204", null, "Benin", "ENG", null },
                    { "060", null, "Bermuda", "ENG", null },
                    { "064", null, "Bhutan", "ENG", null },
                    { "112", null, "Bielorussia", "ENG", null },
                    { "068", null, "Bolivia", "ENG", null },
                    { "070", null, "Bosnia Erzegovina", "ENG", null },
                    { "076", null, "Brasile", "ENG", null },
                    { "222", null, "El Salvador", "ENG", null },
                    { "784", null, "Emirati Arabi Uniti", "ENG", null },
                    { "232", null, "Eritrea", "ENG", null },
                    { "356", null, "India", "ENG", null },
                    { "360", null, "Indonesia", "ENG", null },
                    { "364", null, "Iran", "ENG", null },
                    { "368", null, "Iraq", "ENG", null },
                    { "372", null, "Irlanda", "ENG", "IE" },
                    { "352", null, "Islanda", "ENG", null },
                    { "376", null, "Israele", "ENG", null },
                    { "380", null, "Italia", "ITA", "IT" },
                    { "891", null, "Jugoslavia", "ENG", null },
                    { "116", null, "Kampuchea (Cambogia)", "ENG", null },
                    { "398", null, "Kazakistan", "ENG", null },
                    { "404", null, "Kenya", "ENG", null },
                    { "417", null, "Kirghizistan", "ENG", null },
                    { "296", null, "Kiribati", "ENG", null },
                    { "414", null, "Kuwait", "ENG", null },
                    { "418", null, "Laos", "ENG", null },
                    { "426", null, "Lesotho", "ENG", null },
                    { "428", null, "Lettonia", "ENG", "LV" },
                    { "422", null, "Libano", "ENG", null },
                    { "430", null, "Liberia", "ENG", null },
                    { "434", null, "Libia", "ENG", null },
                    { "438", null, "Liechtenstein", "ENG", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Country",
                columns: new[] { "Country_IsoCode", "Country_CFiscCode", "Country_CountryName", "Country_Language", "Country_VIESCode" },
                values: new object[,]
                {
                    { "440", null, "Lituania", "ENG", "LT" },
                    { "442", null, "Lussemburgo", "ENG", "LU" },
                    { "446", null, "Macao", "ENG", null },
                    { "807", null, "Macedonia", "ENG", null },
                    { "450", null, "Madagascar", "ENG", null },
                    { "344", null, "Hong Kong", "ENG", null },
                    { "340", null, "Honduras", "ENG", null },
                    { "332", null, "Haiti", "ENG", null },
                    { "328", null, "Guyana", "ENG", null },
                    { "233", null, "Estonia", "ENG", "EE" },
                    { "231", null, "Etiopia", "ENG", null },
                    { "234", null, "Faeroer", "ENG", null },
                    { "238", null, "Falkland", "ENG", null },
                    { "242", null, "Figi", "ENG", null },
                    { "608", null, "Filippine", "ENG", null },
                    { "246", null, "Finlandia", "ENG", "FI" },
                    { "250", null, "Francia", "ENG", "FR" },
                    { "266", null, "Gabon", "ENG", null },
                    { "270", null, "Gambia", "ENG", null },
                    { "268", null, "Georgia", "ENG", null },
                    { "276", null, "Germania", "ENG", "DE" },
                    { "288", null, "Ghana", "ENG", null },
                    { "458", null, "Malaysia", "ENG", null },
                    { "388", null, "Giamaica", "ENG", null },
                    { "292", null, "Gibilterra", "ENG", null },
                    { "262", null, "Gibuti", "ENG", null },
                    { "400", null, "Giordania", "ENG", null },
                    { "300", null, "Grecia", "ENG", "EL" },
                    { "308", null, "Grenada", "ENG", null },
                    { "304", null, "Groenlandia", "ENG", null },
                    { "312", null, "Guadalupa", "ENG", null },
                    { "316", null, "Guam", "ENG", null },
                    { "320", null, "Guatemala", "ENG", null },
                    { "254", null, "Guayana Francese", "ENG", null },
                    { "624", null, "Guinea Bissau", "ENG", null },
                    { "226", null, "Guinea Equatoriale", "ENG", null },
                    { "324", null, "Guinea Repubblica", "ENG", null },
                    { "392", null, "Giappone", "ENG", null },
                    { "004", null, "Afganistan", "ENG", null }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_DivisionLocalized",
                columns: new[] { "SDL_ID", "SDL_Culture", "SDL_Description", "SDL_DivisionID", "SDL_MaxAge", "SDL_MaxWeight", "SDL_MinAge", "SDL_MinWeight", "SDL_Name", "SDL_UnitOfMeasureID" },
                values: new object[,]
                {
                    { 1, "it", null, 1, null, 52.200000000000003, null, null, "Paglia", (short)1 },
                    { 2, "it", null, 2, null, 56.700000000000003, null, 52.200000000000003, "Mosca", (short)1 },
                    { 3, "it", null, 3, null, 61.200000000000003, null, 56.700000000000003, "Gallo", (short)1 },
                    { 4, "it", null, 4, null, 65.799999999999997, null, 61.200000000000003, "Piuma", (short)1 },
                    { 5, "it", null, 5, null, 70.299999999999997, null, 65.799999999999997, "Leggeri", (short)1 },
                    { 6, "it", null, 6, null, 77.099999999999994, null, 70.299999999999997, "Welter", (short)1 },
                    { 7, "it", null, 7, null, 83.900000000000006, null, 77.099999999999994, "Medi", (short)1 },
                    { 8, "it", null, 8, null, 93.0, null, 83.900000000000006, "Massimi leggeri", (short)1 },
                    { 9, "it", null, 9, null, 120.2, null, 93.0, "Massimi", (short)1 }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_EventResultLocalized",
                columns: new[] { "SerL_ID", "SerL_Culture", "SerL_Description", "SerL_EventResultID", "SerL_Name" },
                values: new object[,]
                {
                    { 2, "it", null, 2, "Pareggio" },
                    { 3, "it", null, 3, "Vittoria" },
                    { 1, "it", null, 1, "Sconfitta" }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_EventResultTypeLocalized",
                columns: new[] { "SertL_ID", "SertL_Culture", "SertL_Description", "SertL_EventResultTypeID", "SertL_Name" },
                values: new object[,]
                {
                    { 1, "it", null, 1, "Decisione unanime" },
                    { 2, "it", null, 2, "Split decision" },
                    { 3, "it", null, 3, "KO tecnico" },
                    { 4, "it", null, 4, "KO" },
                    { 5, "it", null, 5, "Sottomissione" },
                    { 6, "it", null, 6, "Decisione medica" }
                });

            migrationBuilder.InsertData(
                schema: "conf",
                table: "Sport_LevelLocalized",
                columns: new[] { "SLL_ID", "SLL_Culture", "SLL_Description", "SLL_LevelID", "SLL_MaxAge", "SLL_MinAge", "SLL_Name" },
                values: new object[,]
                {
                    { 4, "it", null, 4, null, null, "Pro" },
                    { 1, "it", null, 1, null, null, "Praticante amatoriale" },
                    { 2, "it", null, 2, null, null, "Esordiente" },
                    { 3, "it", null, 3, null, null, "SemiPro" },
                    { 5, "it", null, 5, null, null, "Veterano" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anag_Address_Adr_AnagID",
                schema: "dbo",
                table: "Anag_Address",
                column: "Adr_AnagID");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_Address_Adr_Country",
                schema: "dbo",
                table: "Anag_Address",
                column: "Adr_Country");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_AddressRole_AdrR_AddressID",
                schema: "dbo",
                table: "Anag_AddressRole",
                column: "AdrR_AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_Contact_Cnt_AnagID",
                schema: "dbo",
                table: "Anag_Contact",
                column: "Cnt_AnagID");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_CorporateRole_CR_AnagID",
                schema: "dbo",
                table: "Anag_CorporateRole",
                column: "CR_AnagID");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_Document_Doc_AnagID",
                schema: "dbo",
                table: "Anag_Document",
                column: "Doc_AnagID");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_Master_Ang_BirthCountry",
                schema: "dbo",
                table: "Anag_Master",
                column: "Ang_BirthCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Anag_MasterRole_AngR_AnagID",
                schema: "dbo",
                table: "Anag_MasterRole",
                column: "AngR_AnagID");

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_DivisionXAthlete_DXA_AthleteID",
                schema: "ath",
                table: "Athlete_DivisionXAthlete",
                column: "DXA_AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_DivisionXAthlete_DXA_DivisionID",
                schema: "ath",
                table: "Athlete_DivisionXAthlete",
                column: "DXA_DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_LevelXAthlete_LXA_AthleteID",
                schema: "ath",
                table: "Athlete_LevelXAthlete",
                column: "LXA_AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_LevelXAthlete_LXA_LevelID",
                schema: "ath",
                table: "Athlete_LevelXAthlete",
                column: "LXA_LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_Master_Ath_AnagID",
                schema: "ath",
                table: "Athlete_Master",
                column: "Ath_AnagID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_Master_Ath_SportID",
                schema: "ath",
                table: "Athlete_Master",
                column: "Ath_SportID");

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_WeightXAthlete_WXA_AthleteID",
                schema: "ath",
                table: "Athlete_WeightXAthlete",
                column: "WXA_AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_CMS_History_CMSH_MasterID",
                schema: "cms",
                table: "CMS_History",
                column: "CMSH_MasterID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Country_IsoCode",
                schema: "dbo",
                table: "Country",
                column: "Country_IsoCode",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Sport_DivisionLocalized_SDL_DivisionID",
                schema: "conf",
                table: "Sport_DivisionLocalized",
                column: "SDL_DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_DivisionXSport_DXS_DivisionID",
                schema: "conf",
                table: "Sport_DivisionXSport",
                column: "DXS_DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_DivisionXSport_DXS_SportID",
                schema: "conf",
                table: "Sport_DivisionXSport",
                column: "DXS_SportID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_EventResultLocalized_SerL_EventResultID",
                schema: "conf",
                table: "Sport_EventResultLocalized",
                column: "SerL_EventResultID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_EventResultTypeLocalized_SertL_EventResultTypeID",
                schema: "conf",
                table: "Sport_EventResultTypeLocalized",
                column: "SertL_EventResultTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_LevelLocalized_SLL_LevelID",
                schema: "conf",
                table: "Sport_LevelLocalized",
                column: "SLL_LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_LevelXSport_LXS_LevelID",
                schema: "conf",
                table: "Sport_LevelXSport",
                column: "LXS_LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_LevelXSport_LXS_SportID",
                schema: "conf",
                table: "Sport_LevelXSport",
                column: "LXS_SportID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_Schedule_SS_SportID",
                schema: "conf",
                table: "Sport_Schedule",
                column: "SS_SportID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anag_AddressRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Anag_Contact",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Anag_CorporateRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Anag_Document",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Anag_MasterRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Athlete_DivisionXAthlete",
                schema: "ath");

            migrationBuilder.DropTable(
                name: "Athlete_LevelXAthlete",
                schema: "ath");

            migrationBuilder.DropTable(
                name: "Athlete_WeightXAthlete",
                schema: "ath");

            migrationBuilder.DropTable(
                name: "CMS_History",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "Sport_DivisionLocalized",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_DivisionXSport",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_EventResultLocalized",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_EventResultTypeLocalized",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_LevelLocalized",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_LevelXSport",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_Schedule",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Anag_Address",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Athlete_Master",
                schema: "ath");

            migrationBuilder.DropTable(
                name: "CMS_Master",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "Sport_Division",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_EventResult",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_EventResultType",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Sport_Level",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Anag_Master",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sport_Master",
                schema: "conf");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");
        }
    }
}
