//import "./GameBoard.css";
import { useState } from "react";

export default function GameBoard({ mapData }) {

    const rows = 10; // Liczba rzêdów
    const cols = 10; // Liczba kolumn
    const [playerPosition, setPlayerPosition] = useState({ x: 0, y: 3 }); // Pozycja gracza

    // Obs³uga klikniêcia na kafelek
    //const handleTileClick = (x, y, isWalkable) => {
    //    if (isWalkable) {
    //        setPlayerPosition({ x, y });
    //        console.log(`Player moved to (${x}, ${y})`);
    //    } else {
    //        console.log(`Tile at (${x}, ${y}) is blocked`);
    //    }
    //};

    const handleTileClick = async (x, y, isWalkable) => {
        const url = "https://localhost:7071/api/MoveRequest"
        try {
            const respone = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    currentX: playerPosition.x,
                    currentY: playerPosition.y,
                    newX: x,
                    newY: y,
                }),
            });

            if (respone.ok) {
                const result = await respone.json();
                if (result.canMove) {
                    setPlayerPosition({ x, y });
                    console.log(`Player moved to (${x}, ${y})`);
                } else {
                    console.log(`Cannot move to (${x}, ${y}): ${result.reason}`);
                }

            } else {
                console.error("Error comunicationg with a backend.")
            }

        } catch (error) {
            console.error("Failed to send move request.");
        }
    }

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
