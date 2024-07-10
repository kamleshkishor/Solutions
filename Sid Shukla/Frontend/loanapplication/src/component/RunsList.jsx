import React, { useEffect, useState } from 'react';


const RunsList = () => {

    
    const [runs, setRuns] = useState([]);
    const [selectedRunResults, setSelectedRunResults] = useState(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchRuns();
    }, []);

    const fetchRuns = async () => {
        try {
            const response = await fetch('https://localhost:7204/Loans/runs');
            // const data = response.json();
            const data = await response.json();
            setRuns(data?.result ?? []);
        } catch (error) {
            console.error('Error fetching runs', error);
        }
    };

    const fetchResults = async (runId) => {
        setLoading(true);
        try {
            const response = await fetch(`https://localhost:7204/Loans/results/${runId}`);
            const data = await response.json();
            setSelectedRunResults(data);
        } catch (error) {
            console.error('Error fetching results', error);
        }
        setLoading(false);
    };

    return (
        <div>
            <h2>Calculation Runs</h2>
            <ul>
                {runs.map(run => (
                    <li key={run.id}>
                        <p>Run Date: {new Date(run.runDate).toLocaleString()}</p>
                        <button onClick={() => fetchResults(run.id)}>View Results</button>
                    </li>
                ))}
            </ul>

            {loading && <p>Loading results...</p>}

            {selectedRunResults && (
                <div>
                    <h3>Aggregated Results</h3>
                    <ul>
                        {selectedRunResults.map(result => (
                            <li key={result.portfolioId}>
                                <p>Portfolio ID: {result.portfolioId}</p>
                                <p>Total Outstanding Loan Amount: {result.totalOutstandingLoanAmount}</p>
                                <p>Total Collateral Value: {result.totalCollateralValue}</p>
                                <p>Scenario Collateral Value: {result.scenarioCollateralValue}</p>
                                <p>Expected Loss: {result.expectedLoss}</p>
                            </li>
                        ))}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default RunsList;
