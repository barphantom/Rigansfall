import { useEffect, useState } from 'react';
import './App.css';
import GameBoard from "./components/GameBoard.jsx";

function App() {

    //const mapContenst = loading ?
    //    <p>Loading...</p> : (
    //        <div>
    //            <p>Map Name: {mapData.name}</p>  {/* Wyœwietlanie mapName */}
    //            <p>Map ID: {mapData.mapId}</p>  {/* Wyœwietlanie mapId */}
    //            <p>Map tile type: {typeof (mapData.tiles)}</p>
    //            <p>Array: {Array.isArray(mapData.tiles) ? "true" : "false"}</p>
    //            <p>Length: {mapData.tiles.length}</p>
    //            <p>Length: {mapData.tiles[0].name}</p>

    //            {/*<ul>*/}
    //            {/*    {mapData.tiles.map((tile, index) => (*/}
    //            {/*        <li key={index}>This is tile x: {tile.x}, y: {tile.y}, type: {tile.type}. Index: {index}</li>*/}
    //            {/*    ))}*/}
    //            {/*</ul>*/}
    //        </div>
    //    );



    return (
        <div>
            <div id="welcome">
                <h1>Witaj w Rigansfall</h1>
                <h2>Miejscu dla najodwazniejszych bohaterow!</h2>
                <p>To jest test dla dzia³ania na ga³êziach githuba</p>
            </div>

            {/*{mapContenst }*/}
            {/*<h3>{mapData.name}</h3>*/}
            <GameBoard />

        </div>
    );


}

export default App;
