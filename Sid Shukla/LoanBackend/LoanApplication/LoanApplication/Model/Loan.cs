namespace LoanApplication.Model
{
    public class Loan
    {
        public int Loan_ID { get; set; }
        public int Port_ID { get; set; }
        public decimal OriginalLoanAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal CollateralValue { get; set; }
        public string CreditRating { get; set; }
    }
    public class PortfolioSummary
    {
        public decimal TotalOutstandingLoanAmount { get; set; }
        public decimal TotalCollateralValue { get; set; }
        public double TotalScenarioCollateralValue { get; set; }
        public double TotalExpectedLoss { get; set; }
    }
}
