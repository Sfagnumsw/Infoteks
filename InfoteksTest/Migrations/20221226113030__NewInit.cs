using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoteksTest.Migrations
{
    public partial class _NewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllTime = table.Column<int>(type: "int", nullable: false),
                    FirstOperation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageCompletionTime = table.Column<int>(type: "int", nullable: false),
                    AverageIndicator = table.Column<double>(type: "float", nullable: false),
                    MedianIndicator = table.Column<double>(type: "float", nullable: false),
                    MaxIndicator = table.Column<double>(type: "float", nullable: false),
                    MinIndicator = table.Column<double>(type: "float", nullable: false),
                    CountString = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionTime = table.Column<int>(type: "int", nullable: false),
                    Indicator = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_FileName",
                table: "Values",
                column: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Values");
        }
    }
}
