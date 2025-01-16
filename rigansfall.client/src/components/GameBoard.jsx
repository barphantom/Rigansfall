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

    //GameBoard.propTypes = {

    //}

    //return (
    //    <div className="game-board">
    //        {[...Array(rows)].map((_, rowIndex) => (
    //            <div key={rowIndex} className="row">
    //                {[...Array(cols)].map((_, colIndex) => {
    //                    // Przyk³adowe logika dla "przechodnoœci" kafelka
    //                    const isWalkable = (rowIndex + colIndex) % 2 === 0; // Przyk³ad losowej logiki
    //                    const isPlayer = playerPosition.x === colIndex && playerPosition.y === rowIndex;

    //                    return (
    //                        <div
    //                            key={`${rowIndex}-${colIndex}`}
    //                            className={`tile ${isWalkable ? 'walkable' : 'blocked'} ${isPlayer ? 'player' : ''}`}
    //                            onClick={() => handleTileClick(colIndex, rowIndex, isWalkable)}
    //                        >
    //                            {isPlayer ? 'P' : `${colIndex},${rowIndex}`}
    //                        </div>
    //                    );
    //                })}
    //            </div>
    //        ))}
    //    </div>
    //);

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

    //return (
    //    <div className="game-board">
    //        <h3>{mapData.mapName}</h3>
    //        {renderTiles()}
    //    </div>
    //);
};
