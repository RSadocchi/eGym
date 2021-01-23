using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Domain.Migrations
{
    public partial class cmsTablesRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMS_History",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "CMS_Master",
                schema: "cms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms");

            migrationBuilder.CreateTable(
                name: "CMS_Master",
                schema: "cms",
                columns: table => new
                {
                    CMS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMS_Active = table.Column<bool>(type: "bit", nullable: false),
                    CMS_ContextKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CMS_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CMS_EditorTypeID = table.Column<short>(type: "smallint", nullable: false),
                    CMS_GroupKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CMS_Value = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_Master", x => x.CMS_ID);
                });

            migrationBuilder.CreateTable(
                name: "CMS_History",
                schema: "cms",
                columns: table => new
                {
                    CMSH_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMSH_Active = table.Column<bool>(type: "bit", nullable: false),
                    CMSH_Culture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CMSH_MasterID = table.Column<int>(type: "int", nullable: false),
                    CMSH_Value = table.Column<string>(type: "ntext", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CMS_History_CMSH_MasterID",
                schema: "cms",
                table: "CMS_History",
                column: "CMSH_MasterID");
        }
    }
}
