import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import NavBar from "./components/NavBar.jsx"
import Juego from "./components/Juego.jsx"
import Compañia from "./components/Compañia.jsx"
import Personaje from "./components/Personaje.jsx"

const Rutas = () =>{
  return (
    <Router>
      <NavBar/>
        <Routes>
          <Route exact path="/" element ={<Juego/>} />
          <Route exact path="/Personaje" element={<Personaje/>}/>
          <Route exact path="/Compañia" element={<Compañia/>}/>
        </Routes>
    </Router>
  )
}

export default Rutas;

