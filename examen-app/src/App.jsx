import { useState, useEffect } from "react";
import "./App.css";

function App(){
  const [data, setData] = useState(null);

  useEffect(()=>{
    fetch("http://localhost:5101/api/Juego/Lista")
    .then ((response)=> response.json)
    .then((result)=> console.log(result))
  }, []);

  return(
    <div className="App">
      <h1>fetch</h1>
      <div className = "card"></div>
      <ul>
        {data?.map((Juego)=>(
          <li key={Juego.id}>{Juego.Nombre}</li>
        )
        )}
      </ul>
    </div>
  );

}

export default App;

