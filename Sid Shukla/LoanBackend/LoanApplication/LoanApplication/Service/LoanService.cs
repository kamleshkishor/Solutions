using LoanApplication.Model;
using CsvHelper;
using System.Globalization;
using System.IO;
using LoanApplication.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;

namespace LoanApplication.Service
{
    public class LoanService
    {
        private List<Portfolio> portfolios;
        private List<Loan> loans;
        private List<Ratings> ratings;
        private readonly LoanContext _context;        


        public LoanService(LoanContext context)
        {
            _context = context;
            portfolios = LoadPortfolios();
            loans = LoadLoans();
            ratings = LoadRatings();
        }

        private List<Portfolio> LoadPortfolios()
        {
            using (var reader = new StreamReader("portfolios.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Portfolio>().ToList();
            }
        }

        private List<Loan> LoadLoans()
        {
            using (var reader = new StreamReader("loans.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Loan>().ToList();
            }
        }

        private List<Ratings> LoadRatings()
        {
            using (var reader = new StreamReader("ratings.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Ratings>().ToList();
            }
        }

        public Dictionary<int, PortfolioSummary> CalculateAggregatedData(Dictionary<string, double> percentageChanges)
        {
            var result = new Dictionary<int, PortfolioSummary>();

            foreach (var loan in loans)
            {
                var portfolio = portfolios.First(p => p.Port_ID == loan.Port_ID);
                if (!result.ContainsKey(portfolio.Port_ID))
                {
                    result[portfolio.Port_ID] = new PortfolioSummary();
                }

                var summary = result[portfolio.Port_ID];
                summary.TotalOutstandingLoanAmount += loan.OutstandingAmount;
                summary.TotalCollateralValue += loan.CollateralValue;

                double percentageChange = percentageChanges[portfolio.Port_Country];
                double scenarioCollateralValue = (double)loan.CollateralValue * percentageChange;//(double)(1 + (percentageChange / 100));
                double recoveryRate = (double)scenarioCollateralValue / (double)loan.OutstandingAmount;
                double lossGivenDefault = 1 - recoveryRate;
                double probabilityOfDefault = ratings.First(r => r.Rating == loan.CreditRating).ProbablilityOfDefault;
                double expectedLoss = probabilityOfDefault * lossGivenDefault * (double)loan.OutstandingAmount;

                summary.TotalScenarioCollateralValue += scenarioCollateralValue;
                summary.TotalExpectedLoss += expectedLoss;
            }

            return result;
        }
        public async Task<Dictionary<int, PortfolioSummary>> CalculateAndSaveAggregatedDataAsync(Dictionary<string, double> percentageChanges)
        {
            var result = CalculateAggregatedData(percentageChanges);
            try
            {
                var stopwatch = Stopwatch.StartNew();


                stopwatch.Stop();
                var percentageChange = percentageChanges
                .Select(pc => new PercentageChange
                {
                    Country = pc.Key,
                    Change = pc.Value
                })
                .ToList();
                var run = new CalculationRun
                {
                    RunDate = DateTime.UtcNow,
                    PercentageChanges = percentageChange,
                    TimeTaken = stopwatch.Elapsed
                };
                _context.CalculationRuns.Add(run);
                await _context.SaveChangesAsync();

                foreach (var entry in result)
                {
                    _context.AggregatedResults.Add(new AggregatedResult
                    {
                        CalculationRunId = run.Id,
                        PortfolioId = entry.Key,
                        TotalOutstandingLoanAmount = entry.Value.TotalOutstandingLoanAmount,
                        TotalCollateralValue = entry.Value.TotalCollateralValue,
                        TotalScenarioCollateralValue = entry.Value.TotalScenarioCollateralValue,
                        TotalExpectedLoss = entry.Value.TotalExpectedLoss
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return result;

        }
        public async Task<List<CalculationRun>> GetRuns()
        {

           var runs = await _context.CalculationRuns.ToListAsync();
            return runs;

        }
        public async Task<List<AggregatedResult>> GetResultsAsync(int runId)
        {
            try
            {
                var results = await _context.AggregatedResults
                    .Where(r => r.CalculationRunId == runId)
                    .ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}


