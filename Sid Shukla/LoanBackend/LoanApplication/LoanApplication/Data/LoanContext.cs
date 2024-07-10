using LoanApplication.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoanApplication.Data
{
    public class LoanContext : DbContext
    {
        public DbSet<CalculationRun> CalculationRuns { get; set; }
        public DbSet<AggregatedResult> AggregatedResults { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Loan> Loans { get; set; }
        //public DbSet<Ratings> Rating { get; set; }

        public LoanContext(DbContextOptions<LoanContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
           .HasKey(l => l.Loan_ID);
            modelBuilder.Entity<Portfolio>()
           .HasKey(l => l.Port_ID);

            modelBuilder.Entity<CalculationRun>()
                .HasMany(c => c.PercentageChanges)
                .WithOne(p => p.CalculationRun)
                .HasForeignKey(p => p.CalculationRunId);

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=loans.db");
            }
        }
    }

}
