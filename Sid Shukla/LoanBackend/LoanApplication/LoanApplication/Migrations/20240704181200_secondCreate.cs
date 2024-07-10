using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApplication.Migrations
{
    /// <inheritdoc />
    public partial class secondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Loan_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Port_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalLoanAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    OutstandingAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CollateralValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreditRating = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Loan_ID);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Port_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Port_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Port_Country = table.Column<string>(type: "TEXT", nullable: false),
                    Port_CCY = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Port_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
