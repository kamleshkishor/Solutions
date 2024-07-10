import React, { useState } from 'react';
import PercentageForm from './component/PercentageForm';
import Results from './component/Results';
import RunsList from './component/RunsList';

function App() {
  const [results, setResults] = useState(null);
  const [openRunsList, setOpenRunList] = useState(false);  
  const handleSubmit = async (percentages) => {
    console.log("submit btn clicked")
    console.log(JSON.stringify(percentages))
    const response = await fetch('https://localhost:7204/Loans/CalculateSaveInDb', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(percentages),
    });
    const data = await response.json();
    console.log(data);
    const { result = {} } = data;
    setResults({ ...result });
  };


  

  return (

    <>
      <div>
        <h1>Loan Aggregator</h1>
        <PercentageForm onSubmit={handleSubmit} />
        {results && <Results results={results} />}
      </div>
      <section>
        <button onClick={() => setOpenRunList(true)}>Runs</button>
        {openRunsList && <RunsList />}
      </section>
    </>
  );
}

export default App;
