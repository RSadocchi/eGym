using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eGym.Core.Log.Migrations
{
    public partial class creationLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Batch_Master",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<string>(type: "char(1)", nullable: false),
                    LastExecutionDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    NextExecutionScheduleDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastExecutionDurationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ShorterExecutionDurationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    LongerExecutionDurationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ConcurrencyTimestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batch_Master", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Master",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "ntext", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CallerMemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallerLineNumber = table.Column<int>(type: "int", nullable: true),
                    CallerFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyTimestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_Master_Log_Master_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "Log_Master",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Batch_LogXBatch",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyTimestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batch_LogXBatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batch_LogXBatch_Batch_Master_BatchId",
                        column: x => x.BatchId,
                        principalSchema: "dbo",
                        principalTable: "Batch_Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Batch_LogXBatch_Log_Master_LogId",
                        column: x => x.LogId,
                        principalSchema: "dbo",
                        principalTable: "Log_Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batch_LogXBatch_BatchId",
                schema: "dbo",
                table: "Batch_LogXBatch",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_LogXBatch_LogId",
                schema: "dbo",
                table: "Batch_LogXBatch",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Master_ParentId",
                schema: "dbo",
                table: "Log_Master",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batch_LogXBatch",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Batch_Master",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Log_Master",
                schema: "dbo");
        }
    }
}
