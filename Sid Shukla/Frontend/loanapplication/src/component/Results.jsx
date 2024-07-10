import React from 'react';

function Results({ results }) {
  return (
    <div>
      {Object.entries(results).map(([portfolioId, summary]) => (
        <div key={portfolioId}>
          <h3>Portfolio {portfolioId}</h3>
          <p>Total Outstanding Loan Amount: {summary.totalOutstandingLoanAmount}</p>
          <p>Total Collateral Value: {summary.totalCollateralValue}</p>
          <p>Total Scenario Collateral Value: {summary.totalScenarioCollateralValue}</p>
          <p>Total Expected Loss: {summary.totalExpectedLoss}</p>
        </div>
      ))}
    </div>
  );
}

export default Results;
