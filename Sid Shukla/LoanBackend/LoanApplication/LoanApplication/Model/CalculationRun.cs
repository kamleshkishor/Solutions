namespace LoanApplication.Model
{
    public class CalculationRun
    {
        public int Id { get; set; }
        public DateTime RunDate { get; set; }
        //public Dictionary<string, double> PercentageChanges { get; set; }        
        public ICollection<PercentageChange> PercentageChanges { get; set; } = new List<PercentageChange>();

        public TimeSpan TimeTaken { get; set; }
    }
    public class PercentageChange
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public double Change { get; set; }

        public int CalculationRunId { get; set; }
        public CalculationRun CalculationRun { get; set; }
    }
    public class AggregatedResult
    {
        public int Id { get; set; }
        public int CalculationRunId { get; set; }
        public int PortfolioId { get; set; }
        public decimal TotalOutstandingLoanAmount { get; set; }
        public decimal TotalCollateralValue { get; set; }
        public double TotalScenarioCollateralValue { get; set; }
        public double TotalExpectedLoss { get; set; }
    }

}
