//import "./GameBoard.css";
import { useEffect, useState } from "react";
import stone from "../assets/stone.png";
import grass from "../assets/grass.png";
import hero from "../assets/hero.png";
import goblin from "../assets/goblin.png";
import goblinTNT from "../assets/goblin_TNT.png";
import treasure from "../assets/treasure.png";


export default function GameBoard() {
    //if (!mapData || !mapData?.tiles || mapData.tiles?.length === 0) {
    //    // Zwróæ null lub jakiœ komunikat, jeœli tiles nie s¹ dostêpne
    //    return <div>Loading map...</div>;
    //}

    const rows = 10; // Liczba rzêdów
    const cols = 10; // Liczba kolumn
    const [mapData, setMapData] = useState({});
    const [loading, setLoading] = useState(true);
    const [playerPosition, setPlayerPosition] = useState({ x: 0, y: 4 }); // Pozycja gracza

    useEffect(() => {
        if (mapData?.titles?.length > 0) {
            const startingTile = mapData.tiles.find(title => title.type === 1);
            if (startingTile) {
                setPlayerPosition({ x: startingTile.x, y: startingTile.y })
            }
        }
    }, [mapData]);

    useEffect(() => {
        getMapInfo();
    }, []);


    const handleTileClick = async (x, y) => {
        const url = "https://localhost:7071/api/game/move"
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
                    console.log(`Player moved to (${x}, ${y}): ${result.reason}`);
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

    function getPictureName(type, isWalkable) {
        if (type === 0 && !isWalkable) return stone;
        if (type === 0) return grass;
        if (type === 1) return grass;
        if (type === 2) return goblin;
        if (type === 3) return goblinTNT;
        if (type === 4) return treasure;
    }

    async function getMapInfo() {
        try {
            const response = await fetch('https://localhost:7071/api/game/load-map');  // Adres API
            if (response.ok) {  // Sprawdzamy, czy odpowiedŸ jest poprawna
                const data = await response.json();  // Odczytujemy odpowiedŸ jako JSON
                setMapData(data);  // Ustawiamy dane w stanie mapData
                setLoading(false);  // Ustawiamy stan ³adowania na false, po zakoñczeniu pobierania
            } else {
                console.error('B³¹d odpowiedzi serwera:', response.statusText);
                setLoading(false);  // W przypadku b³êdu ustawiamy ³adowanie na false
            }
        } catch (error) {
            console.error('B³¹d podczas pobierania danych:', error);
            setLoading(false);  // Ustawiamy stan ³adowania na false w przypadku b³êdu
        }

    }

    return (
        <>
            {loading ? <p>Loading...</p> : (
                <ol className="game-board" id={mapData && mapData.mapId}>
                    {[...Array(rows)].map((row, rowIndex) => (
                        <li key={rowIndex}>
                            <ol>
                                {[...Array(cols)].map((col, colIndex) => (
                                    <li key={`${rowIndex}-${colIndex}`}>
                                        <button
                                            onClick={() => handleTileClick(colIndex, rowIndex)}
                                            disabled={ mapData.tiles[rowIndex * 10 + colIndex].isWalkable === false}
                                        >

                                            <img src={mapData && ((isPlayer(colIndex, rowIndex)) ? hero : getPictureName(mapData.tiles[rowIndex * 10 + colIndex].type, mapData.tiles[rowIndex * 10 + colIndex].isWalkable))}
                                                alt="blad" />

                                        </button>
                                    </li>
                                ))}
                            </ol>
                        </li>
                    ))}
                </ol>
            )}
        </>

    )

};
