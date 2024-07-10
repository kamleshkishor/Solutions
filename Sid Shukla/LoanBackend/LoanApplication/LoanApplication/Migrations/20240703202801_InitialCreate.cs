using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AggregatedResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculationRunId = table.Column<int>(type: "INTEGER", nullable: false),
                    PortfolioId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalOutstandingLoanAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCollateralValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalScenarioCollateralValue = table.Column<double>(type: "REAL", nullable: false),
                    TotalExpectedLoss = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregatedResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculationRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RunDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeTaken = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PercentageChange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Change = table.Column<double>(type: "REAL", nullable: false),
                    CalculationRunId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PercentageChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PercentageChange_CalculationRuns_CalculationRunId",
                        column: x => x.CalculationRunId,
                        principalTable: "CalculationRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PercentageChange_CalculationRunId",
                table: "PercentageChange",
                column: "CalculationRunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregatedResults");

            migrationBuilder.DropTable(
                name: "PercentageChange");

            migrationBuilder.DropTable(
                name: "CalculationRuns");
        }
    }
}
