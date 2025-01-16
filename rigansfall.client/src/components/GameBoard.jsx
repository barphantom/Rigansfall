//import "./GameBoard.css";
import { useState } from "react";

export default function GameBoard({ mapData }) {

    const rows = 10; // Liczba rzêdów
    const cols = 10; // Liczba kolumn
    const [playerPosition, setPlayerPosition] = useState({ x: 3, y: 0 }); // Pozycja gracza

    // Obs³uga klikniêcia na kafelek
    const handleTileClick = (x, y, isWalkable) => {
        if (isWalkable) {
            setPlayerPosition({ x, y });
            console.log(`Player moved to (${x}, ${y})`);
        } else {
            console.log(`Tile at (${x}, ${y}) is blocked`);
        }
    };

    function isPlayer(row, column) {
        if (row === playerPosition.x && column === playerPosition.y) {
            return true;
        }
        return false;
    }

    return (
        //<h3>{mapData.mapName}</h3>
        <ol className="game-board" id={mapData.mapId}>
            {[...Array(rows)].map((row, rowIndex) => (
                <li key={rowIndex}>
                    <ol>
                        {[...Array(cols)].map((col, colIndex) => (
                            <li key={`${rowIndex}-${colIndex}`}>
                                <button onClick={() => handleTileClick(rowIndex, colIndex, mapData.Tiles[rowIndex * 10 + colIndex].isWalkable)}>
                                    {isPlayer(rowIndex, colIndex) ? 'X' : 'O'}
                                </button>
                            </li>
                        ))}
                    </ol>
                </li>
            )) }
        </ol>
    )

};
