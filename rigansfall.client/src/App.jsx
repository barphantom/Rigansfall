import { useEffect, useState } from 'react';
import './App.css';
import GameBoard from "./components/GameBoard.jsx";

function App() {
    const [forecasts, setForecasts] = useState();
    const [mapData, setMapData] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        populateWeatherData();
    }, []);

    useEffect(() => {
        getMapInfo();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    //const mapContent = mapData === undefined
    //    ? <p><em>Loading... React app </em></p>
    //    : <table className="table table-striped" aria-labelledby="tableLabel">
    //        <thead>
    //            <tr>
    //                <th>Map id</th>
    //                <th>Map name</th>
    //            </tr>
    //        </thead>
    //        <tbody>
    //            {mapData.map(map =>
    //                <tr key={map.mapId}>
    //                    <td>{map.mapId}</td>
    //                    <td>{map.mapName}</td>
    //                </tr>
    //            )}
    //        </tbody>
    //    </table>;

    const mapContenst = loading ?
        <p>Loading...</p> : (
            <div>
                <p>Map Name: {mapData.mapName}</p>  {/* Wyúwietlanie mapName */}
                <p>Map ID: {mapData.mapId}</p>  {/* Wyúwietlanie mapId */}
                <p>Map width: {mapData.Width}</p>
                <p>Map height: {mapData.Height}</p>
                {/*<p>Map tiles: {mapData.Tiles.length}</p>*/}
                <p>Map tile 0: {typeof (mapData.Tiles)}</p>
                <ul>
                    {mapData.Tiles.map((tile, index) => (
                        <li key={index}>This is tile x: {tile.X}, y: {tile.Y}. Index: {index}</li>
                    ))}
                </ul>
            </div>
        );

    return (
        <div>
            <div id="welcome">
                <h1>Witaj w Rigansfall</h1>
                <h2>Miejscu dla najodwazniejszych bohaterow!</h2>
                <p>To jest test dla dzia≥ania na ga≥Íziach githuba</p>
            </div>

            {/*{mapContenst }*/}

            <GameBoard mapData={mapData} />

        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        console.log(response);
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    }

    async function getMapInfo() {
        try {
            const response = await fetch('https://localhost:7071/api/Maps/new-map');  // Adres API
            if (response.ok) {  // Sprawdzamy, czy odpowiedü jest poprawna
                const data = await response.json();  // Odczytujemy odpowiedü jako JSON
                setMapData(data);  // Ustawiamy dane w stanie mapData
                setLoading(false);  // Ustawiamy stan ≥adowania na false, po zakoÒczeniu pobierania
            } else {
                console.error('B≥πd odpowiedzi serwera:', response.statusText);
                setLoading(false);  // W przypadku b≥Ídu ustawiamy ≥adowanie na false
            }
        } catch (error) {
            console.error('B≥πd podczas pobierania danych:', error);
            setLoading(false);  // Ustawiamy stan ≥adowania na false w przypadku b≥Ídu
        }

    }
}

export default App;
