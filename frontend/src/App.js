import { useEffect, useState } from "react";
import './App.css';

function App() {
  const [data, setData] = useState([]);

  useEffect(() => {
    // Fetch from your Azure backend
    fetch("https://lab05-backend-hbesdqendfhee4cv.eastasia-01.azurewebsites.net/api/WeatherForecast")
      .then(res => res.json())
      .then(data => setData(data))
      .catch(err => console.error(err));
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <h1>Weather Forecast</h1>
        {data.length === 0 ? (
          <p>Loading...</p>
        ) : (
          <ul>
            {data.map((item, index) => (
              <li key={index}>
                {item.date} — {item.temperatureC}°C — {item.summary}
              </li>
            ))}
          </ul>
        )}
      </header>
    </div>
  );
}

export default App;