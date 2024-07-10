using LoanApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("Calculate")]
        public IActionResult Calculate([FromBody] Dictionary<string, double> percentageChanges)
        {
            var result = _loanService.CalculateAggregatedData(percentageChanges);
            return Ok(result);
        }
        [HttpPost("CalculateSaveInDb")]
        public IActionResult CalculateSaveInDb([FromBody] Dictionary<string, double> percentageChanges)
        {
            var result = _loanService.CalculateAndSaveAggregatedDataAsync(percentageChanges);
            return Ok(result);
        }
        [HttpGet("runs")]
        public IActionResult GetRuns()
        {
            var runs = _loanService.GetRuns();
            return Ok(runs);
        }

        [HttpGet("results/{runId}")]
        public async Task<IActionResult> GetResults(int runId)
        {
            var results = await _loanService.GetResultsAsync(runId);

            if (results == null || !results.Any())
            {
                return NotFound(new { Message = "No results found for the provided run ID." });
            }

            return Ok(results);
        }

    }

}
