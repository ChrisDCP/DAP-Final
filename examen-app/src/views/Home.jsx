import React, { useState, useEffect } from 'react';

const Home = () => {
 
  const [datos, setDatos] = useState(null);
 
  const [error, setError] = useState(null);

  useEffect(() => {
 
    const fetchData = async () => {
      try {
  
        const respuesta = await fetch('http://localhost:5101/api/Juego/Lista');
        
 
        if (!respuesta.ok) {
          throw new Error('Error en la solicitud a la API');
        }

  
        const datosJson = await respuesta.json();
        
       
        setDatos(datosJson);
      } catch (error) {
  
        setError(error.message);
      }
    };

   
    fetchData();
  }, []); 
  
  return (
    <div>
      {error && <p>Error: {error}</p>}
      {datos && (
        <div>
          {/* Renderizar los datos de la API como desees */}
          <p>Datos de la API: {JSON.stringify(datos)}</p>
        </div>
      )}
    </div>
  );
};

export default Home;
