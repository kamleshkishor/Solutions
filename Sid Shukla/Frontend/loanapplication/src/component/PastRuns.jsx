import React, { useState, useEffect } from 'react';

function PastRuns({ onSelect }) {
  const [runs, setRuns] = useState([]);

  useEffect(() => {
    fetch('/loans/runs')
      .then(response => response.json())
      .then(data => setRuns(data));
  }, []);

  return (
    <div>
      <h2>Past Runs</h2>
      <ul>
        {runs.map(run => (
          <li key={run.id} onClick={() => onSelect(run.id)}>
            Run at {new Date(run.runDate).toLocaleString()}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default PastRuns;
