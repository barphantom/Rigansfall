//import { useEffect, useState } from "react";
////import "./GameBoard.css";

//const GameBoard = () => {
//    const [map, setMap] = useState(null); // Przechowywanie danych mapy
//    const [error, setError] = useState(null);

//    // Pobranie mapy z API
//    useEffect(() => {
//        fetch("http://localhost:5264/api/Maps/new-map")
//            .then((response) => {
//                if (!response.ok) {
//                    throw new Error("Failed to fetch map");
//                }
//                return response.json();
//            })
//            .then((data) => setMap(data))
//            .catch((err) => setError(err.message));
//    }, []);

//    if (error) {
//        return <div>Error: {error}</div>;
//    }

//    if (!map) {
//        return <div>Loading...</div>;
//    }

//    // Rysowanie kafelków
//    const renderTiles = () => {
//        const grid = [];
//        const tileSize = 40; // Rozmiar kafelka w pikselach

//        for (let y = 0; y < 10; y++) {
//            const row = [];
//            for (let x = 0; x < 10; x++) {
//                const tile = map.tiles.find((t) => t.X === x && t.Y === y);
//                row.push(
//                    <div
//                        key={`${x}-${y}`}
//                        className={`board-tile ${tile.isWalkable ? "walkable" : "blocked"}`}
//                        style={{
//                            width: tileSize,
//                            height: tileSize,
//                        }}
//                    ></div>
//                );
//            }
//            grid.push(
//                <div key={`row-${y}`} className="board-row">
//                    {row}
//                </div>
//            );
//        }

//        return grid;
//    };

//    return (
//        <div className="game-board">
//            <h3>{map.name}</h3>
//            {renderTiles()}
//        </div>
//    );
//};

//export default GameBoard;
