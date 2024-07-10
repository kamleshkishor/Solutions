import React, { useState } from 'react';

const countries = ["GB", "US", "FR", "DE", "SG", "GR"];

function PercentageForm({ onSubmit }) {
  const [percentages, setPercentages] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPercentages({
      ...percentages,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(percentages);
  };

  return (
    <form onSubmit={handleSubmit}>
      {countries.map((country) => (
        <div key={country}>
          <label>
            {country}:
            <input
              type="number"
              name={country}
              value={percentages[country] || ''}
              onChange={handleChange}
            />
          </label>
        </div>
      ))}
      <button type="submit">Submit</button>
    </form>
  );
}

export default PercentageForm;
